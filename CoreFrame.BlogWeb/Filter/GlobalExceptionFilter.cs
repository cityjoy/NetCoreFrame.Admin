using CoreFrame.Business.Common;
using CoreFrame.Util;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CoreFrame.BlogWeb
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private ILog log = LogManager.GetLogger(Startup.Repository.Name, typeof(GlobalExceptionFilter));

        public void OnException(ExceptionContext context)
        {
            log.Error(context.Exception);

            var ex = context.Exception;
            SQLLogHelper.HandleException(ex);

            context.Result = new ContentResult
            {
                Content = new AjaxResult
                {
                    Success = false,
                    Msg = ex.Message
                }.ToJson()
            };
        }
    }
}
