using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CoreFrame.Business;
using CoreFrame.Entity.ArticleManage;
using CoreFrame.Business.AttachmentManage;
using CoreFrame.Business.ArticleManage;
using CoreFrame.Entity;
using CoreFrame.BlogWeb.Common;
using Sakura.AspNetCore;
using System;

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
        public IActionResult Index(int page = 1)
        {
            int pageSize = 10;
            List<Tag> tagList = _tagBusiness.GetList();
            int count = _articleBusiness.GetIQueryableList(m => m.IsPublish == true).Count();
            List<Article> articleList = _articleBusiness.GetIQueryableList(m => m.IsPublish == true).Take((page) * pageSize).ToList();
            var pagedData = articleList.ToPagedList(pageSize, page);
            ViewBag.TagList = tagList;
            ViewBag.PageIndex = page;
            ViewBag.TotalPage = (int)Math.Ceiling(count / (decimal)pageSize);
            return View(pagedData);
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
