using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoUpdate.Common
{
    public class FileHelper
    {
        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="DirectoryPath">要创建的文件夹路径</param>
        /// <returns></returns>
        public static string CreateDirectory(string DirectoryPath)
        {
            try
            {
                if (!Directory.Exists(DirectoryPath))
                    Directory.CreateDirectory(DirectoryPath);
                return DirectoryPath;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        /// <summary>
        /// 指定目录覆盖指定目录
        /// </summary>
        /// <param name="strFromPath">要复制的目录</param>
        /// <param name="strToPath">要覆盖的目录</param>
        public static string CopyFolder(string strFromPath, string strToPath)
        {
            try
            {
                //如果源文件夹不存在，则创建
                if (!Directory.Exists(strFromPath))
                {
                    Directory.CreateDirectory(strFromPath);
                }
                //取得要拷贝的文件夹名
                string strFolderName = strFromPath.Substring(strFromPath.LastIndexOf("\\") +
                   1, strFromPath.Length - strFromPath.LastIndexOf("\\") - 1);
                //如果目标文件夹中没有源文件夹则在目标文件夹中创建源文件夹
                if (!Directory.Exists(strToPath + "\\" + strFolderName))
                {
                    Directory.CreateDirectory(strToPath + "\\" + strFolderName);
                }
                //创建数组保存源文件夹下的文件名
                string[] strFiles = Directory.GetFiles(strFromPath);
                //循环拷贝文件
                for (int i = 0; i < strFiles.Length; i++)
                {
                    //取得拷贝的文件名，只取文件名，地址截掉。
                    string strFileName = strFiles[i].Substring(strFiles[i].LastIndexOf("\\") + 1, strFiles[i].Length - strFiles[i].LastIndexOf("\\") - 1);
                    //开始拷贝文件,true表示覆盖同名文件
                    File.Copy(strFiles[i], strToPath + "\\" + strFolderName + "\\" + strFileName, true);
                }
                //创建DirectoryInfo实例
                DirectoryInfo dirInfo = new DirectoryInfo(strFromPath);
                //取得源文件夹下的所有子文件夹名称
                DirectoryInfo[] ZiPath = dirInfo.GetDirectories();
                for (int j = 0; j < ZiPath.Length; j++)
                {
                    //获取所有子文件夹名
                    string strZiPath = strFromPath + "\\" + ZiPath[j].ToString();
                    //把得到的子文件夹当成新的源文件夹，从头开始新一轮的拷贝
                    CopyFolder(strZiPath, strToPath + "\\" + strFolderName);

                   
                }

                return strToPath;
            }
            catch (Exception ex)
            {
                return "";
            }
        }


        /// <summary>
        /// 拷贝oldlab的文件到newlab下面
        /// </summary>
        /// <param name="sourcePath">lab文件所在目录(@"~\labs\oldlab")</param>
        /// <param name="savePath">保存的目标目录(@"~\labs\newlab")</param>
        /// <param name="TypeDown">0 是升级主程序，1 是升级 下载器</param>
        /// <returns>返回:true-拷贝成功;false:拷贝失败</returns>
        public static bool CopyOldLabFilesToNewLab(string sourcePath, string savePath,int TypeDown)
        {
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
            RemoveDirectory(savePath);

            #region //拷贝labs文件夹到savePath下
            try
            {
                string[] labDirs = Directory.GetDirectories(sourcePath);//目录
                string[] labFiles = Directory.GetFiles(sourcePath);//文件
                if (labFiles.Length > 0)
                {
                    for (int i = 0; i < labFiles.Length; i++)
                    {
                        if (ReCopy(Path.GetFileName(labFiles[i]).ToString(),TypeDown))//排除.lab文件
                        {
                            File.Copy(sourcePath + "\\" + Path.GetFileName(labFiles[i]), savePath + "\\" + Path.GetFileName(labFiles[i]), true);
                        }
                    }
                }
                if (labDirs.Length > 0)
                {
                    for (int j = 0; j < labDirs.Length; j++)
                    {
                        Directory.GetDirectories(sourcePath + "\\" + Path.GetFileName(labDirs[j]));

                        //递归调用
                        CopyOldLabFilesToNewLab(sourcePath + "\\" + Path.GetFileName(labDirs[j]), savePath + "\\" + Path.GetFileName(labDirs[j]), TypeDown);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("升级遇到异常：" + ex.ToString());
                return false;
            }
            #endregion
            return true;
        }

        /// <summary>
        /// 指定是否CoPy 这里先直接列出来
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="TypeDown"></param>
        /// <returns></returns>
        public static bool ReCopy(string filepath,int TypeDown)
        {
            //下载器文件跳过
            if (filepath.Contains("AutoUpdate.exe")|| filepath.Contains("ICSharpCode.SharpZipLib.dll"))
            {
                return false;
            }
            //本地密码文件和服务器IP配置文件
            if (filepath.Contains(".ini")|| filepath.Contains("System.xml"))
                return false;
            //本地存储数据文件 
            if (filepath.Contains(".db"))
                return false;
            return true;
        }
        /// <summary>
        /// 要移出只读属性的文件夹目录
        /// </summary>
        /// <param name="RootSystemDir"></param>
        /// <returns></returns>
        public static bool RemoveDirectory(string RootSystemDir)
        {
            try {

                //去除文件夹的只读属性： 
                System.IO.DirectoryInfo DirInfo = new DirectoryInfo(RootSystemDir);
                DirInfo.Attributes = FileAttributes.Normal & FileAttributes.Directory;

                //去除文件的只读属性：　System.IO.File.SetAttributes("filepath", System.IO.FileAttributes.Normal);

                string [] fileinfo = Directory.GetFiles(DirInfo.FullName.ToString());
                for (int i = 0; i < fileinfo.Length; i++)
                {
                    System.IO.File.SetAttributes(fileinfo[i], System.IO.FileAttributes.Normal);
                }


                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 删除指定目录的文件和文件夹
        /// </summary>
        /// <param name="srcPath"></param>
        public static void DelectDir(string srcPath)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(srcPath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)            //判断是否文件夹
                    {
                        DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                        subdir.Delete(true);          //删除子目录和文件
                    }
                    else
                    {
                        File.Delete(i.FullName);      //删除指定文件
                    }
                }
                Directory.Delete(srcPath);
            }
            catch (Exception e)
            {
                
            }
        }
    }
}
