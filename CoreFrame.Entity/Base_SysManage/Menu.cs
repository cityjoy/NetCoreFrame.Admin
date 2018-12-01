using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreFrame.Entity.MenuManage
{
    /// <summary>
    /// 菜单表
    /// </summary>
    [Table("Base_Menu")]
    public class Menu
    {

        /// <summary>
        /// Id
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 父级菜单id
        /// </summary>
        public int ParentMenuId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 菜单级别
        /// </summary>
        public string MenuLevel { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 菜单显示的必要权限
        /// </summary>
        public string Permission { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get; set; } = true;


        /// <summary>
        /// 模块值
        /// </summary>
        public string ModuleValue { get; set; }


        /// <summary>
        /// 是否显示查询权限
        /// </summary>
        public bool IsShowSearchPermission { get; set; } 


        /// <summary>
        /// 是否显示管理权限
        /// </summary>
        public bool IsShowManagePermission { get; set; }
        /// <summary>
        /// 子菜单
        /// </summary>
        [NotMapped]
        public virtual List<Menu> SubMenus { get; set; }
    }
}