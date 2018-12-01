using CoreFrame.Business.Base_SysManage;
using CoreFrame.Entity.MenuManage;
using CoreFrame.Util;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
namespace CoreFrame.Web
{
    [Area("Base_SysManage")]
    public class Base_MenuController : BaseMvcController
    {
        private IBase_MenuBusiness _base_MenuBusiness ;

        public Base_MenuController(IBase_MenuBusiness dev_Base_MenuBusiness)
        {
            _base_MenuBusiness = dev_Base_MenuBusiness;
        }

        #region 视图功能

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Form(int id)
        {
            var theData = id.IsNullOrEmpty() ? new Menu() : _base_MenuBusiness.GetEntity(id);

            return View(theData);
        }

        #endregion

        #region 获取数据

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="condition">查询类型</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public ActionResult GetDataList(string condition, string keyword, Pagination pagination)
        {
            var dataList = _base_MenuBusiness.GetDataList(condition, keyword, pagination);

            return Content(pagination.BuildTableResult_DataGrid(dataList).ToJson());
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="menuLevel">查询类型</param>
        /// <returns></returns>
        public ActionResult GetMenuLevel(string menuLevel)
        {
            var dataList = _base_MenuBusiness.GetIQueryableList(m=>m.MenuLevel== menuLevel).ToList();

            return Content(dataList.ToJson());
        }


        #endregion

        #region 提交数据

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="theData">保存的数据</param>
        public ActionResult SaveData(Menu theData)
        {
            
            if (theData.MenuLevel == "3")
            {
                theData.Permission = theData.ModuleValue + "." + "search";
            }
            if (theData.Id==0)
            {
                if (theData.MenuLevel == "1")
                {
                    if (_base_MenuBusiness.GetIQueryableList(m => m.ModuleValue == theData.ModuleValue).Any())
                    {
                        return Error("保存失败,模块值已存在");

                    }
                }
                _base_MenuBusiness.Insert(theData);
            }
            else
            {
                if (theData.MenuLevel == "1")
                {
                    if (_base_MenuBusiness.GetIQueryableList(m => m.ModuleValue == theData.ModuleValue && m.MenuLevel == "1"&&m.Id!=theData.Id).Any())
                    {
                        return Error("保存失败,模块值已存在");

                    }
                }
                _base_MenuBusiness.Update(theData);
            }

            return  Success();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="theData">删除的数据</param>
        public ActionResult DeleteData(string ids)
        {
            _base_MenuBusiness.Delete(ids.ToList<string>());

            return Success("删除成功！");
        }

        #endregion
    }
}