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
        public static Dictionary<string, PopupMenu> GetMenus(RibbonControl ribbon, XmlDocument doc, Dictionary<string, CtrlAction> ctrlActions)
        {
            Dictionary<string, PopupMenu> menus = new Dictionary<string, PopupMenu>();
            foreach (XmlNode popupMenus in doc.DocumentElement.ChildNodes)
            {
                if (XMLManager.GetNodeType(popupMenus) == XMLNodeType.PopupMenus)
                {
                    foreach (XmlNode popupItem in popupMenus.ChildNodes)
                    {
                        PopupMenu menu = XMLPopupMenu.CreatePopupMenu(popupItem);
                        if (!menus.ContainsKey(menu.Name))
                        {
                            menu.Ribbon = ribbon;
                            menus.Add(menu.Name, menu);
                            foreach (XmlNode item in popupItem.ChildNodes)
                            {
                                XMLItem.CreateBarItem(item, ribbon, menu.ItemLinks, ctrlActions);
                                //XMLItem xmlItem = XMLItem.GetXMLItem(item);
                                //CtrlAction ctrlAction = null;
                                //BarItem barItem = null;
                                //if (ctrlActions.Keys.Contains(xmlItem.ItemBindClass))
                                //{
                                //    ctrlAction = ctrlActions[xmlItem.ItemBindClass];
                                //    barItem = XMLItem.GetBarItem(xmlItem, ctrlAction, menu.ItemLinks);
                                //}

                            }
                        }
                    }
                }

            }
            return menus;

        }
    }
}
