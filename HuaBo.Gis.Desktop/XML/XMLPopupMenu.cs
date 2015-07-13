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

        public static PopupMenu CreatePopupMenu(XmlNode xmlNode)
        {
            PopupMenu popupMenu = new PopupMenu();
            popupMenu.Name = NodeAttr.GetSetNodeAttrValue(xmlNode, Name, Guid.NewGuid() + "");
            popupMenu.BeforePopup += popupMenu_BeforePopup;
            return popupMenu;
        }

        static void popupMenu_BeforePopup(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (BarItemLink item in ((PopupMenu)sender).ItemLinks)
            {
                BarItem barItem = item.Item as BarItem;
                if (barItem == null) continue;
                CtrlAction ctrl = barItem.Tag as CtrlAction;
                if (ctrl != null)
                {
                    barItem.Enabled = ctrl.Enable();
                    if ((barItem as BarCheckItem) != null)
                    {
                        (barItem as BarCheckItem).Checked = ctrl.Check() == CheckState.Checked;
                    }
                    if ((barItem as BarButtonItem) != null)
                    {
                        (barItem as BarButtonItem).Down = ctrl.Check() == CheckState.Checked;
                    }
                }
            }
        }
    }
}
