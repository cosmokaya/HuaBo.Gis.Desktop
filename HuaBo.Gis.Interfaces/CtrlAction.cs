using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using SuperMap.Data;

namespace HuaBo.Gis.Interfaces
{
    public abstract class CtrlAction
    {
        public CtrlAction() { }

        //测试
        public BarItem BarItem { get; set; }
        public IForm Form { get; set; }

        public virtual CheckState Check()
        {
            return CheckState.Unchecked;
        }

        public virtual bool Enable()
        {
            return true;
        }

        public virtual void Run()
        { }

    }
}
