/* 
 * Author yenbay
 * Date 2017-09-22
 * Good Luck!
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Windows.Forms;

namespace HttpTest
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
        public static readonly Encoding DefaultEncoding = Encoding.UTF8;
        public static readonly string DefaultEncodingName = DefaultEncoding.HeaderName.ToUpper();
        public static CookieContainer cookieContainer = null;
        private static string cookieString = "";
        private static readonly string commonPath = HttpUtils.replace(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "(?i)[\\\\//]*documents[\\\\//]*$", "") + "\\HttpTester";
        private static readonly string errFilePath = commonPath + "\\error.txt";//请求错误记录
        private static readonly string logFilePath = commonPath + "\\log.txt";//日志
        private static readonly string favourFilePath = commonPath + "\\favour.txt";//收藏夹 

        public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
            //init log file path
            InitLogFilePath();
            //init config
            InitConfig();
            //init favour 
            loadRequestInfoToFile();
            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
        }
       
       
        
        private void InitLogFilePath() {
            //建立日志文件夹
            this.log.Text = "日志目录:" + commonPath;
            if (!Directory.Exists(commonPath))
            {
                Directory.CreateDirectory(commonPath);
            }
        }
        //初始配置
        private void InitConfig() {
            //设置请求方法默认值
            this.requestMethod.Text = this.requestMethod.Items[0].ToString();
            //设置content-type默认值
            this.contentTypeCombo.Text = this.contentTypeCombo.Items[0].ToString();
            //编码默认utf-8
            foreach (EncodingInfo encoding in Encoding.GetEncodings())
            {
                this.encodeList.Items.Add(encoding.GetEncoding().HeaderName);
            } 
            //初始化refer
            /*
            if (this.referText.Text.Trim().Length == 0)
            {
                string host = HttpUtils.regexMatch(this.baseIp.Text.Trim(), "(?i)https?") ? this.baseIp.Text.Trim() : "http://"+this.baseIp.Text.Trim();
                if(this.basePort.Text.Trim().Length>0) host += ":"+this.basePort.Text.Trim(); 
                this.referText.Text = host;
            }*/
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
            System.Threading.Thread.Sleep(200);
            //responseResult.Select(this.responseResult.Text.Length,0);
            responseResult.SelectionStart = responseResult.Text.Length; //Set the current caret position at the end
			responseResult.ScrollToCaret(); 
		}

        private void MsgRequest(string sMsg) {
            if (sMsg != null)
                if (sMsg.EndsWith("\r\n"))
                    requestResult.AppendText(sMsg);
                else
                    requestResult.AppendText(sMsg + "\r\n"); 
            //requestResult.Select(this.requestResult.Text.Length, 0);
            requestResult.SelectionStart = requestResult.Text.Length;
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
                    this.log.Text = "不合法的请求地址！";
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
            this.log.Text = "打开请求文件,执行批量请求!";
            //打开文件（链接文件）//批量请求
            string strTemp;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = @commonPath;
            ofd.Title = "选择文件(Open)";
            ofd.FileName = "";
            ofd.RestoreDirectory = true;
            ofd.Filter = "文本文件(*.txt)|*.txt";
            ofd.ValidateNames = true;
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(ofd.FileName, Encoding.Default);
                strTemp = sr.ReadToEnd();
                sr.Close(); 
                ofd.Dispose();
            }
            else
            {
                return;
            }
            //////////////////////// 开始执行批量请求 //////////////// 
            //先关闭自动收藏
            bool enableAutoFavour = this.enableAutoFavour.Checked;
            this.enableAutoFavour.Checked = false;
            //
            string[] records = HttpUtils.regexSplit(strTemp, "\r*\n+");
            //过滤Url
            if (records != null && records.Length > 0)
            {
                foreach (string record in records)
                {
                    if (HttpUtils.isNotBlank(record))
                    {
                        //加载一条
                        loadOneRecord(record);
                        //开启请求
                        this.log.Text = "开始请求" + record;
                        TestGetApi(createRequestUrl());
                        //请求结束,等待3s
                        System.Threading.Thread.Sleep(1000); 
                    }
                }
            }
            //还原自动收藏状态
            this.enableAutoFavour.Checked = enableAutoFavour;
            this.log.Text = "批量请求结束!";
        }

         
        /**
         *点击发送,发送api
         */
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
            //refer
            if (!this.fixedRefer.Checked && this.referText.Text.Trim().Length == 0) {
                this.referText.Text = HttpUtils.regexFindFirst(myUrl,"(?i)https?://[^/]+");
            }
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
            myRequest.Referer = HttpUtils.replace(this.referText.Text, "[\\s\\r\\n]", ""); 
            //处理cookie
            if (cookieContainer == null || cookieContainer.Count == 0)
            {
                myRequest.CookieContainer = new CookieContainer();
                cookieContainer = myRequest.CookieContainer;
            }
            else
            {
                if(cookieString.Trim().Length > 0) myRequest.Headers.Add("Cookie", cookieString.Trim()); 
                myRequest.CookieContainer = cookieContainer;  
            } 
            //发送请求 
            WebResponse response = null;
            string result = "";
            string responseHead = "";
            string responseBody = ""; 
            bool isSuccess = false;
            DateTime start = DateTime.Now;
            DateTime end = start;
            try
            {
                response = myRequest.GetResponse(); 
                end = DateTime.Now;
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), encoding))
                    {
                    //if (((HttpWebResponse)response).StatusCode == HttpStatusCode.OK) { }
                    string content_type = ((HttpWebResponse)response).GetResponseHeader("content-type");
                    if (content_type.StartsWith("image/")) {
                        System.Drawing.Image image = Image.FromStream(reader.BaseStream);
                        responseBody += content_type;
                        this.showImage.Image = image;  
                    }else
                    {
                        responseBody += reader.ReadToEnd();
                    } 
                        responseHead = "\r\n"+response.ResponseUri.ToString() + "\r\n" + response.Headers.ToString();
                        if (((HttpWebResponse)response).StatusCode == HttpStatusCode.OK) isSuccess = true; 
                    }
            }
            catch (Exception error) {
                responseBody += error.ToString();
            } 
            double timeCost = (end - start).TotalSeconds; //请求耗时秒数
            //写入request
            string request = "";
            try
            {
               WebHeaderCollection headers =  myRequest.Headers;
                if (headers != null && headers.Count > 0) {
                    for (int j = 0; j < headers.Count; j++) {
                        string head = headers.GetKey(j);
                        string value=headers.Get(head);   
                        request += head + "=" + value+"\r\n";
                    }
                }
            }
            catch (Exception err)
            {
                MsgRequest(err.Message);
            }
            //cookie 
            List<Cookie> list = HttpUtils.GetAllCookies(myRequest.CookieContainer);
            cookieString = "";
            foreach (Cookie ck in list) { 
                if (ck.Domain == HttpUtils.regexFindFirst(myUrl, "(?i)([\\da-z-_]+\\.)+[\\da-z-_]+"))
                {
                    //request += ck.Name + "=" + ck.Value + "\r\n";
                    string _cookie = ck.Name + "=" + ck.Value + ";";
                    if (!cookieString.Contains(_cookie)) cookieString += _cookie;
                } 
            }
            this.cookieBox.Clear();
            this.cookieBox.AppendText(cookieString.Trim());
            result = request + "\r\n" + GeneralString(30, "*")+"\r\n";
            //---------------
            result += responseHead.Trim() + "\r\n";//添加头
            result += "======================================================= 请求返回结果 -->>\r\n";
            result += responseBody.Trim(); 
            string record = parseRecord();
            string log =  (record != null ? record+"\r\n": "")+result; 
            if (isSuccess)
            {
                //成功
                MsgRequest("【成功】" + "耗时" + timeCost+"秒");
                this.log.Text = "请求成功("+timeCost+"s):" + myUrl; 
                 //(HttpUtils.regexFindFirst(myUrl, "(?i)^https?://") + myRequest.Host+"/"); 
                //更新refer
                if (!this.fixedRefer.Checked && method.ToLower().Trim().Equals("get")) {
                    this.referText.Text = HttpUtils.replace(myUrl,"[\\r\\s\\n]","");
                }
                //如果开启自动收藏成功记录,加载收藏
                if (this.enableAutoFavour.Checked) {
                    saveRequestInfoToFile();
                }
            } 
            else {
                //失败
                MsgRequest("【失败】" + "耗时" + timeCost + "秒");
                this.log.Text = "请求失败("+timeCost+"s):" + myUrl;
                //记录错误日志
                HttpUtils.writeToFile(errFilePath,log,true); 
            }
            //写入日志
            HttpUtils.writeToFile(logFilePath,log,true);
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
            cookieString = "";
            this.cookieBox.Clear();
            this.log.Text = "清空Cookies!";
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
            this.urlList.Items.Clear();
            clearFavour();
            this.log.Text = "清空收藏!";
        }

        private void clearParam_Click(object sender, EventArgs e)
        {
            this.getParam_view.Rows.Clear();
        }


///////////////////////////////////////////////////////////////////////////////////////////////////////
        //添加到收藏夹
        private void saveRequestInfoToFile() { 
            //去掉旧的
            removeFromFile();  
            //保存到文件
            string record  = parseRecord();
            if (record != null && record.Trim().Length > 0) File.AppendAllText(favourFilePath, record, Encoding.Default);
            if (!this.urlList.Items.Contains(this.urlList.Text.Trim())) this.urlList.Items.Add(this.urlList.Text.Trim());
        }

        //转换当前为一条记录
        private string parseRecord() {
            //ip
            string ip = this.baseIp.Text.Trim();
            string port = this.basePort.Text.Trim();
            string baseUrl = this.webName.Text.Trim();
            string encode = this.encodeList.Text.Trim();
            string autoRedirect = this.enableAllowAutoRedirect.Checked ? "true" : "false";
            string contentType = this.contentTypeCombo.Text.Trim();
            string method = this.requestMethod.Text.Trim();
            string accept = this.acceptCombo.Text.Trim();
            string url = this.urlList.Text.Trim();
            string head = "";
            for (int i = 0; i < this.getParam_view.Rows.Count; i++)
            {
                if (this.getParam_view.Rows[i].Cells[0].Value != null)
                {
                    head += "&" + this.getParam_view.Rows[i].Cells[0].Value.ToString().Trim() + "=";
                    if (this.getParam_view.Rows[i].Cells[1].Value != null)
                        head += this.getParam_view.Rows[i].Cells[1].Value.ToString().Trim();
                }
            }
            string body = HttpUtils.replace(this.postData.Text.Trim(), "\\r\\n", " ");
            string fixedRefer = this.fixedRefer.Checked ? "true" : "false";
            string refer = this.referText.Text.Trim();
            string user = this.edtUser.Text.Trim();
            string pass = this.edtPassword.Text.Trim();
            //数量为15
            string[] arr = new string[] { ip, port, baseUrl, encode, autoRedirect, contentType, method, accept, url, head, body, fixedRefer, refer, user, pass };
            if(arr!=null && arr.Length == 15) return string.Join("::", arr) + "\r\n";
            return null;
        }

        //加载所有收藏夹
        private void loadRequestInfoToFile() {
           string[] records = HttpUtils.readUrlFromFile(favourFilePath);
           string choose = null;
           foreach (string record in records)
           {
               if (HttpUtils.isNotBlank(record))
               {
                   try
                   {
                       string[] r = HttpUtils.regexSplit(record, "::");
                       //new string[]{ip,port,baseUrl,encode,autoRedirect,contentType,method,accept,url,head,body,fixedRefer,refer,user,pass};
                       if (r.Length >= 15)
                       {
                           //有效
                           choose = record;
                           if (!this.urlList.Items.Contains(r[8])) this.urlList.Items.Add(r[8]);
                       }
                   }
                   catch (Exception e)
                   {
                       this.log.Text = e.Message;
                   }
               }
           }
            //加载最后一个
           loadOneRecord(choose);
        } 

        //加载一条收藏夹记录(具体的解析)
        private void loadOneRecord(string record)
        {
            try
            {
                string[] r = HttpUtils.regexSplit(record, "::");
                //new string[]{ip,port,baseUrl,encode,autoRedirect,contentType,method,accept,url,head,body,fixedRefer,refer,user,pass};
                if (r.Length >= 15)
                {
                    this.baseIp.Text = r[0];
                    this.basePort.Text = r[1];
                    this.webName.Text = r[2];
                    this.encodeList.Text = r[3];
                    this.enableAllowAutoRedirect.Checked = r[4].Equals("true") ? true : false;
                    this.contentTypeCombo.Text = r[5];
                    this.requestMethod.Text = r[6];
                    this.acceptCombo.Text = r[7];
                    this.urlList.Text = r[8];
                    string head = r[9];
                    string[] headData = HttpUtils.regexSplit(head, "&");
                    this.getParam_view.Rows.Clear();
                    foreach (string _p in headData)
                    {
                        if (HttpUtils.isNotBlank(_p))
                        {
                            string name = HttpUtils.regexFindFirst(_p, "^[^=]+");
                            string value = _p.Replace(name, "");
                            if (HttpUtils.isNotBlank(name))
                            {
                                DataGridViewRow row = new DataGridViewRow();
                                row.Cells[0].Value = name;
                                row.Cells[1].Value = value;
                                this.getParam_view.Rows.Add(row); 
                            }
                        }
                    }
                    this.postData.Text = r[10];
                    this.fixedRefer.Checked = r[11].Equals("true") ? true : false;
                    this.referText.Text = r[12];
                    this.edtUser.Text = r[13];
                    this.edtPassword.Text = r[14];
                }
            }catch(Exception e){
                this.log.Text = e.Message;
            }
        }
                
        //通过url获取行完整url记录
        private string getRowByUrl(string url) {
            if (HttpUtils.isNotBlank(url))
            { 
                string[] urls = HttpUtils.readUrlFromFile(favourFilePath);//收藏夹中所有的记录 
                if(urls != null) foreach (string _url in urls) {
                    if (_url.Contains("::" + url.Trim() + "::")) { 
                        //包含
                        return _url;
                    }
                }
            }
            return null;
        }

        //清空收藏夹
        private void clearFavour() {
            HttpUtils.deleteFile(favourFilePath);
        }

        //从收藏夹删除
        private void removeFromFile() { 
            string url = this.urlList.Text.Trim();
            string port = this.basePort.Text.Trim(); 
            string baseUrl = this.baseIp.Text.Trim();
            string basePart = this.webName.Text.Trim();
            string[] urls = HttpUtils.readUrlFromFile(favourFilePath);//收藏夹中所有的记录 
            StringBuilder sb = new StringBuilder();//存储有用的
            List<string> duplicateRemove = new List<string>();//用于过滤
            if (urls != null) {
                foreach (string _url in urls)
                {
                    string[] urlArr = HttpUtils.regexSplit(_url,"::");
                    if (urlArr != null && urlArr.Length >= 15) {
                        //有效链接
                        if (!duplicateRemove.Contains(_url))
                        {
                            //没有重复 
                            if (HttpUtils.regexMatch(url, "(?i)^https?\\://"))
                            {
                                if (!_url.Contains("::" + url + "::"))
                                {
                                    //不是删除项
                                    duplicateRemove.Add(_url);
                                    sb.Append(_url + "\r\n");
                                }
                            }
                            else
                            {
                                if (!(_url.Contains("::" + url + "::") && _url.StartsWith(baseUrl + "::" + port + "::" + basePart + "::")))
                                {
                                    //包含,则删除 
                                    duplicateRemove.Add(_url);
                                    sb.Append(_url + "\r\n");
                                }
                            }
                        }
                    }  
                }
                //写入文件
                File.WriteAllText(favourFilePath, sb.ToString(), Encoding.Default);
            }
            if (this.urlList.Text.Trim().Length>0 && this.urlList.Items.Contains(this.urlList.Text.Trim())) this.urlList.Items.Remove(this.urlList.Text.Trim());
        }
 /////////////////////////////////////////////////////////////////////////////\
       




/// <summary>
///
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void edtUser_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void edtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void cookieBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void enableAllowAutoRedirect_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void acceptCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void contentTypeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void encodeList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void requestMethod_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void webName_TextChanged(object sender, EventArgs e)
        {

        }

        private void labelPort_Click(object sender, EventArgs e)
        {

        }

        private void basePort_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void getParam_view_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void postData_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void referText_TextChanged(object sender, EventArgs e)
        {

        }

        private void fixedRefer_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void showImage_Click(object sender, EventArgs e)
        {

        }

        private void resultPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void log_Click(object sender, EventArgs e)
        {

        }

        private void urlList_SelectedValueChanged(object sender, EventArgs e)
        {
            //选取事件
            object sv = this.urlList.SelectedItem;
            if (sv != null && sv.ToString().Trim().Length>0)
            {
                loadOneRecord(getRowByUrl(sv.ToString().Trim()));
                this.log.Text = "加载URL请求:" + sv.ToString().Trim();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //添加到收藏 
           saveRequestInfoToFile();
           this.log.Text = "已收藏" + this.urlList.Text.Trim(); 
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //取消收藏 
           removeFromFile();
           this.log.Text = "取消收藏" + this.urlList.Text.Trim(); 
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void enableAutoFavour_MouseHover(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            //清空日志 
            HttpUtils.deleteFile(logFilePath); 
            //清空错误记录
            HttpUtils.deleteFile(errFilePath);
            this.log.Text = "清理日志文件!";
        }
    }
}
