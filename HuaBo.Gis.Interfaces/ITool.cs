using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperMap.UI;

namespace HuaBo.Gis.Interfaces
{
    public interface ITool
    {
        /// <summary>
        /// 主要用来监测多次点击的工具是否已经开启了点击，对点击一次的工具无用
        /// 1.点击之后为true2.结束或者未开始为false
        /// </summary>
        bool IsRunning { get; }
        /// <summary>
        /// 
        /// </summary>
        Action3D Action { get; }
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
