using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Docking;
using SuperMap.UI;
using HuaBo.Gis.Interfaces;

namespace HuaBo.Gis.Desktop
{
    public partial class FormMain : DevExpress.XtraBars.Ribbon.RibbonForm, IFormMain
    {
        public RibbonControl RibbonView
        {
            get { return this.ribbon; }
            set { this.ribbon = value; }
        }


        public DevExpress.XtraBars.Docking2010.DocumentManager DocumentManager
        {
            get { return this.m_documentManager; }
        }

        public DevExpress.XtraBars.Docking.DockManager DockManager
        {
            get { return this.m_dockManager; }
        }

        private Dictionary<string, PopupMenu> m_popupMenus;
        public Dictionary<string, PopupMenu> PopupMenus
        {
            get { return this.m_popupMenus; }
            set { this.m_popupMenus = value; }
        }

        public FormMain()
        {
            InitializeComponent();
            m_popupMenus = new Dictionary<string, PopupMenu>();
            if (!DevExpress.Skins.SkinManager.AllowFormSkins)
            {
                DevExpress.Skins.SkinManager.EnableFormSkins();
            }
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Visual Studio 2013 Blue");
            DevExpress.UserSkins.BonusSkins.Register();

            this.DocumentManager.View.DocumentActivated += View_DocumentActivated;
            this.DocumentManager.View.DocumentDeactivated += View_DocumentDeactivated;
            this.DocumentManager.View.DocumentClosed += View_DocumentClosed;
            this.DocumentManager.View.DocumentAdded += View_DocumentAdded;
            this.RibbonView.SelectedPageChanged += RibbonView_SelectedPageChanged;
        }

        void RibbonView_SelectedPageChanged(object sender, EventArgs e)
        {
            if (DocumentManager.View.ActiveDocument != null && RibbonView.PageCategories.Count > 0)
            {
                RibbonView.PageCategories[0].Tag = RibbonView.SelectedPage;
            }
        }


        void View_DocumentClosed(object sender, DevExpress.XtraBars.Docking2010.Views.DocumentEventArgs e)
        {
            if (DocumentManager.View.ActiveDocument == null)
            {
                GisApp.ActiveApp.ActiveForm = null;
                RibbonView.PageCategories.Clear();
            }
        }

        void View_DocumentDeactivated(object sender, DevExpress.XtraBars.Docking2010.Views.DocumentEventArgs e)
        {
            RibbonPageCategory category = e.Document.Form.Tag as RibbonPageCategory;
            RibbonView.PageCategories.Remove(category);
        }

        void View_DocumentActivated(object sender, DevExpress.XtraBars.Docking2010.Views.DocumentEventArgs e)
        {
            GisApp.ActiveApp.ActiveForm = e.Document.Form as IForm;
            RibbonPageCategory category = e.Document.Form.Tag as RibbonPageCategory;
            RibbonView.PageCategories.Add(category);
            if (category.Tag != null)
            {
                RibbonView.SelectedPage = category.Tag as RibbonPage;
            }
        }

        void View_DocumentAdded(object sender, DevExpress.XtraBars.Docking2010.Views.DocumentEventArgs e)
        {
            //思路：新建了Form，则创建PageCategory，添加进去
            //前面的隐藏
            //如果是窗体移除，则删除掉PageCategory    
            //PageCategory保存SelectPage，Form保存PageCategory
            IForm form = e.Document.Form as IForm;
            string formType = form.GetType() + "";
            RibbonView.PageCategories.Clear();
            //todo：这个地方可能有问题。是否通过GisApp.ActiveApp.Doc来传值值得考虑
            RibbonPageCategory category = XMLToPageCategory.Create(formType, e.Document.Form.Text, GisApp.ActiveApp.Doc.DocumentElement);
            e.Document.Form.Tag = category;

            RibbonView.PageCategories.Add(category);
            RibbonView.SelectedPage = category.Pages[0];
        }




        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            GisApp.ActiveApp.CreateFormScene();

        }





    }

}