using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreFrame.Entity.ArticleManage
{
    /// <summary>
    /// 文章
    /// </summary>
    [Table("Article")]
    public class Article
    {

        /// <summary>
        /// 唯一id
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// TagIdList
        /// </summary>
        public string TagIdList { get; set; }
        
        /// <summary>
        /// Cover
        /// </summary>
        public string Cover { get; set; }

        /// <summary>
        /// PageView
        /// </summary>
        public int PageView { get; set; }

        /// <summary>
        /// Summary
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// Content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// CreateTime
        /// </summary>
        public DateTime CreateTime { get; set; }
       
        /// <summary>
        /// IsPublish
        /// </summary>
        public bool IsPublish { get; set; }
    }

    public class ArticleDto
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
        /// TagIdList
        /// </summary>
        public string TagIdList { get; set; }

        /// <summary>
        /// Cover
        /// </summary>
        public string Cover { get; set; }

        /// <summary>
        /// PageView
        /// </summary>
        public int PageView { get; set; }

        /// <summary>
        /// Summary
        /// </summary>
        public string Summary { get; set; }
        /// <summary>
        /// Content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// CreateTime
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// IsPublish
        /// </summary>
        public bool IsPublish { get; set; }

        public string Attachments { get; set; }




    }

    public class AttachmentDto
    {
        public string Path { get; set; }
        public string Directory { get; set; }

    }
}