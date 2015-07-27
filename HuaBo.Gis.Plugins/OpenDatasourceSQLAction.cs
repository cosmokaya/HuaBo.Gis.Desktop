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
    public class OpenDatasourceSQLAction : CtrlAction
    {
        public override void Run()
        {
            System.Windows.Forms.MessageBox.Show("OpenDatasourceSQLAction未实现！");
        }
    }
}
