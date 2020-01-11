using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace YiFlag.Tools
{
    public class FileUploadHelper
    {
        /// <summary>
        /// 上传单个图片
        /// </summary>
        /// <param name="filePath">图片目录</param>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string UploadSingleFile(string filePath, HttpPostedFileBase file)
        {
            string fileTypes = "gif,jpg,jpeg,png,bmp";

            if (file == null)
                throw new Exception("请选择上传图片");

            string dirPath = HttpContext.Current.Server.MapPath(filePath);
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            string fileName = file.FileName;
            string fileExt = Path.GetExtension(fileName).ToLower();

            if (string.IsNullOrEmpty(fileExt) || fileTypes.Contains(fileExt))
            {
                throw new Exception("请上传" + fileTypes + "格式");
            }
            string guidStr = Guid.NewGuid().ToString().Substring(0, 4);
            string newFileName = DateTime.Now.ToString("yyyyMMddhhmmssffff") + "_" + guidStr + fileExt;

            string fullFile = dirPath + newFileName;
            file.SaveAs(fullFile);
            string fileUrl = Path.Combine(filePath, newFileName);
            return fileUrl;
        }

        /// <summary>
        /// 上传单个图片
        /// </summary>
        /// <param name="filePath">图片目录</param>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string UploadAudioFile(string filePath, HttpPostedFileBase file)
        {
            string fileTypes = "mp3,ogg,wav";

            if (file == null)
                throw new Exception("请选择上传");

            string dirPath = HttpContext.Current.Server.MapPath(filePath);
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            string fileName = file.FileName;
            string fileExt = Path.GetExtension(fileName).ToLower();

            if (string.IsNullOrEmpty(fileExt) || fileTypes.Contains(fileExt))
            {
                throw new Exception("请上传" + fileTypes + "格式");
            }
            string guidStr = Guid.NewGuid().ToString().Substring(0, 4);
            string newFileName = DateTime.Now.ToString("yyyyMMddhhmmssffff") + "_" + guidStr + fileExt;

            string fullFile = dirPath + newFileName;
            file.SaveAs(fullFile);
            string fileUrl = Path.Combine(filePath, newFileName);
            return fileUrl;
        }
        public static string UploadVideoFile(string filePath, HttpPostedFileBase file)
        {
            if (file == null)
                throw new Exception("请选择上传");

            string dirPath = HttpContext.Current.Server.MapPath(filePath);
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            string fileName = file.FileName;
            string fileExt = Path.GetExtension(fileName).ToLower();
            string guidStr = Guid.NewGuid().ToString().Substring(0, 4);
            string newFileName = DateTime.Now.ToString("yyyyMMddhhmmssffff") + "_" + guidStr + fileExt;
            string fullFile = dirPath + newFileName;
            file.SaveAs(fullFile);
            string fileUrl = Path.Combine(filePath, newFileName);
            return fileUrl;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public static void DeleteFile(string filePath)
        {
            try
            {

                if (!string.IsNullOrEmpty(filePath))
                {
                    string fullFilePath = HttpContext.Current.Server.MapPath(filePath);
                    if (File.Exists(fullFilePath))
                    {
                        File.Delete(fullFilePath);
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// 图片 转为    base64编码的文本
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        public static string ImgToBase64String(Bitmap bmp)
        {
            //Bitmap bmp = new Bitmap(Imagefilename);
            //this.pictureBox1.Image = bmp;
            //FileStream fs = new FileStream(Imagefilename + ".txt", FileMode.Create);
            //StreamWriter sw = new StreamWriter(fs);

            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] arr = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(arr, 0, (int)ms.Length);
            ms.Close();
            String strbaser64 = Convert.ToBase64String(arr);

            return strbaser64;
        }

        /// <summary>
        ///  Base64转为    图片
        /// </summary>
        /// <param name="base64Img"></param>
        /// <returns></returns>
        public static Bitmap Base64StringToImage(string base64Img)
        {
            base64Img = base64Img.Substring(base64Img.IndexOf(',') + 1);
            byte[] bytes = Convert.FromBase64String(base64Img);
            MemoryStream ms = new MemoryStream();
            ms.Write(bytes, 0, bytes.Length);
            Bitmap bmp = new Bitmap(ms);
            return bmp;
        }

        /// <summary>
        /// base64Img 保存图片
        /// </summary>
        /// <param name="base64Img"></param>
        /// <param name="imgPath"></param>
        /// <param name="imgFormat">EX: System.Drawing.Imaging.Jpeg </param>
        public static void SaveFile(string base64Img, string imgPath, ImageFormat imgFormat)
        {
            string dir = HttpContext.Current.Server.MapPath(Path.GetDirectoryName(imgPath));
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            var bitmap = Base64StringToImage(base64Img);
            bitmap.Save(HttpContext.Current.Server.MapPath(imgPath), imgFormat);
        }
    }
}
