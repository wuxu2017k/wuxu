using DLTLib.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

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
        public string LogIn(Stream dt)
        {
            StreamReader reader = new StreamReader(dt);
            string body = reader.ReadToEnd();
            Debug.Print(body);
            string[] str = body.Split('|');
            string name = str[0].ToString();
            string pwd = str[1].ToString();
            string select = "SELECT * FROM [dbo].[tlogin] where [loginname]='" + name.Trim() + "'and [password]='" + pwd.Trim() + "'";
           
            DataTable data = ClsMSSQL.GetDataTable(select, ClsDBCon.ConStrKj);
            string sJSON = JsonConvert.SerializeObject(data, Formatting.Indented);
            return sJSON;

        }
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "SET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public string SET(Stream dt)
        {
            StreamReader reader = new StreamReader(dt);
            string body = reader.ReadToEnd();
            Debug.Print(body);
            string[] str = body.Split('|');
            string name = str[0].ToString();
            string pwd = str[1].ToString();
            string select = "UPDATE [dbo].[tlogin]SET [password] ='"+pwd+ "' WHERE [loginname]='" + name.Trim() + "'";

            int data = ClsMSSQL.Update(select, ClsDBCon.ConStrKj);
            string sJSON = JsonConvert.SerializeObject(data, Formatting.Indented);
            return sJSON;

        }
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "SELECT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public string SELECT(Stream dt)
        {
            StreamReader reader = new StreamReader(dt);
            string body = reader.ReadToEnd();
            Debug.Print(body);
            string[] str = body.Split('|');
            string id = str[0].ToString();
            string role = str[1].ToString();
            string select = "SELECT [id],[loginname],[xm],[ygid] FROM [dbo].[tlogin] where [ygid]='" + id.Trim() + "'and[role] = '" + role.Trim() + "'";
            
            DataTable data = ClsMSSQL.GetDataTable(select, ClsDBCon.ConStrKj);
            string sJSON = JsonConvert.SerializeObject(data, Formatting.Indented);
            return sJSON;

        }
        public void DoWork()
        {
            // 在此处添加操作实现
            return;
        }
        
        // 在此处添加更多操作并使用 [OperationContract] 标记它们
    }
}
