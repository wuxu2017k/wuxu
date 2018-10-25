using Gizmox.WebGUI.Forms.Charts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTLib.Classes
{
 public class ClsVisifire
    {
        public static DataSeries DataTableToDataSeries(string aSelect,string aCntStr,string xField,string yField,bool isPerc=false)
        {
            DataTable dt = ClsMSSQL1.GetDataTable(aSelect, aCntStr);
            DataSeries ds = new DataSeries();/*DataSeries类包括了一个ArrayList数组PointList，它持有用于三维折线图的数据点集*/
            double total = 0, v;
            foreach (DataRow r in dt.Rows )
            {
                v = Convert.ToDouble(r[yField]);
                total += v;
                ds.Add(r[xField].ToString(), v);
            }
            if (isPerc)
                foreach (DataPoint p in ds)
                    p.YValue = p.YValue / total * 100;
            return ds;
        }
        public static DataSeries DataTableToDataSeries1(string aSelect, string aCntStr,string s1,string xField, string yField, DateTime t1, DateTime t2, bool isPerc = true)
        {
            DataTable dt = ClsMSSQL1.GetDataTable(aSelect, aCntStr);
            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            DataSeries ds = new DataSeries();
            double total = 0, v;
            foreach (DataRow r in dt.Rows)
            {
                v = Convert.ToDouble(r[yField]);
                total += v;
                //ds.Add(r[xField].ToString(), v,a);
                if (s1.Equals("yy"))
                    ds.Add(r[xField].ToString(), v, int.Parse(r[xField].ToString()) - t1.Year + 1);
                if (s1.Equals("yymm"))
                {
                    string y1 = r[xField].ToString().Substring(0, 4);
                    string m1 = r[xField].ToString().Substring(5, 2);
                    if (t1.Year == t2.Year)
                        ds.Add(r[xField].ToString(), v, int.Parse(m1) - t1.Month + 1);
                    else if (y1.Equals(t1.Year.ToString()))
                    {
                        ds.Add(r[xField].ToString(), v, int.Parse(m1) - t1.Month + 1);
                    }
                    else
                    {
                        ds.Add(r[xField].ToString(), v, int.Parse(m1) + (12 - t1.Month) + 1);
                    }

                }
                if (s1.Equals("yymmdd"))
                {
                    string d1 = r[xField].ToString().Substring(8, 2);
                    string m1 = r[xField].ToString().Substring(5, 2);
                    if (t1.Month == t2.Month)
                        ds.Add(r[xField].ToString(), v, int.Parse(d1) - t1.Day + 1);
                    else if (d1.Equals(t1.Day.ToString()))
                    {
                        ds.Add(r[xField].ToString(), v, int.Parse(d1) - t1.Day + 1);
                    }
                    else
                    {
                        ds.Add(r[xField].ToString(), v, int.Parse(d1) + (DateTime.DaysInMonth(t1.Year, t1.Month) - t1.Day) + 1);
                    }
                }
                foreach (DataPoint p in ds)
                {
                    p.ZValue = p.YValue;
                    p.ZValue = p.YValue / total * 100;
                }
            }

            if (isPerc)
                foreach (DataPoint p in ds)
                {
                    //p.YValue = p.YValue;
                    //p.AxisLabel =( p.YValue / total * 100).ToString ();
                    p.ZValue = p.YValue;
                    p.YValue = p.YValue / total * 100;

                }
            return ds;
        }
    }
}
