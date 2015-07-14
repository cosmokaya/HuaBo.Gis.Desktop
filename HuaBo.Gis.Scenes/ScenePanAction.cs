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
    class ScenePanAction : CtrlAction
    {
        private bool m_isRunning = false;
        private IFormScene m_formScene;
        public override void Run()
        {
            if (GisApp.ActiveApp.FormMain.DocumentManager.View.ActiveDocument != null)
            {
                m_formScene = GisApp.ActiveApp.FormMain.DocumentManager.View.ActiveDocument.Form as IFormScene;
            }
            if (!m_isRunning)
            {
                Register();
            }
        }

        private void Register()
        {
            m_isRunning = true;
            m_formScene.OperateChanged += m_formScene_OperateChanged;
            m_formScene.SceneControl.ObjectSelected += SceneControl_ObjectSelected;
            m_formScene.OperateType = OperateType.Pan;
        }

        private void UnRegister()
        {
            m_isRunning = false;
            m_formScene.OperateChanged -= m_formScene_OperateChanged;
            m_formScene.SceneControl.ObjectSelected -= SceneControl_ObjectSelected;
        }

        void SceneControl_ObjectSelected(object sender, SuperMap.UI.ObjectSelectedEventArgs e)
        {

        }

        void m_formScene_OperateChanged(object sender, OperateChangedEventArgs e)
        {
            if (e.NewOperateType == OperateType.Pan)
            {
                m_formScene.SceneControl.Action = Action3D.Pan;
            }
            else
            {
                UnRegister();
            }
        }


        public override CheckState Check()
        {
            if (m_formScene != null && m_formScene.OperateType == OperateType.Pan)
            {
                return CheckState.Checked;
            }
            return CheckState.Unchecked;
        }
    }
}