using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreFrame.Entity.Base_SysManage
{
    /// <summary>
    /// ���ݿ�����
    /// </summary>
    [Table("Base_DatabaseLink")]
    public class Base_DatabaseLink
    {

        /// <summary>
        /// ��������
        /// </summary>
        [Key]
        public String Id { get; set; }

        /// <summary>
        /// ������
        /// </summary>
        public String LinkName { get; set; }

        /// <summary>
        /// �����ַ���
        /// </summary>
        public String ConnectionStr { get; set; }

        /// <summary>
        /// ���ݿ�����
        /// </summary>
        public String DbType { get; set; }

        /// <summary>
        /// ������
        /// </summary>
        public String SortNum { get; set; }

    }
}