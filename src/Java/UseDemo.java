/**
 * @author Xin Chen
 * Created on 2009-11-11
 * Updated on 2010-08-09
 * Email:  xchen@ir.hit.edu.cn
 * Blog:   http://hi.baidu.com/����ͬ��_����
 */
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.URL;


/**
 * TextExtractor���ܲ�����.
 */

public class UseDemo {
	
	public static void main(String[] args) throws IOException {
		
		/* 
		 * ������վ��
		 * �ٶȲ��Ϳռ�             http://hi.baidu.com/liyanhong/
		 * ��������������������Ϣ	http://ent.sina.com.cn/music/roll.html
		 * ��Ѷ������������Ϣ		http://ent.qq.com/m_news/mnews.htm
		 * �Ѻ���������				http://music.sohu.com/news.shtml
		 * ��������ҵ��ѧУ����Ϣ�� http://today.hit.edu.cn/
		 * ��������ҵ��ѧУ�������� http://news.hit.edu.cn/
		 */


		/* ע�⣺����ֻΪչʾ��ȡЧ������������ҳ�������⣬getHTMLֻ�ܽ���GBK�������ҳ�������������� */
		String content = getHTML("http://ent.qq.com/a/20100417/000119.htm");

		// http://ent.sina.com.cn/y/2010-04-18/08332932833.shtml
		// http://ent.qq.com/a/20100416/000208.htm
		// http://ent.sina.com.cn/y/2010-04-18/15432932937.shtml
		// http://ent.qq.com/a/20100417/000119.htm
		// http://news.hit.edu.cn/articles/2010/04-12/04093006.htm
	

		/* 
		 * ������ȡ����ҳ�����������ɿ�����ű���δ�޳�ʱ��ֻҪ�������ֵ���ɡ�
		 * �෴������Ҫ��ȡ���������ݳ��Ƚ϶̣�����ֻ��һ�仰�����ţ����С����ֵ���ɡ�
		 * ��ֵ����׼ȷ���������ٻ����½���ֵ��С��������󣬵����Ա�֤�鵽ֻ��һ�仰������ 
		 */
		//TextExtract.setThreshold(76); // Ĭ��ֵ86

		System.out.println("got html");
		System.out.println(TextExtract.parse(content));
	}


	public static String getHTML(String strURL) throws IOException {
		URL url = new URL(strURL);
		BufferedReader br = new BufferedReader(new InputStreamReader(url.openStream()));
		String s = "";
		StringBuilder sb = new StringBuilder("");
		while ((s = br.readLine()) != null) {
			sb.append(s + "\n");
		}
		return sb.toString();
	}
}
