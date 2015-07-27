using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HuaBo.Gis.Desktop
{
    public class XMLManager
    {
        public static XMLNodeType GetNodeType(XmlNode xmlNode)
        {
            XMLNodeType nodeType = XMLNodeType.Plugin;
            switch (xmlNode.Name)
            {
                case XMLCommandType.Ribbon:
                    nodeType = XMLNodeType.Ribbon;
                    break;
                case XMLCommandType.DockPanels:
                    nodeType = XMLNodeType.DockPanels;
                    break;
                case XMLCommandType.PopupMenus:
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

        /// <summary>
        /// 获取需要的XmlNode,只循环一次
        /// </summary>
        /// <param name="xmlNode"></param>
        /// <param name="type">类型只支持单一类型的，比如Ribbon，DockPanels，PopupMenus</param>
        /// <returns></returns>
        public static XmlNode GetSelectNode(XmlNode xmlNode, XMLNodeType type)
        {
            XmlNode result = null;

            if (XMLManager.GetNodeType(xmlNode) == type)
            {
                result = xmlNode;
            }
            else
            {
                foreach (XmlNode item in xmlNode.ChildNodes)
                {
                    if (XMLManager.GetNodeType(item) == type)
                    {
                        result = item;
                        break;
                    }
                }
            }
            return result;
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
