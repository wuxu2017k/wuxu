using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;

namespace DLTLib.Classes
{
    public class ClsJSON
    {
        #region
        /// <summary>
        ///将DataTable转换为Json
        /// </summary>
        public static string DataTableToJSON(DataTable dt)//将DataTable转换为Json
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);//JavaScriptSerializer.Serialize将对象转换为JSON
        }
        /// <summary>
        ///类转换成JSON串
        /// </summary>
        //public static string ObjectToJson(object ob)//类转换成JSON串
        //{
        //    JavaScriptSerializer jss = new JavaScriptSerializer();
        //    return jss.Serialize(ob);

        //}
        /// <summary>
        ///将字符串转换成类
        /// </summary>
        //public static T FromJSON<T>(string input)//将字符串转换成类
        //{

        //    return JsonConvert.DeserializeObject<T>(input);


        //}

        /// <summary>
        ///将DataSet转换为Json
        /// </summary>
        //public static string TOJson(DataSet ds)//将DataSet转换为Json
        //{
        //    string jsonString = "{";
        //    foreach (DataTable table in ds.Tables)
        //    {
        //        jsonString += "\"" + table.TableName + "\":" + DataTableToJSON(table) + ",";
        //    }
        //    jsonString = jsonString.TrimEnd(',');
        //    return jsonString + "}";
        //}

        /// <summary>
        ///将JSON转换成字典Dictionary
        /// </summary>
        //public static Dictionary<string, string> JsonToDictionary(string jsonData)//将JSON转换成字典Dictionary
        //{
        //    //实例化JavaScriptSerializer类的新实例
        //    JavaScriptSerializer jss = new JavaScriptSerializer();
        //    try
        //    {

        //        //将指定的 JSON 字符串转换为 Dictionary<string, object> 类型的对象
        //        return jss.Deserialize<Dictionary<string, string>>(jsonData);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}
        /// <summary>
        ///将字典Dictionary转换为Json
        /// </summary>
        //public static string DictionaryToJson(Dictionary<string, string> dic)//将字典Dictionary转换为Json
        //{
        //    //实例化JavaScriptSerializer类的新实例
        //    JavaScriptSerializer jss = new JavaScriptSerializer();
        //    try
        //    {
        //        //将指定的 JSON 字符串转换为 Dictionary<string, object> 类型的对象
        //        return jss.Serialize(dic);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}
        /// <summary>
        ///将json转换成DataTable
        /// </summary>

        //public static DataTable DeserializeObject(string json)//将json转换成DataTable
        //{
        //    DataTable dt = new DataTable();

        //    try
        //    {
        //        JavaScriptSerializer jssl = new JavaScriptSerializer();
        //        jssl.MaxJsonLength = Int32.MaxValue;

        //        ArrayList arrayList = jssl.Deserialize<ArrayList>(json);
        //        if (arrayList.Count > 0)
        //        {
        //            foreach (Dictionary<string, object> dictionary in arrayList)
        //            {
        //                if (dictionary.Keys.Count<string>() == 0)
        //                {
        //                    return dt;
        //                }
        //                if (dt.Columns.Count == 0)
        //                {
        //                    foreach (string current in dictionary.Keys)
        //                    {
        //                        dt.Columns.Add(current, dictionary[current].GetType());
        //                    }
        //                }
        //                DataRow dataRow = dt.NewRow();
        //                foreach (string current in dictionary.Keys)
        //                {
        //                    dataRow[current] = dictionary[current];
        //                }
        //                dt.Rows.Add(dataRow);
        //            }

        //        }
        //    }
        //    catch
        //    {
        //    }
        //    return dt;

        //}
        #endregion
    }
}
