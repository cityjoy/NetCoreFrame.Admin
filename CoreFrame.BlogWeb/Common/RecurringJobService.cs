using CoreFrame.Business.ArticleManage;
using CoreFrame.Entity.ArticleManage;
using Hangfire.RecurringJobExtensions;
using Hangfire.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreFrame.BlogWeb.Common
{
    public class RecurringJobService
{

          IArticleBusiness _articleBusiness = new ArticleBusiness() ;
        #region 使用特性标识要执行的任务计划
        //[RecurringJob("*/1 * * * *")]
        //[Queue("jobs")]
        //public void TestJob1(PerformContext context)
        //{
        //    context.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")} TestJob1 Running ...");
        //}
        //[RecurringJob("*/2 * * * *", RecurringJobId = "TestJob2")]
        //[Queue("jobs")]
        //public void TestJob2(PerformContext context)
        //{
        //    context.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")} TestJob2 Running ...");
        //}
        [RecurringJob("*/59 * * * *", RecurringJobId = "MakeIndex")]
        public void MakeIndex(PerformContext context)
        {
            List<ArticleIndex> list = _articleBusiness.GetIQueryable().Select(m => new ArticleIndex { Id = m.Id, Title = m.Title, Summary = m.Summary, CreateTime = m.CreateTime }).ToList();
            bool result = LuceneIndexHelper.MakeIndex(list);
        }
        //[RecurringJob("*/5 * * * *", "jobs")]
        //public void InstanceTestJob(PerformContext context)
        //{
        //    context.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")} InstanceTestJob Running ...");
        //}

        //[RecurringJob("*/6 * * * *", "UTC", "jobs")]
        //public static void StaticTestJob(PerformContext context)
        //{
        //    context.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")} StaticTestJob Running ...");
        //}
        #endregion
    }
}
