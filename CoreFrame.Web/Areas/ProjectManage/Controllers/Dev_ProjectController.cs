using CoreFrame.Business.ProjectManage;
using CoreFrame.Entity.ProjectManage;
using CoreFrame.Util;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CoreFrame.Web
{
    [Area("ProjectManage")]
    public class Dev_ProjectController : BaseMvcController
    {
        private IDev_ProjectBusiness _dev_ProjectBusiness ;

        public Dev_ProjectController(IDev_ProjectBusiness dev_Dev_ProjectBusiness)
        {
            _dev_ProjectBusiness = dev_Dev_ProjectBusiness;
        }

        #region 视图功能

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Form(string id)
        {
            var theData = id.IsNullOrEmpty() ? new Dev_Project() : _dev_ProjectBusiness.GetEntity(id);

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
            var dataList = _dev_ProjectBusiness.GetDataList(condition, keyword, pagination);

            return Content(pagination.BuildTableResult_DataGrid(dataList).ToJson());
        }

        #endregion

        #region 提交数据

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="theData">保存的数据</param>
        public ActionResult SaveData(Dev_Project theData)
        {
            if(theData.Id.IsNullOrEmpty())
            {
                theData.Id = Guid.NewGuid().ToSequentialGuid();

                _dev_ProjectBusiness.Insert(theData);
            }
            else
            {
                _dev_ProjectBusiness.Update(theData);
            }

            return Success();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="theData">删除的数据</param>
        public ActionResult DeleteData(string ids)
        {
            _dev_ProjectBusiness.Delete(ids.ToList<string>());

            return Success("删除成功！");
        }

        #endregion
    }
}