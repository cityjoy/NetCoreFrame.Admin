using CoreFrame.Business;
using CoreFrame.Entity;
using CoreFrame.Util;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CoreFrame.Web
{
    [Area("TagManage")]
    public class TagController : BaseMvcController
    {
        private ITagBusiness _tagBusiness ;
        public TagController(ITagBusiness dev_TagBusiness)
        {
            _tagBusiness = dev_TagBusiness;
        }

        #region 视图功能

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Form(int id = 0)
        {
            Tag model = new Tag();
            if (id > 0)
            {
                model= _tagBusiness.GetEntity(id);
            }

            return View(model);
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
            var dataList = _tagBusiness.GetDataList(condition, keyword, pagination);

            return Content(pagination.BuildTableResult_DataGrid(dataList).ToJson());
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="condition">查询类型</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public ActionResult GetAllTagList()
        {
            var dataList = _tagBusiness.GetList();

            return Content(dataList.ToJson());
        }

        #endregion

        #region 提交数据

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="theData">保存的数据</param>
        public ActionResult SaveData(Tag theData)
        {
            if(theData.Id == 0)
            {
                _tagBusiness.Insert(theData);
            }
            else
            {
                _tagBusiness.Update(theData);
            }

            return Success();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="theData">删除的数据</param>
        public ActionResult DeleteData(string ids)
        {
            _tagBusiness.Delete(ids.ToList<string>());

            return Success("删除成功！");
        }

        #endregion
    }
}