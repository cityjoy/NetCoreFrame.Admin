using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using CoreFrame.Util;
using Newtonsoft.Json;
using CoreFrame.Util.FileSystem;
using System.Web;
using System.Net;

namespace CoreFrame.Util
{
    /// <summary>
    /// 文件存储处理器
    /// </summary>

    public class DefaultFileStoreHandler : IFileStoreHandler
    {
        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="fileStream"></param>
        /// <param name="store"></param>
        /// <param name="saveRelativePath"></param>
        /// <param name="thumbConfig"></param>
        /// <param name="cutImage"></param>
        /// <param name="sourthFile"></param>
        /// <returns></returns>
        public UploadResult Upload(Stream fileStream, int store, string saveRelativePath, string thumbConfig, string cutImage, params string[] sourthFile)
        {
            UploadResult result = new UploadResult();


            string url = GetFileStoreHandlerUrl(store);
            string site = FileStoreUtil.GetFileStoreSite(store);

            NameValueCollection nvc = new NameValueCollection
            {
                { "Signature", SignatureUtil.BuildSignature() },
                { "Action", "1" },
                { "SaveRelativePath", saveRelativePath }
            };
            if (!string.IsNullOrEmpty(thumbConfig))
            {
                nvc.Add("ThumbConfig", thumbConfig);
            }
            if (!string.IsNullOrEmpty(cutImage))
            {
                nvc.Add("CutImageConfig", cutImage);
            }
            //传递源文件的文件名和扩展名
            int num = 0;
            foreach (var file in sourthFile)
            {
                if (num == 0)
                {
                    nvc.Add("SourthFileName", file);
                }
                else if (num == 1)
                {
                    nvc.Add("FileExt", file);
                }
                num++;
            }

            try
            {
                XWebRequest webRequest = new XWebRequest(url)
                {
                    Method = "POST"
                };
                foreach (string key in nvc.Keys)
                {
                    string value = nvc[key];

                    webRequest.AddPostData(key, value);
                }
                webRequest.AddPostFile("__UploadFile", saveRelativePath, fileStream);
                string jsonResult = webRequest.ApplyForm();
                PageActionResult uploadResult = JsonConvert.DeserializeObject<PageActionResult>(jsonResult);

                if (uploadResult != null)
                {
                    result.Message = uploadResult.Message;
                    if (uploadResult.Result == PageActionResultType.Success)
                    {
                        result.IsSuccess = true;
                        result.Store = store;
                        result.RelativePath = uploadResult.Data.ToString();
                        result.FullPath = string.Concat(site, "/", result.RelativePath);
                    }
                    else
                    {
                        result.IsSuccess = false;
                    }
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "没有得到响应结果";
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog_LocalTxt("上传文件异常："+ex);
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="store"></param>
        /// <param name="saveRelativePath"></param>
        /// <param name="sourthFile"></param>
        /// <returns></returns>
        public DeleteFileResult Delete(int store, string saveRelativePath, params string[] sourthFile)
        {
            DeleteFileResult result = new DeleteFileResult();
            string url = GetFileStoreHandlerUrl(store);

            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("Signature", SignatureUtil.BuildSignature());
            nvc.Add("Action", "2");

            nvc.Add("SaveRelativePath", saveRelativePath);
            //传递源文件的扩展名
            int i = 0;
            string attachmentId = string.Empty;
            foreach (var file in sourthFile)
            {
                if (i == 0)
                {
                    nvc.Add("FileExt", file);
                }
                else if (i == 1)//附件id
                {
                    attachmentId = file;
                }
                i++;
            }
            
            try
            {
                XWebRequest webRequest = new XWebRequest(url)
                {
                    Method = "POST"
                };

                foreach (string key in nvc.Keys)
                {
                    string value = nvc[key];

                    webRequest.AddPostData(key, value);
                }
                
                string jsonResult = webRequest.ApplyForm();
                PageActionResult deleteResult = JsonConvert.DeserializeObject<PageActionResult>(jsonResult);

                if (deleteResult != null)
                {
                    result.Message = deleteResult.Message;
                    if (deleteResult.Result == PageActionResultType.Success)
                    {
                        result.IsSuccess = true;
                        //删除webUI的swf文件
                        if (!string.IsNullOrEmpty(attachmentId))
                        {
                            HttpWebResponse httpWebResponse = HttpRequestHelper.CreateGetHttpResponse(GetWebUIHandlerUrl(1) + "/SwfFolder/SwfFileManager.ashx?attachmentId=" + attachmentId, null, "", null);
                        }
                    }
                    else
                    {
                        result.IsSuccess = false;
                    }
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "没有得到响应结果";
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }

            return result;
        }


        /// <summary>
        /// 获取文件处理程序
        /// </summary>
        /// <param name="store"></param>
        /// <returns></returns>
        private string GetFileStoreHandlerUrl(int store)
        {
            string site = FileStoreUtil.GetFileStoreSite(store);
            string url = string.Concat(site, "/FileHandler/Upload");

            return url;
        }
        /// <summary>
        /// 获取文件url
        /// </summary>
        /// <param name="store"></param>
        /// <returns></returns>
        private string GetWebUIHandlerUrl(int store)
        {
            string site = FileStoreUtil.GetFileStoreSite(store);
            string url = site;

            return url;
        }
    }
}
