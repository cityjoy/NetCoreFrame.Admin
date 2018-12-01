using Microsoft.AspNetCore.Mvc;

namespace CoreFrame.Web
{
    [IgnoreLogin]
    public class TestController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RequestDemo()
        {
            return View();
        }
    }
}