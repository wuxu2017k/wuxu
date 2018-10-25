using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTLib.Classes
{
    public class ClsQ
    {
        #region Q1 
        /// <summary>
        /// 在字符串两端添加英文单引号
        /// </summary>
        public static string Q1(string s)
        {
            return Q0(s, '\'');
        }
        #endregion

        #region Q2
        /// <summary>
        /// <para>在字符串两端添加单引号、双引号、或括号，</para>
        /// <para>引号包括英文的和中文的。</para>
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string Q2(string s)
        {
            return Q0(s, '\"');
        }
        #endregion

        #region Q0
        public static string Q0(string s, char quoter)
        {
            char[] quoters = { '"', '\'', '(', '[', '‘', '“' };
            if (!quoters.Contains(quoter))
                return s;
            else
                switch (quoter)
                {
                    case '"':
                        return '"' + s + '"';
                    case '\'':
                        return '\'' + s + '\'';
                    case '(':
                        return '(' + s + ')';
                    case '[':
                        return '[' + s + ']';
                    case '‘':
                        return '‘' + s + '’';
                    case '“':
                        return '“' + s + '”';
                    default:
                        return s;
                }
        }
        #endregion
    }
}
