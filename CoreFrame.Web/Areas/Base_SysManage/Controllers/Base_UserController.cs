using CoreFrame.Business.Base_SysManage;
using CoreFrame.Entity.Base_SysManage;
using CoreFrame.Util;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CoreFrame.Web
{
    [Area("Base_SysManage")]
    public class Base_UserController : BaseMvcController
    {
        Base_UserBusiness _base_UserBusiness = new Base_UserBusiness();

        #region ��ͼ����

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Form(string id)
        {
            var theData = id.IsNullOrEmpty() ? new Base_User() : _base_UserBusiness.GetTheData(id);

            return View(theData);
        }

        public ActionResult ChangePwdForm()
        {
            return View();
        }

        public ActionResult PermissionForm(string userId)
        {
            ViewData["userId"] = userId;

            return View();
        }

        #endregion

        #region ��ȡ����

        /// <summary>
        /// ��ȡ�����б�
        /// </summary>
        /// <param name="condition">��ѯ����</param>
        /// <param name="keyword">�ؼ���</param>
        /// <returns></returns>
        public ActionResult GetDataList(string condition, string keyword, Pagination pagination)
        {
            var dataList = _base_UserBusiness.GetDataList(condition, keyword, pagination);

            return Content(pagination.BuildTableResult_DataGrid(dataList).ToJson());
        }

        #endregion

        #region �ύ����

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="theData">���������</param>
        public ActionResult SaveData(Base_User theData,string Pwd, string RoleIdList)
        {
            if (!Pwd.IsNullOrEmpty())
                theData.Password = Pwd.ToMD5String();
            var roleIdList = RoleIdList.ToList<string>();

            if (theData.Id.IsNullOrEmpty())
            {
                theData.Id = Guid.NewGuid().ToSequentialGuid();
                theData.UserId= Guid.NewGuid().ToSequentialGuid();

                _base_UserBusiness.AddData(theData);
            }
            else
            {
                _base_UserBusiness.UpdateData(theData);
            }

            _base_UserBusiness.SetUserRole(theData.UserId, roleIdList);

            return Success();
        }

        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="theData">ɾ��������</param>
        public ActionResult DeleteData(string ids)
        {
            _base_UserBusiness.DeleteData(ids.ToList<string>());

            return Success("ɾ���ɹ���");
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="oldPwd">������</param>
        /// <param name="newPwd">������</param>
        public ActionResult ChangePwd(string oldPwd, string newPwd)
        {
            var res = _base_UserBusiness.ChangePwd(oldPwd, newPwd);

            return Content(res.ToJson());
        }

        /// <summary>
        /// ����Ȩ��
        /// </summary>
        /// <param name="userId">�û�Id</param>
        /// <param name="permissions">Ȩ��</param>
        /// <returns></returns>
        public ActionResult SavePermission(string userId, string permissions)
        {
            PermissionManage.SetUserPermission(userId, permissions.ToList<string>());

            return Success();
        }

        #endregion
    }
}