﻿using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;
using HuaBo.Gis.Interfaces;

namespace HuaBo.Gis.Desktop
{
    /// <summary>
    /// XML转换为PopupMenu，但是必须早就指定好Ribbon
    /// </summary>
    public class XMLPopupMenu
    {
        /// <summary>
        /// 标签名
        /// </summary>
        public const string Name = "name";
        public static RibbonControl Ribbon;

        public XmlNode XmlNode { get; set; }

        public XMLPopupMenu(XmlNode xmlNode)
        {
            XmlNode = xmlNode;
        }

        static XMLPopupMenu()
        {
            if (GisApp.ActiveApp.FormMain.RibbonView != null)
            {
                Ribbon = GisApp.ActiveApp.FormMain.RibbonView;
            }
        }

        public PopupMenu CreatePopupMenu()
        {
            if (Ribbon == null)
            {
                MessageBox.Show("未指定XMLPopupMenu的Ribbon!");
                return null;
            }
            PopupMenu popupMenu = new PopupMenu();
            popupMenu.Ribbon = Ribbon;
            popupMenu.Name = NodeAttr.GetOrDefaultNodeAttrValue(XmlNode, Name, Guid.NewGuid() + "");
            popupMenu.BeforePopup += popupMenu_BeforePopup;
            return popupMenu;
        }

        void popupMenu_BeforePopup(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (BarItemLink item in ((PopupMenu)sender).ItemLinks)
            {
                BarItem barItem = item.Item as BarItem;
                CtrlAction ctrlAction = barItem.Tag as CtrlAction;
                if (ctrlAction != null)
                {
                    ctrlAction.ActiveApp_Refreshed("Refresh", new EventArgs());
                }

            }
        }
    }
}
