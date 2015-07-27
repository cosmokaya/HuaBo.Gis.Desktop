using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuaBo.Gis.Desktop
{
    /// <summary>
    /// 用来添加普通类的文件夹
    /// </summary>
    public class BitMapManager
    {
        public static Bitmap GetBitMap(string fileName)
        {
            Bitmap bitMap = null;
            try
            {

                bitMap = new Bitmap(fileName);
            }
            catch (Exception ex)
            {
                bitMap = null;
            }
            return bitMap;
        }
    }
}
