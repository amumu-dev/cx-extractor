using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using System.Text.RegularExpressions;

namespace TextExtractor
{

    public static class SourceHTML
    {
        private static Encoding GB18030 = Encoding.GetEncoding("GB18030");   // GB18030兼容GBK和GB2312
        private static Encoding UTF8    = Encoding.UTF8;

        public static string GetUrlHTML(string url, Encoding en)
        {
            //数据包头部
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method    = "GET";
            request.Accept    = "*/*";
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1)";

            //服务器返回的内容
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response != null && HttpStatusCode.OK == response.StatusCode)
            {
                StreamReader sr = new StreamReader(response.GetResponseStream(), en);
                return sr.ReadToEnd();
            }

            return "";
        }

    }

}
