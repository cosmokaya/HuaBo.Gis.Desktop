using HuaBo.Gis.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuaBo.Gis.Interfaces
{
    public class OperateChangedEventArgs : EventArgs
    {
        public OperateType NewOperateType;
        public OperateType OldOperateType;

        public OperateChangedEventArgs(OperateType oldOperateType, OperateType newOperateType)
        {
            NewOperateType = newOperateType;
            OldOperateType = oldOperateType;
        }
    }
}
