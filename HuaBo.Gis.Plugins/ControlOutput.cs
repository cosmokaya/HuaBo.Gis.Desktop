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

namespace HuaBo.Gis.Plugins
{
    [Export(typeof(XtraUserControl))]
    public partial class ControlOutput : DevExpress.XtraEditors.XtraUserControl
    {
        public ControlOutput()
        {
            InitializeComponent();

            GisApp.ActiveApp.Output.DoingOutput += Output_DoingOutput;
        }

        void Output_DoingOutput(object sender, EventArgs e)
        {
            string result = sender.ToString();

            m_textEdit.Text += string.Format("[{0}] {1}{2}", DateTime.Now.ToShortTimeString(), result, "\r\n");

        }
    }
}
