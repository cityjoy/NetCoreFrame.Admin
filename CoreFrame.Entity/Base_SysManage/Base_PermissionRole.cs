using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreFrame.Entity.Base_SysManage
{
    /// <summary>
    /// ��ɫȨ�ޱ�
    /// </summary>
    [Table("Base_PermissionRole")]
    public class Base_PermissionRole
    {

        /// <summary>
        /// �߼�����
        /// </summary>
        [Key]
        public String Id { get; set; }

        /// <summary>
        /// ��ɫ����Id
        /// </summary>
        public String RoleId { get; set; }

        /// <summary>
        /// Ȩ��ֵ
        /// </summary>
        public String PermissionValue { get; set; }

    }
}