using Gizmox.WebGUI.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTLib.Classes
{
    public class ClsMsgBox
    {
        /// <summary>
        /// 提示对话框
        /// </summary>
        /// <param name="mess"></param>
        public static void Ts(string mess)
        {
            MessageBox.Show(mess, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// 警告对话框
        /// </summary>
        /// <param name="mess"></param>
        public static void Jg(string mess)
        {
            MessageBox.Show(mess, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
           
        }
        /// <summary>
        /// 错误对话框
        /// </summary>
        /// <param name="mess"></param>
        public static void Cw(string mess)
        {
            MessageBox.Show(mess, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        /// <summary>
        /// YesNo询问对话框
        /// </summary>
        /// <param name="mess"></param>
        public static void YesNo(string mess, EventHandler hdl)
        {
            MessageBox.Show(mess, "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, hdl);
        }
        /// <summary>
        /// OKCancel询问对话框
        /// </summary>
        /// <param name="mess"></param>
        public static void OKCancel(string mess, EventHandler hdl)
        {
            MessageBox.Show(mess, "询问", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, hdl);
        }
        /// <summary>
        /// Exception错误信息对话框
        /// </summary>
        /// <param name="mess">附加的错误发生位置说明</param>
        public static void Cw(string mess, Exception ex)
        {
            Cw(mess + "\n错误类型：" + ex.GetType() + "\n错误信息：\n" + ex.Message);
        }
    }
}
