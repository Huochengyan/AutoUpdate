namespace AutoUpdate
{
    partial class FrmSetServer
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSetServer));
            this.btn_save = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_newSoftURL = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lab_showDesc = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.lab_newVersionURL = new System.Windows.Forms.LinkLabel();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(483, 133);
            this.btn_save.Margin = new System.Windows.Forms.Padding(4);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(100, 29);
            this.btn_save.TabIndex = 0;
            this.btn_save.Text = "保存";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(53, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "版本获取:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(53, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "文件获取:";
            // 
            // txt_newSoftURL
            // 
            this.txt_newSoftURL.Location = new System.Drawing.Point(135, 38);
            this.txt_newSoftURL.Name = "txt_newSoftURL";
            this.txt_newSoftURL.Size = new System.Drawing.Size(406, 25);
            this.txt_newSoftURL.TabIndex = 2;
            this.txt_newSoftURL.Text = "http://xxxx/NewVersion.zip";
            this.txt_newSoftURL.TextChanged += new System.EventHandler(this.txt_newSoftURL_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lab_showDesc);
            this.groupBox1.Controls.Add(this.linkLabel2);
            this.groupBox1.Controls.Add(this.lab_newVersionURL);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btn_save);
            this.groupBox1.Controls.Add(this.txt_newSoftURL);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(34, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(683, 205);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "配置升级文件和路径";
            // 
            // lab_showDesc
            // 
            this.lab_showDesc.AutoSize = true;
            this.lab_showDesc.LinkColor = System.Drawing.Color.Magenta;
            this.lab_showDesc.Location = new System.Drawing.Point(61, 147);
            this.lab_showDesc.Name = "lab_showDesc";
            this.lab_showDesc.Size = new System.Drawing.Size(97, 15);
            this.lab_showDesc.TabIndex = 5;
            this.lab_showDesc.TabStop = true;
            this.lab_showDesc.Text = "查看配置说明";
            this.lab_showDesc.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lab_showDesc_LinkClicked);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.LinkColor = System.Drawing.Color.Blue;
            this.linkLabel2.Location = new System.Drawing.Point(305, 147);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(127, 15);
            this.linkLabel2.TabIndex = 4;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "双击此处复制路径";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // lab_newVersionURL
            // 
            this.lab_newVersionURL.AutoSize = true;
            this.lab_newVersionURL.Location = new System.Drawing.Point(135, 93);
            this.lab_newVersionURL.Name = "lab_newVersionURL";
            this.lab_newVersionURL.Size = new System.Drawing.Size(215, 15);
            this.lab_newVersionURL.TabIndex = 3;
            this.lab_newVersionURL.TabStop = true;
            this.lab_newVersionURL.Text = "http://xxxx/NewVersion.txt";
            // 
            // FrmSetServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(750, 258);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmSetServer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "软件升级";
            this.Load += new System.EventHandler(this.FrmSetServer_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_newSoftURL;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.LinkLabel lab_newVersionURL;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.LinkLabel lab_showDesc;
    }
}

