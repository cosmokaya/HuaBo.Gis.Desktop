using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraEditors;
using SuperMap.Data;
using SuperMap.UI;
using HuaBo.Gis.Interfaces;
using DevExpress.XtraBars.Ribbon;

namespace HuaBo.Gis.Desktop
{

    public partial class GisApp
    {
        public static GisApp ActiveApp { get; set; }

        public IFormMain FormMain { get; set; }
        public Workspace Workspace { get; set; }
        public Output Output { get; set; }
        public WorkspaceTreeNodeBase SelectNode { get; set; }
        //public IForm ActiveForm { get; set; }
        public Dictionary<string, PopupMenu> PopupMenus { get; set; }


        private XmlDocument m_doc;//界面的配置文件
        public XmlDocument Doc
        {
            get { return m_doc; }
            set { m_doc = value; }
        }
        private string m_xmlPath = @"D:\360云盘\Code\DevGis\插件式Gis开发\WorkEnvironment\Default2.xml";
        private string m_extensionDir = @"D:\360云盘\Code\DevGis\插件式Gis开发\Plugins";
        //保存加载的控件和CtrlAction
        Dictionary<string, CtrlAction> m_ctrlActions = new Dictionary<string, CtrlAction>();
        //控件也应该有这个集合,考虑- -

        static GisApp()
        {

        }
        public GisApp()
        {
            Workspace = new Workspace();
            Output = new Output();
        }

        public void Run()
        {
            //获取所有插件
            Compose();
            //解析插件
            m_doc = new XmlDocument();
            m_doc.Load(m_xmlPath);
            foreach (var item in Actions)
            {
                m_ctrlActions.Add(item.Value.ToString(), item.Value);
            }

            //
            this.FormMain = new FormMain();

            this.FormMain.PopupMenus = XMLToPopupMenus.GetMenus(this.FormMain.RibbonView, m_doc, m_ctrlActions);
            XMLToPage.Parse(this.FormMain.RibbonView, m_doc, m_ctrlActions);
            XMLToDockpanels.Parse(this.FormMain.DockManager, m_doc);


            //刷新主Page的状态属性
            RefreshTab();

            Application.Run(this.FormMain as Form);

        }





        void GisApp_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Workspace != null)
            {
                this.Workspace.Close();
            }
        }

        /// <summary>
        /// todo：刷新Tab的状态，应该写在一个线程里面
        /// </summary>
        private void RefreshTab()
        {
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 50;
            timer.Tick += (m, n) =>
            {
                for (int i = 0; i < this.FormMain.RibbonView.SelectedPage.Groups.Count; i++)
                {
                    for (int j = 0; j < this.FormMain.RibbonView.SelectedPage.Groups[i].ItemLinks.Count; j++)
                    {
                        BarItem barItem = this.FormMain.RibbonView.SelectedPage.Groups[i].ItemLinks[j].Item;

                        CtrlAction ctrl = barItem.Tag as CtrlAction;
                        if (ctrl != null)
                        {
                            barItem.Enabled = ctrl.Enable();
                            if ((barItem as BarCheckItem) != null)
                            {
                                (barItem as BarCheckItem).Checked = ctrl.Check() == CheckState.Checked;
                            }
                            if ((barItem as BarButtonItem) != null)
                            {
                                (barItem as BarButtonItem).Down = ctrl.Check() == CheckState.Checked;
                            }
                        }
                    }
                }
            };
            timer.Start();
        }

        public IFormScene CreateFormScene(string sceneName = "")
        {
            FormScene formscene = new FormScene();
            formscene.Name = Guid.NewGuid() + ""; ;
            formscene.Dock = DockStyle.Fill;
            formscene.Text = sceneName == "" ? "未命名场景" : sceneName; //todo:名字应该是不断加的

            formscene.MdiParent = this.FormMain as Form;
            formscene.Show();
            return formscene;
        }

        public IFormMap CreateFormMap(string mapScene = "")
        {
            return null;
        }

    }




}
