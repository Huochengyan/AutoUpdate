using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoUpdate
{
    public partial class FrmSetServer : Form
    {
        public FrmSetServer()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string input = this.txt_newSoftURL.Text.Trim().ToString();
            //配置了节点可以了。
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["softURL"].Value = input;
            config.Save();
            ConfigurationManager.RefreshSection("appSettings");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Windows.Forms.Clipboard.SetText(lab_newVersionURL.Text);
        }

        private void txt_newSoftURL_TextChanged(object sender, EventArgs e)
        {
            string input = this.txt_newSoftURL.Text.Trim().ToString();
            if (this.txt_newSoftURL.Text == ""|| input.Length<4)
                 return;
            lab_newVersionURL.Text = input.Substring(0, input.Length-4)+".txt";
        }

        private void lab_showDesc_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string msg = "1. 配置升级的压缩包放到升级路径下，文件名：NewVersion.zip\n";
                   msg+= "2. 配置升级的最新版本号放到文件里：NewVersion.txt\n";
                   msg+= "3. 主程序对比NewVersion.txt里的版本号，运行升级程序即可\n";
            MessageBox.Show(msg,"如何配置升级？");
        }

        private void FrmSetServer_Load(object sender, EventArgs e)
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                this.txt_newSoftURL.Text = config.AppSettings.Settings["softURL"].Value;
            }
            catch (Exception ex) {
                MessageBox.Show("读取配置失败！"+ex.ToString());
            }
        }
    }
}
