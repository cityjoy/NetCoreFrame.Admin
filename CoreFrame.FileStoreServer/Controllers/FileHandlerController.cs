using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreFrame.Util;
using System.IO;
using System.Drawing.Imaging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using CoreFrame.FileStore.Filter;
using Microsoft.AspNetCore.Authorization;
using CoreFrame.FileStoreServer.Models;

namespace CoreFrame.FileStoreServer.Controllers
{
    [ApiController]
    [Authorize]
    public class FileHandlerController : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public FileHandlerController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [Route("api/FileHandler/Index")]
        public ActionResult Index()
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            string contentRootPath = _hostingEnvironment.ContentRootPath;

            return Content(webRootPath + "\n" + contentRootPath);
        }

        /// <summary>
        /// 上传文件 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/FileHandler/UploadArticelFile")]
        public PageActionResult UploadArticelFile([FromForm]UploadFileDto model)
        {
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
        [Route("api/FileHandler/DeleteArticelFile")]
        public PageActionResult DeleteArticelFile([FromForm]string savePath)
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

            return  (operateResult);

        }


    }

}
