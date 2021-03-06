using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreFrame.Entity
{
    /// <summary>
    /// 标签
    /// </summary>
    [Table("Tag")]
    public class Tag
    {

        /// <summary>
        /// Id
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 标签名称
        /// </summary>
        public string TagName { get; set; }

    }
}