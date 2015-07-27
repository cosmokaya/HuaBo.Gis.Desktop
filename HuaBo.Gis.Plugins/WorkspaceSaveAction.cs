using HuaBo.Gis.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuaBo.Gis.Desktop;
using System.Windows.Forms;

namespace HuaBo.Gis.Plugins
{
    [Export(typeof(CtrlAction))]
    public class WorkspaceSaveAction : CtrlAction
    {
        public override void Run()
        {
            bool result = GisApp.ActiveApp.Workspace.Save();
            if (result == false)
            {
                GisApp.ActiveApp.Output.Warning("保存失败");
            }
        }

        public override bool Enable()
        {
            return GisApp.ActiveApp.Workspace.IsModified;
        }


    }
}
