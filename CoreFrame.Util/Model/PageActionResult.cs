using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFrame.Util
{
    [Serializable]
    public class PageActionResult
    {
        /// <summary>
        /// 默认实例化
        /// </summary>
        public PageActionResult()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resultType"></param>
        public PageActionResult(PageActionResultType resultType)
        {
            this.Result = resultType;
            this.ErrorCode = 0;
            this.Message = string.Empty;
            this.Keywords = string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resultType"></param>
        /// <param name="data"></param>
        public PageActionResult(PageActionResultType resultType, object data)
        {
            this.Result = resultType;
            this.ErrorCode = 0;
            this.Message = string.Empty;
            this.Keywords = string.Empty;
            this.Data = data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resultType"></param>
        /// <param name="message"></param>
        /// <param name="errorCode"></param>
        public PageActionResult(PageActionResultType resultType, string message, int errorCode)
        {
            this.Result = resultType;
            this.ErrorCode = errorCode;
            this.Message = message;
            this.Keywords = string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resultType"></param>
        /// <param name="message"></param>
        /// <param name="errorCode"></param>
        /// <param name="keywords"></param>
        public PageActionResult(PageActionResultType resultType, string message, int errorCode, string keywords)
        {
            this.Result = resultType;
            this.ErrorCode = errorCode;
            this.Message = message;
            this.Keywords = keywords;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resultType"></param>
        /// <param name="message"></param>
        /// <param name="errorCode"></param>
        /// <param name="keywords"></param>
        /// <param name="data"></param>
        public PageActionResult(PageActionResultType resultType, string message, int errorCode, string keywords, object data)
        {
            this.Result = resultType;
            this.ErrorCode = errorCode;
            this.Message = message;
            this.Keywords = keywords;
            this.Data = data;
        }

        /// <summary>
        /// 
        /// </summary>

        public virtual PageActionResultType Result
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>

        public virtual string Message
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>

        public virtual int ErrorCode
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>

        public virtual string Keywords
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>

        public virtual object Data
        {
            get;
            set;
        }

    }
    /// <summary>
    /// 页面操作结果
    /// </summary>
    public enum PageActionResultType : int
    {
        /// <summary>
        /// 无操作
        /// </summary>
        Default = 0,
        /// <summary>
        /// 操作成功
        /// </summary>
        Success = 1,
        /// <summary>
        /// 操作失败
        /// </summary>
        Failed = -1
    }
}
