namespace TextExtractor
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxURL = new System.Windows.Forms.TextBox();
            this.buttonExtract = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageText = new System.Windows.Forms.TabPage();
            this.richTextBoxText = new System.Windows.Forms.RichTextBox();
            this.tabPageWeb = new System.Windows.Forms.TabPage();
            this.webBrowserPreview = new System.Windows.Forms.WebBrowser();
            this.groupBoxCodeType = new System.Windows.Forms.GroupBox();
            this.groupBoxMethod = new System.Windows.Forms.GroupBox();
            this.radioButtonUTF8 = new System.Windows.Forms.RadioButton();
            this.radioButtonGB18030 = new System.Windows.Forms.RadioButton();
            this.radioButtonJoin = new System.Windows.Forms.RadioButton();
            this.radioButtonOnce = new System.Windows.Forms.RadioButton();
            this.tabControl1.SuspendLayout();
            this.tabPageText.SuspendLayout();
            this.tabPageWeb.SuspendLayout();
            this.groupBoxCodeType.SuspendLayout();
            this.groupBoxMethod.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "网址";
            // 
            // textBoxURL
            // 
            this.textBoxURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxURL.Location = new System.Drawing.Point(51, 23);
            this.textBoxURL.Name = "textBoxURL";
            this.textBoxURL.Size = new System.Drawing.Size(440, 21);
            this.textBoxURL.TabIndex = 0;
            // 
            // buttonExtract
            // 
            this.buttonExtract.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExtract.Location = new System.Drawing.Point(497, 21);
            this.buttonExtract.Name = "buttonExtract";
            this.buttonExtract.Size = new System.Drawing.Size(75, 23);
            this.buttonExtract.TabIndex = 1;
            this.buttonExtract.Text = "提取";
            this.buttonExtract.UseVisualStyleBackColor = true;
            this.buttonExtract.Click += new System.EventHandler(this.buttonExtract_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "标题";
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTitle.Location = new System.Drawing.Point(51, 57);
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.Size = new System.Drawing.Size(521, 21);
            this.textBoxTitle.TabIndex = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPageText);
            this.tabControl1.Controls.Add(this.tabPageWeb);
            this.tabControl1.Location = new System.Drawing.Point(18, 94);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(688, 494);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPageText
            // 
            this.tabPageText.Controls.Add(this.richTextBoxText);
            this.tabPageText.Location = new System.Drawing.Point(4, 22);
            this.tabPageText.Name = "tabPageText";
            this.tabPageText.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageText.Size = new System.Drawing.Size(680, 468);
            this.tabPageText.TabIndex = 0;
            this.tabPageText.Text = "正文";
            this.tabPageText.UseVisualStyleBackColor = true;
            // 
            // richTextBoxText
            // 
            this.richTextBoxText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxText.Location = new System.Drawing.Point(-4, -3);
            this.richTextBoxText.Name = "richTextBoxText";
            this.richTextBoxText.Size = new System.Drawing.Size(688, 474);
            this.richTextBoxText.TabIndex = 0;
            this.richTextBoxText.Text = "";
            // 
            // tabPageWeb
            // 
            this.tabPageWeb.Controls.Add(this.webBrowserPreview);
            this.tabPageWeb.Location = new System.Drawing.Point(4, 22);
            this.tabPageWeb.Name = "tabPageWeb";
            this.tabPageWeb.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageWeb.Size = new System.Drawing.Size(680, 468);
            this.tabPageWeb.TabIndex = 1;
            this.tabPageWeb.Text = "网页预览";
            this.tabPageWeb.UseVisualStyleBackColor = true;
            // 
            // webBrowserPreview
            // 
            this.webBrowserPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowserPreview.Location = new System.Drawing.Point(0, 0);
            this.webBrowserPreview.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserPreview.Name = "webBrowserPreview";
            this.webBrowserPreview.ScriptErrorsSuppressed = true;
            this.webBrowserPreview.Size = new System.Drawing.Size(684, 472);
            this.webBrowserPreview.TabIndex = 0;
            // 
            // groupBoxCodeType
            // 
            this.groupBoxCodeType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxCodeType.Controls.Add(this.radioButtonGB18030);
            this.groupBoxCodeType.Controls.Add(this.radioButtonUTF8);
            this.groupBoxCodeType.Location = new System.Drawing.Point(590, 10);
            this.groupBoxCodeType.Name = "groupBoxCodeType";
            this.groupBoxCodeType.Size = new System.Drawing.Size(122, 34);
            this.groupBoxCodeType.TabIndex = 5;
            this.groupBoxCodeType.TabStop = false;
            // 
            // groupBoxMethod
            // 
            this.groupBoxMethod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxMethod.Controls.Add(this.radioButtonOnce);
            this.groupBoxMethod.Controls.Add(this.radioButtonJoin);
            this.groupBoxMethod.Location = new System.Drawing.Point(590, 50);
            this.groupBoxMethod.Name = "groupBoxMethod";
            this.groupBoxMethod.Size = new System.Drawing.Size(121, 32);
            this.groupBoxMethod.TabIndex = 0;
            this.groupBoxMethod.TabStop = false;
            // 
            // radioButtonUTF8
            // 
            this.radioButtonUTF8.AutoSize = true;
            this.radioButtonUTF8.Checked = true;
            this.radioButtonUTF8.Location = new System.Drawing.Point(6, 12);
            this.radioButtonUTF8.Name = "radioButtonUTF8";
            this.radioButtonUTF8.Size = new System.Drawing.Size(47, 16);
            this.radioButtonUTF8.TabIndex = 0;
            this.radioButtonUTF8.TabStop = true;
            this.radioButtonUTF8.Text = "UTF8";
            this.radioButtonUTF8.UseVisualStyleBackColor = true;
            // 
            // radioButtonGB18030
            // 
            this.radioButtonGB18030.AutoSize = true;
            this.radioButtonGB18030.Location = new System.Drawing.Point(56, 12);
            this.radioButtonGB18030.Name = "radioButtonGB18030";
            this.radioButtonGB18030.Size = new System.Drawing.Size(65, 16);
            this.radioButtonGB18030.TabIndex = 1;
            this.radioButtonGB18030.Text = "GB18030";
            this.radioButtonGB18030.UseVisualStyleBackColor = true;
            // 
            // radioButtonJoin
            // 
            this.radioButtonJoin.AutoSize = true;
            this.radioButtonJoin.Checked = true;
            this.radioButtonJoin.Location = new System.Drawing.Point(6, 10);
            this.radioButtonJoin.Name = "radioButtonJoin";
            this.radioButtonJoin.Size = new System.Drawing.Size(47, 16);
            this.radioButtonJoin.TabIndex = 0;
            this.radioButtonJoin.TabStop = true;
            this.radioButtonJoin.Text = "拼接";
            this.radioButtonJoin.UseVisualStyleBackColor = true;
            // 
            // radioButtonOnce
            // 
            this.radioButtonOnce.AutoSize = true;
            this.radioButtonOnce.Location = new System.Drawing.Point(56, 10);
            this.radioButtonOnce.Name = "radioButtonOnce";
            this.radioButtonOnce.Size = new System.Drawing.Size(59, 16);
            this.radioButtonOnce.TabIndex = 1;
            this.radioButtonOnce.TabStop = true;
            this.radioButtonOnce.Text = "不拼接";
            this.radioButtonOnce.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AcceptButton = this.buttonExtract;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 599);
            this.Controls.Add(this.groupBoxMethod);
            this.Controls.Add(this.groupBoxCodeType);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.textBoxTitle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonExtract);
            this.Controls.Add(this.textBoxURL);
            this.Controls.Add(this.label1);
            this.MinimumSize = new System.Drawing.Size(739, 637);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "正文提取";
            this.tabControl1.ResumeLayout(false);
            this.tabPageText.ResumeLayout(false);
            this.tabPageWeb.ResumeLayout(false);
            this.groupBoxCodeType.ResumeLayout(false);
            this.groupBoxCodeType.PerformLayout();
            this.groupBoxMethod.ResumeLayout(false);
            this.groupBoxMethod.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxURL;
        private System.Windows.Forms.Button buttonExtract;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageText;
        private System.Windows.Forms.TabPage tabPageWeb;
        private System.Windows.Forms.RichTextBox richTextBoxText;
        private System.Windows.Forms.WebBrowser webBrowserPreview;
        private System.Windows.Forms.GroupBox groupBoxCodeType;
        private System.Windows.Forms.GroupBox groupBoxMethod;
        private System.Windows.Forms.RadioButton radioButtonGB18030;
        private System.Windows.Forms.RadioButton radioButtonUTF8;
        private System.Windows.Forms.RadioButton radioButtonJoin;
        private System.Windows.Forms.RadioButton radioButtonOnce;
    }
}

