using CoreFrame.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreFrame.FileStore.Filter
{
    public class AuthenticationFilterAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {

            string signature = context.HttpContext.Request.Headers["signature"];
            string timestamp = context.HttpContext.Request.Headers["timestamp"];
            string nonce = context.HttpContext.Request.Headers["nonce"];
            if (string.IsNullOrEmpty(signature) || string.IsNullOrEmpty(timestamp) || string.IsNullOrEmpty(nonce))
            {
                context.Result = new ContentResult()
                {
                    Content = "Resource unavailable - header should not be set"
                };
            }
            else
            {
                bool check = SignatureUtil.CheckSignature(signature, timestamp,nonce);
                if (!check)
                {

                    context.Result = new ContentResult()
                    {
                        Content = "Resource unavailable - invalidated signature"
                    };
                }
            }


        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
