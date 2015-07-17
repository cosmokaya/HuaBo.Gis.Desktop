using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuaBo.Gis.Desktop;
using HuaBo.Gis.Interfaces;

namespace HuaBo.Gis.Plugins
{
    /// <summary>
    /// 创建空白的窗体
    /// </summary>
    [Export(typeof(CtrlAction))]
    public class NewSceneAction : CtrlAction
    {

        public override void Run()
        {
            GisApp.ActiveApp.CreateFormScene();
        }
    }
}
