using CoreFrame.Business.Base_SysManage;
using CoreFrame.Business.Common;
using CoreFrame.Util;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CoreFrame.Web
{
    public class HomeController : BaseMvcController
    {
        public HomeController(IHomebusiness homebus )
        {
            _homeBus = homebus;
        }

        private IHomebusiness _homeBus { get; }

        #region 视图功能

        public ActionResult Index()
        {
            return View();
        }

        [IgnoreLogin]
        public ActionResult Login()
        {
            if (Operator.Logged())
            {

                string loginUrl = Url.Content("~/");
                string script = $@"    
<html>
    <script>
        top.location.href = '{loginUrl}';
    </script>
</html>
";
                return Content(script, "text/html");
            }

            return View();
        }

        public ActionResult Desktop()
        {
            return View();
        }

        #endregion

        #region 获取数据

        #endregion

        #region 提交数据

        [IgnoreLogin]
        public ActionResult SubmitLogin(string userName, string password)
        {
            AjaxResult res = _homeBus.SubmitLogin(userName, password);

            return Content(res.ToJson());
        }

        /// <summary>
        /// 注销
        /// </summary>
        public ActionResult Logout()
        {
            Operator.Logout();

            return Success("注销成功！");
        }

        #endregion

        public async Task<ActionResult> Token()
        {
            var client = new HttpClient();
            TokenRequest request = new TokenRequest
            {
                Address = "http://localhost:5001/connect/token",
                GrantType = "client_credentials",

                ClientId = "myblogclient666",
                ClientSecret = "myblogsecret999",
            };
            var tokenResponse = await client.RequestTokenAsync(request);
            //AjaxResult res = _homeBus.SubmitLogin(userName, password);
            string res = JsonConvert.SerializeObject(tokenResponse);

            // 调用api
            client.SetBearerToken(tokenResponse.AccessToken);
            MultipartFormDataContent content = new MultipartFormDataContent();
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            keyValues["id"] = "100".ToString();
            FormUrlEncodedContent formContent = new FormUrlEncodedContent(keyValues);
            formContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            content.Add(formContent);
            var response = await client.PostAsync("http://localhost:5001/FileHandler/index", content);
            res += response.StatusCode.ToString();
                var contents = await response.Content.ReadAsStringAsync();
            res += contents;
            return Content(res);
        }

    }
}