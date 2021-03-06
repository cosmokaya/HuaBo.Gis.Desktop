﻿using System;
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
    public class ScenePanAction : CtrlAction, ITool
    {
        public bool IsRunning
        {
            get { return m_isRunning; }
        }

        public Action3D Action
        {
            get { return Action3D.Pan; }
        }

        public bool m_isRunning = false;
        public override void Run()
        {
            (Form as IFormScene).CurrentTool = this;
        }

        public void RegisterEvent()
        {
            (Form as IFormScene).SceneControl.ObjectSelected += SceneControl_ObjectSelected;
        }

        void SceneControl_ObjectSelected(object sender, ObjectSelectedEventArgs e)
        {

        }

        public void UnRegisterEvent()
        {
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
