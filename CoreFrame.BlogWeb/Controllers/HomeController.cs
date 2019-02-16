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
using CoreFrame.Entity;
using CoreFrame.BlogWeb.Common;

namespace CoreFrame.BlogWeb.Controllers
{
    public class HomeController : Controller
    {
         IAttachmentBusiness _attachmentBusiness;
         IArticleBusiness _articleBusiness;
         ITagBusiness _tagBusiness;

        public HomeController(IArticleBusiness dev_ArticleBusiness, IAttachmentBusiness dev_attachmentBusiness, ITagBusiness tagBusiness)
        {
            _articleBusiness = dev_ArticleBusiness;
            _attachmentBusiness = dev_attachmentBusiness;
            _tagBusiness = tagBusiness;
        }
        public IActionResult Index()
        {
            List<Tag> tagList = _tagBusiness.GetList();
            List<Article> articleList = _articleBusiness.GetIQueryableList(m => m.IsPublish == true).Take(10).ToList();
            ViewBag.TagList = tagList;
            return View(articleList);
        }


        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Search(string keywords)
        {
            List<Article> alist = null;
            List<Article> recommendList = null;
            recommendList = _articleBusiness.GetIQueryableList(m => m.PageView > 50).Take(10).ToList();

            if (!string.IsNullOrWhiteSpace(keywords))
            {
                List<int> ids = LuceneIndexHelper.Search(keywords);
                List<string> fields = new List<string>() { "Id", "Title", "Summary", "Cover" };
                alist = _articleBusiness.GetDataListByIds(fields, "id", ids);
            }
            ViewBag.Keywords = keywords;
            ViewBag.RecommendList = recommendList;
            return View(alist);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
