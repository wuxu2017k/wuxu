using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DLTLib.Classes
{
    public class ClsOplog
    {
        public static void Oplog(string ygid, string type, string module, string values)
        {
            string record = "";

            string hostName = Dns.GetHostName();   //获取本机名
            IPHostEntry localhost = Dns.GetHostByName(hostName);    //方法已过期，可以获取IPv4的地址
            //IPHostEntry localhost = Dns.GetHostEntry(hostName);   //获取IPv6地址
            IPAddress IP = localhost.AddressList[0];

            switch (type)
            {
                case "10": record = "后台登录！"; break;//登录
                case "20": record = "访问" + module; break;//访问
                case "30": record = "修改记录：" + values; break;//修改
                case "40": record = "新增记录：" + values; break;//新增
                case "50": record = "删除记录：" + values; break;//删除
                case "60": record = "查询记录：" + values; break;//查询
                case "70": record = "退款记录：" + values; break;//退款
            }
            string log_cmd = string.Format(@"INSERT INTO toplog(ygid,IP,module,type,record) VALUES ('{0}','{1}','{2}','{3}','{4}')", ygid, IP.ToString(), module, type, record);
            ClsMSSQL1.InsertAffairs(log_cmd, ClsDBCon1.ConStrKj);
        }
    }
}
