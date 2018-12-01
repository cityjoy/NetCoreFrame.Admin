using CoreFrame.Business.AttachmentManage;
using CoreFrame.Entity.ArticleManage;
using CoreFrame.Entity.AttachmentManage;
using CoreFrame.Util;
using CoreFrame.Web.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoreFrame.Web
{
    [Area("AttachmentManage")]
    public class AttachmentController : BaseMvcController
    {
        private IAttachmentBusiness _attachmentBusiness;
        private readonly IHttpClientFactory _httpClientFactory;
        public AttachmentController(IHttpClientFactory httpClientFactory, IAttachmentBusiness dev_AttachmentBusiness)
        {
            _attachmentBusiness = dev_AttachmentBusiness;
            _httpClientFactory = httpClientFactory;
        }

        #region 视图功能

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UploadFileForm(int articleId)
        {
            ViewBag.ArticleId = articleId;
            return View();
        }

        #endregion

        #region 获取数据

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="condition">查询类型</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public ActionResult GetDataList(string condition, string keyword, Pagination pagination)
        {
            var dataList = _attachmentBusiness.GetDataList(condition, keyword, pagination);

            return Content(pagination.BuildTableResult_DataGrid(dataList).ToJson());
        }

        #endregion

        #region 提交数据
        /// <summary>
        /// 上传数据
        /// </summary>
        /// <param name="fileBase64"></param>
        /// <param name="fileName"></param>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public ActionResult UploadFile(IList<IFormFile> files, int articleId = 0)
        {
            PageActionResult operateResult = new PageActionResult();
            RestApiClient restApiClient = new RestApiClient(Vars.FILESTORE_SITE);
            operateResult = restApiClient.PostFiles<PageActionResult>("/FileHandler/UploadArticelFile", new { ArticleId = articleId }, files[0]);

            if (operateResult != null)
            {
                if (operateResult.Result == PageActionResultType.Failed)
                {
                    return Error(operateResult.Message);
                    
                }
            }
            else
            {
                return Error("上传失败");
            }
            return Success(operateResult.Data);

        }
        /// <summary>
        /// 上传数据
        /// </summary>
        /// <param name="fileBase64"></param>
        /// <param name="fileName"></param>
        /// <param name="articleId"></param>
        /// <returns></returns>
        //public ActionResult UploadFile(string fileBase64, string fileName, int articleId = 0)
        //{
        //    byte[] bytes = fileBase64.ToBytes_FromBase64Str();
        //    PageActionResult operateResult = new PageActionResult();
        //    Dictionary<string, object> formData = new Dictionary<string, object>();
        //    formData.Add("ArticleId", articleId);
        //    formData.Add("FileBytes", bytes);
        //    formData.Add("FileName", fileName);
        //    string result = HttpHelper.PostData("http://localhost:5001/FileHandler/UploadArticelFile", formData, null, ContentType.Json);
        //    operateResult = JsonConvert.DeserializeObject<PageActionResult>(result);

        //    if (operateResult != null)
        //    {
        //        if (operateResult.Result == PageActionResultType.Failed)
        //        {
        //            return Error(operateResult.Message);

        //        }
        //        //else
        //        //{
        //        //    Attachment attach = new Attachment();
        //        //    attach.Secure = true;
        //        //    attach.Name = Path.GetFileNameWithoutExtension(fileName);
        //        //    attach.FileExt = Path.GetExtension(fileName);
        //        //    attach.Type = 1;
        //        //    attach.TargetId = articleId;
        //        //    attach.CreateTime = DateTime.Now;
        //        //    attach.Store = 0;
        //        //    attach.SavePath = operateResult.Data.ToString();
        //        //}

        //    }
        //    return Success(operateResult.Data);

        //}


        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="theData">删除的数据</param>
        public ActionResult DeleteArticelFile(int attachmentId, string path)
        {
            PageActionResult operateResult = new PageActionResult();

            int result = 0;
            if (attachmentId > 0)
            {
                result = _attachmentBusiness.Delete(attachmentId);
                if (result > 0)
                {

                }

            }
            RestApiClient restApiClient = new RestApiClient(Vars.FILESTORE_SITE);
            operateResult = restApiClient.Post<PageActionResult>("/FileHandler/DeleteArticelFile", new { savePath = path });
            if (operateResult != null)
            {
                if (operateResult.Result == PageActionResultType.Failed)
                {
                    return Error(operateResult.Message);

                }
            }
            return Success(operateResult.Data);

        }

        #endregion
    }

}