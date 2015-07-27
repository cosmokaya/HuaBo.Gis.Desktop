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
        public Dictionary<string, XtraUserControl> PluginControls { get; set; }
        //保存加载的控件和CtrlAction
        public Dictionary<string, CtrlAction> CtrlActions { get; set; }

        private XmlDocument m_doc;//界面的配置文件
        private string m_xmlPath = @"..\WorkEnvironment\Default2.xml";
        private string m_extensionDir = @"..\Plugins\";


        public GisApp()
        {
            Workspace = new Workspace();
            Output = new Output();
            PluginControls = new Dictionary<string, XtraUserControl>();
            m_doc = new XmlDocument();
            this.FormMain = new FormMain();
            CtrlActions = new Dictionary<string, CtrlAction>();
        }

        public void Run()
        {
            //获取所有插件
            Compose();
            //解析插件
            m_doc.Load(m_xmlPath);
            //1.先解析所有的BarItem
            //2.获取所有的右键菜单
            //3.获取所有的DockPanel控件
            //原因：控件包含菜单，菜单包含BarItem
            foreach (var item in m_ctrlActions)
            {
                this.CtrlActions.Add(item.Value.ToString(), item.Value);
            }
            XmlNode popupMenusNode = XMLManager.GetSelectNode(m_doc.DocumentElement, XMLNodeType.PopupMenus);
            this.PopupMenus = XMLToPopupMenus.GetPopupMenus(popupMenusNode, CtrlActions);
            foreach (var item in m_controls)
            {
                PluginControls.Add(item.Value.ToString(), item.Value);
            }
            //
            XmlNode ribbonNode = XMLManager.GetSelectNode(m_doc.DocumentElement, XMLNodeType.Ribbon);
            XMLToPage xmlToPage = new XMLToPage(ribbonNode, CtrlActions);
            xmlToPage.CreateRibbonOrCategory();
            //生成DockPanel，并加载控件.细节再考虑
            XmlNode dockpanelsNode = XMLManager.GetSelectNode(m_doc.DocumentElement, XMLNodeType.DockPanels);
            XMLToDockpanels.Parse(this.FormMain.DockManager, dockpanelsNode, PluginControls);

            //刷新主Page的状态属性
            //RefreshTab();
            Thread ts = new Thread(new ThreadStart(RefreshTab));
            ts.Priority = ThreadPriority.BelowNormal;
            ts.Start();
            (this.FormMain as Form).FormClosed += (m, n) =>
            {
                ts.Abort();
            };

            Application.Run(this.FormMain as Form);
        }


        void GisApp_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Workspace != null)
            {
                this.Workspace.Close();
            }
        }

        //刷新RibbonView
        private void RefreshTab()
        {
            while (true)
            {
                for (int i = 0; i < this.FormMain.RibbonView.SelectedPage.Groups.Count; i++)
                {
                    for (int j = 0; j < this.FormMain.RibbonView.SelectedPage.Groups[i].ItemLinks.Count; j++)
                    {
                        BarItem barItem = this.FormMain.RibbonView.SelectedPage.Groups[i].ItemLinks[j].Item;
                        CtrlAction ctrl = barItem.Tag as CtrlAction;
                        if (ctrl != null)
                        {
                            ctrl.ActiveApp_Refreshed("Refresh", new EventArgs());
                        }
                    }
                }
                this.FormMain.RibbonView.Refresh();//有时form会不刷新
                Thread.Sleep(50);
            }

        }


        public IFormScene CreateFormScene(string sceneName = "")
        {
            (this.FormMain as Form).Cursor = Cursors.WaitCursor;
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

            FormScene formscene = new FormScene();
            formscene.Name = Guid.NewGuid() + ""; ;
            formscene.Dock = DockStyle.Fill;
            formscene.Text = resultName;
            //一定要在Mdi赋值前就要把所有属性设置好，因为设置这个属性就会触发documentadd事件。则这个Form的属性不全
            formscene.Tag = CreateCategoryByIForm((typeof(IFormScene)).ToString());
            formscene.MdiParent = this.FormMain as Form;
            formscene.Show();
            (this.FormMain as Form).Cursor = Cursors.Default;
            return formscene;
        }

        private RibbonPageCategory CreateCategoryByIForm(string formType)
        {
            RibbonPageCategory result = null;
            try
            {
                //
                XmlNode node = XMLManager.GetSelectNode(m_doc.DocumentElement, XMLNodeType.Ribbon);
                foreach (XmlNode item in node.ChildNodes)
                {
                    if (XMLManager.GetNodeType(item) == XMLNodeType.PageCategory && item.Attributes[XMLPageCategory.Form].Value == formType)
                    {
                        node = item;
                        break;
                    }
                }
                result = XMLPageCategory.CreateCategory(node);
                XMLToPage xmlToPage = new XMLToPage(node, CtrlActions);
                xmlToPage.CreateRibbonOrCategory(result);
            }
            catch (Exception)
            {
                result = null;
            }
            return result;
        }

        public IFormMap CreateFormMap(string mapScene = "")
        {
            (this.FormMain as Form).Cursor = Cursors.WaitCursor;
            (this.FormMain as Form).Cursor = Cursors.Default;
            return null;
        }


    }




}
