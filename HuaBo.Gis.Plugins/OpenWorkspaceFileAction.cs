using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SuperMap.Data;
using HuaBo.Gis.Interfaces;
using HuaBo.Gis.Desktop;

namespace HuaBo.Gis.Plugins
{
    [Export(typeof(CtrlAction))]
    public class OpenWorkspaceFileAction : CtrlAction
    {
        public override void Run()
        {
            String itemType = BarItem.GetType().ToString();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "打开工作控件";
            openFileDialog.Filter = "工作控件|(*.sxw;*.sxwu;*.smw;*.smwu;)";
            openFileDialog.Multiselect = false;
            openFileDialog.CheckFileExists = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                (GisApp.ActiveApp.FormMain as Form).Cursor = Cursors.WaitCursor;
                OpenWorkspace(openFileDialog.FileName, GisApp.ActiveApp.Workspace);

                GisApp.ActiveApp.Output.Warning("已经打开工作空间!");
                (GisApp.ActiveApp.FormMain as Form).Cursor = Cursors.Default;
            }
        }


        public Boolean OpenWorkspace(String WorkSpacePath, Workspace workspace, string password = "")
        {
            Boolean result = false;
            try
            {
                WorkspaceConnectionInfo conInfo = new WorkspaceConnectionInfo(WorkSpacePath);
                conInfo.Password = password;
                result = workspace.Open(conInfo);
            }
            catch (Exception ex)
            {
                //Output.OutputClass.Infomation = ex.Message;
            }
            return result;
        }
    }
}
