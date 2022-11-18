using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace AutoUpdate.Common
{
    /// <summary>
    /// 专门的下载文件类
    /// </summary>
    public class HttpHelper
    {
        /// <summary>
        /// http下载文件
        /// </summary>
        /// <param name="url"></param>
        /// <param name="path"></param>
        /// </summary>
        /// <param name="url">下载文件地址</param>
        /// <param name="path">文件存放地址，包含文件名</param>
        /// <param name="progressBar1"></param>
        /// <param name="label_Info">提示信息 ，时时提示下载多少K的 </param>
        /// <returns></returns>
        public static bool HttpDownload(string url, string path, ProgressBar progressBar1, Label label_Info)
        {
            string tempPath = System.IO.Path.GetDirectoryName(path) + @"\temp";
            if (!System.IO.Directory.Exists(tempPath))
                System.IO.Directory.CreateDirectory(tempPath);  //创建临时文件目录
            string tempFile = tempPath + @"\" + System.IO.Path.GetFileName(path) + ".temp"; //临时文件
            try
            {
                FileStream fs = new FileStream(tempFile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                // 设置参数
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                //发送请求并获取相应回应数据
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                Stream responseStream = response.GetResponseStream();
                //创建本地文件写入流
                //Stream stream = new FileStream(tempFile, FileMode.Create);
                byte[] bArr = new byte[1024];
                int size = responseStream.Read(bArr, 0, (int)bArr.Length);


                if (response.ContentLength != 0)
                {
                    if (request.ContentLength > int.MaxValue)
                    {
                        MessageBox.Show("要下载的文件太大，超出范围！");
                        return false;
                    }

                    progressBar1.Maximum = Convert.ToInt32(response.ContentLength.ToString());
                }

                while (size > 0)
                {
                    //stream.Write(bArr, 0, size);
                    fs.Write(bArr, 0, size);
                    size = responseStream.Read(bArr, 0, (int)bArr.Length);

                    progressBar1.Invoke(new Action(() => {
                        progressBar1.Value += size;
                    }));
                }
                fs.Close();
                responseStream.Close();
                System.IO.File.Move(tempFile, path);

                if (System.IO.File.Exists(tempFile))
                {
                    System.IO.File.Delete(tempFile);    //存在则删除
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        delegate  void SetTextCallBack(string text,Label txt_a);
        private static void SetText(string text, Label txt_a)
        {
            if (txt_a.InvokeRequired)
            {
                SetTextCallBack stcb = new SetTextCallBack(SetText);
                txt_a.Invoke(stcb, new object[] { text, txt_a });
            }
            else
            {
                txt_a.Text = text;
            }
        }



        public static string SendGetRequest(string url)
        {
            try
            {

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "get";

                //if (CookiesContainer == null)
                //{
                //    CookiesContainer = new CookieContainer();
                //}

                //request.CookieContainer = CookiesContainer;  //启用cookie

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream response_stream = response.GetResponseStream();

                int count = (int)response.ContentLength;
                int offset = 0;
                byte[] buf = new byte[count];
                while (count > 0)  //读取返回数据
                {
                    int n = response_stream.Read(buf, offset, count);
                    if (n == 0) break;
                    count -= n;
                    offset += n;
                }
                return Encoding.UTF8.GetString(buf);
            }
            catch
            {
                return null;
            }
        }




    }
}
