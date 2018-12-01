using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreFrame.Util.FileSystem
{
    /// <summary>
    /// 
    /// </summary>
    /// /// <remarks>
    ///  Author:         
    ///  Description:    office文件转换PDF接口
    ///                  
    ///  History:         2016/11/17 15:45:09 创建
    ///                  
    /// </remarks>
    public interface IFileConversion
    {
        void WordToPdf(string sourceFilePath, string destFilePath);
        void ExcelToPdf(string sourceFilePath, string destFilePath);
        void PptToPdf(string sourceFilePath, string destFilePath);
        void PdfToSwf(string pdfFilePath, string swfFilePath);
    }
}
