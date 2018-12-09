using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoreFrame.Util;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreFrame.FileStoreServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public FilesController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        public ActionResult<string> Index()
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            string contentRootPath = _hostingEnvironment.ContentRootPath;

            return Content(webRootPath + "\n" + contentRootPath);
        }

        [HttpPost]
        public PageActionResult UploadArticelFile([FromForm]UploadFileDto model)
        {
            var res = HttpContext.Request.Form["articleId"];
            PageActionResult operateResult = new PageActionResult();
            List<UploadFileViewModel> filelist = new List<UploadFileViewModel>();
            try
            {
                foreach (var formFile in model.Files)
                {
                    if (formFile.Length > 0)
                    {
                        string time = DateTime.Now.ToString("yyyyMMddHHmmss");
                        string fileDir = Path.Combine("wwwroot", "Upload", "File", "Article", time);
                        if (!Directory.Exists(fileDir))
                            Directory.CreateDirectory(fileDir);
                        string filePath = Path.Combine(fileDir, formFile.FileName);
                        string fileExt = Path.GetExtension(formFile.FileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            formFile.CopyTo(stream);
                        }
                        if (ImageHelper.IsWebImage(formFile.FileName))
                        {
                            string thumbConfig = "340x200";
                            ImageFormat imgFormat;
                            #region 图片格式
                            if (string.Compare(fileExt, ".jpg", true) == 0 || string.Compare(fileExt, ".jpeg", true) == 0)
                            {
                                imgFormat = ImageFormat.Jpeg;
                            }
                            else if (string.Compare(fileExt, ".gif", true) == 0)
                            {
                                imgFormat = ImageFormat.Gif;
                            }
                            else if (string.Compare(fileExt, ".bmp", true) == 0)
                            {
                                imgFormat = ImageFormat.Bmp;
                            }
                            else if (string.Compare(fileExt, ".ico", true) == 0)
                            {
                                imgFormat = ImageFormat.Icon;
                            }
                            else
                            {
                                imgFormat = ImageFormat.Png;
                            }
                            #endregion
                            #region 生成缩略图

                            string[] thumbConfigSizes = thumbConfig.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string thumbConfigSize in thumbConfigSizes)
                            {
                                try
                                {
                                    string[] thumbSizeInfo = thumbConfigSize.Split(new string[] { "x" }, StringSplitOptions.RemoveEmptyEntries);
                                    if (thumbSizeInfo.Length < 2)
                                    {
                                        continue;
                                    }
                                    int width = XConvert.ToInt32(thumbSizeInfo[0], 0);
                                    int height = XConvert.ToInt32(thumbSizeInfo[1], 0);
                                    if (width <= 0 || height <= 0)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        string thumbSavePath = FileStoreUtil.GenerateThumbnailSavePath(filePath, width, height);
                                        ImageHelper.BuildThumbnail(filePath, thumbSavePath, imgFormat, width, height, false);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    LogHelper.WriteLog_LocalTxt(ex.ToJson());
                                }
                            }

                            #endregion
                        }

                        UploadFileViewModel file = new UploadFileViewModel
                        {
                            ArticleId = model.ArticleId,
                            FileName = formFile.FileName,
                            FileExt = fileExt,
                            Path = Vars.FILESTORE_SITE + "/Upload/File/Article/" + time + @"/" + formFile.FileName,
                            Thumb = Vars.FILESTORE_SITE + "/Upload/File/Article/" + time + @"/thumbs_" + Path.GetFileNameWithoutExtension(formFile.FileName) + @"/340_200" + fileExt,
                            Directory = time//文件所在目录 
                        };
                        filelist.Add(file);
                    }
                }
                operateResult.Data = new { List = filelist };
                operateResult.Result = PageActionResultType.Success;
                operateResult.Message = "上传成功";
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog_LocalTxt(ex.ToJson());
                operateResult.Result = PageActionResultType.Failed;
                operateResult.Message = "上传失败";
            }
            return (operateResult);
        }


        /// <summary>
        /// 删除文章文件
        /// </summary>
        /// <param name="savePath">文件夹</param>
        /// <returns></returns>
        [HttpPost]
        public PageActionResult DeleteArticelFile(string savePath)
        {
            PageActionResult operateResult = new PageActionResult();

            try
            {
                string saveDirPath = Path.Combine(_hostingEnvironment.WebRootPath, "Upload", "File", "Article", savePath);

                if (Directory.Exists(saveDirPath))
                    Directory.Delete(saveDirPath, true);
                operateResult.Result = PageActionResultType.Success;
                operateResult.Message = "删除成功";
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog_LocalTxt(ex.ToJson());
                operateResult.Result = PageActionResultType.Failed;
                operateResult.Message = "删除失败";
            }

            return (operateResult);

        }


    }
    public class UploadFileViewModel
    {
        public int ArticleId { get; set; }
        public string FileName { get; set; }
        public string FileExt { get; set; }
        public string Path { get; set; }
        public string Thumb { get; set; }
        public string Directory { get; set; }
    }
    public class UploadFileDto
    {
        public int ArticleId { get; set; }
        public IList<IFormFile> Files { get; set; }
    }
}