﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SuperMap.UI;
using HuaBo.Gis.Interfaces;
using HuaBo.Gis.Desktop;
using System.ComponentModel.Composition;

namespace HuaBo.Gis.Plugins
{
    [Export(typeof(XtraUserControl))]
    public partial class ControlLayerManager : ControlBase
    {
        private Dictionary<IForm, LayersControl> m_layer3DsTrees = null;
        public ControlLayerManager()
        {
            InitializeComponent();
            m_layer3DsTrees = new Dictionary<IForm, LayersControl>();
        }


        protected override void View_DocumentActivated(object sender, DevExpress.XtraBars.Docking2010.Views.DocumentEventArgs e)
        {
            IForm form = e.Document.Form as IForm;
            this.Controls.Clear();
            if (form is IFormScene)
            {
                if (!m_layer3DsTrees.ContainsKey(form))
                {
                    LayersControl layer3dsTree = new LayersControl((form as IFormScene).SceneControl.Scene);
                    layer3dsTree.Dock = DockStyle.Fill;
                    m_layer3DsTrees.Add(form, layer3dsTree);
                }
                this.Controls.Add(m_layer3DsTrees[form]);
            }
        }

        protected override void View_DocumentRemoved(object sender, DevExpress.XtraBars.Docking2010.Views.DocumentEventArgs e)
        {
            IForm form = e.Document.Form as IForm;
            this.Controls.Clear();
            if (form != null && m_layer3DsTrees.ContainsKey(form))
            {
                m_layer3DsTrees.Remove(form);
            }
        }

        protected override void ControlBase_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                IForm form = null;
                if (GisApp.ActiveApp.FormMain.DocumentManager.View.ActiveDocument != null)
                {
                    form = GisApp.ActiveApp.FormMain.DocumentManager.View.ActiveDocument.Form as IForm;
                }
                this.Controls.Clear();
                if (form == null) return;
                if (form is IFormScene)
                {
                    if (!m_layer3DsTrees.ContainsKey(form))
                    {
                        LayersControl layer3dsTree = new LayersControl((form as IFormScene).SceneControl.Scene);
                        layer3dsTree.Dock = DockStyle.Fill;
                        m_layer3DsTrees.Add(form, layer3dsTree);
                    }
                }
                this.Controls.Add(m_layer3DsTrees[form]);
            }
        }

        


        private void ControlLayerManager_Load(object sender, EventArgs e)
        {

        }
    }
}
