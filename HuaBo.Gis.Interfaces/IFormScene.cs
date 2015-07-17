using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperMap.UI;

namespace HuaBo.Gis.Interfaces
{
    public interface IFormScene : IForm
    {
        SceneControl SceneControl { get; set; }

        //OperateType OperateType { get; set; }
        ITool CurrentTool { get; set; }

        event EventHandler<ToolChangedEventArgs> ToolChanged;
    }
}
