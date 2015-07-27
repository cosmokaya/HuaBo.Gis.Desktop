using DevExpress.XtraBars.Docking;
using HuaBo.Gis.Desktop;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HuaBo.Gis.Scenes
{
    [Export(typeof(CtrlAction))]
    public class ScenePropertyAction : CtrlAction
    {
        public override void Run()
        {
            DockPanel dockPanel = GisApp.ActiveApp.FormMain.DockManager[typeof(ControlSceneProperty) + ""];

            if (dockPanel != null)
            {
                if (dockPanel.Visibility == DockVisibility.Hidden)
                {
                    dockPanel.Visibility = DockVisibility.Visible;
                }
                else
                {
                    dockPanel.Visibility = DockVisibility.Hidden;
                }
            }
        }

        public override CheckState Check()
        {
            DockPanel dockPanel = GisApp.ActiveApp.FormMain.DockManager[typeof(ControlSceneProperty) + ""];
            if (dockPanel != null)
            {
                if (dockPanel.Visibility != DockVisibility.Hidden)
                {
                    return CheckState.Checked;
                }
            }
            return CheckState.Unchecked;
        }
    }
}
