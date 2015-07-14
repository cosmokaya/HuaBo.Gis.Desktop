using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars.Docking;
using SuperMap.UI;
using HuaBo.Gis.Interfaces;
using HuaBo.Gis.Desktop;
using DevExpress.XtraBars;

namespace HuaBo.Gis.Plugins
{
    [Export(typeof(CtrlAction))]
    public class WorkspaceManagerAction : CtrlAction
    {

        public override void Run()
        {
            BarCheckItem item = this.BarItem as BarCheckItem;
            if (item != null)
            {
                DockPanel dockPanel = GisApp.ActiveApp.FormMain.DockManager[typeof(ControlWorkspaceTree) + ""];
                if (item.Checked)
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
            DockPanel dockPanel = GisApp.ActiveApp.FormMain.DockManager[typeof(ControlWorkspaceTree) + ""];
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
