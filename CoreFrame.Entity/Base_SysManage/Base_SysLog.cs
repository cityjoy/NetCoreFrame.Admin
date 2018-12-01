using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreFrame.Entity.Base_SysManage
{
    /// <summary>
    /// ϵͳ��־��
    /// </summary>
    [Table("Base_SysLog")]
    public class Base_SysLog
    {

        /// <summary>
        /// ��������
        /// </summary>
        [Key]
        public String Id { get; set; }

        /// <summary>
        /// ��־����
        /// </summary>
        public String LogType { get; set; }

        /// <summary>
        /// ��־����
        /// </summary>
        public String LogContent { get; set; }

        /// <summary>
        /// ����Ա�û���
        /// </summary>
        public String OpUserName { get; set; }

        /// <summary>
        /// ��־��¼ʱ��
        /// </summary>
        public DateTime? OpTime { get; set; }

    }
}