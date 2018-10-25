using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTLib.Classes
{
    /// <summary>
    /// 抽象功能类，用于实现通用功能的调用机制。
    /// </summary>
    public abstract class ClsFuncs
    {
        public abstract void Call(int aId, string aBm, string aMc);
    }
}
