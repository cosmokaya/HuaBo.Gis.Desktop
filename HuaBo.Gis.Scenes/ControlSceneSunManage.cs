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
    public partial class ControlSceneSunManage : ControlBase
    {
        private Dictionary<IForm, ControlSunManage> m_controlSunManagers = null;
        public ControlSceneSunManage()
        {
            InitializeComponent();
            m_controlSunManagers = new Dictionary<IForm, ControlSunManage>();
        }

        protected override void View_DocumentActivated(object sender, DevExpress.XtraBars.Docking2010.Views.DocumentEventArgs e)
        {
        }

        protected override void View_DocumentRemoved(object sender, DevExpress.XtraBars.Docking2010.Views.DocumentEventArgs e)
        {
        }

        protected override void ControlBase_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                IForm form = GisApp.ActiveApp.FormMain.ActiveForm;
                this.Controls.Clear();
                if (form == null) return;
                if (form is IFormScene)
                {
                    if (!m_controlSunManagers.ContainsKey(form))
                    {
                        ControlSunManage controlSunManage = new ControlSunManage((form as IFormScene).SceneControl);
                        controlSunManage.Dock = DockStyle.Fill;
                        m_controlSunManagers.Add(form, controlSunManage);
                    }
                }
                this.Controls.Add(m_controlSunManagers[form]);
            }
        }


    }
}
