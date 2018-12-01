using Microsoft.AspNetCore.Mvc;

namespace CoreFrame.Web
{
    public class CommonController : Controller
    {
        public ActionResult ShowBigImg(string url)
        {
            ViewData["url"] = url;
            return View();
        }
    }
}