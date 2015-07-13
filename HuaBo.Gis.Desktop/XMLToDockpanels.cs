using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraEditors;
using HuaBo.Gis.Desktop.XML;
using System.Windows.Forms;

namespace HuaBo.Gis.Desktop
{
    public class XMLToDockpanels
    {
        private static DockManager m_dockManager;

        public static void Parse(DockManager dockManager, XmlDocument doc)
        {
            m_dockManager = dockManager;
            CreatePanels(doc.DocumentElement);
        }
        static void CreatePanels(XmlElement rootElement)
        {
            foreach (XmlNode item in rootElement.ChildNodes)
            {
                if (XMLManager.GetNodeType(item) == XMLNodeType.DockPanels)
                {
                    //page-pagegroup-item-dropdown
                    foreach (XmlNode panelNode in item.ChildNodes)
                    {
                        XMLDockPanel xmlPanel = XMLDockPanel.GetXMLDockPanel(panelNode);
                        //此时已经添加进去了
                        DockPanel panel = XMLDockPanel.CreateDockPanel(xmlPanel, m_dockManager);
                    }
                }
            }
        }

        


    }
}
