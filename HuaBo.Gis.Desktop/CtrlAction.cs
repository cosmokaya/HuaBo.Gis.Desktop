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
    public abstract class CtrlAction
    {
        public CtrlAction() { }

        //public CtrlAction(BarItem barItem, IForm form)
        //{
        //    BarItem = barItem;
        //    Form = form;
        //}

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


    }
}
