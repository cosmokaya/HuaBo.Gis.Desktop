using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HuaBo.Gis.Desktop;
using HuaBo.Gis.Interfaces;

namespace HuaBo.Gis.Scenes
{
    //高度测量
    [Export(typeof(CtrlAction))]
    public class SceneAltitudeAction : SceneMeasureAction, ITool
    {
        //
        private bool m_isRunning = false;
        public bool IsRunning
        {
            get { return m_isRunning; }
        }

        public SuperMap.UI.Action3D Action
        {
            get { return SuperMap.UI.Action3D.MeasureAltitude; }
        }

        public void RegisterEvent()
        {

        }

        public void UnRegisterEvent()
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
