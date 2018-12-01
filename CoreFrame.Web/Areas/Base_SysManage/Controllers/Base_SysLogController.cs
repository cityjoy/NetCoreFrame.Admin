using CoreFrame.Business.Base_SysManage;
using CoreFrame.Entity.Base_SysManage;
using CoreFrame.Util;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CoreFrame.Web
{
    [Area("Base_SysManage")]
    public class Base_SysLogController : BaseMvcController
    {
        Base_SysLogBusiness _base_SysLogBusiness = new Base_SysLogBusiness();

        #region ��ͼ����

        public ActionResult Index()
        {
            return View();
        }

        #endregion

        #region ��ȡ����

        /// <summary>
        /// ��ȡ��־�б�
        /// </summary>
        /// <param name="logContent">��־����</param>
        /// <param name="logType">��־����</param>
        /// <param name="opUserName">�������û���</param>
        /// <param name="startTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <param name="pagination">��ҳ����</param>
        /// <returns></returns>
        public ActionResult GetLogList(
            string logContent,
            string logType,
            string opUserName,
            DateTime? startTime,
            DateTime? endTime,
            Pagination pagination)
        {
            var dataList = _base_SysLogBusiness.GetLogList(logContent, logType, opUserName, startTime, endTime, pagination);

            return Content(pagination.BuildTableResult_DataGrid(dataList).ToJson());
        }

        public ActionResult GetLogTypeList()
        {
            List<object> logTypeList = new List<object>();
            Enum.GetNames(typeof(EnumType.LogType)).ForEach(aName =>
            {
                logTypeList.Add(new { Name = aName, Value = aName });
            });

            return Content(logTypeList.ToJson());
        }

        #endregion

        #region �ύ����

        #endregion
    }
}