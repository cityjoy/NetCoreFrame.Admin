using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreFrame.Util;
using System.IO;
using System.Drawing.Imaging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace CoreFrame.IdentityServer.Controllers
{
    [Authorize]
    public class ValueController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public ValueController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public ActionResult Index()
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            string contentRootPath = _hostingEnvironment.ContentRootPath;

            return Content(webRootPath + "\n" + contentRootPath);
        }
 

    }
}
