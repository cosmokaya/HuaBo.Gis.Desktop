using HuaBo.Gis.Desktop;
using HuaBo.Gis.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperMap.Realspace;

namespace HuaBo.Gis.Scenes
{
    [Export(typeof(CtrlAction))]
    public class SceneMeasureClearAction : SceneMeasureAction
    {
        public override void Run()
        {
            if (this.Form != null && (this.Form as IFormScene) != null)
            {
                RemoveTrackinglayer(this.TrackerLayerTag, (this.Form as IFormScene).SceneControl.Scene);
            }
        }

        /// <summary>
        /// 移除制定的跟踪图层
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="scene"></param>
        private void RemoveTrackinglayer(string tag, Scene scene)
        {
            try
            {
                int index = scene.TrackingLayer.IndexOf(tag);
                while (index != -1)
                {
                    scene.TrackingLayer.Remove(index);
                    index = scene.TrackingLayer.IndexOf(tag);
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
