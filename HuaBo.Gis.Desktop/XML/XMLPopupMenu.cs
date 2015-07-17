using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;
using HuaBo.Gis.Interfaces;

namespace HuaBo.Gis.Desktop.XML
{
    public class XMLPopupMenu
    {
        /// <summary>
        /// 标签名
        /// </summary>
        public const string Name = "name";

        public XmlNode XmlNode { get; set; }

        public XMLPopupMenu(XmlNode xmlNode)
        {
            XmlNode = xmlNode;
        }

        public static PopupMenu CreatePopupMenu(XmlNode xmlNode, RibbonControl ribbon)
        {
            PopupMenu popupMenu = new PopupMenu();
            popupMenu.Ribbon = ribbon;
            popupMenu.Name = NodeAttr.GetSetNodeAttrValue(xmlNode, Name, Guid.NewGuid() + "");
            popupMenu.BeforePopup += popupMenu_BeforePopup;
            return popupMenu;
        }

        static void popupMenu_BeforePopup(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (BarItemLink item in ((PopupMenu)sender).ItemLinks)
            {
                BarItem barItem = item.Item as BarItem;
                CtrlAction ctrlAction = barItem.Tag as CtrlAction;
                if (ctrlAction != null)
                {
                    //ctrlAction.Form = GisApp.ActiveApp.FormMain.ActiveForm;
                    GisApp.ActiveApp.RefreshItem(barItem);
                }

            }
        }
    }
}
