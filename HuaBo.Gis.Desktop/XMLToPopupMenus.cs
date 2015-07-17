using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using HuaBo.Gis.Desktop.XML;
using HuaBo.Gis.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HuaBo.Gis.Desktop
{
    class XMLToPopupMenus
    {
        /// <summary>
        /// 获取所有右键菜单
        /// </summary>
        /// <param name="ribbon"></param>
        /// <param name="doc"></param>
        /// <param name="ctrlActions"></param>
        /// <returns></returns>
        public static Dictionary<string, PopupMenu> GetMenus(RibbonControl ribbon, XmlDocument doc, Dictionary<string, CtrlAction> ctrlActions)
        {
            Dictionary<string, PopupMenu> menus = new Dictionary<string, PopupMenu>();
            foreach (XmlNode popupMenus in doc.DocumentElement.ChildNodes)
            {
                if (XMLManager.GetNodeType(popupMenus) == XMLNodeType.PopupMenus)
                {
                    foreach (XmlNode popupItem in popupMenus.ChildNodes)
                    {
                        PopupMenu menu = XMLPopupMenu.CreatePopupMenu(popupItem, ribbon);
                        if (!menus.ContainsKey(menu.Name))
                        {
                            menus.Add(menu.Name, menu);
                            foreach (XmlNode item in popupItem.ChildNodes)
                            {
                                XMLItem.CreateBarItem(item, ribbon, menu.ItemLinks, ctrlActions);

                            }
                        }
                    }
                }

            }
            return menus;

        }
    }
}
