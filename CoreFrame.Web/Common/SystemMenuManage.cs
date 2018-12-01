using CoreFrame.Business.Base_SysManage;
using CoreFrame.Business.Common;
using CoreFrame.Entity.MenuManage;
using CoreFrame.Util;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace CoreFrame.Web
{
    /// <summary>
    /// 系统菜单管理
    /// </summary>
    public class SystemMenuManage
    {
        #region 构造函数

        /// <summary>
        /// 静态构造函数
        /// </summary>
        public SystemMenuManage()
        {
            InitAllMenu();
        }

        #endregion

        #region 私有成员

        private static string _configFile
        {
            get
            {
                string rootPath = AutofacHelper.GetService<IHostingEnvironment>().WebRootPath;
                return Path.Combine(rootPath, "Config", "SystemMenu.config");
            }
        }
        private List<Menu> _allMenu { get; set; }
        private static List<Menu> InitAllMenu()
        {
            Action<Menu, XElement> SetMenuProperty = (menu, element) =>
            {
                List<string> exceptProperties = new List<string> { "Id", "IsShow" };
                var types = menu.GetType().GetProperties().Where(x => !exceptProperties.Contains(x.Name)).ToList();
                types.ForEach(aProperty =>
                {
                    aProperty.SetValue(menu, element.Attribute(aProperty.Name)?.Value);
                });
            };

            string filePath = _configFile;
            XElement xe = XElement.Load(filePath);
            List<Menu> menus = new List<Menu>();
            int menuIndex = 10000;
            xe.Elements("FirstMenu")?.ForEach(aElement1 =>
            {
                menuIndex++;
                Menu newMenu1 = new Menu() { Id = menuIndex };
                menus.Add(newMenu1);
                SetMenuProperty(newMenu1, aElement1);
                newMenu1.SubMenus = new List<Menu>();
                aElement1.Elements("SecondMenu")?.ForEach(aElement2 =>
                {
                    menuIndex++;
                    Menu newMenu2 = new Menu() { Id = menuIndex };
                    newMenu1.SubMenus.Add(newMenu2);
                    SetMenuProperty(newMenu2, aElement2);
                    newMenu2.SubMenus = new List<Menu>();

                    aElement2.Elements("ThirdMenu")?.ForEach(aElement3 =>
                    {
                        menuIndex++;
                        Menu newMenu3 = new Menu() { Id = menuIndex };
                        newMenu2.SubMenus.Add(newMenu3);
                        SetMenuProperty(newMenu3, aElement3);
                        if (!newMenu3.Url.IsNullOrEmpty())
                        {
                            UrlHelper urlHelper = new UrlHelper(AutofacHelper.GetService<IActionContextAccessor>().ActionContext);
                            newMenu3.Url = urlHelper.Content(newMenu3.Url);
                        }
                    });
                });
            });
            Base_MenuBusiness menuService = new Base_MenuBusiness();

            List<Menu> allMenu = menuService.GetList();
            if (allMenu.Count > 0)
            {
                List<Menu> firstMenu = allMenu.Where(m => m.MenuLevel == "1").ToList();
                if (firstMenu.Count > 0)
                {
                    firstMenu.ForEach(m1 =>
                    {
                        m1.SubMenus = allMenu.Where(m => m.ParentMenuId == m1.Id).ToList();

                        if (m1.SubMenus.Count > 0)
                        {
                            m1.SubMenus.ForEach(m2 =>
                            {
                                List<Menu> sub2 = allMenu.Where(m => m.ParentMenuId == m2.Id).ToList();
                                m2.SubMenus = sub2;


                            });

                        }
                        menus.Add(m1);

                    });

                }

            }

            if (GlobalSwitch.RunModel == RunModel.LocalTest)
            {
                Menu newMenu1 = new Menu
                {
                    Id = menuIndex++,
                    Name = "开发",
                    Icon = "icon_menu_prod",
                    SubMenus = new List<Menu>()
                };
                menus.Add(newMenu1);
                Menu newMenu1_1 = new Menu
                {
                    Id = menuIndex++,
                    Name = "快速开发",
                    SubMenus = new List<Menu>()
                };
                newMenu1.SubMenus.Add(newMenu1_1);
                Menu newMenu1_1_1 = new Menu
                {
                    Id = menuIndex++,
                    Name = "代码生成",
                    Url = GetUrl("~/Base_SysManage/RapidDevelopment/Index")
                };
                newMenu1_1.SubMenus.Add(newMenu1_1_1);

                Menu newMenu1_1_2 = new Menu
                {
                    Id = menuIndex++,
                    Name = "数据库连接管理",
                    Url = GetUrl("~/Base_SysManage/Base_DatabaseLink/Index")
                };
                newMenu1_1.SubMenus.Add(newMenu1_1_2);

                Menu newMenu1_1_3 = new Menu
                {
                    Id = menuIndex++,
                    Name = "UEditor Demo",
                    Url = GetUrl("~/Demo/UMEditor")
                };
                newMenu1_1.SubMenus.Add(newMenu1_1_3);

                Menu newMenu1_1_4 = new Menu
                {
                    Id = menuIndex++,
                    Name = "文件上传Demo",
                    Url = GetUrl("~/Demo/UploadFileIndex")
                };
                newMenu1_1.SubMenus.Add(newMenu1_1_4);

                Menu newMenu1_1_5 = new Menu
                {
                    Id = menuIndex++,
                    Name = "菜单权限管理",
                    Url = GetUrl("~/Base_SysManage/Base_Menu/Index")
                };
                newMenu1_1.SubMenus.Add(newMenu1_1_5);

            }

            return menus;
        }
        private static void SetSubMenuShow(List<Menu> menus, List<string> userPermissionValues, int level)
        {
            if (level >= 4)
                return;
            menus?.ForEach(aMenu =>
            {
                if (!aMenu.Permission.IsNullOrEmpty() && !userPermissionValues.Contains(aMenu.Permission))
                {
                    aMenu.IsShow = false;
                    return;
                }
                else
                {
                    SetSubMenuShow(aMenu.SubMenus, userPermissionValues, level + 1);
                }

                if ((!aMenu?.SubMenus?.Any(x => x.IsShow)) ?? false)
                    aMenu.IsShow = false;
            });
        }
        private static string GetUrl(string virtualUrl)
        {
            UrlHelper urlHelper = new UrlHelper(AutofacHelper.GetService<IActionContextAccessor>().ActionContext);
            return urlHelper.Content(virtualUrl);
        }

        #endregion

        #region 外部接口

        /// <summary>
        /// 获取系统所有菜单
        /// </summary>
        /// <returns></returns>
        public List<Menu> GetAllSysMenu()
        {
            return _allMenu.DeepClone();
        }

        /// <summary>
        /// 获取用户菜单
        /// </summary>
        /// <returns></returns>
        public static List<Menu> GetOperatorMenu()
        {
            List<Menu> resList = InitAllMenu();

            if (Operator.IsAdmin())
                return resList;

            var userPermissions = PermissionManage.GetUserPermissionValues(Operator.UserId);
            SetSubMenuShow(resList, userPermissions, 1);

            return resList;
        }

        #endregion
    }

    #region 数据模型

    //public class Menu
    //{
    //    public string Id { get; set; } = Guid.NewGuid().ToString();
    //    public string Name { get; set; }
    //    public string Icon { get; set; }
    //    public string Url { get; set; }
    //    public string Permission { get; set; }
    //    public bool IsShow { get; set; } = true;
    //    public List<Menu> SubMenus { get; set; }
    //}

    #endregion
}