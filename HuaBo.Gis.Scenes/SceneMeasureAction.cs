using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuaBo.Gis.Desktop;

namespace HuaBo.Gis.Scenes
{
    public abstract class SceneMeasureAction : CtrlAction
    {
        public Unit DistanceUnit = Unit.Meter;
        public Unit AreaUnit = Unit.Meter;
        public string TrackerLayerTag = "Measure";
    }

    public enum Unit
    {
        Meter = 1,
        KiloMeter = 1000
    }


}
