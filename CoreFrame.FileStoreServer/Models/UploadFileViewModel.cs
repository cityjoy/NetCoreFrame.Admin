using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreFrame.FileStoreServer.Models
{
    public class UploadFileViewModel
    {
        public int ArticleId { get; set; }
        public string FileName { get; set; }
        public string FileExt { get; set; }
        public string Path { get; set; }
        public string Thumb { get; set; }
        public string Directory { get; set; }
    }
    public class UploadFileDto
    {
        public int ArticleId { get; set; }
        public IList<IFormFile> Files { get; set; }
    }
}
