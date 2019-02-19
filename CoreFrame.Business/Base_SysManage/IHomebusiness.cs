using CoreFrame.DataRepository;
using CoreFrame.Entity.Base_SysManage;
using CoreFrame.Util;

namespace CoreFrame.Business.Base_SysManage
{
    public interface IHomebusiness: IBusiness<Base_User>
    {
        AjaxResult SubmitLogin(string userName, string password);
    }
}