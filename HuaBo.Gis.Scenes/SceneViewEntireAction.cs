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
    public class SceneViewEntireAction : CtrlAction
    {
        public override void Run()
        {
            Form form = GisApp.ActiveApp.FormMain.ActiveForm as Form;
            if (form != null)
            {
                IFormScene formScene = form as IFormScene;
                if (formScene != null)
                {
                    formScene.SceneControl.Scene.ViewEntire();
                }
            }
        }
    }
}
