﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace CoreFrame.Util
{
    /// <summary>
    /// 身份验证签名串助手
    /// </summary>
    public class SignatureUtil
    {
    

        /// <summary>
        /// 密钥
        /// </summary>
        public static string SIGNATURE_SAFE_KEY = "!cybd*2017$";

        /// <summary>
        /// 检查签名串
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <returns></returns>
        public static bool CheckSignature(string signature,string timestamp,string nonce)
        {
            if (string.IsNullOrEmpty(signature))
            {
                return false;
            }
            long iTimestamp = XConvert.ToInt64(timestamp, 0);

            string userSignature = CreateSignature(SIGNATURE_SAFE_KEY, iTimestamp, nonce);

            if (userSignature == signature)
            {
                long nowTimestamp = DateTimeHelper.ConvertToUnixTimestamp(DateTime.Now);

                if (nowTimestamp - iTimestamp > 600)
                {
                    return false;
                }
                else if (iTimestamp > nowTimestamp)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
 
        /// <summary>
        /// 构建签名串
        /// </summary>
        /// <returns></returns>
        public static string BuildSignature()
        {
            return BuildSignature(true);
        }
        /// <summary>
        /// 构建签名串
        /// </summary>
        /// <param name="urlEncode"></param>
        /// <returns></returns>
        public static string BuildSignature(bool urlEncode)
        {
            string key = SIGNATURE_SAFE_KEY;
            string nonce = Guid.NewGuid().ToString().Replace("-", string.Empty);

            long timestamp = DateTimeHelper.ConvertToUnixTimestamp(DateTime.Now);

            string signature = CreateSignature(key, timestamp, nonce);

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("signature", signature);
            dic.Add("timestamp", timestamp.ToString());
            dic.Add("nonce", nonce);

            string token = SecurityHelper.DESCryptoEncode(JsonConvert.SerializeObject(dic));

            if (urlEncode)
            {
                token =HttpUtility.UrlEncode(token);
            }

            return token;
        }

        /// <summary>
        /// 构建签名串
        /// </summary>
        /// <param name="key">用户密钥</param>
        /// <param name="timestamp">当前时间戳</param>
        /// <param name="nonce">随机字符串</param>
        /// <returns>System.String.</returns>
        private static string CreateSignature(string key, long timestamp, string nonce)
        {
            string[] tmpArr = new string[] { key, timestamp.ToString(), nonce };
            Array.Sort(tmpArr);
            string tmpStr = StringHelper.Join<string>(tmpArr, "");
            tmpStr = SecurityHelper.SHA1(tmpStr).ToLower();

            return tmpStr;
        }


    }
}
