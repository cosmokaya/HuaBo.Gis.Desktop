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
        private Dictionary<string, CtrlAction> CtrlActions { get; set; }

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
                CtrlActions.Add(item.Value.ToString(), item.Value);
            }
            this.PopupMenus = XMLToPopupMenus.GetPopupMenus(this.FormMain.RibbonView, m_doc, CtrlActions);
            foreach (var item in m_controls)
            {
                PluginControls.Add(item.Value.ToString(), item.Value);
            }
            XMLToPage.Parse(this.FormMain.RibbonView, m_doc, CtrlActions);
            XMLToDockpanels.Parse(this.FormMain.DockManager, m_doc, PluginControls);

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
            //一定要在Mdi赋值前就要把所有属性设置好，因为设置这个属性就会触发documentadd事件。则这个Form的属性不全
            formscene.Tag = XMLToPageCategory.Create((typeof(IFormScene)).ToString(), resultName, m_doc.DocumentElement);
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

        //public RibbonPageCategory CreatePageCategory(string formType, string categoryName)
        //{
        //    return XMLToPageCategory.Create(formType, categoryName, m_doc.DocumentElement);
        //}

    }




}
