using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuaBo.Gis.Interfaces;
using System.Windows.Forms;
using HuaBo.Gis.Desktop;

namespace HuaBo.Gis.Scenes
{
    [Export(typeof(CtrlAction))]
    public class SceneRefreshAction : CtrlAction
    {
        public override void Run()
        {
            Form form = GisApp.ActiveApp.FormMain.ActiveForm as Form;
            if (form != null)
            {
                IFormScene formScene = form as IFormScene;
                if (formScene != null)
                {
                    formScene.SceneControl.Refresh();
                    GisApp.ActiveApp.Output.Warning("刷新完成!");
                }
            }
        }
    }
}
