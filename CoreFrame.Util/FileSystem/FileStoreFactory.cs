using System;
using System.Collections.Generic;
using System.Text;
using CoreFrame.Util;

namespace CoreFrame.Util
{
    /// <summary>
    /// 文件存储工厂
    /// </summary>

    public class FileStoreFactory
    {

        /// <summary>
        /// 创建文件存储处理器
        /// </summary>
        /// <returns></returns>
        public static IFileStoreHandler CreateFileStoreHandler()
        {
            int store = Vars.CURRENT_FILE_STORE;
            switch (store)
            {
                case 0:
                default:
                    return new DefaultFileStoreHandler();
            }
        }


    }
}
