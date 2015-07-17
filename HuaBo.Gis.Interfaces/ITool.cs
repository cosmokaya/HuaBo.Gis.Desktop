using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuaBo.Gis.Interfaces
{
    public interface ITool
    {
        /// <summary>
        /// 是否正在运行
        /// </summary>
        //bool IsRunning { get; set; }
        /// <summary>
        /// 注册事件
        /// </summary>
        void RegisterEvent();
        /// <summary>
        /// 取消注册事件
        /// </summary>
        void UnRegisterEvent();
    }
}
