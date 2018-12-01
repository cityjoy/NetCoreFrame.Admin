namespace CoreFrame.Util
{
    /// <summary>
    /// 文件信息
    /// </summary>
    public struct FileEntry
    {

        /// <summary>
        /// 所属文章id
        /// </summary>
        public string ArticleId { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件字节
        /// </summary>
        public byte[] FileBytes { get; set; }
    }
}
