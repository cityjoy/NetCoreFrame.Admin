using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreFrame.FileStore.Models
{
    public class UploadFileModel
    {
        public string Signature { get; set; }
        public string Action { get; set; }
        public string SaveRelativePath { get; set; }
        public string ThumbConfig { get; set; }
        public string CutImageConfig { get; set; }
        public string SourthFileName { get; set; }
        public string FileExt { get; set; }
        public string UrlEncode { get; set; }
        
    }
}
