using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuaBo.Gis.Desktop;
using SuperMap.Data;

namespace HuaBo.Gis.Plugins
{
    /// <summary>
    /// 新建内存型数据源。
    /// </summary>
    [Export(typeof(CtrlAction))]
    public class DatasourceNewMemoryAction : CtrlAction
    {
        public override void Run()
        {
            //System.Windows.Forms.MessageBox.Show("DatasourceNewMemoryAction未实现！");
            CreateMemoryDatasource();
        }

        private Datasource CreateMemoryDatasource()
        {
            Datasource datasource = null;
            try
            {
                DatasourceConnectionInfo connectionInfo = new DatasourceConnectionInfo();
                connectionInfo.EngineType = EngineType.UDB;
                connectionInfo.Server = ":memory:";
                connectionInfo.Alias = "test";
                datasource = GisApp.ActiveApp.Workspace.Datasources.Create(connectionInfo);
            }
            catch
            {
            }
            return datasource;
        }
    }




}
