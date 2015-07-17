using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuaBo.Gis.Interfaces
{
    public class ToolChangedEventArgs : EventArgs
    {
        public ITool OldTool;
        public ITool NewTool;

        public ToolChangedEventArgs(ITool oldTool, ITool newTool)
        {
            OldTool = oldTool;
            NewTool = newTool;
        }
    }
}
