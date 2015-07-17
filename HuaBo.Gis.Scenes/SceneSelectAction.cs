using HuaBo.Gis.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HuaBo.Gis.Desktop;
using SuperMap.Data;
using SuperMap.Realspace;
using SuperMap.UI;
using DevExpress.XtraBars;

namespace HuaBo.Gis.Scenes
{
    [Export(typeof(CtrlAction))]
    public class SceneSelectAction : CtrlAction, ITool
    {
        public bool m_isRunning = false;
        public override void Run()
        {
            (Form as IFormScene).CurrentTool = this;
        }

        public void RegisterEvent()
        {
            m_isRunning = true;
            (Form as IFormScene).SceneControl.MouseClick += SceneControl_MouseClick;
            (Form as IFormScene).SceneControl.ObjectSelected += SceneControl_ObjectSelected;
            (Form as IFormScene).SceneControl.Action = Action3D.Select;
        }


        public void UnRegisterEvent()
        {
            m_isRunning = false;
            (Form as IFormScene).SceneControl.MouseClick -= SceneControl_MouseClick;
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



        void SceneControl_ObjectSelected(object sender, SuperMap.UI.ObjectSelectedEventArgs e)
        {

        }



        void SceneControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                (GisApp.ActiveApp.FormMain.ActiveForm as IFormScene).CurrentTool = null;
            }
        }
    }
}
