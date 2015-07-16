using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using HuaBo.Gis.Desktop;
using HuaBo.Gis.Interfaces;

namespace HuaBo.Gis.Scenes
{

    [Export(typeof(CtrlAction))]
    public class SceneSunVisibleAction : CtrlAction
    {
        public override void Run()
        {
            Form form = GisApp.ActiveApp.FormMain.ActiveForm as Form;
            if (form != null)
            {
                IFormScene formScene = form as IFormScene;
                if (formScene != null)
                {
                    BarButtonItem item = BarItem as BarButtonItem;
                    if (item.Down)
                    {
                        formScene.SceneControl.Scene.Sun.IsVisible = true;
                    }
                    else
                    {
                        formScene.SceneControl.Scene.Sun.IsVisible = false;
                    }

                }
            }
        }

        public override CheckState Check()
        {
            Form form = GisApp.ActiveApp.FormMain.ActiveForm as Form;
            if (form != null)
            {
                IFormScene formScene = form as IFormScene;
                if (formScene != null)
                {
                    if (formScene.SceneControl.Scene.Sun.IsVisible)
                        return CheckState.Checked;
                }
            }
            return CheckState.Unchecked;
        }
    }
}
