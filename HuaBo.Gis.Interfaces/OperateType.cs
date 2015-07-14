using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuaBo.Gis.Interfaces
{
    public enum OperateType
    {
        //无
        Pan = 1,
        Select = 2,
        //测量距离
        Distance = 11,
        //测量面积
        Area = 12,
        //测量体积
        Volumn = 13,
        //测量角度
        Angle = 14,
        //高程距离
        TerrainDistance = 15,
        //测量高程
        Altitude = 16,
        //
        CreateLabel3D = 17
    }

}
