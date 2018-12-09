using CoreFrame.Business.AttachmentManage;
using CoreFrame.Entity.ArticleManage;
using CoreFrame.Entity.AttachmentManage;
using CoreFrame.Util;
using CoreFrame.Web.Common;
using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
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
        public async Task<ActionResult> UploadFile(IList<IFormFile> files, int articleId = 0)
        {
            PageActionResult operateResult = new PageActionResult();
            #region HttpClient调用
            //HttpClient client = await CreateTokenRequestHttpClient();
            //MultipartFormDataContent multipartFormDataContent = new MultipartFormDataContent();//创建多种类型的表单内容
            //Dictionary<string, string> keyValues = new Dictionary<string, string>();
            //keyValues["ArticleId"] = articleId.ToString();
            //foreach (var keyValuePair in keyValues)
            //{
            //    StringContent formContent = new StringContent(keyValuePair.Value);

            //    multipartFormDataContent.Add(formContent, keyValuePair.Key);//添加StringContent，表单Name属性一定要传且要和api接口参数一致，否则api端无法获取
            //}
            //foreach (var formFile in files)
            //{
            //    if (formFile.Length > 0)
            //    {
            //        multipartFormDataContent.Add(new StreamContent(formFile.OpenReadStream()), "files", formFile.FileName);//添加StreamContent表单Name属性一定要传且要和api接口参数一致，否则api端无法获取
            //    }
            //}
            //var response = await client.PostAsync(Vars.FILESTORE_SITE + "/FileHandler/UploadArticelFile", multipartFormDataContent);

            //string msgBody = await response.Content.ReadAsStringAsync();
            //try
            //{
            //    operateResult = JsonConvert.DeserializeObject<PageActionResult>(msgBody);
            //}
            //catch (Exception ex) { }

            #endregion

            RestApiClient restApiClient = new RestApiClient(Vars.FILESTORE_SITE);
            await restApiClient.AddAuthorization(Vars.IDENTITYSERVER_SITE + "/connect/token");

            operateResult = restApiClient.PostFiles<PageActionResult>("/api/FileHandler/UploadArticelFile", new { ArticleId = articleId }, files);

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
        /// 创建tokenHttpClient
        /// </summary>
        /// <returns></returns>
        private static async Task<HttpClient> CreateTokenRequestHttpClient()
        {
            var client = new HttpClient();
            TokenRequest request = new TokenRequest
            {
                Address = Vars.FILESTORE_SITE + "/connect/token",
                GrantType = "client_credentials",

                ClientId = "myblogclient666",
                ClientSecret = "myblogsecret999",
            };
            var tokenResponse = await client.RequestTokenAsync(request);
            //AjaxResult res = _homeBus.SubmitLogin(userName, password);
            string res = JsonConvert.SerializeObject(tokenResponse);

            // 调用api
            client.SetBearerToken(tokenResponse.AccessToken);
            return client;
        }



        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="theData">删除的数据</param>
        public async Task<ActionResult> DeleteArticelFile(int attachmentId, string path)
        {
            PageActionResult operateResult = new PageActionResult();

            int result = 0;
            if (attachmentId > 0)
            {
                result = _attachmentBusiness.Delete(m=>m.Id== attachmentId);
                if (result > 0)
                {
                    RestApiClient restApiClient = new RestApiClient(Vars.FILESTORE_SITE);
                    await restApiClient.AddAuthorization(Vars.IDENTITYSERVER_SITE + "/connect/token");
                    operateResult = restApiClient.Post<PageActionResult>("/api/FileHandler/DeleteArticelFile", new { savePath = path });
                }

            }
            else
            {
                RestApiClient restApiClient = new RestApiClient(Vars.FILESTORE_SITE);
                await restApiClient.AddAuthorization(Vars.IDENTITYSERVER_SITE + "/connect/token");
                operateResult = restApiClient.Post<PageActionResult>("/api/FileHandler/DeleteArticelFile", new { savePath = path });
            }
           
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