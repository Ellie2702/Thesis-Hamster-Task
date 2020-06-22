using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace HamsterTask.Helper
{
    class Images
    {
        public static void UploadImageCompany()
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.FileOk += Ofd_FileOk;
            void Ofd_FileOk(object fileDialogObject, CancelEventArgs cea)
            {
                try
                {
                    string fullname = ofd.FileName;
                    string name = ofd.SafeFileName;

                    string base64 = Convert.ToBase64String(File.ReadAllBytes(fullname));
                    //Http.GetRequest("http://127.0.0.1:8080/uploadImage/");
                    int id = -1;
                
                    id = Convert.ToInt32(PostImage("http://localhost:8080/uploadImage/" + Global.Guid.Split('|')[0], base64));
                    // id - imageid
                    Http.GetRequest("http://localhost:8080/" + "companyLogoUploaded/" + Global.Guid.Split('|')[0] + "/" + id.ToString());
                } 
                catch (Exception Ex)
                {

                }
            }
            ofd.ShowDialog();
        }

        public static void UploadImageAvatar()
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.FileOk += Ofd_FileOk;
            void Ofd_FileOk(object fileDialogObject, CancelEventArgs cea)
            {
                try
                {
                    string fullname = ofd.FileName;
                    string name = ofd.SafeFileName;

                    string base64 = Convert.ToBase64String(File.ReadAllBytes(fullname));
                    //Http.GetRequest("http://127.0.0.1:8080/uploadImage/");
                    int id = -1;

                    string num = PostImage("http://localhost:8080/uploadImage/" + Global.Guid.Split('|')[0], base64);
                    id = Convert.ToInt32(num);
                    // id - imageid
                    Http.GetRequest("http://localhost:8080/" + "avatarUploaded/" + Global.Guid.Split('|')[0] + "/" + id.ToString());
                }
                catch (Exception Ex)
                {

                }
            }
            ofd.ShowDialog();
        }
        public static BitmapImage GetMyAvatar()
        {
            using (var ms = new MemoryStream(Convert.FromBase64String(Http.GetRequest("http://localhost:8080/GetAvatarUser/" + Global.Guid.Split('|')[0]))))
            {
                BitmapImage bi = new BitmapImage();
                try
                {
                    bi.BeginInit();
                    bi.CacheOption = BitmapCacheOption.OnLoad;
                    bi.StreamSource = ms;
                    bi.EndInit();
                }
                catch
                {
                    bi = null;
                }

                return bi;
            }
        }
        public static BitmapImage GetCompanyLogo()
        {
            using (var ms = new MemoryStream(Convert.FromBase64String(Http.GetRequest("http://localhost:8080/GetAvatarCompany/" + Global.Guid.Split('|')[0]))))
            {
                BitmapImage bi = new BitmapImage();
                try {
                    bi.BeginInit();
                    bi.CacheOption = BitmapCacheOption.OnLoad;
                    bi.StreamSource = ms;
                    bi.EndInit();
                }
                catch
                {
                    bi = null;
                }

                return bi;
            }
        }
        public BitmapImage GetImage(string name)
        {
            using (var ms = new MemoryStream(new WebClient().DownloadData("http://localhost:8080/getImageFile/" + name)))
            {
                var bi = new BitmapImage();
                bi.BeginInit();
                bi.CacheOption = BitmapCacheOption.OnLoad;
                bi.StreamSource = ms;
                bi.EndInit();

                return bi;
            }
        }

        public BitmapImage ImageFromBytes(byte[] bytes)
        {
            using (var ms = new MemoryStream(bytes))
            {
                var bi = new BitmapImage();
                bi.BeginInit();
                bi.CacheOption = BitmapCacheOption.OnLoad;
                bi.StreamSource = ms;
                bi.EndInit();

                return bi;
            }
        }

        private static string PostImage(string uri, string image)
        {
            var request = (HttpWebRequest)WebRequest.Create(uri);

            //var postData = "base64=" + Uri.EscapeDataString(image);
            var postData = "base64=" + WebUtility.UrlEncode(image);
            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            return new StreamReader(response.GetResponseStream()).ReadToEnd();
        }
    }
}
