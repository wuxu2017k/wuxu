using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Forms.Charts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace DLTLib.Classes
{
    public class ClsVWG
    {

        public static RadioButton GetCheckedRadioBtn(GroupBox grp)
        {
            RadioButton rb = null;
            foreach (Control c in grp.Controls)
            {
                if (c is RadioButton)
                {
                    rb = c as RadioButton;
                    if (rb.Checked)
                        break;
                }
            }
            return rb;
        }

        /// <summary>
        /// <para>用从数据库中选择的数据填充ComboBox, 例如：</para>
        /// <para>SELECT dm,mc FROM toptionxm WHERE lbdm = 'bmxz'</para>
        /// <para>SELECT dm,dm+':'+mc AS dmmc FROM toptionxm WHERE lbdm = 'bmxz'</para>
        /// <para>注意：第0列为选择获得的值，第1列为在ComboBox中下拉显示的内容</para>
        /// <para>第0列转换到c#只能是串类型(System.String)或整数类型</para>
        /// </summary>
        /// <param name="cmb"></param>
        /// <param name="aSelect"></param>
        /// <param name="aCntStr"></param>
        public static void FillCmbBySQL(ComboBox cmb, string aSelect, string aCntStr,bool insFirstLine = false, bool show2Cols = false)
        {
            DataTable dt = ClsMSSQL1.GetDataTable(aSelect, aCntStr);
            if (insFirstLine)
            {
                DataRow dr = dt.NewRow();
                if (dt.Columns[0].DataType == System.Type.GetType("System.String"))
                    dr[0] = "";
                //dr[0] = "!";
                else
                    dr[0] = -1;
                string s = dr[0].ToString();
                dt.Rows.InsertAt(dr, 0);
            }

            cmb.DataSource = dt;//dgv.DataSource = dt;实现在dgv中显示GetDataTable返回的数据表。

            if (dt.Columns.Count == 2)
                cmb.DisplayMember = dt.Columns[1].ToString();/*displaymember：前台显示的字段 ValueMember：后台取值的字段。*/
            else
                cmb.DisplayMember = dt.Columns[0].ToString();
            cmb.ValueMember = dt.Columns[0].ToString();
        }

        /// <summary>
        /// 用数据库的SELECT语句选择数据，将其放入DataSeries对象，以便进行Chart绘制。
        /// </summary>
        /// <param name="aSelect"></param>
        /// <param name="aCntStr"></param>
        /// <param name="xField"></param>
        /// <param name="yField"></param>
        /// <param name="isPerc">是否将数值转换为百分比</param>
        /// <returns></returns>
        public static DataSeries DataTableToDataSeries(string aSelect, string aCntStr,  string xField, string yField, bool isPerc = true, string tooltip = "", List<string> arr_dataSeriesLable = null)
        {
            DataTable dt = ClsMSSQL1.GetDataTable(aSelect, aCntStr);
            DataSeries ds = new DataSeries();
            double total = 0, v;
            int xValue = -1;
            foreach (DataRow r in dt.Rows)
            {
                v = Convert.ToDouble(r[yField]);
                total += v;
                if (arr_dataSeriesLable != null)
                {
                    xValue = arr_dataSeriesLable.IndexOf(r[xField].ToString());
                    if (xValue != -1)
                        ds.Add(r[xField].ToString(), v, xValue+1);
                    else
                        ds.Add(r[xField].ToString(), v);
                }
                else
                ds.Add(r[xField].ToString(), v);
                foreach (DataPoint p in ds)
                {
                    //p.ZValue = p.YValue;
                    p.ZValue = p.YValue / total * 100;
                }
            }
            if (isPerc)
                foreach (DataPoint p in ds)
                {
                    //p.YValue = p.YValue;
                    //p.AxisLabel =( p.YValue / total * 100).ToString ();
                    p.ZValue = p.YValue;
                    p.YValue = p.YValue / total * 100;//YValues属性用于设置数据点的 Y 值。
                }
            return ds;
        }


        /// <summary>
        /// 路径改为相对路径
        /// </summary>
        /// <param name="sURL"></param>
        /// <returns></returns>
        public static string ExpandURL(string sURL)
        {
            return System.IO.Path.Combine(HttpContext.Current.Request.ApplicationPath,sURL);
        }
        /// <summary>
        /// 火狐、IE转换方法
        /// </summary>
        /// <param name="bbURL"></param>
        /// <returns></returns>
        public static string ExpandURLByBrowsers(string bbURL)
        {
            string p = HttpContext.Current.Request.ApplicationPath;
            if (p.Equals("/") || ClsVWG.IsIE())
                return ClsVWG.ExpandURL(bbURL);
            else
                return bbURL;
        }
        public static bool IsIE()
        {
            string brVer = HttpContext.Current.Request.Browser.Type;
            return brVer.StartsWith("internetexplorer", StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
