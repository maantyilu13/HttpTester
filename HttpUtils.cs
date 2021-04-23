/*
 * Created by SharpDevelop.
 * User: Kevin
 * Date: 2017/5/13
 * Time: 16:31
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Forms;

namespace RestTest
{
	/// <summary>
	/// Description of HttpUtils.
	/// </summary>
	public class HttpUtils
	{ 
        public static readonly string urlRegex = "^((ht|f)tps?):\\/\\/[\\w\\-]+(\\.[\\w\\-]+)+([\\w\\-\\.,@?^=%&:\\/~\\+#]*[\\w\\-\\@?^=%&\\/~\\+#])?$";
        private static readonly string DefaultAccept = "*/*;";
        //仿google chrome，提高兼容性,反防抓取
        private static readonly string DefaultUserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.101 Safari/537.36";
        public HttpUtils()
		{
		}
        public static bool regexMatch(String str,String pattern) {
            if (str != null) {
                return Regex.IsMatch(str, pattern); 
            }
            return false;
        }
        public static String regexFindFirst(String str,String pattern) {
            String result = "";
            if (str != null) {
                Regex r = new Regex(pattern);
                Match m = r.Match(str);
                if (m.Success) {
                    result = m.Value;
                }
            } 
            return result;
        }
        public static string[] regexSplit(String str, String pattern)
        {
            string[] result = new string[] { };
            if (str != null)
            { 
                result = Regex.Split(str,pattern);
            }
            return result;
        }
        public static String replace(String str,String pattern,String newStr) {
            if (str != null) {
                return Regex.Replace(str, pattern, newStr);
            }
            return null;
        }
        public static bool isNotEmpty(String str) {
            return str != null && str.Length > 0;
        }
        public static bool isNotBlank(String str) {
            return str != null && str.Trim().Length > 0;
        }
         
        public static HttpWebRequest buildHeader(String path, String method, String contentType,String bodyData,Encoding encoding, String accept, bool allowAutoRedirect)
        {
            if (isNotBlank(path))
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(path);
                request.Method = method;
                if (isNotBlank(accept))
                {
                    request.Accept = accept;
                }
                else {
                    request.Accept = DefaultAccept;
                }
                request.UserAgent = DefaultUserAgent;
                request.AllowAutoRedirect = allowAutoRedirect;
                request.KeepAlive = true;
                if (isNotBlank(contentType))
                { 
                    
                    if (regexMatch(request.ContentType, "(?i);charset=[\\da-z]+;?\\s*$"))
                    {
                        request.ContentType = contentType;
                    }
                    else {
                        request.ContentType = contentType + ";charset="+ encoding.HeaderName.ToUpper();
                    }
                }
                else {
                    request.ContentType = "application/x-www-form-urlencoded;charset="+ encoding.HeaderName.ToUpper();
                }

                //添加请求体数据
                if (!isNotBlank(bodyData)) return request;
                byte[] data = MainForm.DefaultEncoding.GetBytes(bodyData);
                try
                {
                    using (Stream stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                }
                catch (Exception err)
                {
                    Console.WriteLine(err);
                }
                return request; 
            }
            else
            {
                MessageBox.Show("请求地址不能为空！");
            }
            return null;
        }
        public static List<Cookie> GetAllCookies(CookieContainer cc)
        {
            List<Cookie> lstCookies = new List<Cookie>();
            Hashtable table = (Hashtable)cc.GetType().InvokeMember("m_domainTable",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField |
                System.Reflection.BindingFlags.Instance, null, cc, new object[] { });
            foreach (object pathList in table.Values)
            {
                SortedList lstCookieCol = (SortedList)pathList.GetType().InvokeMember("m_list",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField
                    | System.Reflection.BindingFlags.Instance, null, pathList, new object[] { });
                foreach (CookieCollection colCookies in lstCookieCol.Values)
                    foreach (Cookie c in colCookies) lstCookies.Add(c);
            }
            return lstCookies;
        }

        public static void readCookieFromFile(string cookieFilePath, CookieContainer ckContainer){
            //从文件中读取cookie
            string[] cookies = File.ReadAllText(cookieFilePath, Encoding.Default).Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (string c in cookies)
            {
                string[] cc = c.Split(";".ToCharArray());
                Cookie ck = new Cookie(); ;
                ck.Discard = false;
                ck.Domain = cc[0];
                ck.Expired = true;
                ck.HttpOnly = true;
                ck.Name = cc[1];
                ck.Path = cc[2];
                ck.Port = cc[3];
                ck.Secure = bool.Parse(cc[4]);
                ck.Value = cc[5];
                ckContainer.Add(ck);
            }
        }

        public static void writeCookieToFile(string cookieFilePath, CookieContainer ckContainer) {
            //将cookie写入到文件
            StringBuilder sbc = new StringBuilder();
            List<Cookie> cooklist = GetAllCookies(ckContainer);
            foreach (Cookie cookie in cooklist)
            {
                sbc.AppendFormat("{0};{1};{2};{3};{4};{5}\r\n",
                    cookie.Domain, cookie.Name, cookie.Path, cookie.Port,
                    cookie.Secure.ToString(), cookie.Value);
            } 
            File.WriteAllText(cookieFilePath, sbc.ToString(),Encoding.Default);
        }

        public static void writeUrlToFile(string cookieFilePath, string method, string url,string port,string baseUrl, string data, string contentType, string encoding, string acceptType,bool isAllowRedirect,bool isAppend)
        {
            //将Url写入到文件，追加
            //每个请求一行记录
            StringBuilder sbc = new StringBuilder(); 
            sbc.AppendFormat("{0}::{1}::{2}::{3}::{4}::{5}::{6}::{7}::{8}\r\n",
                    method, url, port,baseUrl, data, contentType,encoding,acceptType,isAllowRedirect.ToString());
            if (isAppend)
            {
                File.AppendAllText(cookieFilePath, sbc.ToString(), Encoding.Default);
            }
            else {
                    File.WriteAllText(cookieFilePath, sbc.ToString(), Encoding.Default);
            }
        }

        public static void writeLogToFile(string cookieFilePath,string message, string method, string url, string data, string contentType, string encoding, string acceptType, bool isAllowRedirect)
        {
            //将Url写入到文件，追加
            //每个请求一行记录
            StringBuilder sbc = new StringBuilder();
            sbc.Append("***********************************************************\r\n");
            sbc.AppendFormat("{0}\r\n", DateTime.Now.ToLocalTime().ToString());
            sbc.AppendFormat("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\r\n",
                    method, url, data, contentType, encoding, acceptType, isAllowRedirect.ToString());
            sbc.Append(message);
            sbc.Append("\r\n"); 
            File.AppendAllText(cookieFilePath, sbc.ToString(), Encoding.Default);
        }

        public static string[] readUrlFromFile(string urlFilePath)
        {
            //从文件中读取URL列表 
            return File.ReadAllText(urlFilePath, Encoding.Default).Split("\r*\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
         }
    }
}
