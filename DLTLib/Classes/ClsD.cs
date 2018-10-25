using Gizmox.WebGUI.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTLib.Classes
{
    public class ClsD
    {
        #region TextBoxTrim 消除TextBox中首尾的空格
        /// <summary>
        /// 消除TextBox中首尾的空格
        /// </summary>
        /// <param name="ctrl"></param>
        public static void TextBoxTrim(Control ctrl)
        {
            foreach (Control c in ctrl.Controls)//遍历form上的每个控件元素，把每个当前控件装箱成 control实例，进行操作。百度搜的。
            {
                if (c is GroupBox)
                    TextBoxTrim(c);
                else if (c is TextBox)
                {
                    TextBox t = (TextBox)c;
                    t.Text = t.Text.Trim();
                }
            }
        }
        #endregion

        #region SetMaxLength 设置最大长度
        /// <summary>
        /// 为窗体上所有的绑定了system.string类型的数据表字段的textbox根据字段最大长度设置maxlength，
        /// 支持GroupBox的递归.
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="tbl"></param>
        public static void SetMaxLength(Control ctrl, DataTable tbl)
        {
            foreach (Control c in ctrl.Controls)//遍历页面控件
            {
                if (c is GroupBox)
                    SetMaxLength(c, tbl);
                else if (c is TextBox)
                {
                    TextBox t = (TextBox)c;
                    if (t.Enabled && !t.ReadOnly)
                        if (t.DataBindings["Text"] != null)//一方面是用于与数据库的数据进行绑定，进行数据显示。另一方面用于与控件或类的对象进行数据绑定
                        {
                            string fld = t.DataBindings["text"].BindingMemberInfo.BindingField;
                            if (String.Compare(tbl.Columns[fld].DataType.ToString(), "System.String", true) == 0)
                            {
                                t.MaxLength = tbl.Columns[fld].MaxLength;
                            }
                        }
                }
            }
        }
        #endregion

        #region TurnDgvToBdsCurrRecdgv 在新增记录时如果页数大于1则可以跳转到新增的记录的所在页
        /// <summary>
        /// 在新增记录时如果页数大于1则可以跳转到新增的记录的所在页
        /// </summary>
        /// <param name="dgv"></param>
        public static void TurnDgvToBdsCurrRec(DataGridView dgv)
        {
            BindingSource bds = (BindingSource)dgv.DataSource;
            /*
             *DataGridView是个控件
             *BindingSource组件是数据源和控件间的一座桥,同时提供了大量的API和Event供我们使用。使用这些API我们可以将Code与各种具体类型数据源进行解耦；使用这些Event我们可以洞察数据的变化。
             *数据源DataSource是JNDI资源的一种，很简单，就是将“DataSource”字符串名称与真正的DataSource对象绑定起来，方便获取。
             */
            if (bds.Position == -1)
                return;
            int page = (int)Math.Ceiling(bds.Position / (decimal)dgv.ItemsPerPage);
            dgv.CurrentPage = page;
            //将当前记录显示在可见的DataGridView区域内
            dgv.FirstDisplayedScrollingRowIndex = bds.Position;
        }
        #endregion
    }
}
