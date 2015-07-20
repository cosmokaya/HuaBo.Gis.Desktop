using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraBars;

namespace HuaBo.Gis.Scenes
{
    public class SceneDistanceUnitAction : SceneMeasureAction
    {
        public override void Run()
        {
            //这样做不对
            //BarEditItem item = BarItem as BarEditItem;
            //if (item != null)
            //{
            //    if (item.Caption == "米")
            //    {
            //        base.DistanceUnit = Unit.Meter;
            //    }
            //    else if (item.Caption == "千米")
            //    {
            //        this.DistanceUnit = Unit.KiloMeter;
            //    }
            //}
        }
    }
}
