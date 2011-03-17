using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TextExtractorProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            string sourceFile = "";
            Encoding GB2132 = Encoding.GetEncoding("GB2312");

            // 测试文档来源于http://baike.baidu.com/view/2394.htm
            StreamReader sr = new StreamReader(@"F:\temp\tiananmen.htm", GB2132);
            sourceFile = sr.ReadToEnd();
           
            // 提取网页正文
            TextExtractor te = new TextExtractor(sourceFile);  // sourceFile 为 string格式 的HTML源码
            te.extract();                                      // 网页正文已经提取到  te.text

            // 输出提取到的网页正文
            StreamWriter sw = new StreamWriter(@"F:\temp\tiananmen.txt", false);
            sw.Write(te.text);


            sw.Close();
            sr.Close();
        }
    }
}
