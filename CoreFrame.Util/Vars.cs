using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace CoreFrame.Util
{
    /// <summary>
    /// 系统使用的一些常量
    /// </summary>
    public class Vars
    {
      

        /// <summary>
        /// ASP.NET_SessionId COOKIE键名
        /// </summary>
        public const string ASP_NET_SESSIONID_KEY = "51SessionId";
        /// <summary>
        /// The sessio n_ expire d_ seconds
        /// </summary>
        public const double SESSION_EXPIRED_SECONDS = 3600;

        /// <summary>
        /// 
        /// </summary>
        public const string SESSION_KEY_CURRENT_USER = "CURRENT_USER";
        /// <summary>
        /// 用户授权访问令牌COOKIE键值
        /// </summary>
        public const string COOKIEKEY_USER_ACCESS_TOKEN = "51USER_ACCESS_TOKEN";

        /// <summary>
        /// 登录用户COOKIE信息分隔符
        /// </summary>
        public const string SEPARATOR_LOGIN_USER_COOKIE = "|||";

       
      

       
        public static string COOKIE_DOMAIN
        {
            get
            {
 
                    return ConfigHelper.GetValue("COOKIE_DOMAIN");

            }
        }

        /// <summary>
        /// 主站
        /// </summary>
        public static string MAIN_SITE
        {
            get
            {
                    return ConfigHelper.GetValue("MAIN_SITE");
            }
        }
        /// <summary>
        /// m站
        /// </summary>
        public readonly static string MOBILE_SITE = ConfigHelper.GetValue("MOBILE_SITE");
        /// <summary>
        /// 后台站
        /// </summary>
        public readonly static string ADMIN_SITE = ConfigHelper.GetValue("ADMIN_SITE");
        /// <summary>
        /// API站点
        /// </summary>
        public readonly static string API_SITE = ConfigHelper.GetValue("API_SITE");
        /// <summary>
        /// 文件上传存储站
        /// </summary>
        public readonly static string FILESTORE_SITE = ConfigHelper.GetValue("FILESTORE_SITE");


        /// <summary>
        /// 默认的文件存储点
        /// </summary>
        public static readonly int DEFAULT_FILE_STORE = 0;
        /// <summary>
        /// 当前的文件存储点
        /// </summary>
        public static readonly int CURRENT_FILE_STORE = 0;

        /// <summary>
        /// 默认头像
        /// </summary>
        public static readonly string DEFAULT_AVATAR = "/Content/images/default.png";
        /// <summary>
        /// 默认头像(小)
        /// </summary>
        public static readonly string DEFAULT_AVATAR30 = "/Content/images/default_md.png";
        /// <summary>
        /// 默认头像(普通)
        /// </summary>
        public static readonly string DEFAULT_AVATAR100 = "/Content/images/default_lg.png";

        /// <summary>
        /// 用户默认密码
        /// </summary>
        public static readonly string DEFAULT_USER_PASSWORD = "bat1688";

        public static readonly string NOT_FOUND_UNIVERSITY_IMAGE = Vars.MAIN_SITE + "/Content/images/defaultUniversity.jpg";

    }
}
