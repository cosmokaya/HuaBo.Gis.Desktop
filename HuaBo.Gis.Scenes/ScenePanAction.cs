using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HuaBo.Gis.Desktop;
using HuaBo.Gis.Interfaces;
using SuperMap.UI;

namespace HuaBo.Gis.Scenes
{

    [Export(typeof(CtrlAction))]
    class ScenePanAction : CtrlAction, ITool
    {

        public bool m_isRunning = false;
        public override void Run()
        {
            (Form as IFormScene).CurrentTool = this;
        }

        public void RegisterEvent()
        {
            m_isRunning = true;
            (Form as IFormScene).SceneControl.Action = Action3D.Pan;
            (Form as IFormScene).SceneControl.ObjectSelected += SceneControl_ObjectSelected;
        }

        void SceneControl_ObjectSelected(object sender, ObjectSelectedEventArgs e)
        {

        }

        public void UnRegisterEvent()
        {
            m_isRunning = false;
            (Form as IFormScene).SceneControl.ObjectSelected -= SceneControl_ObjectSelected;
        }




        public override CheckState Check()
        {
            if (Form != null && (Form as IFormScene).CurrentTool == this)
            {
                return CheckState.Checked;
            }
            return CheckState.Unchecked;
        }



    }
}
