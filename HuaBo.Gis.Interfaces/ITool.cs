using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuaBo.Gis.Interfaces
{
    public interface ITool
    {
        bool IsRunning { get; set; }
        void RegisterEvent();
        void UnRegisterEvent();
    }
}
