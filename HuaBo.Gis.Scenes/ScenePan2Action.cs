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
    class ScenePan2Action : CtrlAction
    {
        private IFormScene m_formScene;
        public override void Run()
        {
            if (GisApp.ActiveApp.FormMain.DocumentManager.View.ActiveDocument != null)
            {
                (GisApp.ActiveApp.FormMain.DocumentManager.View.ActiveDocument.Form as IFormScene).OperateType = OperateType.Pan2;
            }
        }

        public override CheckState Check()
        {
            if ((GisApp.ActiveApp.FormMain.DocumentManager.View.ActiveDocument.Form as IFormScene).OperateType == OperateType.Pan2)
            {
                return CheckState.Checked;
            }
            return CheckState.Unchecked;
        }
    }
}
