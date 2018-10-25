using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTLib.Classes
{
    public class ClsBhMc
    {
        public ClsBhMc(object abh, string amc)
        {
            Bh = abh;
            Mc = amc;
        }
        public object Bh { get; set; }
        public string Mc { get; set; }
        public string BhMc
        {
            get
            {
                return Bh + ":" + Mc;
            }
        }
    }
    public class ClsStu
    {
        public ClsStu(string aXh, string aXm, ClsBhMc aBj, ClsBhMc aZy)
        {
            Xh = aXh;
            Xm = aXm;
            Bj = aBj;
            Zy = aZy;
        }
        /// <summary>
        /// 学号
        /// </summary>
        public string Xh { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Xm { get; set; }
        /// <summary>
        /// 班级
        /// </summary>
        public ClsBhMc Bj { get; set; }
        /// <summary>
        /// 专业
        /// </summary>
        public ClsBhMc Zy { get; set; }
    }
    public class ClsBhMcByte : ClsBhMc
    {
        public ClsBhMcByte(byte abh, string amc) : base(abh, amc)
        {
        }
    }
    public class ClsBhMcString : ClsBhMc
    {
        public ClsBhMcString(string abh, string amc) : base(abh, amc)
        {
        }
    }
    public class ClsBhMcBoolean: ClsBhMc
    {
        public ClsBhMcBoolean(bool abh, string amc) : base(abh, amc)
        {
        }
    }
}
