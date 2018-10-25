using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTLib.Classes
{
    public class ClsMSSQL
    {
        #region 存储过程—datarow
        /// <summary>
        /// 
        /// </summary>
        /// <param name="aSQLCmd"></param>
        /// <param name="aConStr"></param>
        /// <param name="arrLstParams"></param>
        /// <param name="isStProc"></param>
        /// <returns></returns>
        public static DataRow GetDataRow(string aSQLCmd, string aConStr, ArrayList arrLstParams = null, bool isStProc = false)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(aConStr))
            {
                conn.Open();
                SqlTransaction AffairsBegin = conn.BeginTransaction();
                SqlCommand cmd = new SqlCommand(aSQLCmd, conn);
                if (arrLstParams != null) //如果是null不能AddRange
                {
                    cmd.Parameters.AddRange(arrLstParams.ToArray(typeof(SqlParameter)));
                }
                if (isStProc)
                    cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    cmd.Transaction = AffairsBegin;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    AffairsBegin.Commit();
                }
                catch 
                {
                }
                finally
                {
                    conn.Close();
                }
            }
            return (dt.Rows.Count > 0) ? dt.Rows[0] : null;
            //return dt.Rows[0];
        }
        #endregion

        #region GetDataRow 
        /// <summary>
        /// 返回行
        /// </summary>
        /// <param name="aSelectCmd"></param>
        /// <param name="acntStr"></param>
        /// <returns></returns>
        public static DataRow GetDataRow(string aSelectCmd, string aCntStr)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(aCntStr))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(aSelectCmd, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                try
                {
                    da.Fill(dt);
                }
                catch 
                {
                }
                finally
                {
                    conn.Close();
                }
            }
            return (dt.Rows.Count > 0) ? dt.Rows[0] : null;
        }
        #endregion


        #region GetDataTable  
        /// <summary>
        /// 返回数据表
        /// </summary>
        /// <param name="aSelectCmd"></param>
        /// <param name="acntStr"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(string aSelectCmd, string aCntStr)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(aCntStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(aSelectCmd, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                try
                {
                    da.Fill(dt);
                }
                catch 
                {
                }
                finally
                {
                    conn.Close();
                }
            }
            return dt;
        }
        //自己写的GetDataTable
        public static DataTable GetDataTable1(string aSelectCmd, string aCntStr)
        {

            DataTable dt = new DataTable();



            return dt;
        }
        public static DataTable GetDataTable1(string aSqlcmd, string aConstr,out bool aSuccess,out string aErrMsg)//asqlcmd 连接数据库  aconstr 对数据库操作语句
        {
            aSuccess = true;
            aErrMsg = "";
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(aConstr))
            { 
                try
                {

                    conn.Open();
                    SqlCommand cmd = new SqlCommand(aSqlcmd, conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                catch(Exception ex)
                {
                    aSuccess = false;
                    aErrMsg = ex.Message;
                }
                finally
                {
                    conn.Close();
                }
            }

                return dt;
        }
        public static DataTable GetDataTable(string aSelectCmd, string aCntStr, SqlParameter para, ArrayList arrLstParams = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(aCntStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(aSelectCmd, conn);
                //单独穿一个数
                cmd.Parameters.Add(para);
                //定义list数组可以用这个
                //if (arrLstParams != null) //如果是null不能AddRange
                //{
                //    //cmd.Parameters.AddRange向SqlParameterCollection的末尾添加值数组，
                //    //arrLstParams.ToArray将arraylist的元素复制到一个指定元素类型的新数组
                //    cmd.Parameters.AddRange(arrLstParams.ToArray(typeof(SqlParameter)));
                //}
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                try
                {
                    da.Fill(dt);
                }
                catch 
                {
                }
                finally
                {
                    conn.Close();
                }
            }
            return dt;
        }


        public static DataTable GetDataTable(string aSelectCmd, string aCntStr, SqlParameter[] Params = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(aCntStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(aSelectCmd, conn);
                //定义list数组可以用这个
                if (Params != null) //如果是null不能AddRange
                {
                    //cmd.Parameters.AddRange向SqlParameterCollection的末尾添加值数组，
                    //arrLstParams.ToArray将arraylist的元素复制到一个指定元素类型的新数组
                    //cmd.Parameters.AddRange(Params.ToArray(typeof(SqlParameter)));
                    cmd.Parameters.AddRange(Params);
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                try
                {
                    da.Fill(dt);
                }
                catch
                {
                  
                }
                finally
                {
                    conn.Close();
                }
            }
            return dt;
        }
        #endregion

        #region Exists 
        /// <summary>
        /// <para>判断表中是否存在给定字段取给定值的记录,即：</para>
        /// <para>SELECT * FROM t WHERE f=v</para>
        /// <para>是否返回记录。</para>
        /// </summary>
        /// <param name="aTable"></param>
        /// <param name="aQueryField"></param>
        /// <param name="aQueryValue"></param>
        /// <param name="acntStr"></param>
        /// <returns></returns>
        public static bool Exists(string aTable, string aQueryField, string aQueryValue, string acntStr)
        {
            string cmdText = string.Format("SELECT * FROM {0} WHERE {1}='{2}'",aTable, aQueryField, aQueryValue);
            return Exists(cmdText, acntStr);
        }
        public static bool Exists2(string aTable, string aQueryField, string aQueryValue, string acntStr)
        {
            string cmdText = string.Format("SELECT * FROM {0} WHERE {1}='{2}'", aTable, aQueryField, aQueryValue);
            return Exists1(cmdText, acntStr);
        }
        public static bool Exists1(string aTable, string name, string aname,string pwd,string apwd, string acntStr)
        {
            string cmdText = string.Format("SELECT * FROM {0} WHERE {1}='{2}'and {3}={4}", aTable,name,aname,pwd,apwd);
            return Exists(cmdText, acntStr);
        }
        /// <summary>
        /// <para>根据一个SELECT语句判断数据表中是否存在记录.</para>
        /// <para>cmdText参数的形式应该形如：SELECT * FROM t1 WHERE id=5.</para>
        /// <para>函数通过将它转换为如下形式而达致功能实现：</para>
        /// <para>SELECT CASE WHEN EXISTS(SELECT * FROM t1 WHERE id=5) THEN 1 ELSE 0 END.</para>
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="acntStr"></param>
        /// <returns></returns>
        public static bool Exists(string cmdText, string acntStr)
        {
            bool b = false;
            using (SqlConnection conn = new SqlConnection(acntStr))
            {
                conn.Open();
                string cmdTextA = "EXISTS" + ClsQ.Q0(cmdText, '(');
                cmdTextA = "SELECT CASE WHEN " + cmdTextA + " THEN 1 ELSE 0 END";
                SqlCommand cmd = new SqlCommand(cmdTextA, conn); try
                {

                    b = Convert.ToBoolean(cmd.ExecuteScalar());
                }
                catch 
                {
                   
                }
                finally
                {
                    conn.Close();
                }
            }
            return b;
        }
        #endregion

        public static bool Exists1(string cmdText, string acntStr)
        {
            bool b = false;
            using (SqlConnection conn = new SqlConnection(acntStr))
            {
                conn.Open();
                
                SqlCommand cmd = new SqlCommand(cmdText,conn);
                try
                {
                    b = Convert.ToBoolean(cmd.ExecuteNonQuery());
                }
                catch
                {

                }
                finally
                {
                    conn.Close();
                }
            }
            return b;
        }
        #region GetValue 
        /// <summary>
        /// <para>执行SQL语句或存储过程，返回结果集第一行第一列的值。</para>
        /// <para>可以使用ArrayList arrLstParams传入参数，参数可以是输入型也可以是输出型，</para>
        /// <para>调用者可使用arrLstParams的实参检查输出型参数的返回值。</para>
        /// <para>如果是存储过程，则需要设置isStProc为true。</para>
        /// </summary>
        /// <param name="aSQLCmd"></param>
        /// <param name="aConStr"></param>
        /// <param name="arrLstParams"></param>
        /// <param name="isStProc"></param>
        public static Object GetValue(string aSQLCmd, string aConStr,ArrayList arrLstParams = null, bool isStProc = false)
        {
            Object ret = null;
            using (SqlConnection conn = new SqlConnection(aConStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(aSQLCmd, conn);

                if (arrLstParams != null) //如果是null不能AddRange
                {
                    //cmd.Parameters.AddRange向SqlParameterCollection的末尾添加值数组，
                    //arrLstParams.ToArray将arraylist的元素复制到一个指定元素类型的新数组
                    cmd.Parameters.AddRange(arrLstParams.ToArray(typeof(SqlParameter)));
                }
                if (isStProc)
                    cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    ret = cmd.ExecuteScalar();
                }
                catch 
                {
                   
                }
                finally
                {
                    conn.Close();
                }
            }
            return ret;
        }
        /// <summary>
        /// 根据给定的条件返回指定表中指定字段的值
        /// </summary>
        /// <param name="aOutField"></param>
        /// <param name="aTable"></param>
        /// <param name="aWhere"></param>
        /// <param name="aConStr"></param>
        /// <returns></returns>
        public static Object GetValue(string aOutField, string aTable, string aWhere, string aConStr)
        {
            string cmdText = string.Format("SELECT {0} FROM {1} WHERE {2}",aOutField, aTable, aWhere);
            return GetValue(cmdText, aConStr);
        }
        /// <summary>
        /// 根据给定字段的值从一个表中查询某个字段的值
        /// </summary>
        /// <param name="aOutField"></param>
        /// <param name="aTable"></param>
        /// <param name="aQueryField"></param>
        /// <param name="aQueryValue"></param>
        /// <param name="aConStr"></param>
        /// <returns></returns>
        public static Object GetValue(string aOutField, string aTable,string aQueryField, string aQueryValue, string aConStr)
        {
            string aWhere = string.Format("{0}='{1}'", aQueryField, aQueryValue);
            return GetValue(aOutField, aTable, aWhere, aConStr);
        }
        #endregion

        #region ExecuteCmd 
        /// <summary>
        /// <para>执行SQL语句或存储过程，返回影响的行数。</para>
        /// <para>可以使用ArrayList arrLstParams传入参数，参数可以是输入型也可以是输出型，</para>
        /// <para>调用者可使用arrLstParams的实参检查输出型参数的返回值。</para>
        /// <para>如果是存储过程，则需要设置isStProc为true。</para>
        /// </summary>
        /// <param name="aSQLCmd"></param>
        /// <param name="aConStr"></param>
        /// <param name="arrLstParams"></param>
        /// <param name="isStProc"></param>
        public static int ExecuteCmd(string aSQLCmd, string aConStr, ArrayList arrLstParams = null, bool isStProc = false)
        {
            int rows = 0;
            using (SqlConnection conn = new SqlConnection(aConStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(aSQLCmd, conn);
                if (arrLstParams != null) //如果是null不能AddRange
                {
                    cmd.Parameters.AddRange(arrLstParams.ToArray(typeof(SqlParameter)));//ArrayList提供了一个将List转为数组的一个非常方便的方法toArray
                }
                if (isStProc)
                    cmd.CommandType = CommandType.StoredProcedure;
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

        //自己写的数据库操作
        public static int Update(string aSqlcmd, string aConstr)//asqlcmd 连接数据库  aconstr 对数据库操作语句
        {
            int rows = 0;
            using (SqlConnection conn = new SqlConnection(aConstr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(aSqlcmd, conn);
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
        public static int Inter(string aSqlcmd,string aConstr)//asqlcmd 连接数据库  aconstr 对数据库操作语句
        {
            int rows = 0;
            using (SqlConnection conn = new SqlConnection(aConstr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(aSqlcmd, conn);
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
        public static int SetValue1(string aItemDm, object aItemValue, string aConStr)
        {
            string aSqlcmd = "INSERT INTO [dbo].[tlogin] ([loginname],[password])VALUES('"+ aItemDm+ "','"+aItemValue+"')";
            return Inter(aSqlcmd ,aConStr);
        }
        public static int SetConfigItem1(string aItemDm, object aItemValue, string aConStr)
        {
            return SetValue1( aItemDm, aItemValue.ToString(), aConStr);
        }

        public static int ExecuteCmd(string aSQLCmd, string aConStr, SqlParameter[] pra)
        {
            int rows = 0;
            using (SqlConnection conn = new SqlConnection(aConStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(aSQLCmd, conn);
                if (pra != null) //如果是null不能AddRange
                {
                    cmd.Parameters.AddRange(pra);
                }

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
                
                    conn.Dispose();
                }
            }
            return rows;
        }
        #endregion

        #region GetStr
        public static string GetStr(string aSQLCmd, string aConStr, ArrayList arrLstParams = null, bool isStProc = false)
        {
            Object o = GetValue(aSQLCmd, aConStr, arrLstParams, isStProc);
            return Convert.ToString(o);
        }
        public static string GetStr(string aOutField, string aTable, string aWhere, string aConStr)
        {
            Object o = GetValue(aOutField, aTable, aWhere, aConStr);
            return Convert.ToString(o);
        }
        public static string GetStr(string aOutField, string aTable, string aQueryField, string aQueryValue, string aConStr)
        {
            Object o = GetValue(aOutField, aTable, aQueryField, aQueryValue, aConStr);
            return Convert.ToString(o);
        }
        #endregion

        #region GetInt
        public static int GetInt(string aOutField, string aTable, string aQueryField, string aQueryValue, string aConStr)
        {
            Object o = GetStr(aOutField, aTable, aQueryField, aQueryValue, aConStr);
            return Convert.ToInt32(o);
        }
        #endregion

        #region SetValue
        /// <summary>
        /// <para>将指定数据表tbn中键字段名为keyFldn值为keyFldv的记录的</para>
        /// <para>名为valueFldn的字段的值置为valueFldv。</para>
        /// </summary>
        /// <param name="tbn"></param>
        /// <param name="keyFldn">串类型和整型都可工作</param>
        /// <param name="keyFldv"></param>
        /// <param name="valueFldn">串类型和整型都可工作</param>
        /// <param name="valueFldv"></param>
        /// <param name="acntStr"></param>
        public static int SetValue(string tbn, string keyFldn, string keyFldv, string valueFldn, object valueFldv, string acntStr)
        {
            return ExecuteCmd(string.Format("update {0} set {1}='{2}' where {3} = '{4}'",tbn, valueFldn, valueFldv.ToString(), keyFldn, keyFldv), acntStr);
        }
        #endregion
       
        #region tconfig表的读写操作
        public static string GetConfigStr(string aItemDm, string aConStr)
        {
            return GetStr("nr", "tconfig", "dm", aItemDm, aConStr);
        }
        public static int GetConfigInt(string aItemDm, string aConStr)
        {
            return Convert.ToInt32(GetConfigStr(aItemDm, aConStr));
        }
        public static int SetConfigItem(string aItemDm, object aItemValue, string aConStr)
        {
           return  SetValue("tconfig", "dm", aItemDm, "nr", aItemValue.ToString(), aConStr);
        }
        #endregion

        #region 事务进行insert存储
        /// <summary>
        /// 使用事务进行insert存储
        /// </summary>
        /// <param name="cmdStr1"></param>
        /// <param name="ConStr"></param>
        public static object InsertAffairs(string cmdStr1, string ConStr)
        {
            object ret = null;
            using (SqlConnection conn = new SqlConnection(ConStr))
            {

                conn.Open();
                SqlTransaction AffairsBegin = conn.BeginTransaction();
                SqlCommand cmd = new SqlCommand(cmdStr1, conn);
                cmd.Transaction = AffairsBegin;
                try
                {
                    ret = cmd.ExecuteNonQuery();
                    AffairsBegin.Commit();
                }
                catch (Exception ex1)
                {
                    try
                    {
                        AffairsBegin.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        throw ex2;
                    }
                    throw ex1;
                }
                finally
                {
                }
            }
            return ret;
        }
        #endregion

        #region getValue

        public static Object GetValue(string aSQLCmd, string aConStr, SqlParameter para, ArrayList arrLstParams = null, bool isStProc = false)
        {
            Object ret = null;
            using (SqlConnection conn = new SqlConnection(aConStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(aSQLCmd, conn);
                cmd.Parameters.Add(para);
                if (arrLstParams != null) //如果是null不能AddRange
                {
                    //cmd.Parameters.AddRange向SqlParameterCollection的末尾添加值数组，
                    //arrLstParams.ToArray将arraylist的元素复制到一个指定元素类型的新数组
                    cmd.Parameters.AddRange(arrLstParams.ToArray(typeof(SqlParameter)));
                }
                if (isStProc)
                    cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    ret = cmd.ExecuteScalar();
                }
                catch 
                {
                 
                }
                finally
                {
                    conn.Close();
                }
            }
            return ret;
        }
        #endregion

        //public static DataTable 返回查询结果(SqlCommand _Cmd)
        //{
        //    DataTable _table = new DataTable();
        //    return _table;
        //}

        //public static DataTable 返回查询结果(SqlCommand _Cmd, SqlParameter _Parameter)
        //{
        //    DataTable _table = new DataTable();
        //    return _table;
        //}

        public static DataTable 返回查询结果(SqlCommand _Cmd, SqlParameter[] _Parameter)
        {
            DataTable _table = new DataTable();
            using (SqlConnection conn = new SqlConnection(ClsDBCon.ConStrKj))
            {
                conn.Open();
                _Cmd.Connection = conn;
                _Cmd.Parameters.AddRange(_Parameter);
                SqlDataAdapter da = new SqlDataAdapter(_Cmd);
                try
                {
                    da.Fill(_table);
                }
                catch
                {
                }
                finally
                {
                    conn.Close();
                }
            }
            return _table;
        }
    }
}
