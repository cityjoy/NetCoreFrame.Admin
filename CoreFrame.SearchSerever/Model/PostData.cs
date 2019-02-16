using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreFrame.SearchSerever.Model
{
    public class Article
    {
        public Guid Id { get; set; }
        [Text(Name = "title")]
        public string Title { get; set; }
        [Text(Name = "description")]
        public string Description { get; set; }
        [Keyword(Name = "tag")]
        public string[] Tags { get; set; }
    }

     
}
