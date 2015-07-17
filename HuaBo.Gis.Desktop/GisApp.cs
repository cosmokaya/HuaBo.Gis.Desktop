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
        //控件也应该有这个集合,考虑- -
        Dictionary<string, XtraUserControl> PluginControls { get; set; }

        private XmlDocument m_doc;//界面的配置文件
        public XmlDocument Doc
        {
            get { return m_doc; }
            set { m_doc = value; }
        }
        private string m_xmlPath = @"..\WorkEnvironment\Default2.xml";
        private string m_extensionDir = @"..\Plugins\";
        //保存加载的控件和CtrlAction
        private Dictionary<string, CtrlAction> m_ctrlActions = new Dictionary<string, CtrlAction>();


        static GisApp()
        {

        }
        public GisApp()
        {
            Workspace = new Workspace();
            Output = new Output();
            FormMain = new FormMain();
            PluginControls = new Dictionary<string, XtraUserControl>();
        }

        public void Run()
        {
            //获取所有插件
            Compose();
            //解析插件
            m_doc = new XmlDocument();
            m_doc.Load(m_xmlPath);
            foreach (var item in m_actions)
            {
                m_ctrlActions.Add(item.Value.ToString(), item.Value);
            }
            foreach (var item in m_controls)
            {
                PluginControls.Add(item.Value.Name, item.Value);
            }

            this.PopupMenus = XMLToPopupMenus.GetMenus(this.FormMain.RibbonView, m_doc, m_ctrlActions);
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
                        RefreshItem(this.FormMain.RibbonView.SelectedPage.Groups[i].ItemLinks[j].Item);
                    }
                }
            };
            timer.Start();
        }

        public void RefreshItem(BarItem barItem)
        {
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

        public IFormScene CreateFormScene(string sceneName = "")
        {
            (this.FormMain as Form).Cursor = Cursors.WaitCursor;
            FormScene formscene = new FormScene();
            formscene.Name = Guid.NewGuid() + ""; ;
            formscene.Dock = DockStyle.Fill;
            string resultName = "";
            string defaultName = "未命名场景";
            if (sceneName == "")
            {
                int count = 1;
                resultName = defaultName;
                while (true)
                {
                    var result = this.FormMain.DocumentManager.View.Documents.Where(s => (s.Form as IFormScene) != null).Select(s => s.Form.Text).ToList();
                    if (result.Contains(resultName))
                    {
                        resultName = defaultName + count;
                        count++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                resultName = sceneName;
            }
            formscene.Text = resultName;
            formscene.MdiParent = this.FormMain as Form;
            formscene.Show();

            (this.FormMain as Form).Cursor = Cursors.Default;
            return formscene;
        }

        public IFormMap CreateFormMap(string mapScene = "")
        {
            (this.FormMain as Form).Cursor = Cursors.WaitCursor;
            (this.FormMain as Form).Cursor = Cursors.Default;
            return null;
        }

    }




}
