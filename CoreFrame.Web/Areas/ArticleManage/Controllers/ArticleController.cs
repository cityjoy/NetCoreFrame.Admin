using CoreFrame.Business.ArticleManage;
using CoreFrame.Business.AttachmentManage;
using CoreFrame.Entity.ArticleManage;
using CoreFrame.Entity.AttachmentManage;
using CoreFrame.Util;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;

namespace CoreFrame.Web
{
    [Area("ArticleManage")]
    public class ArticleController : BaseMvcController
    {
        private IAttachmentBusiness _attachmentBusiness;
        private IArticleBusiness _articleBusiness;

        public ArticleController(IArticleBusiness dev_ArticleBusiness, IAttachmentBusiness dev_attachmentBusiness)
        {
            _articleBusiness = dev_ArticleBusiness;
            _attachmentBusiness = dev_attachmentBusiness;
        }

        #region 视图功能

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Form(int id = 0)
        {
            ArticleDto theData = new ArticleDto();
            List<Attachment> alist = new List<Attachment>();

            if (id <= 0)
            {
                theData.Article = new Article();
                theData.Attachments = "";
            }
            else
            {
                theData.Article = _articleBusiness.GetEntity(id);
                if (theData.Article != null)
                {
                    alist = _attachmentBusiness.GetIQueryableList(m => m.TargetId == theData.Article.Id).CastToList<Attachment>();
                }


            }
            ViewBag.AttachmentList = alist;
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
            var dataList = _articleBusiness.GetDataList(condition, keyword, pagination);

            return Content(pagination.BuildTableResult_DataGrid(dataList).ToJson());
        }

        #endregion

        #region 提交数据

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="theData">保存的数据</param>
        public ActionResult SaveData(ArticleDto theData)
        {
            if (!string.IsNullOrEmpty(theData.Article.Cover))
            {
                theData.Article.Cover = theData.Article.Cover.Substring(theData.Article.Cover.IndexOf("/Upload"));
            }
            if (theData.Article.Id == 0)
            {
                theData.Article.CreateTime = DateTime.Now;
                _articleBusiness.Insert(theData.Article);
            }
            else
            {
                _articleBusiness.Update(theData.Article);
                _attachmentBusiness.Delete(m => m.TargetId == theData.Article.Id);

            }
            List<string> attachments = new List<string>();
            if (!string.IsNullOrEmpty(theData.Attachments))
            {
                attachments = theData.Attachments.Split(',').CastToList<string>();
            }
            List<Attachment> attachmentList = new List<Attachment>();
            attachments.ForEach(m =>
            {
                var data = m.Split('&');
                Attachment attach = new Attachment();
                attach.Directory = data[0];
                attach.SavePath = data[1].Substring(data[1].IndexOf("/Upload"));
                attach.Thumb = data[2].Substring(data[1].IndexOf("/Upload")); 
                attach.FileExt = Path.GetExtension(data[1]);
                attach.Name = data[3];
                attach.Type = 1;
                attach.TargetId = theData.Article.Id;
                attach.CreateTime = DateTime.Now;
                attach.Store = 0;
                attachmentList.Add(attach);
            });
            if (attachmentList.Count > 0)
            {
                _attachmentBusiness.BulkInsert(attachmentList);
            }

            return Success();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="theData">删除的数据</param>
        public ActionResult DeleteData(string ids)
        {
            _articleBusiness.Delete(ids.ToList<string>());

            return Success("删除成功！");
        }

        #endregion
    }
}