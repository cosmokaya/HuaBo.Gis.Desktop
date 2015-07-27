using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using HuaBo.Gis.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HuaBo.Gis.Desktop
{
    internal class XMLToPopupMenus
    {
        /// <summary>
        /// 获取所有右键菜单
        /// </summary>
        /// <param name="ribbon"></param>
        /// <param name="doc"></param>
        /// <param name="ctrlActions"></param>
        /// <returns></returns>
        public static Dictionary<string, PopupMenu> GetPopupMenus(XmlNode xmlNode, Dictionary<string, CtrlAction> ctrlActions)
        {
            Dictionary<string, PopupMenu> menus = new Dictionary<string, PopupMenu>();

            if (XMLManager.GetNodeType(xmlNode) == XMLNodeType.PopupMenus)
            {
                foreach (XmlNode popupItemNode in xmlNode.ChildNodes)
                {
                    PopupMenu popupMenu = (new XMLPopupMenu(popupItemNode)).CreatePopupMenu();
                    if (!menus.ContainsKey(popupMenu.Name))
                    {
                        menus.Add(popupMenu.Name, popupMenu);
                        foreach (XmlNode item in popupItemNode.ChildNodes)
                        {
                            //BarItem item = new BarItem();
                            XMLBarItem.CreateBarItem(item, popupMenu.ItemLinks, ctrlActions);
                        }
                    }
                }
            }

            return menus;

        }
    }
}
