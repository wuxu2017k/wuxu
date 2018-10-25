using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTLib.Classes
{
     public class SqlOperation
    {
        public static DataTable Returnqueryresults(SqlParameter[] _Parameter)
        {
            string sele = "select * from tlogin where loginname = @loginname and password = @password";
            DataTable data = new DataTable();
            using (SqlConnection conn = new SqlConnection(ClsDBCon.ConStrKj))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sele, conn);
                cmd.Parameters.AddRange(_Parameter);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                try
                {
                    da.Fill(data);
                }
                catch
                {
                }
                finally
                {
                    conn.Close();
                }
            }
                return data;
        }
        public static DataTable Returnqueryresults1(SqlParameter[] _Parameter)
        {
           string sele = "SELECT [id] ,[loginname] ,[xm] ,[education] FROM [dbo].[tlogin] where ygid=@ygid and role=@role";
           // string sele = "SELECT * FROM [dbo].[tlogin] ";
            DataTable data = new DataTable();
            using (SqlConnection conn = new SqlConnection(ClsDBCon.ConStrKj))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sele, conn);

                cmd.Parameters.AddRange(_Parameter);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                try
                {
                    da.Fill(data);
                }
                catch
                {

                }
                finally
                {
                    conn.Close();
                }
            }
            return data;
        }
        public static int UpDat(SqlParameter[] _Parameter)
        {
            string sele = "UPDATE [dbo].[tlogin]SET [password] =@newpassword WHERE [loginname]=@loginname";
            //string sele = "UPDATE [dbo].[tlogin]SET [password] ='1234567' WHERE [loginname]='wuxu'";
            int rows = 0;
            using (SqlConnection conn = new SqlConnection(ClsDBCon.ConStrKj))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sele, conn);
                cmd.Parameters.AddRange(_Parameter);
                try
                {
                    rows = cmd.ExecuteNonQuery();
                }
                catch
                {
                    rows = -1;
                }
                finally
                {
                    conn.Close();
                }

            }
            return rows;          
        }
    }
}
