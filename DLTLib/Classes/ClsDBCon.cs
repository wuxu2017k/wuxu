using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTLib.Classes
{
    public class ClsDBCon
    {
        static ClsDBCon()
        {
            // ConStrKj = ConfigurationManager.ConnectionStrings["dltjckjaConnectionString"].ConnectionString;
           
                ConStrKj = ConfigurationManager.ConnectionStrings["dltjckjaConnectionString"].ConnectionString;

        }
        /// <summary>
        /// 基础框架数据库连接串                                
        /// </summary>
        public static string ConStrKj { get; set; }
 

      

     

        
    }
}
