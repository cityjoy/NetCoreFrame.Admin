using CoreFrame.Entity.MenuManage;
using CoreFrame.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace CoreFrame.Business.Base_SysManage
{
    public class Base_MenuBusiness : BaseBusiness<Menu>,IBase_MenuBusiness
    {
        #region 外部接口
        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体对象</param>
        public override void Update(Menu entity)
        {
            base.Update(entity);
        }
        #endregion

        #region 私有成员

        #endregion

        #region 数据模型

        #endregion
    }
}