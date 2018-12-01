using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreFrame.Entity.AttachmentManage
{
    /// <summary>
    /// 附件
    /// </summary>
    [Table("Attachment")]
    public class Attachment
    {

        /// <summary>
        /// 唯一ID
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 附件所属类型
        /// </summary>
        public byte Type { get; set; }

        /// <summary>
        /// 附件所属对象ID
        /// </summary>
        public int TargetId { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 文件扩展名(不包含.)
        /// </summary>
        public string FileExt { get; set; }

        /// <summary>
        /// 保存点(-1:未上传,0:默认,>1:其它)
        /// </summary>
        public int Store { get; set; }

        /// <summary>
        /// 原图保存路径
        /// </summary>
        public string SavePath { get; set; }
        /// <summary>
        /// 缩略图保存路径
        /// </summary>
        public string Thumb { get; set; }

        /// <summary>
        /// 目录
        /// </summary>
        public string Directory { get; set; }


        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

    }
}