using CoreFrame.Business.Base_SysManage;
using CoreFrame.Entity.Base_SysManage;
using CoreFrame.Util;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CoreFrame.Web
{
    [Area("Base_SysManage")]
    public class Base_SysRoleController : BaseMvcController
    {
        Base_SysRoleBusiness _base_SysRoleBusiness = new Base_SysRoleBusiness();

        #region ��ͼ����

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Form(string id)
        {
            var theData = id.IsNullOrEmpty() ? new Base_SysRole() : _base_SysRoleBusiness.GetTheData(id);

            return View(theData);
        }

        public ActionResult PermissionForm(string roleId)
        {
            ViewData["roleId"] = roleId;

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
            var dataList = _base_SysRoleBusiness.GetDataList(condition, keyword, pagination);

            return Content(pagination.BuildTableResult_DataGrid(dataList).ToJson());
        }

        /// <summary>
        /// ��ȡ��ɫ�б�
        /// ע���޷�ҳ
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDataList_NoPagin()
        {
            Pagination pagination = new Pagination
            {
                PageIndex = 1,
                PageRows = int.MaxValue
            };
            var dataList = _base_SysRoleBusiness.GetDataList(null, null, pagination);

            return Content(dataList.ToJson());
        }

        #endregion

        #region �ύ����

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="theData">���������</param>
        public ActionResult SaveData(Base_SysRole theData)
        {
            if (theData.Id.IsNullOrEmpty())
            {
                theData.Id = Guid.NewGuid().ToSequentialGuid();
                theData.RoleId = Guid.NewGuid().ToSequentialGuid();

                _base_SysRoleBusiness.AddData(theData);
            }
            else
            {
                _base_SysRoleBusiness.UpdateData(theData);
            }

            return Success();
        }

        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="theData">ɾ��������</param>
        public ActionResult DeleteData(string ids)
        {
            _base_SysRoleBusiness.DeleteData(ids.ToList<string>());

            PermissionManage.ClearUserPermissionCache();

            return Success("ɾ���ɹ���");
        }

        /// <summary>
        /// ���ý�ɫȨ��
        /// </summary>
        /// <param name="roleId">��ɫId</param>
        /// <param name="permissions">Ȩ��ֵ</param>
        /// <returns></returns>
        public ActionResult SavePermission(string roleId,string permissions)
        {
            _base_SysRoleBusiness.SavePermission(roleId, permissions.ToList<string>());

            PermissionManage.ClearUserPermissionCache();

            return Success();
        }

        #endregion
    }
}