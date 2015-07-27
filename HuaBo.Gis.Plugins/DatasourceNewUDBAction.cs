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
    /// 新建文件型（UDB）数据源
    /// </summary>
    [Export(typeof(CtrlAction))]
    public class DatasourceNewUDBAction : CtrlAction
    {
        public override void Run()
        {
            System.Windows.Forms.MessageBox.Show("DatasourceNewUDBAction未实现！");
        }
    }

}
