import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.UnsupportedEncodingException;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.net.URLDecoder;
import java.net.URLEncoder;
import java.util.ArrayList;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

/**
 * 给定URL下载网页信息的类，包括指定编码进行处理和自动判断编码2种情况<br>
 * 自动判断编码的函数代价较大<br>
 * 请特别注意：Google的检索结果在本程序发送的FF的head的情况下，没有编码信息。
 * 所以在爬Google的检索结果的时候请务必手动指定使用UTF-8编码
 * 
 * @author BlueJade, Fandy Wang(lfwang@ir.hit.edu.cn)
 * @version 1.0
 */
public class DownloadURL {
  /**
   * 该变量保存着寻找charset的正则表达式
   */
  private static Pattern charsetPattern = Pattern.compile(
      "charset\\s*=\\s*([^\"]*)", Pattern.CASE_INSENSITIVE);

  /**
   * 给定内容和编码，将内容转换成编码对应的字节码
   * 
   * @param content
   * @param encoding
   * @return 转换后的结果
   */
  public static String encodeContent(String content, String encoding) {
    try {
      return URLEncoder.encode(content, encoding);
    } catch (UnsupportedEncodingException e) {
      e.printStackTrace();
      return null;
    }
  }

  /**
   * 根据字节码和相应的编码，将内容转换成对应的原始文字
   * 
   * @param byteCode
   * @param encoding
   * @return 转换后的结果
   */
  public static String decodeContent(String byteCode, String encoding) {
    try {
      return URLDecoder.decode(byteCode, encoding);
    } catch (UnsupportedEncodingException e) {
      e.printStackTrace();
      return null;
    }
  }

  /**
   * @param urladdr
   *          URL Address
   * @param type
   *          FF3.0<br>
   *          IE8.0
   * @return HTTPURLConnection
   */
  private static HttpURLConnection getHttpURLConnectionInstance(String urladdr,
      String type) {
    URL url;
    HttpURLConnection httpUrl = null;
    try {
      url = new URL(urladdr);
      httpUrl = (HttpURLConnection) url.openConnection();
      if (type.equals("FF3.0")) {
        httpUrl
            .addRequestProperty(
                "User-Agent",
                "Mozilla/5.0 (Windows; U; Windows NT 6.0; zh-CN; rv:1.9.0.8) Gecko/2009032609 Firefox/3.0.8 (.NET CLR 3.5.30729)");
        httpUrl.addRequestProperty("Accept",
            "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
        httpUrl.addRequestProperty("Accept-Language", "zh-cn,zh;q=0.5");
        httpUrl.addRequestProperty("Keep-Alive", "300");
        httpUrl.addRequestProperty("Connection", "Keep-Alive");
      } else if (type.equals("IE8.0")) {
        httpUrl
            .addRequestProperty(
                "User-Agent",
                "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.0; Trident/4.0; SLCC1; .NET CLR 2.0.50727; Media Center PC 5.0; .NET CLR 3.5.21022; .NET CLR 3.5.30729; .NET CLR 3.0.30729)");
        httpUrl.addRequestProperty("Accept", "*/*");
        httpUrl.addRequestProperty("Accept-Language", "zh-cn");
        httpUrl.addRequestProperty("Connection", "Keep-Alive");
      }
      httpUrl.connect();
    } catch (MalformedURLException e) {
      e.printStackTrace();
    } catch (IOException e) {
      e.printStackTrace();
    }
    return httpUrl;
  }

  /**
   * 给定URL以及网页的编码，爬取网页内容
   * 
   * @param urladdr
   * @param encoding
   *          网页编码：UTF-8 or GB18030(兼容GBK、GB2312)
   * @type 模拟浏览器抓取网页，浏览器类型：IE8.0 or FF3.0
   * @return URL对应网页内容
   */
  public static String downURL(String urladdr, String encoding, String type) {
    StringBuilder result = new StringBuilder();
    try {
      HttpURLConnection httpUrl = getHttpURLConnectionInstance(urladdr, type);
      BufferedReader reader = new BufferedReader(new InputStreamReader(httpUrl
          .getInputStream(), encoding));
      String line;
      while ((line = reader.readLine()) != null) {
        result.append(line);
        result.append('\n');
      }
      reader.close();
      httpUrl.disconnect();
    } catch (MalformedURLException e) {
      System.out.println(e); // 出现异常不退出
    } catch (IOException e) {
      System.out.println(e);
    }
    return result.toString();
  }

  /**
   * 给定URL以及网页，爬取网页内容，如果网页有指定编码，则本程序会自动判断编码<br>
   * 若网页没有指定编码，则默认为GB18030编码（兼容GBK，GB2312）
   * 
   * @param urladdr
   *          URL
   * @param type
   *          模拟浏览器抓取网页，浏览器类型：IE8.0 or FF3.0
   * @return URL对应网页内容
   */
  public static String downURL(String urladdr, String type) {
    ArrayList<Byte> allbyte = new ArrayList<Byte>();
    try {
      HttpURLConnection httpUrl = getHttpURLConnectionInstance(urladdr, type);
      InputStream is = httpUrl.getInputStream();
      byte temp[] = new byte[2048];
      int readNum;
      while ((readNum = is.read(temp, 0, 2048)) != -1) {
        for (int i = 0; i < readNum; i++) {
          allbyte.add(temp[i]);
        }
      }
      is.close();
      httpUrl.disconnect();
    } catch (MalformedURLException e) {
      e.printStackTrace();
    } catch (IOException e) {
      e.printStackTrace();
    }
    byte temp1[] = new byte[allbyte.size()];
    for (int i = 0; i < allbyte.size(); i++) {
      temp1[i] = allbyte.get(i);
    }
    String result = null;
    try {
      result = new String(temp1, "GB18030");
    } catch (UnsupportedEncodingException e) {
      e.printStackTrace();
      result = null;
    }
    String temp2 = result.substring(0, result.indexOf("<body"));
    Matcher m = charsetPattern.matcher(temp2);
    String charset = null;
    if (m.find()) {
      charset = temp2.substring(m.start(1), m.end(1)).toLowerCase();
      try {
        result = new String(temp1, charset);
      } catch (UnsupportedEncodingException e) {
        e.printStackTrace();
      }
    }

    return result;
  }

  public static void main(String[] args) {
    // System.out.println(URLTOString.downURL("http://www.google.cn/search?as_q=%E4%BF%A1%E6%81%AF%E6%A3%80%E7%B4%A2&hl=zh-CN&newwindow=1&num=100&btnG=Google+%E6%90%9C%E7%B4%A2&as_epq=&as_oq=&as_eq=&lr=lang_zh-CN&cr=&as_ft=i&as_filetype=&as_qdr=all&as_occt=any&as_dt=i&as_sitesearch=&as_rights=",
    // "UTF-8"));
    // System.out.println(URLTOString.downURL("http://product.pcpop.com/000053337/Detail.html",
    // "GB18030"));
    // System.out.println(URLTOString.downURL("http://product.pcpop.com/000053337/Detail.html"));
    // System.out.println(URLTOString.downURL(
    // "http://zh.wikipedia.org/w/index.php?title=Wikipedia:%E9%A6%96%E9%A1%B5&variant=zh-cn"
    // , 2));
    System.out.println(DownloadURL.downURL("http://news.cjn.cn/whyw/201007/t1183385.htm", "IE8.0"));
    // System.out.println();
    // System.out.println(URLTOString.downURL("http://www.baidu.com/s?wd=%D0%C5%CF%A2%BC%EC%CB%F7",
    // "GB18030"));
//    System.out
//        .println(DownloadURL
//            .decodeContent(
//                "http://www.google.cn/search?as_q=%E5%8D%8E%E4%B8%BA+%E4%BB%BB%E6%AD%A3%E9%9D%9E&hl=zh-CN&newwindow=1&num=10&btnG=Google+%E6%90%9C%E7%B4%A2&as_epq=&as_oq=%E6%80%BB%E8%A3%81+%E6%80%BB%E7%90%86+%E6%80%BB%E7%BB%9F&as_eq=&lr=&cr=&as_ft=i&as_filetype=&as_qdr=all&as_occt=any&as_dt=i&as_sitesearch=&as_rights=",
//                "UTF-8"));
  }

}
