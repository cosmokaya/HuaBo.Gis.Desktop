using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SuperMap.UI;
using System.ComponentModel.Composition;
using HuaBo.Gis.Interfaces;
using HuaBo.Gis.Desktop;
using DevExpress.XtraBars.Ribbon;

namespace HuaBo.Gis.Plugins
{
    [Export(typeof(XtraUserControl))]
    public partial class ControlWorkspaceTree : DevExpress.XtraEditors.XtraUserControl
    {
        public ControlWorkspaceTree()
        {
            InitializeComponent();

            WorkspaceTree workspaceTree = new WorkspaceTree(GisApp.ActiveApp.Workspace);
            workspaceTree.Dock = DockStyle.Fill;
            workspaceTree.SelectionChanged += workspaceTree_SelectionChanged;
            workspaceTree.NodeMouseDoubleClick += workspaceTree_NodeMouseDoubleClick;
            workspaceTree.NodeMouseClick += workspaceTree_NodeMouseClick;
            this.Controls.Add(workspaceTree);
        }

        void workspaceTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (GisApp.ActiveApp.SelectNode.NodeType == WorkspaceTreeNodeDataType.Workspace)
                {
                    Point pt = this.PointToScreen(new Point(e.X, e.Y));
                    GisApp.ActiveApp.PopupMenus["HuaBo.Gis.ContextWorkspace"].ShowPopup(pt);
                }
                else if (GisApp.ActiveApp.SelectNode.NodeType == WorkspaceTreeNodeDataType.Scenes)
                {
                    Point pt = this.PointToScreen(new Point(e.X, e.Y));
                    GisApp.ActiveApp.PopupMenus["HuaBo.Gis.ContextScenes"].ShowPopup(pt);
                }
                else if (GisApp.ActiveApp.SelectNode.NodeType == WorkspaceTreeNodeDataType.SceneName)
                {
                    Point pt = this.PointToScreen(new Point(e.X, e.Y));
                    GisApp.ActiveApp.PopupMenus["HuaBo.Gis.ContextScene"].ShowPopup(pt);
                }
            }
        }

        private void ControlWorkspaceTree_Load(object sender, EventArgs e)
        {


        }

        void workspaceTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (GisApp.ActiveApp.SelectNode != null)
            {
                WorkspaceTreeNodeBase node = GisApp.ActiveApp.SelectNode;
                if (node.NodeType == WorkspaceTreeNodeDataType.SceneName)
                {
                    string sceneName = node.Text;
                    //打开场景，假如说已经打开，则激活
                    //否则，直接新建这个场景
                    var document = GisApp.ActiveApp.FormMain.DocumentManager.View.Documents.Where(s =>
                        s.Caption == sceneName && (s.Form as IFormScene) != null).FirstOrDefault();

                    IFormScene formScene = null;
                    if (document != null)
                    {
                        formScene = document.Form as IFormScene;
                        GisApp.ActiveApp.FormMain.DocumentManager.View.ActivateDocument(document.Control);
                    }
                    else
                    {
                        formScene = GisApp.ActiveApp.CreateFormScene(sceneName);
                    }
                    formScene.SceneControl.Scene.Workspace = GisApp.ActiveApp.Workspace;
                    formScene.SceneControl.Scene.Open(sceneName);
                }
            }
        }

        void workspaceTree_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.SelectedNodes.Count() != 0)
            {
                GisApp.ActiveApp.SelectNode = e.SelectedNodes[0] as WorkspaceTreeNodeBase;
            }
        }

    }
}
