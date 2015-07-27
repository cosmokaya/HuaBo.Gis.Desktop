using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;

namespace HuaBo.Gis.Desktop
{
    internal class XMLBarButtonDropDownItem : XMLBarButtonItem
    {
        public XMLBarButtonDropDownItem(XmlNode xmlNode, BarItemLinkCollection itemlinks, Dictionary<string, CtrlAction> CtrlActions)
            : base(xmlNode, itemlinks, CtrlActions)
        {
        }

        protected override BarItem CreateBarItem()
        {
            this.BarItem = new BarButtonItem();
            (this.BarItem as BarButtonItem).ButtonStyle = BarButtonStyle.DropDown;

            bool isBeginGroup = this.ItemBeginGroup == "true";
            this.ItemLinks.Add(this.BarItem, isBeginGroup);
            this.BarItem.Name = Guid.NewGuid() + "";
            this.BarItem.Caption = this.ItemText;
            this.BarItem.Visibility = this.ItemVisible != "false" ? BarItemVisibility.Always : BarItemVisibility.Never;
            this.BarItem.RibbonStyle = this.ItemRibbonStyle == "large" ? RibbonItemStyles.Large : RibbonItemStyles.Default;
            this.BarItem.Glyph = BitMapManager.GetBitMap(this.ItemImage);
            CtrlAction ctrlAction = this.CtrlActions.ContainsKey(this.ItemBindClass) ? CtrlActions[ItemBindClass] : null;
            if (ctrlAction != null)
            {
                ctrlAction.BarItem = this.BarItem;
                this.BarItem.Tag = ctrlAction;
                this.BarItem.ItemClick += (m, n) =>
                {
                    ctrlAction.Run();
                };
            }

            PopupMenu popup = (new XMLPopupMenu(this.XmlNode)).CreatePopupMenu();
            (this.BarItem as BarButtonItem).DropDownControl = popup;

            foreach (XmlNode dropItemNode in this.XmlNode.ChildNodes)
            {
                BarItem barDropItem = XMLBarItem.CreateBarItem(dropItemNode, popup.ItemLinks, this.CtrlActions);
            }
            return this.BarItem;
        }
    }
}
