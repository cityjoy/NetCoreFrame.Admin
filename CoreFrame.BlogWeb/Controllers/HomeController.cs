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
using System.Threading.Tasks;
using CoreFrame.BlogWeb.Models;

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
            int pageSize = 8;
            int count = 0;
            List<Tag> tagList = _tagBusiness.GetList();
            var dataList = _articleBusiness.GetPageList(pageSize, page, out count, m => m.IsPublish == true);
            List<ArticleListModel> articleList =
                 dataList
                .OrderByDescending(m=>m.CreateTime)
                .Select(m => new ArticleListModel { Id = m.Id, Title = m.Title, Cover = m.Cover, PageView = m.PageView })
                .ToList();
            ViewBag.TagList = tagList;
            ViewBag.PageIndex = page;
            ViewBag.TotalPage = (int)Math.Ceiling(count / (decimal)pageSize);
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

        public async Task<IActionResult> Search(string keywords,int page=1)
        {
            int count = 0;
            int pageSize = 4;
            List<Article> list = null;
            List<Article> recommendList = null;
            //recommendList = _articleBusiness.GetIQueryableList(m => m.PageView > 50).Take(10).OrderByDescending(m=>m.CreateTime).ToList(); 
            recommendList = await _articleBusiness.GetListAsync(m => m.PageView > 50, 10, m => m.CreateTime, false); 

            if (!string.IsNullOrWhiteSpace(keywords))
            {
                List<int> ids = LuceneIndexHelper.Search(keywords, page, pageSize, out count);
                List<string> fields = new List<string>() { "Id", "Title", "Summary", "Cover" };
                list = _articleBusiness.GetDataListByIds(fields, "id", ids);
            }
            ViewBag.Keywords = keywords;
            ViewBag.RecommendList = recommendList;
            ViewBag.Count = count;
            ViewBag.PageIndex = page;
            ViewBag.TotalPage = (int)Math.Ceiling(count / (decimal)pageSize);
            return View(list);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
