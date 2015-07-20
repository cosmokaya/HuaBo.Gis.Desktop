using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuaBo.Gis.Desktop;

namespace HuaBo.Gis.Plugins
{
    [Export(typeof(CtrlAction))]
    public class ClipRasterAction : CtrlAction
    {
        public override void Run()
        {
            FormRasterClip frc = new FormRasterClip(GisApp.ActiveApp.Workspace);
            frc.ShowDialog();
        }

    }
}
