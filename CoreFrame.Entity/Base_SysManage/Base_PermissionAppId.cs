using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreFrame.Entity.Base_SysManage
{
    /// <summary>
    /// AppIdȨ�ޱ�
    /// </summary>
    [Table("Base_PermissionAppId")]
    public class Base_PermissionAppId
    {

        /// <summary>
        /// ��������
        /// </summary>
        [Key]
        public String Id { get; set; }

        /// <summary>
        /// AppId
        /// </summary>
        public String AppId { get; set; }

        /// <summary>
        /// Ȩ��ֵ
        /// </summary>
        public String PermissionValue { get; set; }

    }
}