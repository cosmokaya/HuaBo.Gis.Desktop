using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HuaBo.Gis.Desktop;
using SuperMap.Data;

namespace HuaBo.Gis.Plugins
{
    [Export(typeof(CtrlAction))]
    public class OpenDatasourceFileAction : CtrlAction
    {
        public override void Run()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "打开文件型数据源";
            openFileDialog.Filter = "文件型数据源|*.udb";//其它的先不管了
            openFileDialog.Multiselect = false;
            openFileDialog.CheckFileExists = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                (GisApp.ActiveApp.FormMain as Form).Cursor = Cursors.WaitCursor;
                OpenDatasource(openFileDialog.FileName, openFileDialog.SafeFileName, GisApp.ActiveApp.Workspace);

                GisApp.ActiveApp.Output.Warning("已经打开文件型数据源!");
                (GisApp.ActiveApp.FormMain as Form).Cursor = Cursors.Default;
            }
        }

        public void OpenDatasource(string fileName, string safename, Workspace workspace)
        {
            DatasourceConnectionInfo dc = new DatasourceConnectionInfo(fileName, safename, "");
            workspace.Datasources.Open(dc);
        }
    }
}
