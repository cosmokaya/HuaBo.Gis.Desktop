using System;
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
    public class ScenePan2Action : CtrlAction
    {
        private IFormScene m_formScene;
        public override void Run()
        {
            if (Form != null)
            {
                (Form as IFormScene).CurrentTool = null;
            }
        }

        public override CheckState Check()
        {
            if (Form != null && (Form as IFormScene).CurrentTool == null)
            {
                return CheckState.Checked;
            }
            return CheckState.Unchecked;
        }
    }
}
