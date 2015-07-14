using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuaBo.Gis.Interfaces;
using HuaBo.Gis.Desktop;
using System.Windows.Forms;
using DevExpress.XtraBars.Docking2010.Views;

namespace HuaBo.Gis.Plugins
{
    [Export(typeof(CtrlAction))]
    public class FullScreenAction : CtrlAction
    {
        public override void Run()
        {

            MessageBox.Show("暂时有问题！FormScene全屏报错");
            if (GisApp.ActiveApp.FormMain.DocumentManager.View.ActiveDocument != null)
            {
                if (GisApp.ActiveApp.FormMain.DocumentManager.View.Type != ViewType.NativeMdi)
                {
                    GisApp.ActiveApp.FormMain.DocumentManager.View = GisApp.ActiveApp.FormMain.DocumentManager.CreateView(ViewType.NativeMdi);
                }
                Form form = GisApp.ActiveApp.FormMain.DocumentManager.View.ActiveDocument.Form;
                form.MdiParent = null;
                form.FormBorderStyle = FormBorderStyle.None;
                form.WindowState = FormWindowState.Maximized;
                form.KeyPreview = true;
                form.KeyDown += form_KeyDown;
            }

        }

        void form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Form form = sender as Form;
                form.WindowState = FormWindowState.Normal;
                form.KeyDown -= form_KeyDown;
                form.MdiParent = GisApp.ActiveApp.FormMain as Form;

            }
        }

        public override bool Enable()
        {
            bool result = false;
            if (GisApp.ActiveApp.FormMain.DocumentManager.View.ActiveDocument != null)
            {
                result = true;
            }
            return result;
        }
    }
}
