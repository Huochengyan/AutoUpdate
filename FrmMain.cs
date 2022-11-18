using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace AutoUpdate
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        string Rootpath = Environment.CurrentDirectory;

        /// <summary>
        /// 本地存储目录
        /// </summary>
        string ZipFilePath = Common.FilePathConfig.DownZIPPath;
        /// <summary>
        /// 本地解压目录
        /// </summary>
        string UnFilePath = Common.FilePathConfig.UnDownZIPPath;

        /// <summary>
        /// 开始升级程序客户端
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Start_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            Thread th = new Thread(StartUpdate);
            th.IsBackground = true;
            th.Start();
        }
        /// <summary>
        /// 开始下载文件 升级
        /// </summary>
        private void StartUpdate()
        {
            bool result = Common.HttpHelper.HttpDownload(Common.FilePathConfig.DownZIPUrl, ZipFilePath, this.progressBar1, this.label2_tishi);
            timer1.Enabled = false;
            if (result)
            {
                RefThisForm("下载成功！");
                Thread.Sleep(100);
                RefThisForm("正在解压，请稍后");
                if (!Directory.Exists(UnFilePath))
                    Directory.CreateDirectory(UnFilePath);
                //UnFilePath = new FileInfo(ZipFilePath).DirectoryName;


                string reusult = Common.ZIPHelper.UnZipFile(ZipFilePath,UnFilePath);
                if (reusult != "")
                {
                    RefThisForm("解压成功！");
                    CheckUnZIPFile(reusult);
                }
                else
                {
                    RefThisForm("解压失败！压缩包路径：" + ZipFilePath);
                }
            }
            else
            {
                MsgShow("下载失败！");
            }


        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            InitConfig();
            FrmMain.CheckForIllegalCrossThreadCalls = false;

            //删除陈旧的历史下载记录ZIP信息
            try
            {
                File.Delete(ZipFilePath);
            }
            catch (Exception ex)
            {

            }
            //检查下载器文件的dll

            button1_Start_Click(null, null);

        }
        private void CheckFile()
        {
            string UnZipdllPath = Rootpath + "/ICSharpCode.SharpZipLib.dll";         
            if (!File.Exists(UnZipdllPath))
            {
                MessageBox.Show("下载器文件丢失：ICSharpCode.SharpZipLib.dll");
            }
        }
        private void InitConfig() {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                Common.FilePathConfig.DownZIPUrl = config.AppSettings.Settings["softURL"].Value;

            }
            catch (Exception ex)
            {
                MessageBox.Show("读取配置失败！" + ex.ToString());
            }
        }
        /// <summary>
        /// 检查文件 解压
        /// </summary>
        /// <param name="UnZipFilePath"></param>
        private void CheckUnZIPFile(string UnZipFileDirPath)
        { 
            string mainexe = UnZipFileDirPath + "/LongDeTools.exe";
            Directory.SetCurrentDirectory(Directory.GetParent(UnZipFileDirPath).FullName);
            string OldFolder = Directory.GetCurrentDirectory();
            if (!File.Exists(mainexe))
            {
                MessageBox.Show("未能找到主程序：" + mainexe);
                return;
            }
            else {
                //覆盖源目录文件 
                // string result=Common.FileHelper.CopyFolder(OldFolder,UnZipFileDirPath);
                //MessageBox.Show("请确认开始替换原始文件！");
                RefThisForm("安装中..."); //RefThisForm("替换原始主程序中。。。。");
               
                bool result1=Common.FileHelper.CopyOldLabFilesToNewLab(UnZipFileDirPath,OldFolder,0);
                if (result1)
                {
                    RefThisForm("安装中..."); //RefThisForm("替换原始程序完毕。。。。");
                    //清空解压的文件
                    FileInfo fileinfo = new FileInfo(UnZipFileDirPath);
                    try
                    {
                        if (Directory.Exists(fileinfo.FullName))
                        {
                            // MessageBox.Show("要删除的文件目录：" + fileinfo.FullName);
                            Common.FileHelper.DelectDir(fileinfo.FullName);
                            Common.FileHelper.DelectDir(Common.FilePathConfig.UnDownZIPPath);
                            GC.Collect();
                        }
                    }
                    catch (Exception ex)
                    {
                        MsgShow("清理下载文件垃圾失败！"+ex.ToString());
                    }
                }
                else
                {
                    MsgShow("升级失败！");
                }
            }
            //2. 启动新下载的程序  
        
            StartNewSystem();

            GC.Collect();

        }
        /// <summary>
        /// 启动最新下载的程序
        /// </summary>
        private void StartNewSystem()
        {
            //string path = Environment.CurrentDirectory;
            //Directory.SetCurrentDirectory(Directory.GetParent(path).FullName);
            //path = Directory.GetCurrentDirectory();
            //string mainexe = path + "/LongDeTools.exe";
            string  path=System.Windows.Forms.Application.StartupPath;
            string mainexe = "";
            if (Directory.Exists(path)) {
                mainexe = Directory.GetParent(path)+"/" + "LongDeTools.exe";
            }
            if (File.Exists(mainexe))
            {
               Process.Start(mainexe);
                RefThisForm("升级完成");
                MessageBox.Show("升级到最新版本！！！");
                this.Close();
               
            }
            else {
                MsgShow("要启动的文件不存在！！！"+mainexe);
            }
            ///清理下载的文件的缓存垃圾
            GC.Collect();
            
        }
        private void RefThisForm(string text)
        {
            this.Text = text;
        }
        /// <summary>
        /// 时间  戳 事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            //Console.WriteLine("时间timer\r\n"+DateTime.Now.ToString()+"\r\n");

            label2_tishi.Text = progressBar1.Value/1048576+"M/"+ progressBar1.Maximum/ 1048576 + "M";
            if(progressBar1.Value== progressBar1.Maximum)
                label2_tishi.Text= progressBar1.Maximum / 1048576 + "M/" + progressBar1.Maximum / 1048576 + "M";
        }

        private void MsgShow(string msg) {
            MessageBox.Show(msg);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmSetServer frm = new FrmSetServer();
            frm.ShowDialog();
        }
    }
}
