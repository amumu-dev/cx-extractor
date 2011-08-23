using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;

/**
 * 根据陈鑫在《基于行块分布函数的通用网页正文抽取》中提出的算法，实现C#版本
 *
 * @author 张帆 zfannn@gmail.com
 * @date  2011.03.16
 * 
 */

namespace TextExtractor
{
    class TextExtract
    {
        private const int blockHeight = 3;    // 行快大小(方向向下）
        private const int threshold   = 150;  // 阈值

        private string html;         // 网页源码
        private int textStart;       // 网页正文开始行数
        private int textEnd;         // 网页正文结束行数
        private string textBody;     // 提取到的<body>标签内的内容
        private string[] lines;      // 按行存储textBody的内容
        private List<int> blockLen;  // 每个行快的总字数

        public string content;       // 提取到的网页正文
        public string title;         // 网页标题
        public string webPreview;    // 预览页面（去除JS和图片）

        private bool bJoinMethond;   // ture时使用拼接方法，否则使用直接提取

        // 隐藏默认构造函数
        private TextExtract()
        {
        }

        // 构造函数,参数为HTML源码
        public TextExtract(string newHtml, bool bMethond)
        {
            html = newHtml;

            textStart  = 0;
            textEnd    = 0;
            textBody   = "";
            blockLen   = new List<int>();
            title      = "";
            content    = "";
            webPreview = "";

            bJoinMethond = bMethond;

            extract();
        }

        // 提取网页正文
        public void extract()
        {
            extractTitle();    // 提取标题
            extractBody();     // 提取<body>标签中的内容
            removeTags();      // 去除textBody中的HTML标签
            extractText();     // 提取网页正文,根据bJoinMethond选择不同的方法
            extractPreview();  // 提取预览页面的HTML代码（去除图片和JS）
        }

        private void extractTitle()
        {
            string pattern = @"(?is)<title>(.*?)</title>";
            Match m = Regex.Match(html, pattern);
            if (m.Success)
            {
                title = m.Groups[1].Value;
                title = Regex.Replace(title, @"(?is)\s*", "");
            }
        }

        private void extractBody()
        {
            string pattern = @"(?is)<body.*?</body>";
            Match m = Regex.Match(html, pattern);
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

            textBody = Regex.Replace(textBody, docType, "");
            textBody = Regex.Replace(textBody, comment, "");
            textBody = Regex.Replace(textBody, js, "");
            textBody = Regex.Replace(textBody, css, "");
            textBody = Regex.Replace(textBody, specialChar, "");
            textBody = Regex.Replace(textBody, otherTag, "");
        }

        private void extractText()
        {
            // 去除每行的空白字符
            lines = textBody.Split('\n');
            for (int i = 0; i < lines.Length; i++)
                lines[i] = Regex.Replace(lines[i], @"(?is)\s*", "");

            // 去除上下紧邻行为空,且该行字数小于30的行
            for (int i = 1; i < lines.Length - 1; i++)
            {
                if (lines[i].Length < 30 && 0 == lines[i-1].Length && 0 == lines[i+1].Length)
                    lines[i] = "";
            }

            // 统计去除空白字符后每个行块所含总字数
            for (int i = 0; i < lines.Length - blockHeight; i++)
            {
                int len = 0;
                for (int j = 0; j < blockHeight; j++)
                    len += lines[i + j].Length;
                blockLen.Add(len);
            }

            // 寻找各个正文块起始和结束行,并进行拼接
            textStart = FindTextStart(0);

            if (0 == textStart)
                content = "未能提取到正文!";
            else
            {
                if (bJoinMethond)
                {
                    while (textEnd < lines.Length)
                    {
                        textEnd = FindTextEnd(textStart);
                        content += GetText();
                        textStart = FindTextStart(textEnd);
                        if (0 == textStart)
                            break;
                        textEnd = textStart;
                    }
                } 
                else
                {
                    textEnd = FindTextEnd(textStart);
                    content += GetText();
                }
            }

        }

        // 如果一个行块大小超过阈值,且紧跟其后的1个行块大小不为0,则此行块为起始点（即连续的4行文字长度超过阈值）
        private int FindTextStart(int index)
        {
            for (int i = index; i < blockLen.Count - 1; i++)
            {
                if (blockLen[i] > threshold && blockLen[i + 1] > 0)
                    return i;
            }
            return 0;
        }

        // 起始点之后,如果2个连续行块大小都为0,则认为其是结束点（即连续的4行文字长度为0）
        private int FindTextEnd(int index)
        {
            for (int i = index + 1; i < blockLen.Count - 1; i++)
            {
                if (0 == blockLen[i] &&  0 == blockLen[i + 1])
                    return i;
            }
            return lines.Length - 1;
        }

        private string GetText()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = textStart; i < textEnd; i++)
            {
                if (lines[i].Length != 0)
                    sb.Append(lines[i]).Append("\n\n");
            }
            return sb.ToString();
        }

        private void extractPreview()
        {
            webPreview = Regex.Replace(html, @"(?is)<[^>]*jpg.*?>", "");
            webPreview = Regex.Replace(webPreview, @"(?is)<[^>]*gif.*?>", "");
            webPreview = Regex.Replace(webPreview, @"(?is)<[^>]*png.*?>", "");
            webPreview = Regex.Replace(webPreview, @"(?is)<[^>]*js.*?>", "");
            webPreview = Regex.Replace(webPreview, @"(?is)<script.*?>.*?</script>", "");
        }

    }
}
