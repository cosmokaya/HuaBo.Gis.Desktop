using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars;

namespace HuaBo.Gis.Interfaces
{
    public interface IFormMain : IForm
    {
        RibbonControl RibbonView { get; set; }

        DevExpress.XtraBars.Docking2010.DocumentManager DocumentManager { get; }

        DevExpress.XtraBars.Docking.DockManager DockManager { get; }

        //Dictionary<string, PopupMenu> PopupMenus { get; set; }

        //为了方便，不然太长了
        IForm ActiveForm { get; }


    }
}
