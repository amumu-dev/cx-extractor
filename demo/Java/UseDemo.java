/**
 * @author Xin Chen
 * Created on 2009-11-11
 * Updated on 2010-08-09
 * Email:  xchen@ir.hit.edu.cn
 * Blog:   http://hi.baidu.com/爱心同盟_陈鑫
 */
import java.io.BufferedInputStream;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.URL;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import org.mozilla.intl.chardet.nsDetector;
import org.mozilla.intl.chardet.nsICharsetDetectionObserver;
import org.mozilla.intl.chardet.nsPSMDetector;


/**
 * TextExtractor功能测试类.
 */

public class UseDemo {
	
	public static void main(String[] args) throws IOException {
		
		/* 
		 * 测试网站：
		 * 百度博客空间             http://hi.baidu.com/liyanhong/
		 * 新浪娱乐音乐新闻与信息	http://ent.sina.com.cn/music/roll.html
		 * 腾讯娱乐新闻与信息		http://ent.qq.com/m_news/mnews.htm
		 * 搜狐音乐新闻				http://music.sohu.com/news.shtml
		 * 哈尔滨工业大学校内信息网 http://today.hit.edu.cn/
		 * 哈尔滨工业大学校内新闻网 http://news.hit.edu.cn/
		 */


		/* 注意：本处只为展示抽取效果，不处理网页编码问题，getHTML只能接收GBK编码的网页，否则会出现乱码 */
		String content = new UseDemo().getHTML(args[0]);

		// http://ent.sina.com.cn/y/2010-04-18/08332932833.shtml
		// http://ent.qq.com/a/20100416/000208.htm
		// http://ent.sina.com.cn/y/2010-04-18/15432932937.shtml
		// http://ent.qq.com/a/20100417/000119.htm
		// http://news.hit.edu.cn/articles/2010/04-12/04093006.htm
	

		/* 
		 * 当待抽取的网页正文中遇到成块的新闻标题未剔除时，只要增大此阈值即可。
		 * 相反，当需要抽取的正文内容长度较短，比如只有一句话的新闻，则减小此阈值即可。
		 * 阈值增大，准确率提升，召回率下降；值变小，噪声会大，但可以保证抽到只有一句话的正文 
		 */
		//TextExtract.setThreshold(76); // 默认值86

		System.out.println("got html");
		String html = new TextExtract().parse(content);
		
		System.out.println(html);
	}


	public String getHTML(String strURL) throws IOException {
		URL url = new URL(strURL);
		BufferedInputStream in =  new BufferedInputStream(url.openStream());
		byte[] bytes = new byte[1024000];
		int len = -1;
		int pos = 0;
		while ((len = in.read(bytes, pos, bytes.length - pos)) != -1) {
			pos += len;
		}
		
		detectCharset(bytes);
		
		String html = null;
		if (detCharset != null)
		{
			html = new String(bytes, 0, pos, this.detCharset);
		}
		else
		{
			return new String(bytes, 0, pos);
		}
		System.out.println("Detcharset = " + detCharset);
		return html;
	}
	
	private String detCharset = null;

	private Pattern pGB2312 = Pattern.compile("GB2312", Pattern.CASE_INSENSITIVE);
	private Pattern pUTF8 = Pattern.compile("(UTF8)|(UTF-8)", Pattern.CASE_INSENSITIVE);
	
	public void detectCharset(byte[] content)
	{
		String html = new String(content); 
		Matcher m = pGB2312.matcher(html);
		if (m.find())
		{
			detCharset = "gb2312";
			return ;
		}
		m = pUTF8.matcher(html);
		if (m.find())
		{
			detCharset = "utf-8";
			return;
		}
		
		int lang = nsPSMDetector.ALL;
		nsDetector det = new nsDetector(lang);
		det.Init(new nsICharsetDetectionObserver() {
			public void Notify(String charset) {
				detCharset = charset;
			} 
		});
		boolean isAscii = true;

		if (isAscii)
			isAscii = det.isAscii(content, content.length);

		if (!isAscii)
			det.DoIt(content, content.length, false);

		det.DataEnd();

		boolean found = false;
		if (isAscii) {
			this.detCharset = "US-ASCII";
			found = true;
		}

		if (!found && detCharset == null) {
			detCharset = det.getProbableCharsets()[0];
		}
	}
}