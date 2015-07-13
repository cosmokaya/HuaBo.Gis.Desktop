using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HuaBo.Gis.Desktop.XML
{
    public class XMLManager
    {
        public static XMLNodeType GetNodeType(XmlNode xmlNode)
        {
            XMLNodeType nodeType = XMLNodeType.Plugin;
            switch (xmlNode.Name)
            {
                case XMLRoot.Ribbon:
                    nodeType = XMLNodeType.Ribbon;
                    break;
                case XMLRoot.DockPanel:
                    nodeType = XMLNodeType.DockPanels;
                    break;
                case XMLRoot.PopupMenu:
                    nodeType = XMLNodeType.PopupMenus;
                    break;
                case XMLPageCategory.Name:
                    nodeType = XMLNodeType.PageCategory;
                    break;
                case XMLPage.Name:
                    nodeType = XMLNodeType.Page;
                    break;
                case XMLPageGroup.Name:
                    nodeType = XMLNodeType.PageGroup;
                    break;
                default:
                    nodeType = XMLNodeType.Plugin;
                    break;
            }
            return nodeType;
        }

    }

    public enum XMLNodeType
    {
        Ribbon,
        DockPanels,
        PopupMenus,
        PageCategory,
        Page,
        PageGroup,
        Plugin
    }
}
