using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace TextExtractor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonExtract_Click(object sender, EventArgs e)
        {
            Encoding GB18030  = Encoding.GetEncoding("GB18030");   // GB18030兼容GBK和GB2312
            Encoding UTF8     = Encoding.UTF8;
            Encoding en       = UTF8;
            bool bJoinMethond = true;

            string url = Regex.Replace(textBoxURL.Text, @"(?is)\s*", "");
            if (radioButtonGB18030.Checked)
                en = GB18030;
            if (radioButtonOnce.Checked)
                bJoinMethond = false;

            if (0 == url.Length)
                MessageBox.Show("请输入网址", "提示");
            else
            {
                string html = SourceHTML.GetUrlHTML(url, en);
                if (html.Length != 0)
                {
                    TextExtract te = new TextExtract(html, bJoinMethond);

                    textBoxTitle.Text              = te.title;
                    richTextBoxText.Text           = te.content;
                    webBrowserPreview.DocumentText = te.webPreview;
                }
            }

        }

    }
}
