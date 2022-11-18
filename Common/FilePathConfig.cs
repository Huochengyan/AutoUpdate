using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoUpdate.Common
{
    /// <summary>
    /// 设置 文件ZIP 操作目录 类
    /// </summary>
    public  class FilePathConfig
    {
   
        /// <summary>
        /// 要下载更新程序的网络路径
        /// </summary>
        public static string DownZIPUrl = "http://51souta.com/software/NewVersion.zip";

        ///// <summary>
        ///// 下载文件后的ZIP 文件保存的本地目录
        ///// </summary>
        //public static string DownZIPPath = System.Environment.CurrentDirectory+"/"+DateTime.Now.ToString("yyyy_NewVersion") +"/New.zip";

        ///// <summary>
        ///// 解压ZIP后的保存目录 注意这里是文件夹
        ///// </summary>
        //public static string UnDownZIPPath = System.Environment.CurrentDirectory+"//"+ DateTime.Now.ToString("yyyy_NewVersion_File");    

        /// <summary>
        /// 下载文件后的ZIP 文件保存的本地目录
        /// </summary>
        public static string DownZIPPath = System.Environment.CurrentDirectory + "/" + DateTime.Now.ToString("yyyy")+ "_NewVersion" + "/New.zip";

        /// <summary>
        /// 解压ZIP后的保存目录 注意这里是文件夹
        /// </summary>
        public static string UnDownZIPPath = System.Environment.CurrentDirectory + "//" + DateTime.Now.ToString("yyyy")+ "_NewVersion_File";
    }
}
