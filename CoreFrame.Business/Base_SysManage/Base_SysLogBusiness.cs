using CoreFrame.Entity.Base_SysManage;
using CoreFrame.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreFrame.Business.Base_SysManage
{
    public class Base_SysLogBusiness : BaseBusiness<Base_SysLog>
    {
        #region �ⲿ�ӿ�

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
        public List<Base_SysLog> GetLogList(
            string logContent,
            string logType,
            string opUserName,
            DateTime? startTime,
            DateTime? endTime,
            Pagination pagination)
        {
            var whereExp = LinqHelper.True<Base_SysLog>();
            if (!logContent.IsNullOrEmpty())
                whereExp = whereExp.And(x => x.LogContent.Contains(logContent));
            if (!logType.IsNullOrEmpty())
                whereExp = whereExp.And(x => x.LogType == logType);
            if (!opUserName.IsNullOrEmpty())
                whereExp = whereExp.And(x => x.OpUserName.Contains(opUserName));
            if (!startTime.IsNullOrEmpty())
                whereExp = whereExp.And(x => x.OpTime >= startTime);
            if (!endTime.IsNullOrEmpty())
                whereExp = whereExp.And(x => x.OpTime <= endTime);

            return GetIQueryable().Where(whereExp).GetPagination(pagination).ToList();
        }

        #endregion

        #region ˽�г�Ա

        #endregion

        #region ����ģ��

        #endregion
    }
}