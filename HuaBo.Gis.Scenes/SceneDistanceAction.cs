using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HuaBo.Gis.Desktop;
using HuaBo.Gis.Interfaces;
using SuperMap.UI;
using System.ComponentModel.Composition;

namespace HuaBo.Gis.Scenes
{
    //计算空间距离
    [Export(typeof(CtrlAction))]
    public class SceneDistanceAction : SceneMeasureAction, ITool
    {

        //
        private bool m_isRunning = false;
        public bool IsRunning
        {
            get { return m_isRunning; }
        }

        public SuperMap.UI.Action3D Action
        {
            get { return SuperMap.UI.Action3D.MeasureDistance; }
        }

        public void RegisterEvent()
        {
            (this.Form as IFormScene).SceneControl.Tracking += SceneControl_Tracking;
            (this.Form as IFormScene).SceneControl.Tracked += SceneControl_Tracked;
        }

        public void UnRegisterEvent()
        {

        }

        void SceneControl_Tracking(object sender, Tracking3DEventArgs e)
        {

        }
        void SceneControl_Tracked(object sender, Tracked3DEventArgs e)
        {

        }


        public override void Run()
        {
            (Form as IFormScene).CurrentTool = this;
        }

        public override System.Windows.Forms.CheckState Check()
        {
            if (Form != null && (Form as IFormScene).CurrentTool == this)
            {
                return CheckState.Checked;
            }
            return CheckState.Unchecked;
        }
    }
}
