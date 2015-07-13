using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraEditors;
using System.ComponentModel.Composition;
using HuaBo.Gis.Interfaces;
using System.ComponentModel.Composition.Hosting;

namespace HuaBo.Gis.Desktop
{
    /// <summary>
    /// 插件管理部分
    /// </summary>
    public partial class GisApp
    {
        public void LoadPlugins()
        {
            Compose();
        }

        [ImportMany]
        public Lazy<CtrlAction, IDictionary<string, object>>[] Actions { get; set; }
        [ImportMany]
        public Lazy<XtraUserControl, IDictionary<string, object>>[] Controls { get; set; }

        private CompositionContainer _container;
        private bool Compose()
        {
            _container = GetContainerFromDirectory();
            try
            {
                _container.ComposeParts(this);
            }
            catch (CompositionException compException)
            {
                MessageBox.Show(compException.ToString());
                return false;
            }
            return true;
        }

        private CompositionContainer GetContainerFromDirectory()
        {
            var catalog = new AggregateCatalog();
            var thisAssembly = new AssemblyCatalog(System.Reflection.Assembly.GetExecutingAssembly());
            catalog.Catalogs.Add(thisAssembly);
            catalog.Catalogs.Add(new DirectoryCatalog(m_extensionDir));
            var container = new CompositionContainer(catalog);
            return container;
        }
    }
}
