using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraBars.Ribbon;

namespace HuaBo.Gis.Desktop
{
    internal class XMLSkinRibbonGalleryItem : XMLBarItem
    {
        public XMLSkinRibbonGalleryItem(XmlNode xmlNode, BarItemLinkCollection itemlinks, Dictionary<string, CtrlAction> CtrlActions)
            : base(xmlNode, itemlinks, CtrlActions)
        { }


        protected override BarItem CreateBarItem()
        {
            this.BarItem = new SkinRibbonGalleryBarItem();
            bool isBeginGroup = this.ItemBeginGroup == "true";
            this.ItemLinks.Add(this.BarItem, isBeginGroup);
            this.BarItem.Name = Guid.NewGuid() + "";
            this.BarItem.Caption = this.ItemText;
            this.BarItem.Visibility = this.ItemVisible != "false" ? BarItemVisibility.Always : BarItemVisibility.Never;
            this.BarItem.RibbonStyle = this.ItemRibbonStyle == "large" ? RibbonItemStyles.Large : RibbonItemStyles.Default;
            this.BarItem.Glyph = BitMapManager.GetBitMap(this.ItemImage);

            SkinHelper.InitSkinGallery(BarItem as SkinRibbonGalleryBarItem);
            //CtrlAction ctrlAction = this.CtrlActions.ContainsKey(this.ItemBindClass) ? CtrlActions[ItemBindClass] : null;
            //if (ctrlAction != null)
            //{
            //    ctrlAction.BarItem = this.BarItem;
            //    this.BarItem.Tag = ctrlAction;
            //    this.BarItem.ItemClick += (m, n) =>
            //        {
            //            ctrlAction.Run();
            //        };
            //}
            return this.BarItem;
        }
    }
}
