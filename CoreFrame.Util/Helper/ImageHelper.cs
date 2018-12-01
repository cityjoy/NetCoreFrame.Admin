using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace CoreFrame.Util
{
    public sealed class ImageHelper
    {

        /// <summary>
        /// 是否Web图片文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool IsWebImage(string fileName)
        {
            string fileExtension = Path.GetExtension(fileName);
            if (!string.IsNullOrEmpty(fileExtension))
            {
                if (
                        string.Compare(fileExtension, ".jpg", true) == 0
                    ||
                        string.Compare(fileExtension, ".png", true) == 0
                    ||
                        string.Compare(fileExtension, ".gif", true) == 0
                    ||
                        string.Compare(fileExtension, ".bmp", true) == 0
                    ||
                        string.Compare(fileExtension, ".ico", true) == 0
                    ||
                        string.Compare(fileExtension, ".jpeg", true) == 0
                    )
                {
                    return true;
                }
            }
            return false;
        }


        #region 图片缩略图

        /// <summary>
        /// 生成指定图片的缩略图
        /// </summary>
        /// <param name="sourceImagePath">源图片文件完整路径</param>
        /// <param name="targetImagePath">保存缩略图的完整路径</param>
        /// <param name="targetMaxWidth">最大宽度</param>
        /// <param name="targetMaxHeight">最大高度</param>
        /// <returns></returns>
        public static bool BuildThumbnail(string sourceImagePath, string targetImagePath, int targetMaxWidth, int targetMaxHeight)
        {
            return BuildThumbnail(sourceImagePath, targetImagePath, ImageFormat.Jpeg, ref targetMaxWidth, ref targetMaxHeight, false);
        }
        /// <summary>
        /// 生成指定图片的缩略图
        /// </summary>
        /// <param name="sourceImagePath">源图片文件完整路径</param>
        /// <param name="targetImagePath">保存缩略图的完整路径</param>
        /// <param name="targetMaxWidth">最大宽度，引用值</param>
        /// <param name="targetMaxHeight">最大高度，引用值</param>
        /// <returns></returns>
        public static bool BuildThumbnail(string sourceImagePath, string targetImagePath, ref int targetMaxWidth, ref int targetMaxHeight)
        {
            return BuildThumbnail(sourceImagePath, targetImagePath, ImageFormat.Jpeg, ref targetMaxWidth, ref targetMaxHeight, false);
        }
        /// <summary>
        /// 生成指定图片的缩略图
        /// </summary>
        /// <param name="sourceImagePath">源图片文件完整路径</param>
        /// <param name="targetImagePath">保存缩略图的完整路径</param>
        /// <param name="targetMaxWidth">最大宽度</param>
        /// <param name="targetMaxHeight">最大高度</param>
        /// <param name="forceUseTargetSize">强制为指定的最大宽度与高度</param>
        /// <returns></returns>
        public static bool BuildThumbnail(string sourceImagePath, string targetImagePath, int targetMaxWidth, int targetMaxHeight, bool forceUseTargetSize)
        {
            return BuildThumbnail(sourceImagePath, targetImagePath, ImageFormat.Jpeg, ref targetMaxWidth, ref targetMaxHeight, forceUseTargetSize);
        }
        /// <summary>
        /// 生成指定图片的缩略图
        /// </summary>
        /// <param name="sourceImagePath">源图片文件完整路径</param>
        /// <param name="targetImagePath">保存缩略图的完整路径</param>
        /// <param name="imageFormat"></param>
        /// <param name="targetMaxWidth">最大宽度</param>
        /// <param name="targetMaxHeight">最大高度</param>
        /// <param name="forceUseTargetSize">强制为指定的最大宽度与高度</param>
        /// <returns></returns>
        public static bool BuildThumbnail(string sourceImagePath, string targetImagePath, ImageFormat imageFormat, int targetMaxWidth, int targetMaxHeight, bool forceUseTargetSize)
        {
            return BuildThumbnail(sourceImagePath, targetImagePath, imageFormat, ref targetMaxWidth, ref targetMaxHeight, forceUseTargetSize);
        }
        /// <summary>
        /// 生成指定图片的缩略图
        /// </summary>
        /// <param name="sourceImagePath">源图片文件完整路径</param>
        /// <param name="targetImagePath">保存缩略图的完整路径</param>
        /// <param name="imageFormat"></param>
        /// <param name="targetMaxWidth">最大宽度</param>
        /// <param name="targetMaxHeight">最大高度</param>
        /// <param name="forceUseTargetSize">强制为指定的最大宽度与高度</param>
        /// <returns></returns>
        public static bool BuildThumbnail(string sourceImagePath, string targetImagePath, ImageFormat imageFormat, ref int targetMaxWidth, ref int targetMaxHeight, bool forceUseTargetSize)
        {
            bool result = false;
            using (FileStream fs = new FileStream(sourceImagePath, FileMode.Open))
            {
                result = BuildThumbnail(fs, targetImagePath, imageFormat, ref targetMaxWidth, ref targetMaxHeight, forceUseTargetSize);
            }
            return result;
        }


        /// <summary>
        /// 生成指定图片的缩略图
        /// </summary>
        /// <param name="sourceImageStream">源图片文件流</param>
        /// <param name="targetImagePath">保存缩略图的完整路径</param>
        /// <param name="targetMaxWidth">最大宽度</param>
        /// <param name="targetMaxHeight">最大高度</param>
        /// <returns></returns>
        public static bool BuildThumbnail(Stream sourceImageStream, string targetImagePath, int targetMaxWidth, int targetMaxHeight)
        {
            return BuildThumbnail(sourceImageStream, targetImagePath, ImageFormat.Jpeg, ref targetMaxWidth, ref targetMaxHeight, false);
        }
        /// <summary>
        /// 生成指定图片的缩略图
        /// </summary>
        /// <param name="sourceImageStream">源图片文件流</param>
        /// <param name="targetImagePath">保存缩略图的完整路径</param>
        /// <param name="targetMaxWidth">最大宽度，引用值</param>
        /// <param name="targetMaxHeight">最大高度，引用值</param>
        /// <returns></returns>
        public static bool BuildThumbnail(Stream sourceImageStream, string targetImagePath, ref int targetMaxWidth, ref int targetMaxHeight)
        {
            return BuildThumbnail(sourceImageStream, targetImagePath, ImageFormat.Jpeg, ref targetMaxWidth, ref targetMaxHeight, false);
        }
        /// <summary>
        /// 生成指定图片的缩略图
        /// </summary>
        /// <param name="sourceImageStream">源图片文件流</param>
        /// <param name="targetImagePath">保存缩略图的完整路径</param>
        /// <param name="targetMaxWidth">最大宽度</param>
        /// <param name="targetMaxHeight">最大高度</param>
        /// <param name="forceUseTargetSize">强制为指定的最大宽度与高度</param>
        /// <returns></returns>
        public static bool BuildThumbnail(Stream sourceImageStream, string targetImagePath, int targetMaxWidth, int targetMaxHeight, bool forceUseTargetSize)
        {
            return BuildThumbnail(sourceImageStream, targetImagePath, ImageFormat.Jpeg, ref targetMaxWidth, ref targetMaxHeight, forceUseTargetSize);
        }
        /// <summary>
        /// 生成指定图片的缩略图
        /// </summary>
        /// <param name="sourceImageStream">源图片文件流</param>
        /// <param name="targetImagePath">保存缩略图的完整路径</param>
        /// <param name="imageFormat"></param>
        /// <param name="targetMaxWidth">最大宽度</param>
        /// <param name="targetMaxHeight">最大高度</param>
        /// <param name="forceUseTargetSize">强制为指定的最大宽度与高度</param>
        /// <returns></returns>
        public static bool BuildThumbnail(Stream sourceImageStream, string targetImagePath, ImageFormat imageFormat, int targetMaxWidth, int targetMaxHeight, bool forceUseTargetSize)
        {
            return BuildThumbnail(sourceImageStream, targetImagePath, imageFormat, ref targetMaxWidth, ref targetMaxHeight, forceUseTargetSize);
        }
        /// <summary>
        /// 生成指定图片的缩略图
        /// </summary>
        /// <param name="sourceImageStream">源图片文件流</param>
        /// <param name="targetImagePath">保存缩略图的完整路径</param>
        /// <param name="imageFormat">图像文件格式</param>
        /// <param name="targetMaxWidth">最大宽度</param>
        /// <param name="targetMaxHeight">最大高度</param>
        /// <param name="forceUseTargetSize">强制为指定的最大宽度与高度</param>
        /// <returns></returns>
        public static bool BuildThumbnail(Stream sourceImageStream, string targetImagePath, ImageFormat imageFormat, ref int targetMaxWidth, ref int targetMaxHeight, bool forceUseTargetSize)
        {
            int orgMaxWidth = targetMaxWidth;
            int orgMaxHeight = targetMaxHeight;

            #region 生成文件路径，并确保文件夹存在

            string targetImageDirectory = Path.GetDirectoryName(targetImagePath);

            if (Directory.Exists(targetImageDirectory) == false)
                Directory.CreateDirectory(targetImageDirectory);

            #endregion

            if (sourceImageStream != null)
            {
                try
                {
                    using (Image image = Image.FromStream(sourceImageStream))
                    {
                        #region 根据图片比例生成缩略图尺寸

                        //得到等比压缩的宽高
                        float w, h;
                        if (image.Width <= targetMaxWidth && image.Height <= targetMaxHeight)
                        {
                            w = image.Width;
                            h = image.Height;
                        }
                        else
                        {
                            w = targetMaxWidth;
                            h = targetMaxHeight;

                            if (image.Width / w > image.Height / h)
                            {
                                //w = targetMaxWidth;
                                h = w * ((float)image.Height / (float)image.Width);
                            }
                            else
                            {
                                //h = targetMaxHeight;
                                w = h * ((float)image.Width / (float)image.Height);
                            }
                        }

                        #endregion

                        #region 生成缩略图并保存

                        ImageCodecInfo s_ImageCodeInfo = null;

                        using (Bitmap thumb = new Bitmap(image, (int)w, (int)h))
                        {
                            using (EncoderParameters encodeParams = new EncoderParameters())
                            {
                                using (EncoderParameter encodeParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 80L))
                                {
                                    encodeParams.Param[0] = encodeParam;

                                    if (s_ImageCodeInfo == null)
                                    {
                                        ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

                                        foreach (ImageCodecInfo codec in codecs)
                                        {
                                            if (codec.FormatID == imageFormat.Guid)
                                            {
                                                s_ImageCodeInfo = codec;
                                            }
                                        }
                                    }

                                    thumb.Save(targetImagePath, s_ImageCodeInfo, encodeParams);
                                    targetMaxWidth = (int)w;
                                    targetMaxHeight = (int)h;
                                }
                            }
                        }

                        if (forceUseTargetSize)
                        {
                            using (Bitmap thumb = new Bitmap(orgMaxWidth, orgMaxHeight))
                            {
                                Graphics g = Graphics.FromImage(thumb);
                                g.Clear(Color.Transparent);
                                using (Image thumbImage = Image.FromFile(targetImagePath))
                                {
                                    int thumbWidth = thumbImage.Width;
                                    int thumbHeight = thumbImage.Height;

                                    Point point = new Point((orgMaxWidth - thumbWidth) / 2, (orgMaxHeight - thumbHeight) / 2);
                                    g.DrawImage(thumbImage, point);
                                }
                                g.Dispose();

                                File.Delete(targetImagePath);

                                thumb.Save(targetImagePath, imageFormat);
                                targetMaxWidth = orgMaxWidth;
                                targetMaxHeight = orgMaxHeight;
                            }
                        }

                        #endregion
                    }

                    return true;
                }
                catch(Exception ex)
                { }
            }

            return false;
        }

        #endregion

        #region 图片EXIF

        /// <summary>
        /// 获取EXIF指定项的值
        /// </summary>
        /// <param name="exifInfo">EXIF信息字符串</param>
        /// <param name="itemName">项名</param>
        /// <returns></returns>
        public static string ParseExifItemValue(string exifInfo, string itemName)
        {
            return ParseExifItemValue(exifInfo, itemName, "|||", "@@");
        }
        /// <summary>
        /// 获取EXIF指定项的值
        /// </summary>
        /// <param name="exifInfo">EXIF信息字符串</param>
        /// <param name="itemName">项名</param>
        /// <param name="itemSeperator">EXIF项之间的分隔符</param>
        /// <param name="nameSeperator">EXIF项名与项值之间的分隔符</param>
        /// <returns></returns>
        public static string ParseExifItemValue(string exifInfo, string itemName, string itemSeperator, string nameSeperator)
        {
            string[] exifInfos = exifInfo.Split(new string[] { itemSeperator }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string exifItemInfo in exifInfos)
            {
                string[] itemNameValuePair = exifItemInfo.Split(new string[] { nameSeperator }, StringSplitOptions.None);
                string theItemName = itemNameValuePair[0];
                if (string.Compare(theItemName, itemName, true) == 0)
                {
                    return itemNameValuePair[1];
                }
            }
            return string.Empty;
        }
        /// <summary>
        /// 获取EXIF指定项的值
        /// </summary>
        /// <param name="exifInfo">EXIF信息字符串</param>
        /// <returns></returns>
        public static Dictionary<string, string> ParseExifItemValue(string exifInfo)
        {
            return ParseExifItemValue(exifInfo, "|||", "@@");
        }
        /// <summary>
        /// 获取EXIF指定项的值
        /// </summary>
        /// <param name="exifInfo">EXIF信息字符串</param>
        /// <param name="itemSeperator">EXIF项之间的分隔符</param>
        /// <param name="nameSeperator">EXIF项名与项值之间的分隔符</param>
        /// <returns></returns>
        public static Dictionary<string, string> ParseExifItemValue(string exifInfo, string itemSeperator, string nameSeperator)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string[] exifInfos = exifInfo.Split(new string[] { itemSeperator }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string exifItemInfo in exifInfos)
            {
                string[] itemNameValuePair = exifItemInfo.Split(new string[] { nameSeperator }, StringSplitOptions.None);
                string theItemName = itemNameValuePair[0];
                string theItemValue = itemNameValuePair[1];

                dic.Add(theItemName, theItemValue);
            }
            return dic;
        }

        /// <summary>
        /// 获取EXIF信息，默认分隔符使用"|||"与"@@"
        /// </summary>
        /// <param name="filePath">图片文件完整路径</param>
        /// <returns></returns>
        public static string GetExifInfo(string filePath)
        {
            return GetExifInfo(filePath, "|||", "@@");
        }
        /// <summary>
        /// 获取EXIF信息
        /// </summary>
        /// <param name="filePath">图片文件完整路径</param>
        /// <param name="itemSeperator">EXIF项之间的分隔符</param>
        /// <param name="nameSeperator">EXIF项名与项值之间的分隔符</param>
        /// <returns></returns>
        public static string GetExifInfo(string filePath, string itemSeperator, string nameSeperator)
        {
            try
            {
                Image img = Image.FromFile(filePath);
                return GetExifInfo(img, itemSeperator, nameSeperator);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取EXIF信息，默认分隔符使用"|||"与"@@"
        /// </summary>
        /// <param name="stream">图片文件流</param>
        /// <returns></returns>
        public static string GetExifInfo(Stream stream)
        {
            return GetExifInfo(stream, "|||", "@@");
        }
        /// <summary>
        /// 获取EXIF信息
        /// </summary>
        /// <param name="stream">图片文件流</param>
        /// <param name="itemSeperator">EXIF项之间的分隔符</param>
        /// <param name="nameSeperator">EXIF项名与项值之间的分隔符</param>
        /// <returns></returns>
        public static string GetExifInfo(Stream stream, string itemSeperator, string nameSeperator)
        {
            try
            {
                Image img = Image.FromStream(stream);
                return GetExifInfo(img, itemSeperator, nameSeperator);
            }
            catch
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// 获取EXIF信息
        /// </summary>
        /// <param name="img">图片</param>
        /// <param name="itemSeperator">EXIF项之间的分隔符</param>
        /// <param name="nameSeperator">EXIF项名与项值之间的分隔符</param>
        /// <returns></returns>
        private static string GetExifInfo(Image img, string itemSeperator, string nameSeperator)
        {
            PropertyItem[] pt = img.PropertyItems;

            StringBuilder sb = new StringBuilder("");
            for (int i = 0; i < pt.Length; i++)
            {
                PropertyItem p = pt[i];

                switch (pt[i].Id)
                {
                    case 0x010F: // 设备制造商 20. 
                        string camera = System.Text.ASCIIEncoding.ASCII.GetString(pt[i].Value);
                        sb.Append("Camera");
                        sb.Append(nameSeperator);
                        sb.Append(camera);
                        sb.Append(itemSeperator);

                        break;

                    case 0x0110: // 设备型号 25. 
                        string model = GetValueOfType2(p.Value);
                        sb.Append("Model");
                        sb.Append(nameSeperator);
                        sb.Append(model);
                        sb.Append(itemSeperator);

                        break;

                    case 0x0132: // 拍照时间 30.
                        string createTime = GetValueOfType2(p.Value);
                        sb.Append("Time");
                        sb.Append(nameSeperator);
                        sb.Append(createTime);
                        sb.Append(itemSeperator);

                        break;

                    case 0x829A: // .曝光时间 second
                        string exposal = GetValueOfType5(p.Value);
                        sb.Append("Exposal");
                        sb.Append(nameSeperator);
                        sb.Append(exposal);
                        sb.Append(itemSeperator);

                        break;

                    case 0x8827: // ISO 40.  
                        string iso = GetValueOfType3(p.Value);
                        sb.Append("ISO");
                        sb.Append(nameSeperator);
                        sb.Append(iso);
                        sb.Append(itemSeperator);

                        break;

                    case 0x010E: // 图像说明info.description
                        string desc = GetValueOfType2(p.Value);
                        sb.Append("Description");
                        sb.Append(nameSeperator);
                        sb.Append(desc);
                        sb.Append(itemSeperator);

                        break;

                    case 0x920a: //相片的焦距 mm
                        string focus = GetValueOfType5A(p.Value);
                        sb.Append("Focus");
                        sb.Append(nameSeperator);
                        sb.Append(focus);
                        sb.Append(itemSeperator);

                        break;

                    case 0x829D: //相片的光圈值
                        string aperture = GetValueOfType5A(p.Value);
                        sb.Append("Aperture");
                        sb.Append(nameSeperator);
                        sb.Append(aperture);
                        sb.Append(itemSeperator);

                        break;

                    default:

                        break;

                }

            }

            img.Dispose();
            string exif = sb.ToString();
            exif = exif.Replace("\0", string.Empty);
            return exif;
        }

        /// <summary>
        /// 对type=2 的value值进行读取
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        private static string GetValueOfType2(byte[] b)
        {
            return System.Text.Encoding.ASCII.GetString(b);
        }
        /// <summary>
        /// 对type=3 的value值进行读取
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        private static string GetValueOfType3(byte[] b)
        {
            if (b.Length != 2) return "unknow";
            return Convert.ToUInt16(b[1] << 8 | b[0]).ToString();
        }
        /// <summary>
        /// 对type=5 的value值进行读取
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        private static string GetValueOfType5(byte[] b)
        {
            if (b.Length != 8) return "unknow";
            UInt32 fm, fz;
            fm = 0;
            fz = 0;
            fz = Convert.ToUInt32(b[7] << 24 | b[6] << 16 | b[5] << 8 | b[4]);
            fm = Convert.ToUInt32(b[3] << 24 | b[2] << 16 | b[1] << 8 | b[0]);
            return fm.ToString() + "/" + fz.ToString() + " sec";
        }
        /// <summary>
        /// 获取光圈的值
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        private static string GetValueOfType5A(byte[] b)
        {
            if (b.Length != 8) return "unknow";
            UInt32 fm, fz;
            fm = 0;
            fz = 0;
            fz = Convert.ToUInt32(b[7] << 24 | b[6] << 16 | b[5] << 8 | b[4]);
            fm = Convert.ToUInt32(b[3] << 24 | b[2] << 16 | b[1] << 8 | b[0]);
            double temp = (double)fm / fz;
            return (temp).ToString();
        }

        #endregion

        /// <summary>
        /// 按百分比裁切图片
        /// </summary>
        /// <param name="sourceImagePath">原始图片地址</param>
        /// <param name="newImagePath">新图片地址</param>
        /// <param name="percentage">百分比</param>
        /// <param name="imageFormat"></param>
        /// <param name="position">裁切开始位置</param>
        /// <returns></returns>
        public static bool CutImage(string sourceImagePath, string newImagePath, double percentage, ImageFormat imageFormat, CutImagePosition position)
        {
            int orgWidth = 0, orgHeight = 0;
            int newWidth = 0, newHeight = 0;

            using (Image img = Image.FromFile(sourceImagePath))
            {
                orgWidth = img.Width;
                orgHeight = img.Height;

                switch (position)
                {
                    case CutImagePosition.Top:
                    case CutImagePosition.Bottom:
                        newWidth = orgWidth;
                        newHeight = Convert.ToInt32(orgHeight * percentage);
                        break;
                    case CutImagePosition.Left:
                    case CutImagePosition.Right:
                        newWidth = Convert.ToInt32(orgWidth * percentage);
                        newHeight = orgHeight;
                        break;
                    default:
                        newWidth = Convert.ToInt32(orgWidth * percentage);
                        newHeight = Convert.ToInt32(orgHeight * percentage);
                        break;
                }
            }

            return CutImage(sourceImagePath, newImagePath, newWidth, newHeight, imageFormat, position);
        }
        /// <summary>
        /// 按百分比裁切图片
        /// </summary>
        /// <param name="sourceStream">原始图片流</param>
        /// <param name="newImagePath">新图片地址</param>
        /// <param name="percentage">百分比</param>
        /// <param name="imageFormat"></param>
        /// <param name="position">裁切开始位置</param>
        /// <returns></returns>
        public static bool CutImage(Stream sourceStream, string newImagePath, double percentage, ImageFormat imageFormat, CutImagePosition position)
        {
            int orgWidth = 0, orgHeight = 0;
            int newWidth = 0, newHeight = 0;

            using (Image img = Image.FromStream(sourceStream))
            {
                orgWidth = img.Width;
                orgHeight = img.Height;

                switch (position)
                {
                    case CutImagePosition.Top:
                    case CutImagePosition.Bottom:
                        newWidth = orgWidth;
                        newHeight = Convert.ToInt32(orgHeight * percentage);
                        break;
                    case CutImagePosition.Left:
                    case CutImagePosition.Right:
                        newWidth = Convert.ToInt32(orgWidth * percentage);
                        newHeight = orgHeight;
                        break;
                    default:
                        newWidth = Convert.ToInt32(orgWidth * percentage);
                        newHeight = Convert.ToInt32(orgHeight * percentage);
                        break;
                }
            }

            return CutImage(sourceStream, newImagePath, newWidth, newHeight, imageFormat, position);
        }

        /// <summary>
        /// 按指定大小裁切图片，如果指定大小超过原始图片，将按原始图片尺寸输出
        /// </summary>
        /// <param name="sourceImagePath">原始图片地址</param>
        /// <param name="newImagePath">新图片地址</param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="imageFormat"></param>
        /// <param name="position">裁切开始位置</param>
        /// <returns></returns>
        public static bool CutImage(string sourceImagePath, string newImagePath, int width, int height, ImageFormat imageFormat, CutImagePosition position)
        {
            using (FileStream fs = new FileStream(sourceImagePath, FileMode.Open))
            {
                return CutImage(fs, newImagePath, width, height, imageFormat, position);
            }
        }
        /// <summary>
        /// 按指定大小裁切图片，如果指定大小超过原始图片，将按原始图片尺寸输出
        /// </summary>
        /// <param name="sourceStream">原始图片流</param>
        /// <param name="newImagePath">新图片地址</param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="imageFormat"></param>
        /// <param name="position">裁切开始位置</param>
        /// <returns></returns>
        public static bool CutImage(Stream sourceStream, string newImagePath, int width, int height, ImageFormat imageFormat, CutImagePosition position)
        {
            bool result = false;

            try
            {
                string newImageDirPath = Path.GetDirectoryName(newImagePath);
                if (!Directory.Exists(newImageDirPath))
                {
                    Directory.CreateDirectory(newImageDirPath);
                }
                string newImageTempPath = Path.Combine(newImageDirPath, Path.GetFileNameWithoutExtension(newImagePath) + "_temp_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + Path.GetExtension(newImagePath));

                int orgWidth = 0, orgHeight = 0;
                using (Image img = Image.FromStream(sourceStream))
                {
                    orgWidth = img.Width;
                    orgHeight = img.Height;

                    if (width >= orgWidth && height >= orgHeight)
                    {
                        using (FileStream fs = new FileStream(newImagePath, FileMode.OpenOrCreate))
                        {
                            byte[] sourceBuffer = new byte[sourceStream.Length];
                            fs.Write(sourceBuffer, 0, sourceBuffer.Length);
                        }
                        return true;
                    }
                    else
                    {
                        if (width > orgWidth)
                        {
                            width = orgWidth;
                        }
                        if (height > orgHeight)
                        {
                            height = orgHeight;
                        }
                    }
                }

                //获取系统编码类型数组,包含了jpeg,bmp,png,gif,tiff
                ImageCodecInfo[] icis = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo ici = null;
                foreach (ImageCodecInfo i in icis)
                {
                    if (i.FormatID == imageFormat.Guid)
                    {
                        ici = i;
                        break;
                    }
                }
                EncoderParameters ep = new EncoderParameters(1);
                ep.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100);

                using (Bitmap bitmap = new Bitmap(width, height))
                {
                    bitmap.Save(newImageTempPath, ici, ep);
                }

                int x, y;
                switch (position)
                {
                    case CutImagePosition.TopLeft:
                    default:
                        x = 0;
                        y = 0;
                        break;
                    case CutImagePosition.TopCenter:
                        x = Convert.ToInt32((orgWidth - width) / 2);
                        y = 0;
                        break;
                    case CutImagePosition.TopRight:
                        x = (orgWidth - width);
                        y = 0;
                        break;
                    case CutImagePosition.MiddleRight:
                        x = (orgWidth - width);
                        y = Convert.ToInt32((orgHeight - height) / 2);
                        break;
                    case CutImagePosition.BottomRight:
                        x = (orgWidth - width);
                        y = (orgHeight - height);
                        break;
                    case CutImagePosition.BottomCenter:
                        x = Convert.ToInt32((orgWidth - width) / 2);
                        y = (orgHeight - height);
                        break;
                    case CutImagePosition.BottomLeft:
                        x = 0;
                        y = (orgHeight - height);
                        break;
                    case CutImagePosition.MiddleLeft:
                        x = 0;
                        y = Convert.ToInt32((orgHeight - height) / 2);
                        break;
                    case CutImagePosition.Top:
                        x = 0;
                        y = 0;
                        break;
                    case CutImagePosition.Left:
                        x = 0;
                        y = 0;
                        break;
                    case CutImagePosition.Right:
                        x = (orgWidth - width);
                        y = 0;
                        break;
                    case CutImagePosition.Bottom:
                        x = 0;
                        y = (orgHeight - height);
                        break;
                }

                Graphics g = null;
                using (Image tempImg = Image.FromFile(newImageTempPath))
                {
                    using (Image img = Image.FromStream(sourceStream))
                    {
                        g = Graphics.FromImage(tempImg);
                        g.DrawImage(img, new Rectangle(0, 0, width, height), new Rectangle(x, y, width, height), GraphicsUnit.Pixel);
                        g.Save();
                    }

                    tempImg.Save(newImagePath);
                }
                g.Dispose();

                File.Delete(newImageTempPath);

                result = true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog_LocalTxt(ex.ToString());
                result = false;
            }

            return result;
        }
        /// <summary>
        /// 裁切图片位置
        /// </summary>
        public enum CutImagePosition : byte
        {
            /// <summary>
            /// 从左上角开始
            /// </summary>
            TopLeft = 0,
            /// <summary>
            /// 从顶部中间开始
            /// </summary>
            TopCenter = 1,
            /// <summary>
            /// 从右上角开始
            /// </summary>
            TopRight = 2,
            /// <summary>
            /// 从右边中间开始
            /// </summary>
            MiddleRight = 3,
            /// <summary>
            /// 从右下角开始
            /// </summary>
            BottomRight = 4,
            /// <summary>
            /// 从底部中间开始
            /// </summary>
            BottomCenter = 5,
            /// <summary>
            /// 从左下角开始
            /// </summary>
            BottomLeft = 6,
            /// <summary>
            /// 从左边中间开始
            /// </summary>
            MiddleLeft = 7,
            /// <summary>
            /// 
            /// </summary>
            Top = 10,
            /// <summary>
            /// 
            /// </summary>
            Right = 11,
            /// <summary>
            /// 
            /// </summary>
            Bottom = 12,
            /// <summary>
            /// 
            /// </summary>
            Left = 13
        }


    }
}
