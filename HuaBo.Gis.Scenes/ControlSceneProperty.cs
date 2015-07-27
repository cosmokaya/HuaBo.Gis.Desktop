using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HuaBo.Gis.Interfaces;
using HuaBo.Gis.Desktop;
using System.ComponentModel.Composition;

namespace HuaBo.Gis.Scenes
{
    [Export(typeof(XtraUserControl))]
    public partial class ControlSceneProperty : ControlBase
    {
        private Dictionary<IForm, ControlProperty> m_scenePropertys = null;
        public ControlSceneProperty()
        {
            InitializeComponent();
            m_scenePropertys = new Dictionary<IForm, ControlProperty>();
        }

        protected override void View_DocumentActivated(object sender, DevExpress.XtraBars.Docking2010.Views.DocumentEventArgs e)
        {
            IForm form = e.Document.Form as IForm;
            this.Controls.Clear();
            if (form is IFormScene)
            {
                if (!m_scenePropertys.ContainsKey(form))
                {
                    ControlProperty layer3dsTree = new ControlProperty((form as IFormScene).SceneControl);
                    layer3dsTree.Dock = DockStyle.Fill;
                    m_scenePropertys.Add(form, layer3dsTree);
                }
                this.Controls.Add(m_scenePropertys[form]);
            }
        }

        protected override void View_DocumentRemoved(object sender, DevExpress.XtraBars.Docking2010.Views.DocumentEventArgs e)
        {
            IForm form = e.Document.Form as IForm;
            this.Controls.Clear();
            if (form != null && m_scenePropertys.ContainsKey(form))
            {
                m_scenePropertys.Remove(form);
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
                    if (!m_scenePropertys.ContainsKey(form))
                    {
                        ControlProperty layer3dsTree = new ControlProperty((form as IFormScene).SceneControl);
                        layer3dsTree.Dock = DockStyle.Fill;
                        m_scenePropertys.Add(form, layer3dsTree);
                    }
                }
                this.Controls.Add(m_scenePropertys[form]);
            }
        }
    }
}
