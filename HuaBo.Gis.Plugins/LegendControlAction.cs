using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars.Docking;
using HuaBo.Gis.Interfaces;
using HuaBo.Gis.Desktop;

namespace HuaBo.Gis.Plugins
{
    [Export(typeof(CtrlAction))]
    public class LegendControlAction : CtrlAction
    {
        public override void Run()
        {
            DockPanel dockPanel = GisApp.ActiveApp.FormMain.DockManager[typeof(ControlLayerManager) + ""];

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
            DockPanel dockPanel = GisApp.ActiveApp.FormMain.DockManager[typeof(ControlLayerManager) + ""];
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
