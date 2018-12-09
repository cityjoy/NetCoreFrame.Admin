using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreFrame.Business.ArticleManage;
using CoreFrame.Entity.ArticleManage;
using Microsoft.AspNetCore.Mvc;

namespace CoreFrame.BlogWeb.Controllers
{
    public class ArticleController : Controller
    {
        private IArticleBusiness _articleBusiness;

        public ArticleController(IArticleBusiness dev_ArticleBusiness)
        {
            _articleBusiness = dev_ArticleBusiness;
        }
        public IActionResult Index( )
        {
             
            return View();
        }
        public IActionResult Detail(int id)
        {
            Article article = _articleBusiness.GetEntity(id);
            if (article == null)
            {
                return NotFound();
            }
            article.PageView = article.PageView + 1;
            _articleBusiness.Update(article);
            return View(article);
        }
    }
}