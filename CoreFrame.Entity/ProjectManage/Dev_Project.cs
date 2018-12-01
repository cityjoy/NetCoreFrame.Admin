using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreFrame.Entity.ProjectManage
{
    /// <summary>
    /// 项目表
    /// </summary>
    [Table("Dev_Project")]
    public class Dev_Project
    {

        /// <summary>
        /// 自然主键
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 项目Id
        /// </summary>
        public string ProjectId { get; set; }

        /// <summary>
        /// 项目名
        /// </summary>
        public string ProjectName { get; set; }

    }
}