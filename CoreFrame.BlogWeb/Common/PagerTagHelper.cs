using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreFrame.BlogWeb.Common
{
    public class MyPagerTagHelper : TagHelper
    {
        public PagerOption PagerOption { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            if (PagerOption.CountNum < 1)
            {
                PagerOption.CountNum = 5;
            }
            if (PagerOption.PageIndex < 1)
            {
                PagerOption.PageIndex = 1;
            }
            if (PagerOption.PageIndex > PagerOption.TotalPage)
            {
                PagerOption.PageIndex = PagerOption.TotalPage;
            }
            if (PagerOption.TotalPage <= 1)
            {
                return;
            }
            var queryarr = PagerOption.Query.Where(c => c.Key != "pageindex" && c.Key != "pagesize").ToList();
            string queryurl = string.Empty;
            foreach (var item in queryarr)
            {
                queryurl += "&" + item.Key + "=" + item.Value;
            }

            output.Content.AppendFormat("<a class=\"prev\" href=\"{0}?pageindex={1}&pagesize={2}{3}\">首页</a>", PagerOption.Url, 1, PagerOption.PageSize, queryurl);
            output.Content.AppendFormat("<a class=\"prev\" href=\"{0}?pageindex={1}&pagesize={2}{3}\">上一页</a>", PagerOption.Url, PagerOption.PageIndex - 1, PagerOption.PageSize, queryurl);

            #region 分页逻辑
            if (PagerOption.PageIndex == 1)
            {
                for (int i = PagerOption.PageIndex; i <= PagerOption.PageIndex + PagerOption.CountNum - 1; i++)
                {
                    if (i <= PagerOption.TotalPage)
                    {
                        if (PagerOption.PageIndex == i)
                        {
                            output.Content.AppendFormat("<span class=\"current\">{0}</span>", i);
                        }
                        else
                        {
                            output.Content.AppendFormat("<a class=\"num\" href=\"{0}?pageindex={1}&pagesize={2}{3}\">{1}</a>", PagerOption.Url, i, PagerOption.PageSize, queryurl);

                        }
                    }
                }
            }

            else if (PagerOption.PageIndex % PagerOption.CountNum == 0)
            {
                for (int i = PagerOption.PageIndex - (PagerOption.CountNum / 2); i <= PagerOption.PageIndex + PagerOption.CountNum / 2; i++)
                {
                    if (i <= PagerOption.TotalPage)
                    {
                        if (PagerOption.PageIndex == i)
                        {
                            output.Content.AppendFormat("<span class=\"current\">{0}</span>", i);
                        }
                        else
                        {
                            output.Content.AppendFormat("<a class=\"num\" href=\"{0}?pageindex={1}&pagesize={2}{3}\">{1}</a>", PagerOption.Url, i, PagerOption.PageSize, queryurl);

                        }
                    }
                }
            }
            else
            {
                int startindex = PagerOption.CountNum * (PagerOption.PageIndex / PagerOption.CountNum) + 1;
                for (int i = startindex; i <= startindex + PagerOption.CountNum - 1; i++)
                {
                    if (i <= PagerOption.TotalPage)
                    {
                        if (PagerOption.PageIndex == i)
                        {
                            output.Content.AppendFormat("<span class=\"current\">{0}</span>", i);
                        }
                        else
                        {
                            output.Content.AppendFormat("<a class=\"num\" href=\"{0}?pageindex={1}&pagesize={2}{3}\">{1}</a>", PagerOption.Url, i, PagerOption.PageSize, queryurl);

                        }
                    }
                }

            }

            #endregion

            //for (int i = 1; i <= PagerOption.TotalPage; i++)
            //{


            //    if (PagerOption.PageIndex == i)
            //    {
            //        output.Content.AppendFormat("<span class=\"current\">{0}</span>", i);
            //    }
            //    else
            //    {
            //        output.Content.AppendFormat("<a class=\"num\" href=\"{0}?pageindex={1}&pagesize={2}{3}\">{1}</a>", PagerOption.Url, i, PagerOption.PageSize, queryurl);

            //    }

            //}
            output.Content.AppendFormat("<a class=\"next\" href=\"{0}?pageindex={1}&pagesize={2}{3}\">下一页</a>", PagerOption.Url, PagerOption.PageIndex + 1, PagerOption.PageSize, queryurl);
            output.Content.AppendFormat("<a class=\"next\" href=\"{0}?pageindex={1}&pagesize={2}{3}\">尾页</a>", PagerOption.Url, PagerOption.TotalPage, PagerOption.PageSize, queryurl);

            base.Process(context, output);
        }
    }
}

 
