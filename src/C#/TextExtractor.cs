using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

/**
 * 根据陈鑫在《基于行块分布函数的通用网页正文抽取》中提出的算法，实现C#版本
 *
 * @author 张帆 zfannn@gmail.com
 * @date  2011.03.16
 * 
 */


namespace TextExtractorProgram
{
    // 使用方法请参见Program.cs
    class TextExtractor
    {
        public string sourceHTML = "";  // 完整的网页源文件
        public string title      = "";  // 提取到的网页标题
        public string text       = "";  // 提取到的网页正文
        public int    textStart  = 0;   // 网页正文开始行数
        public int    textEnd    = 0;   // 网页正文结束行数

        private int blockHeight = 3;    // 行快大小
        private int threshold   = 150;  // 阈值

        public string       textBody = "";                // 提取到的<body>标签内的内容
        private string[]    lines;                        // 按行存储textBody的内容
        private List<int>   blockLen = new List<int>();   // 每个行快的总字数

        // 隐藏默认构造函数
        private TextExtractor()
        {
        }

        // 构造函数,参数为HTML源码
        public TextExtractor(string newText)
        {
            sourceHTML = newText;
        }

        // 提取网页正文
        public void extract()
        {
            extractTitle();  // 提取标题
            extractBody();   // 提取<body>标签中的内容
            removeTags();    // 去除textBody中的HTML标签
            extractText();   // 提取网页正文

        }

        private void extractTitle()
        {
            string pattern = @"(?is)<title>(.*?)</title>";
            Match m = Regex.Match(sourceHTML, pattern);
            if (m.Success)
            {
                title = m.Groups[1].Value;
                title = Regex.Replace(title, @"(?is)\s*", "");
            }
        }

        private void extractBody()
        {
            string pattern = @"(?is)<body.*?</body>";
            Match m = Regex.Match(sourceHTML, pattern);
            if (m.Success)
                textBody = m.ToString();
        }

        private void removeTags()
        {
            string docType     = @"(?is)<!DOCTYPE.*?>";
            string comment     = @"(?is)<!--.*?-->";
            string js          = @"(?is)<script.*?>.*?</script>";
            string css         = @"(?is)<style.*?>.*?</style>";
            string specialChar = @"&.{2,8};|&#.{2,8};";
            string otherTag    = @"(?is)<.*?>";

            textBody = Regex.Replace(textBody, docType,     "");
            textBody = Regex.Replace(textBody, comment,     "");
            textBody = Regex.Replace(textBody, js,          "");
            textBody = Regex.Replace(textBody, css,         "");
            textBody = Regex.Replace(textBody, specialChar, "");
            textBody = Regex.Replace(textBody, otherTag,    "");
        }

        private void extractText()
        {
            // 统计去除空白字符后每个行块所含总字数
            lines = textBody.Split('\n');
            for (int i = 0; i < lines.Length; i++)
                lines[i] = Regex.Replace(lines[i], @"(?is)\s*", "");

            for (int i = 0; i < lines.Length - blockHeight; i++)
            {
                int len = 0;
                for (int j = 0; j < blockHeight; j++)
                    len += lines[i + j].Length;
                blockLen.Add(len);
            }

            // 寻找正文起始和结束行
            textStart = FindTextStart();    
            textEnd = FindTextEnd();
            text = GetText();
        }

        // 如果一个行块大小超过阈值,且紧跟其后的3个行块大小都不为0,则此行块为起始点
        private int FindTextStart()
        {
            for (int i = 0; i < blockLen.Count - 3; i++)
            {
                if (blockLen[i] > threshold
                    && blockLen[i + 1] > 0
                    && blockLen[i + 2] > 0
                    && blockLen[i + 3] > 0)
                    return i;
            }
            return 0;
        }

        // 起始点之后,如果两个连续行块大小都为0,则认为其是结束点
        private int FindTextEnd()
        {
            for (int i = textStart + 1; i < blockLen.Count - 1; i++)
            {
                if (blockLen[i] == 0 && blockLen[i + 1] == 0)
                    return i;
            }
            return lines.Length - 1;
        }

        private string GetText()
        {
            if (textStart == 0)
                return "未能提取到正文";

            StringBuilder sb = new StringBuilder();
            for (int i = textStart; i < textEnd; i++ )
            {
                if (lines[i].Length != 0)
                    sb.Append(lines[i]).Append("\n");
            }
            return sb.ToString();
        }
    }
}
