using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HuaBo.Gis.Desktop
{
    /// <summary>
    /// 只是用来减少随窗体变化的dockpanel的代码量
    /// </summary>
    public abstract partial class ControlBase : DevExpress.XtraEditors.XtraUserControl
    {
        public ControlBase()
        {
            InitializeComponent();

            GisApp.ActiveApp.FormMain.DocumentManager.View.DocumentActivated += View_DocumentActivated;
            GisApp.ActiveApp.FormMain.DocumentManager.View.DocumentRemoved += View_DocumentRemoved;
            this.VisibleChanged += ControlBase_VisibleChanged;
        }

        protected virtual void ControlBase_VisibleChanged(object sender, EventArgs e)
        {
            
        }

        protected virtual void View_DocumentRemoved(object sender, DevExpress.XtraBars.Docking2010.Views.DocumentEventArgs e)
        {
        }

        protected virtual void View_DocumentActivated(object sender, DevExpress.XtraBars.Docking2010.Views.DocumentEventArgs e)
        {
            
        }
    }
}
