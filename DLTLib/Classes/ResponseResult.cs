using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DLTLib.Classes
{
    public class ResponseResult
    {
        public static void ErrorResponse(string _Msg, string _Res)
        {
            string _result = "";
            Dictionary<string, string> _RepDic = new Dictionary<string, string>();
            _RepDic.Add("status", "1");
            _RepDic.Add("msg", _Msg);
            _RepDic.Add("res", _Res);
            _result = JsonConvert.SerializeObject(_RepDic);
            HttpContext.Current.Response.ContentType = "application/text";
            HttpContext.Current.Response.Write(_result);
        }
        public static void LoginSuess(string _Msg, string _Res)
        {
            string _result = "";
            Dictionary<string, string> _RepDic = new Dictionary<string, string>();
            _RepDic.Add("status","true");
            _RepDic.Add("msg", _Msg);
            _RepDic.Add("res", _Res);
            _result = JsonConvert.SerializeObject(_RepDic);
            HttpContext.Current.Response.ContentType = "application/text";
            HttpContext.Current.Response.Write(_result);
        }

        public static void ServicingResponse(string _Msg, string _Res)
        {
            string _result = "";
            Dictionary<string, string> _RepDic = new Dictionary<string, string>();
            _RepDic.Add("status", "2");
            _RepDic.Add("msg", _Msg);
            _RepDic.Add("res", _Res);
            _result = JsonConvert.SerializeObject(_RepDic);
            HttpContext.Current.Response.ContentType = "application/text";
            HttpContext.Current.Response.Write(_result);
        }
        public static void UserNO(string _Msg, string _Res)
        {
            string _result = "";
            Dictionary<string, string> _RepDic = new Dictionary<string, string>();
            _RepDic.Add("status", "false");
            _RepDic.Add("msg", _Msg);
            _RepDic.Add("res", _Res);
            _result = JsonConvert.SerializeObject(_RepDic);
            HttpContext.Current.Response.ContentType = "application/text";
            HttpContext.Current.Response.Write(_result);
        }
        public static void AlterSuess(string _Msg, string _Res)
        {
            string _result = "";
            Dictionary<string, string> _RepDic = new Dictionary<string, string>();
            _RepDic.Add("status", "true");
            _RepDic.Add("msg", _Msg);
            _RepDic.Add("res", _Res);
            _result = JsonConvert.SerializeObject(_RepDic);
            HttpContext.Current.Response.ContentType = "application/text";
            HttpContext.Current.Response.Write(_result);
        }
        public static void AlterFail(string _Msg, string _Res)
        {
            string _result = "";
            Dictionary<string, string> _RepDic = new Dictionary<string, string>();
            _RepDic.Add("status", "false");
            _RepDic.Add("msg", _Msg);
            _RepDic.Add("res", _Res);
            _result = JsonConvert.SerializeObject(_RepDic);
            HttpContext.Current.Response.ContentType = "application/text";
            HttpContext.Current.Response.Write(_result);
        }
    }
}
