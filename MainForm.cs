/*
 * Created by SharpDevelop.
 * User: Kevin
 * Date: 2017/5/13
 * Time: 15:49
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Windows.Forms;

namespace RestTest
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
        public static readonly Encoding DefaultEncoding = Encoding.UTF8;
        public static readonly string DefaultEncodingName = DefaultEncoding.HeaderName.ToUpper();
        public static CookieContainer cookieContainer = null; 
        private static readonly string errorFileUrl = Application.StartupPath + "\\HttpTesterError.log";
        private static readonly string lastLogFileUrl = Application.UserAppDataPath + "\\HttpTester.log";


        public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
            //初始化完成后执行事件
            AfterInitCompleted(); 
            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
        }

        public static void alert(Object obj) {
            MessageBox.Show(obj.ToString());
        }
        private void AfterInitCompleted() {
            //设置请求方法默认值
            this.requestMethod.Text = this.requestMethod.Items[0].ToString();
            //设置content-type默认值
            this.contentTypeCombo.Text = this.contentTypeCombo.Items[0].ToString(); 
            //编码默认UTF8
            foreach (EncodingInfo encoding in Encoding.GetEncodings()) {
                this.encodeList.Items.Add(encoding.GetEncoding().HeaderName);
            }
            this.encodeList.Text = "utf-8";
            //加载请求记录到列表  
            string[] urls = new string[] { };
            try
            {
                urls = HttpUtils.readUrlFromFile(lastLogFileUrl);
                if (urls != null && urls.Length > 0) {
                    string last = urls[urls.Length - 1];
                    string[] lastList = HttpUtils.regexSplit(last, "::");
                    if (lastList.Length >= 9) {
                        //初始化最后一条数据
                        // method, url, port,baseUrl, data, contentType,encoding,acceptType,isAllowRedirect 
                        string preUrl = HttpUtils.regexFindFirst(lastList[1], "(?i)^\\s*(https?://([^\\.:/]+\\.)+[^\\.:/]+)");
                        if (HttpUtils.isNotBlank(preUrl))
                        {
                            try
                            {
                                this.baseIp.Text = preUrl.Trim();
                                this.basePort.Text = lastList[2];
                                this.webName.Text = lastList[3];
                                if(lastList[0] != null && this.requestMethod.Items.Contains(lastList[0].ToUpper())) this.requestMethod.Text = lastList[0];
                                this.enableAllowAutoRedirect.Checked = HttpUtils.regexMatch(lastList[8], "false") ? false : true;
                                this.contentTypeCombo.Text = lastList[5];
                                this.acceptCombo.Text = lastList[7];
                                if(lastList[6] != null && this.encodeList.Items.Contains(lastList[6])) this.encodeList.Text = lastList[6];
                                this.postData.Text = lastList[4];
                                string pre = HttpUtils.regexFindFirst(lastList[1], "(?i)^\\s*(https?://([^\\.:/]+\\.)+[^\\.:/]+)(:\\d+)?");
                                string url = lastList[1].Replace(pre, "");
                                if (lastList[3] != null && lastList[3].Length > 0) url = url.Replace(lastList[3], ""); 
                                if(HttpUtils.isNotBlank(url)) this.urlList.Items.Add(url);
                                this.urlList.Text = url;
                            }
                            catch (Exception err) {
                                Console.WriteLine(err);
                            } 
                    } 
                    } 
                }
                HashSet<String> set = new HashSet<string>();
                foreach (string str in urls)
                {
                    if (HttpUtils.isNotBlank(str)) set.Add(str); //先去重复请求
                } 
                //请求
                foreach (String s in set)
                {
                    string[] li = HttpUtils.regexSplit(s, "::");
                    if (li.Length >= 9)
                    {
                        try
                        {
                            string head = HttpUtils.regexFindFirst(li[1], "(?i)^\\s*(https?://([^\\.:/]+\\.)+[^\\.:/]+)(:\\d+)?");
                            string url = li[1].Replace(head, "");
                            if(li[3] != null && li[3].Length >0 ) url = url.Replace(li[3],"");
                            if (HttpUtils.isNotBlank(url) && !this.urlList.Items.Contains(url)) this.urlList.Items.Add(url); 
                        }
                        catch (Exception er)
                        {
                            Console.WriteLine(er);
                        }
                    }
                }

            }
            catch (Exception e) {  } 
        }

        private static Encoding getEncodingByEncodeName(string encodeName)
        {
            Encoding encode = null;
            if(HttpUtils.isNotBlank(encodeName))
            foreach (EncodingInfo encoding in Encoding.GetEncodings())
            {
                    if (encoding.GetEncoding().HeaderName.ToLower().Equals(encodeName.ToLower())) {
                        encode = encoding.GetEncoding();
                        break;
                    }
            }
            return encode;
        }

		private static string GeneralString(int n, string par)
		{
			StringBuilder a = new StringBuilder(n * par.Length);
			for (int i=0;i<n;i++)
			{
				a.Append(par);
			}
			return a.ToString();
		}
		
		private void MsgResponse(string sMsg)
		{
            if (sMsg != null) {
                sMsg = HttpUtils.replace(sMsg,"^\\s*|\\s*$","");
            } 
			responseResult.Text = sMsg.Trim() + "\r\n";
			responseResult.ScrollToCaret();
		}

        private void MsgRequest(string sMsg) {
            if (sMsg != null)
                if (sMsg.EndsWith("\r\n"))
                    requestResult.AppendText(sMsg);
                else
                    requestResult.AppendText(sMsg + "\r\n");
            requestResult.ScrollToCaret();
        }

        private String createRequestUrl() {
            //----------------点击入口程序------------
            String url = null;
            //直接判断url
            if (HttpUtils.regexMatch(urlList.Text, "(?i)^((ht|f)tps?):\\/\\/([\\da-z]+\\.)+[\\da-z]+"))
            {
                url = urlList.Text.Trim();
            }
            else
            {
                //部分拼接
                if (HttpUtils.regexMatch(baseIp.Text, "(?i)^([\\da-z]+\\.)+[\\da-z]+")) {
                    baseIp.Text = "http://" + baseIp.Text;
                }
                //判断BaseIP
                if (HttpUtils.regexMatch(baseIp.Text, "(?i)^((ht|f)tps?):\\/\\/([\\da-z]+\\.)+[\\da-z]+"))
                {
                    //拼接
                    url = baseIp.Text.Trim();
                    //添加端口号
                    if (url.EndsWith(":")) url = url.Replace(":", "");
                    if (HttpUtils.regexMatch(basePort.Text, "^\\s*\\d+\\s*$"))
                    {
                         url += ":"+basePort.Text.Trim(); 
                    }
                    //添加app name
                    if (HttpUtils.isNotBlank(this.webName.Text)) {
                        url += "/"+HttpUtils.replace(this.webName.Text, "^\\s*/|\\s*$", "");
                    }
                    //添加url
                    if (HttpUtils.isNotBlank(urlList.Text))
                    { 
                        url += "/" + HttpUtils.replace(this.urlList.Text, "^\\s*/|\\s*$", "");
                    }
                }
                else
                {
                    MessageBox.Show("不合法的请求地址！");
                }
            } 
            return url;
        }

        void BtnTestClick(object sender, EventArgs e)
		{ 
            // --------------开始请求 ----------------- 
            TestGetApi(createRequestUrl());  
		}


		void Button1Click(object sender, EventArgs e)
		{
            //打开文件（链接文件）
			string strTemp;
			OpenFileDialog ofd = new OpenFileDialog();
	        ofd.Title = "选择文件(Open)";
	        ofd.FileName = "";
	        ofd.RestoreDirectory = true;
	        ofd.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
	        ofd.ValidateNames = true;
	        ofd.CheckFileExists = true;
	        ofd.CheckPathExists = true;
        	if (ofd.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(ofd.FileName, Encoding.Default);
                strTemp = sr.ReadToEnd();
                sr.Close();
            }
        	else
        	{
        		return;
        	} 
            //列表
        	string[] UriList = HttpUtils.regexSplit(strTemp,"\r*\n");
            HashSet<String> set = new HashSet<string>();
        	foreach (string str in UriList) {
                if(HttpUtils.isNotBlank(str)) set.Add(str); //先去重复请求
        	}
            //请求
            foreach (String s in set) { 
                string[] li = HttpUtils.regexSplit(s,"::"); 
                if (li.Length >= 9)
                {
                    try
                    {
                        bool allowRedirect = true; 
                        if (HttpUtils.isNotBlank(li[8]) && li[8].Trim().ToLower().Equals("false"))
                        {
                            allowRedirect = false;
                        }
                        //method, url, port,baseUrl, data, contentType,encoding,acceptType,isAllowRedirect
                        TestGetApi(li[1],li[0],li[5],li[4],li[6],li[7],allowRedirect);
                    }
                    catch (Exception er) {
                        MsgRequest(er.ToString());
                    } 
                }  
            }
        }

        private void TestGetApi(string myUrl)
		{
            string logUrl = "";
            if (!HttpUtils.regexMatch(myUrl, HttpUtils.urlRegex))
            {
                MsgRequest(GeneralString(80, "*"));
                MsgRequest(myUrl + "解析后不是有效的请求地址！");
                return;
            }
            //判断认证信息
            string user = "";
            string pwd = "";
            if (HttpUtils.isNotBlank(edtUser.Text) && HttpUtils.isNotBlank(edtPassword.Text))
            {
                user = edtUser.Text.Trim();
                pwd = edtPassword.Text.Trim();
            }
            //content-type
            String contentType = this.contentTypeCombo.Text;
            if (HttpUtils.isNotBlank(contentType)) {
                contentType = contentType.Trim();
                if (contentType.EndsWith(";")) contentType = contentType.Substring(0,contentType.Length-1);
            }
            //allowAutoRedirect
            bool allowAutoRedirect = enableAllowAutoRedirect.Checked;
            String accept = this.acceptCombo.Text;

            //拼接参数 
            logUrl = myUrl;//日志的URL
            Encoding encoding = getEncodingByEncodeName(this.encodeList.Text);
            if (encoding == null) encoding = DefaultEncoding;
            bool endsWithSplit = HttpUtils.regexMatch(myUrl, "(&|\\?)\\s*$"); 
            for (int i = 0; i < getParam_view.Rows.Count; i++) { 
                    object param = getParam_view.Rows[i].Cells[0].Value;
                    if (param != null && HttpUtils.regexMatch(param.ToString(),"^\\s*[a-zA-Z\\d_]+\\s*$")) {
                    if (myUrl.Contains("?"))
                    {
                        if (endsWithSplit)
                        {
                            //包含有?或者&
                            myUrl += param.ToString().Trim() + "="; 
                            logUrl += param.ToString().Trim() + "=";
                        }
                        else
                        {
                            myUrl += "&" + param.ToString().Trim() + "=";
                            logUrl += "&" + param.ToString().Trim() + "=";
                        }
                    }
                    else {
                        myUrl += "?" + param.ToString().Trim() + "=";
                        logUrl += "?" + param.ToString().Trim() + "=";
                    } 
                    }
                    object value = getParam_view.Rows[i].Cells[1].Value;
                if (value != null && HttpUtils.isNotEmpty(value.ToString())) {
                        logUrl += value.ToString();
                      myUrl += HttpUtility.UrlEncode(value.ToString(),encoding);  
                }
            }
            //记录请求消息
            MsgRequest(GeneralString(30,"*")); 
            string method = requestMethod.Text;
            MsgRequest(method+"\t"+myUrl);
            HttpWebRequest myRequest = null;
            string data = "";
            if (method.ToLower().Trim().Equals("get"))
            {
                myRequest = HttpUtils.buildHeader(myUrl, method, contentType, null,encoding,accept,allowAutoRedirect); 
            }
            else { 
                if (HttpUtils.isNotBlank(postData.Text)) {
                    data = postData.Text;
                }
                MsgRequest("DATA\t"+data);
                myRequest = HttpUtils.buildHeader(myUrl,method,contentType,data,encoding,accept,allowAutoRedirect); 
            } 
            //添加其他信息
			if (!string.IsNullOrEmpty(user)) {
				CredentialCache myCache = new CredentialCache();
				myCache.Add(new Uri(myUrl), "Basic", new NetworkCredential(user, pwd));
				myRequest.Credentials = myCache;
				myRequest.Headers["Authorization"] = "Basic " + Convert.ToBase64String(encoding.GetBytes(user + ":" + pwd));
			}
            //处理cookie
            if (cookieContainer == null || cookieContainer.Count == 0)
            {
                myRequest.CookieContainer = new CookieContainer();
                cookieContainer = myRequest.CookieContainer;
            }
            else
            {
                myRequest.CookieContainer = cookieContainer;
            }

            WebResponse response = null;
            string result = "";
            string responseHead = "";
            string responseBody = ""; 
            bool isSuccess = false;
            try
            {
                response = myRequest.GetResponse(); 
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), encoding))
                    {
                    //if (((HttpWebResponse)response).StatusCode == HttpStatusCode.OK) { } 
                        responseBody += reader.ReadToEnd();
                        responseHead = response.ResponseUri.ToString() + "\r\n" + response.Headers.ToString();
                        if (((HttpWebResponse)response).StatusCode == HttpStatusCode.OK) isSuccess = true; 
                    }
            }
            catch (Exception error) {
                responseBody += error.ToString();
            }
            result += responseHead.Trim() + "\r\n";//添加头
            result += "======================================================= 请求返回结果 -->>\r\n";
            result += responseBody.Trim(); 
            if (isSuccess)
            {
                //成功
                MsgRequest("【成功】"); 
                this.cookieBox.Clear(); 
               // MessageBox.Show(HttpUtils.regexFindFirst(myUrl, "(?i)^https?://") + myRequest.Host+"/");
                List<Cookie> list = HttpUtils.GetAllCookies(cookieContainer);
                foreach(Cookie ck in list) {
                    this.cookieBox.AppendText(ck.Name + "=" + ck.Value);
                }
                //写入文件记录，用于后期载入文件  
                //file,method, url, port,baseUrl, data, contentType,encoding,acceptType,isAllowRedirect
                HttpUtils.writeUrlToFile(lastLogFileUrl, method, logUrl, this.basePort.Text != null ? this.basePort.Text.ToString().Trim() : "", this.webName.Text != null ? this.webName.Text.ToString().Trim() : "", this.postData.Text, contentType, encoding.HeaderName, accept, allowAutoRedirect, true);
                 }
            else {
                //失败
                MsgRequest("【失败】");
                //记录错误日志
                HttpUtils.writeLogToFile(errorFileUrl,result, method, logUrl, data, contentType, encoding.HeaderName, accept, allowAutoRedirect);

            }
            if (!this.urlList.Items.Contains(this.urlList.Text.Trim())) this.urlList.Items.Add(this.urlList.Text.Trim());
            MsgResponse(result);  
        }

        private void TestGetApi(string myUrl,string method,string  contentType,string  data,string  encodingName,string  accept,bool allowAutoRedirect) 
        {
            string logUrl = myUrl;
            if (!HttpUtils.regexMatch(myUrl, HttpUtils.urlRegex))
            {
                MsgRequest(GeneralString(80, "*"));
                MsgRequest(myUrl + "解析后不是有效的请求地址！");
                return;
            }
            //判断认证信息
            string user = "";
            string pwd = "";
            if (HttpUtils.isNotBlank(edtUser.Text) && HttpUtils.isNotBlank(edtPassword.Text))
            {
                user = edtUser.Text.Trim();
                pwd = edtPassword.Text.Trim();
            }
            //content-type 
            if (HttpUtils.isNotBlank(contentType))
            {
                contentType = contentType.Trim();
                if (contentType.EndsWith(";")) contentType = contentType.Substring(0, contentType.Length - 1);
            } 
            //拼接参数 
            Encoding encoding = getEncodingByEncodeName(encodingName);
            if (encoding == null) encoding = DefaultEncoding;
            //参数
            string param = HttpUtils.regexFindFirst(myUrl,"\\?.+$");
            if (HttpUtils.isNotBlank(param)) {
                myUrl = myUrl.Replace(param,"");
                myUrl += "?"+ HttpUtility.UrlEncode(param, encoding);
            } 
            //记录请求消息
            MsgRequest(GeneralString(30, "*")); 
            MsgRequest(method + "\t" + myUrl);
            HttpWebRequest myRequest = null; 
            if (method.ToLower().Trim().Equals("get"))
            {
                myRequest = HttpUtils.buildHeader(myUrl, method, contentType, null, encoding, accept, allowAutoRedirect);
            }
            else
            {
                if (HttpUtils.isNotBlank(postData.Text))
                {
                    data = postData.Text;
                }
                MsgRequest("DATA\t" + data);
                myRequest = HttpUtils.buildHeader(myUrl, method, contentType, data, encoding, accept, allowAutoRedirect);
            }
            //添加其他信息
            if (!string.IsNullOrEmpty(user))
            {
                CredentialCache myCache = new CredentialCache();
                myCache.Add(new Uri(myUrl), "Basic", new NetworkCredential(user, pwd));
                myRequest.Credentials = myCache;
                myRequest.Headers["Authorization"] = "Basic " + Convert.ToBase64String(encoding.GetBytes(user + ":" + pwd));
            }
            //处理cookie
            if (cookieContainer == null || cookieContainer.Count == 0)
            {
                myRequest.CookieContainer = new CookieContainer();
                cookieContainer = myRequest.CookieContainer;
            }
            else
            {
                myRequest.CookieContainer = cookieContainer;
            }

            WebResponse response = null;
            string result = "";
            string responseHead = "";
            bool isSuccess = false;
            try
            {
                response = myRequest.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), encoding))
                {
                    //if (((HttpWebResponse)response).StatusCode == HttpStatusCode.OK) { } 
                    result = reader.ReadToEnd();
                    responseHead = response.ResponseUri.ToString() + "\n" + response.Headers.ToString() + "---------------\n";
                    if (((HttpWebResponse)response).StatusCode == HttpStatusCode.OK) isSuccess = true;
                }
            }
            catch (Exception error)
            {
                result += error.ToString();
            }
            result = responseHead + result;
            if (isSuccess)
            {
                //成功
                MsgRequest("【成功】");

                this.cookieBox.Clear(); 
                List<Cookie> list = HttpUtils.GetAllCookies(cookieContainer);
                foreach (Cookie ck in list)
                {
                    this.cookieBox.AppendText(ck.Name + "=" + ck.Value);
                }
                //写入文件记录，用于后期载入文件  
                //file,method, url, port,baseUrl, data, contentType,encoding,acceptType,isAllowRedirect
                HttpUtils.writeUrlToFile(lastLogFileUrl, method, logUrl, this.basePort.Text != null ? this.basePort.Text.ToString().Trim() : "", this.webName.Text != null ? this.webName.Text.ToString().Trim() : "", this.postData.Text, contentType, encoding.HeaderName, accept, allowAutoRedirect, true);
             }
            else
            {
                //失败
                MsgRequest("【失败】");
                //记录错误日志
                HttpUtils.writeLogToFile(errorFileUrl, result, method, logUrl, data, contentType, encoding.HeaderName, accept, allowAutoRedirect);

            }
            if(!this.urlList.Items.Contains(this.urlList.Text.Trim()))this.urlList.Items.Add(this.urlList.Text.Trim());
            MsgResponse(result);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void edtResult_TextChanged(object sender, EventArgs e)
        {

        }
 

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            cookieContainer = null;
            this.cookieBox.Text = "";
        }

        private void baseIp_TextChanged(object sender, EventArgs e)
        {

        }

        private void urlList_SelectedIndexChanged(object sender, EventArgs e)
        {
             
        }

        private void urlList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                //回车触发发送请求
                // --------------开始请求 ----------------- 
                TestGetApi(createRequestUrl());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.urlList.Text = "";
            this.urlList.Items.Clear();
            HttpUtils.deleteFile(lastLogFileUrl);
            HttpUtils.deleteFile(errorFileUrl);
        }
    }
}
