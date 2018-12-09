using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreFrame.BlogWeb.Models;
using CoreFrame.Business;
using CoreFrame.Entity.ArticleManage;
using CoreFrame.Business.AttachmentManage;
using CoreFrame.Business.ArticleManage;

namespace CoreFrame.BlogWeb.Controllers
{
    public class HomeController : Controller
    {
        private IAttachmentBusiness _attachmentBusiness;
        private IArticleBusiness _articleBusiness;

        public HomeController(IArticleBusiness dev_ArticleBusiness, IAttachmentBusiness dev_attachmentBusiness)
        {
            _articleBusiness = dev_ArticleBusiness;
            _attachmentBusiness = dev_attachmentBusiness;
        }
        public IActionResult Index()
        {
            List<Article> articleList = _articleBusiness.GetIQueryableList(m => m.Id > 0).Take(10).ToList();
            return View(articleList);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
