using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraBars;
using HuaBo.Gis.Desktop;

namespace HuaBo.Gis.Scenes
{
    [Export(typeof(CtrlAction))]
    public class SceneAreaUnitAction : SceneMeasureAction
    {
        public override void Run()
        {
            //BarEditItem item = BarItem as BarEditItem;
            //if (item != null)
            //{
            //    if (item.Caption == "平方米")
            //    {
            //        base.AreaUnit = Unit.Meter;
            //    }
            //    else if (item.Caption == "平方千米")
            //    {
            //        base.AreaUnit = Unit.KiloMeter;
            //    }
            //}
        }
    }
}
