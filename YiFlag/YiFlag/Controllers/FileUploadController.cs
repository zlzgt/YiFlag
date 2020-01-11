using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YiFlag.Tools;

namespace YiFlag.Controllers
{
    public class FileUploadController : Controller
    {
        // GET: FileUpload
        public ActionResult UploadImg()
        {
            try
            {
                var files = Request.Files;
                var filepath = FileUploadHelper.UploadSingleFile("/files/img/", files[0]); //上传图片
                var result = new { uploaded = 1,url =filepath };
                return Json(result);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog($"上传图片时失败{ex.Message}+{ex.InnerException}",Log4NetLevel.Error);
                return Json(new { uploaded=0, error = "上传图片失败" });
            }
        }
        public JsonResult UploadAudio()
        {
            try
            {
                var files = Request.Files;
                var filepath = FileUploadHelper.UploadAudioFile("/files/audio/", files[0]); //上传音频
                var json = new
                {
                    uploaded = 1,
                    url = filepath
                };
                return Json(json);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog($"上传音频时失败{ex.Message}+{ex.InnerException}", Log4NetLevel.Error);
                return Json(new { uploaded = 0, error = "上传音频时失败" });
            }
        }
        public JsonResult UploadVideo()
        {
            try
            {
                var files = Request.Files;
                var filepath = FileUploadHelper.UploadVideoFile("/files/video/", files[0]); //上传视频
                var json = new
                {
                    uploaded = 1,
                    url= filepath
                };
                return Json(json);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog($"上传视频失败{ex.Message}+{ex.InnerException}", Log4NetLevel.Error);
                return Json(new { uploaded = 0, error ="上传视频时失败" });
            }
        }
    }
}