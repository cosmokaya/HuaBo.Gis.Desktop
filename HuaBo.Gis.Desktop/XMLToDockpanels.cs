using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace HuaBo.Gis.Desktop
{
    public class XMLToDockpanels
    {
        private static DockManager m_dockManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dockManager"></param>
        /// <param name="xmlNode"></param>
        /// <param name="pluginCtrls"></param>
        public static void Parse(DockManager dockManager, XmlNode xmlNode, Dictionary<string, XtraUserControl> pluginCtrls)
        {
            m_dockManager = dockManager;
            CreatePanels(xmlNode, pluginCtrls);
        }
        static void CreatePanels(XmlNode xmlNode, Dictionary<string, XtraUserControl> pluginCtrls)
        {
            if (XMLManager.GetNodeType(xmlNode) == XMLNodeType.DockPanels)
            {
                //page-pagegroup-item-dropdown
                foreach (XmlNode panelNode in xmlNode.ChildNodes)
                {
                    XMLDockPanel xmlPanel = XMLDockPanel.CreateXMLDockPanel(panelNode);
                    //此时已经添加进去了
                    DockPanel panel = XMLDockPanel.CreateDockPanel(xmlPanel, m_dockManager, pluginCtrls);
                }

            }
        }




    }
}
