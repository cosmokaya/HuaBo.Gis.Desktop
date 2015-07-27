using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HuaBo.Gis.Desktop
{
    public abstract class FormControlEvents
    {
        public FormControlEvents()
        {
        }

        public virtual void FormEventControl_MaximumSizeChanged(object sender, EventArgs e)
        {
        }

        public virtual void ActiveApp_Refreshed(object sender, EventArgs e)
        {
        }
    }
}
