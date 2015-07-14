using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuaBo.Gis.Interfaces
{
    public enum OperateType
    {
        //选择和平移功能
        Pan = 1,
        //平移功能
        Pan2 = 2,
        //选择
        Select = 3,
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
