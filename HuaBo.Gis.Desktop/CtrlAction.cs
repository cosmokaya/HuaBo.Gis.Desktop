using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using HuaBo.Gis.Interfaces;
using SuperMap.Data;

namespace HuaBo.Gis.Desktop
{
    public abstract class CtrlAction : FormControlEvents
    {
        public CtrlAction()
            : base()
        {
        }

        //测试
        public BarItem BarItem { get; set; }
        /// <summary>
        /// 插件对应的窗体，为当前窗体
        /// </summary>
        public IForm Form
        {
            get
            { return GisApp.ActiveApp.FormMain.ActiveForm; }
        }

        public virtual CheckState Check()
        {
            return CheckState.Unchecked;
        }

        public virtual bool Enable()
        {
            return true;
        }

        public virtual void Run()
        { }

        /// <summary>
        /// 重写ComboEdit之类的编辑工具的刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ActiveApp_Refreshed(object sender, EventArgs e)
        {
            BarItem.Enabled = this.Enable();
            if ((BarItem as BarCheckItem) != null)
            {
                (BarItem as BarCheckItem).Checked = this.Check() == CheckState.Checked;
            }
            if ((BarItem as BarButtonItem) != null)
            {
                (BarItem as BarButtonItem).Down = this.Check() == CheckState.Checked;
            }
        }


    }
}
