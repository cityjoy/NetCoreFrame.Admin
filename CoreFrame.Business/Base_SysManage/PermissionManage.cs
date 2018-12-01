using CoreFrame.Business.Common;
using CoreFrame.Entity.Base_SysManage;
using CoreFrame.Entity.MenuManage;
using CoreFrame.Util;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace CoreFrame.Business.Base_SysManage
{
    /// <summary>
    /// 权限管理静态类
    /// </summary>
    public static class PermissionManage
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        static PermissionManage()
        {
            //InitAllPermissionModules();
            //InitAllPermissionValues();
        }

        #endregion

        #region 内部成员

        private static string _permissionConfigFile
        {
            get
            {
                string rootPath = AutofacHelper.GetService<IHostingEnvironment>().WebRootPath;
                return Path.Combine(rootPath, "Config", "Permission.config");
            }
        }
        private static List<PermissionModule> _allPermissionModules { get; set; }
        private static List<string> _allPermissionValues { get; set; }
        private static List<PermissionModule> InitAllPermissionModules()
        {
            List<PermissionModule> resultList = new List<PermissionModule>();
            string filePath = _permissionConfigFile;
            XElement xe = XElement.Load(filePath);
            xe.Elements("module")?.ForEach(aModule =>
            {
                PermissionModule newModule = new PermissionModule();
                resultList.Add(newModule);

                newModule.Name = aModule.Attribute("name")?.Value;
                newModule.Value = aModule.Attribute("value")?.Value;
                newModule.Items = new List<PermissionItem>();
                aModule?.Elements("permission")?.ForEach(aItem =>
                {
                    PermissionItem newItem = new PermissionItem();
                    newModule.Items.Add(newItem);

                    newItem.Name = aItem?.Attribute("name")?.Value;
                    newItem.Value = aItem?.Attribute("value")?.Value;
                });
            });
            Base_MenuBusiness menuBus = new Base_MenuBusiness();

            List<Menu> menuList = menuBus.GetIQueryableList(m => m.MenuLevel == "3").ToList();
            menuList.ForEach(m=> {

                PermissionModule newModule = new PermissionModule() {
                     Name=m.Name,
                     Value=m.ModuleValue,
                     Items = new List<PermissionItem>()
            };
                if (m.IsShowSearchPermission)
                {
                    newModule.Items.Add(new PermissionItem() { Name = "查询", Value = "search" });
                }
                if (m.IsShowManagePermission)
                {
                    newModule.Items.Add(new PermissionItem() { Name = "管理", Value = "manage" });

                }
                //_allPermissionModules = resultList;
                resultList.Add(newModule);


            });
            return resultList;
        }
        private static List<string> InitAllPermissionValues()
        {
            List<string> resultList = new List<string>();

            GetAllPermissionModules()?.ForEach(aModule =>
            {
                aModule.Items?.ForEach(aItem =>
                {
                    resultList.Add($"{aModule.Value}.{aItem.Value}");
                });
            });
            //_allPermissionValues = resList;
            return resultList;

        }
        private static List<PermissionModule> GetPermissionModules(List<string> hasPermissions)
        {
            var permissionModules = GetAllPermissionModules();
            permissionModules.ForEach(aModule =>
            {
                aModule.Items?.ForEach(aItem =>
                {
                    aItem.IsChecked = hasPermissions.Contains($"{aModule.Value}.{aItem.Value}");
                });
            });

            return permissionModules;
        }
        private static string _cacheKey { get; } = "Permission";
        private static string BuildCacheKey(string key)
        {
            return $"{GlobalSwitch.ProjectName}_{_cacheKey}_{key}";
        }

        #endregion

        #region 所有权限

        /// <summary>
        /// 获取所有权限模块
        /// </summary>
        /// <returns></returns>
        public static List<PermissionModule> GetAllPermissionModules()
        {
            return InitAllPermissionModules();
            //return _allPermissionModules.DeepClone();
        }

        /// <summary>
        /// 获取所有权限值
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAllPermissionValues()
        {
            //return _allPermissionValues.DeepClone();
            return InitAllPermissionValues();
        }

        #endregion

        #region 角色权限

        /// <summary>
        /// 获取角色权限模块
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public static List<PermissionModule> GetRolePermissionModules(string roleId)
        {
            BaseBusiness<Base_PermissionRole> _db = new BaseBusiness<Base_PermissionRole>();
            var hasPermissions = _db.GetIQueryable().Where(x => x.RoleId == roleId).Select(x => x.PermissionValue).ToList();

            return GetPermissionModules(hasPermissions);
        }

        #endregion

        #region AppId权限

        /// <summary>
        /// 获取AppId权限模块
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public static List<PermissionModule> GetAppIdPermissionModules(string appId)
        {
            var hasPermissions = GetAppIdPermissionValues(appId);

            return GetPermissionModules(hasPermissions);
        }

        /// <summary>
        /// 获取AppId权限值
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public static List<string> GetAppIdPermissionValues(string appId)
        {
            string cacheKey = BuildCacheKey(appId);
            var permissions = CacheHelper.Cache.GetCache<List<string>>(cacheKey);
            if (permissions == null)
            {
                BaseBusiness<Base_PermissionAppId> _db = new BaseBusiness<Base_PermissionAppId>();
                permissions = _db.GetIQueryable().Where(x => x.AppId == appId).Select(x => x.PermissionValue).ToList();

                CacheHelper.Cache.SetCache(cacheKey, permissions);
            }

            return permissions.DeepClone();
        }

        /// <summary>
        /// 设置AppId权限
        /// </summary>
        /// <param name="appId">AppId</param>
        /// <param name="permissions">权限值列表</param>
        public static void SetAppIdPermission(string appId, List<string> permissions)
        {
            //更新缓存
            string cacheKey = BuildCacheKey(appId);
            CacheHelper.Cache.SetCache(cacheKey, permissions);

            //更新数据库
            BaseBusiness<Base_UnitTest> _db = new BaseBusiness<Base_UnitTest>();
            var Service = _db.Repository;

            Service.Delete<Base_PermissionAppId>(x => x.AppId == appId);

            List<Base_PermissionAppId> insertList = new List<Base_PermissionAppId>();
            permissions.ForEach(newPermission =>
            {
                insertList.Add(new Base_PermissionAppId
                {
                    Id = Guid.NewGuid().ToSequentialGuid(),
                    AppId = appId,
                    PermissionValue = newPermission
                });
            });

            Service.Insert(insertList);
        }

        #endregion

        #region 用户权限

        /// <summary>
        /// 获取用户权限模块
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static List<PermissionModule> GetUserPermissionModules(string userId)
        {
            var hasPermissions = GetUserPermissionValues(userId);

            return GetPermissionModules(hasPermissions);
        }

        /// <summary>
        /// 获取用户拥有的所有权限值
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public static List<string> GetUserPermissionValues(string userId)
        {
            string cacheKey = BuildCacheKey(userId);
            var permissions = CacheHelper.Cache.GetCache<List<string>>(cacheKey)?.DeepClone();

            if (permissions == null)
            {
                UpdateUserPermissionCache(userId);
                permissions = CacheHelper.Cache.GetCache<List<string>>(cacheKey)?.DeepClone();
            }

            return permissions;
        }

        /// <summary>
        /// 设置用户权限
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="permissions">权限值列表</param>
        public static void SetUserPermission(string userId, List<string> permissions)
        {
            //更新数据库
            BaseBusiness<Base_UnitTest> _db = new BaseBusiness<Base_UnitTest>();
            var Service = _db.Repository;

            Service.Delete<Base_PermissionUser>(x => x.UserId == userId);
            var roleIdList = _db.Repository.GetIQueryable<Base_UserRoleMap>().Where(x => x.UserId == userId).Select(x => x.RoleId).ToList();
            var existsPermissions = Service.GetIQueryable<Base_PermissionRole>()
                .Where(x => roleIdList.Contains(x.RoleId) && permissions.Contains(x.PermissionValue))
                .GroupBy(x => x.PermissionValue)
                .Select(x => x.Key)
                .ToList();
            permissions.RemoveAll(x => existsPermissions.Contains(x));

            List<Base_PermissionUser> insertList = new List<Base_PermissionUser>();
            permissions.ForEach(newPermission =>
            {
                insertList.Add(new Base_PermissionUser
                {
                    Id = Guid.NewGuid().ToSequentialGuid(),
                    UserId = userId,
                    PermissionValue = newPermission
                });
            });

            Service.Insert(insertList);

            //更新缓存
            UpdateUserPermissionCache(userId);
        }

        /// <summary>
        /// 清楚所有用户权限缓存
        /// </summary>
        public static void ClearUserPermissionCache()
        {
            BaseBusiness<Base_UnitTest> _db = new BaseBusiness<Base_UnitTest>();
            var userIdList = _db.Repository.GetIQueryable<Base_User>().Select(x => x.UserId).ToList();
            userIdList.ForEach(aUserId =>
            {
                CacheHelper.Cache.RemoveCache(BuildCacheKey(aUserId));
            });
        }

        /// <summary>
        /// 更新用户权限缓存
        /// </summary>
        /// <param name="userId"><用户Id/param>
        public static void UpdateUserPermissionCache(string userId)
        {
            string cacheKey = BuildCacheKey(userId);
            List<string> permissions = new List<string>();

            BaseBusiness<Base_PermissionUser> _db = new BaseBusiness<Base_PermissionUser>();
            var userPermissions = _db.GetIQueryable().Where(x => x.UserId == userId).Select(x => x.PermissionValue).ToList();
            var theUser = _db.Repository.GetIQueryable<Base_User>().Where(x => x.UserId == userId).FirstOrDefault();
            var roleIdList = Base_UserBusiness.GetUserRoleIds(userId);
            var rolePermissions = _db.Repository.GetIQueryable<Base_PermissionRole>().Where(x => roleIdList.Contains(x.RoleId)).GroupBy(x => x.PermissionValue).Select(x => x.Key).ToList();
            var existsPermissions = userPermissions.Concat(rolePermissions).Distinct();

            permissions = existsPermissions.ToList();
            CacheHelper.Cache.SetCache(cacheKey, permissions);
        }

        #endregion

        #region 当前操作用户权限

        /// <summary>
        /// 获取当前操作者拥有的所有权限值
        /// </summary>
        /// <returns></returns>
        public static List<string> GetOperatorPermissionValues()
        {
            if (Operator.IsAdmin())
                return GetAllPermissionValues();
            else
                return GetUserPermissionValues(Operator.UserId);
        }

        /// <summary>
        /// 判断当前操作者是否拥有某项权限值
        /// </summary>
        /// <param name="value">权限值</param>
        /// <returns></returns>
        public static bool OperatorHasPermissionValue(string value)
        {
            return GetOperatorPermissionValues().Select(x => x.ToLower()).Contains(value.ToLower());
        }

        #endregion
    }

    #region 数据模型

    public class PermissionModule
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public List<PermissionItem> Items { get; set; }
    }

    public class PermissionItem
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public bool IsChecked { get; set; }
    }

    #endregion
}