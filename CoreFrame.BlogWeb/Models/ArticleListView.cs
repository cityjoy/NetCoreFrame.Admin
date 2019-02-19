using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreFrame.BlogWeb.Models
{
    public class ArticleListModel
    {

        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }


        /// <summary>
        /// Cover
        /// </summary>
        public string Cover { get; set; }

        /// <summary>
        /// PageView
        /// </summary>
        public int PageView { get; set; }


    }
}
