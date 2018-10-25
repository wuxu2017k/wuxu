using DLTLib.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using System.Data.SqlClient;

namespace DLT
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class DLT
    {
        // 要使用 HTTP GET，请添加 [WebGet] 特性。(默认 ResponseFormat 为 WebMessageFormat.Json)
        // 要创建返回 XML 的操作，
        //     请添加 [WebGet(ResponseFormat=WebMessageFormat.Xml)]，
        //     并在操作正文中包括以下行:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "LogIn", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public void LogIn(Stream dt)
        {
            try
            {
                StreamReader reader = new StreamReader(dt, Encoding.UTF8);
                string body = reader.ReadToEnd();
                Debug.Print(body);
                Dictionary<string, object> _ReqDic = JsonConvert.DeserializeObject<Dictionary<string, object>>(body);
                string name = "";
                string pwd = "";
                try
                {
                    name = _ReqDic["LoginName"].ToString().Trim();
                    pwd = _ReqDic["Password"].ToString().Trim();
                }
                catch
                {

                    ResponseResult.ErrorResponse("未输入用户名或密码", "ONWATER");
                    return;
                }

                List<SqlParameter> _ParameterList = new List<SqlParameter>();
                _ParameterList.Add(new SqlParameter("@loginname", name));
                _ParameterList.Add(new SqlParameter("@password", pwd));
                if (ClsMSSQL.Exists1("tlogin", "loginname", name, "password", pwd, ClsDBCon.ConStrKj))
                {
                    DataTable data = SqlOperation.Returnqueryresults(_ParameterList.ToArray<SqlParameter>());
                    //DataTable data = ClsMSSQL.返回查询结果(new SqlCommand("select * from tlogin where loginname = @loginname and password = @password"), _ParameterList.ToArray<SqlParameter>());
                    string sJSON = JsonConvert.SerializeObject(data,Formatting.Indented);

                    //return sJSON;
                    HttpContext.Current.Response.ContentType = "application/text";
                    ResponseResult.LoginSuess("ok", sJSON);
                }
                else
                {
                    ResponseResult.UserNO("账户或密码不正确", "");
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.ContentType = "application/text";
                ResponseResult.ErrorResponse(ex.Message, "");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "SET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public void SET(Stream dt)
        {
            string _result = "";
            StreamReader reader = new StreamReader(dt, Encoding.UTF8);
            string body = reader.ReadToEnd();
            Debug.Print(body);
            Dictionary<string, object> _ReqDic = JsonConvert.DeserializeObject<Dictionary<string, object>>(body);
            string name = "";
            string newpwd = "";
            try
            {
                name = _ReqDic["loginname"].ToString().Trim();
                newpwd = _ReqDic["newpassword"].ToString().Trim();
            }
            catch
            {   
                return;
            }
            // string select = "UPDATE [dbo].[tlogin]SET [password] ='" + pwd + "' WHERE [loginname]='" + name + "'";
            List<SqlParameter> _ParameterList = new List<SqlParameter>();
            _ParameterList.Add(new SqlParameter("@loginname", name));
            _ParameterList.Add(new SqlParameter("@newpassword", newpwd));
             int data = SqlOperation.UpDat(_ParameterList.ToArray<SqlParameter>());
            string sJSON = JsonConvert.SerializeObject(data);
            if (Convert.ToInt32(sJSON) == 1)
            {
              
                HttpContext.Current.Response.ContentType = "application/text";
                ResponseResult.AlterSuess("Ok", "修改成功");
                //HttpContext.Current.Response.Write(sJSON);
            }
            else
            {
                ResponseResult.AlterFail("通信异常", "");
            }
        }


        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "SELECT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public void SELECT(Stream dt)
        {
            string _result = "";
            StreamReader reader = new StreamReader(dt, Encoding.UTF8);
            string body = reader.ReadToEnd();
            Debug.Print(body);
            Dictionary<string, object> _ReqDic = JsonConvert.DeserializeObject<Dictionary<string, object>>(body);
            string ygid = "";
            string role= "";
            try
            {
                ygid = _ReqDic["ygid"].ToString().Trim();
                role = _ReqDic["role"].ToString().Trim();
            }
            catch
            {
                return;
            }
            List<SqlParameter> _ParameterList = new List<SqlParameter>();
            _ParameterList.Add(new SqlParameter("@ygid", ygid));
            _ParameterList.Add(new SqlParameter("@role", role));
            if (ClsMSSQL.Exists2("tlogin", "ygid", ygid, ClsDBCon.ConStrKj))
            {
                DataTable data = SqlOperation.Returnqueryresults1(_ParameterList.ToArray<SqlParameter>());
                
                string sJSON = JsonConvert.SerializeObject(data,Formatting.Indented);
               
                HttpContext.Current.Response.ContentType = "application/text";
                ResponseResult.LoginSuess("ok", sJSON);
            }
            else
            {
                ResponseResult.AlterFail("通信异常", "");
            }
        }
        public void DoWork()
        {
            // 在此处添加操作实现
            return;
        }
        
        // 在此处添加更多操作并使用 [OperationContract] 标记它们
    }
}
