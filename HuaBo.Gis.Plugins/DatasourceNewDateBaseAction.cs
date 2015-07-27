using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuaBo.Gis.Desktop;

namespace HuaBo.Gis.Plugins
{
    /// <summary>
    /// 新建数据库型数据源。
    /// </summary>
    [Export(typeof(CtrlAction))]
    public class DatasourceNewDateBaseAction : CtrlAction
    {
        public override void Run()
        {
            System.Windows.Forms.MessageBox.Show("DatasourceNewDateBase未实现！");
        }
    }
}
