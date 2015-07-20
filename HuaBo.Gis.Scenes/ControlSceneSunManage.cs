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
    public partial class ControlSceneSunManage : DevExpress.XtraEditors.XtraUserControl
    {
        private Dictionary<IForm, ControlSunManage> m_controlSunManagers = null;
        public ControlSceneSunManage()
        {
            InitializeComponent();
            m_controlSunManagers = new Dictionary<IForm, ControlSunManage>();

            GisApp.ActiveApp.FormMain.DocumentManager.View.DocumentActivated += View_DocumentActivated;
            GisApp.ActiveApp.FormMain.DocumentManager.View.DocumentRemoved += View_DocumentRemoved;
            this.VisibleChanged += ControlSceneSunManage_VisibleChanged;
        }

        void View_DocumentRemoved(object sender, DevExpress.XtraBars.Docking2010.Views.DocumentEventArgs e)
        {

        }

        void View_DocumentActivated(object sender, DevExpress.XtraBars.Docking2010.Views.DocumentEventArgs e)
        {

        }

        void ControlSceneSunManage_VisibleChanged(object sender, EventArgs e)
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
