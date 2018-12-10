using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreFrame.Entity;
using CoreFrame.Entity.ArticleManage;

namespace CoreFrame.Web.Common
{
    public class AutoMapperConfiguration: Profile
    {
         
        public AutoMapperConfiguration()
        {
            CreateMap<ArticleDto, Article>();
            CreateMap<Article, ArticleDto>();
        }
    }
}
 
