﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;

namespace HuaBo.Gis.Desktop
{
    internal class XMLBarCheckItem : XMLBarItem
    {
        internal string ItemChecked { get; set; }
        /// <summary>
        /// 是否默认选中,如果不是button或者checkbutton类型的，设置为""
        /// </summary>
        public static string Checked = "checked";

        public XMLBarCheckItem(XmlNode xmlNode, BarItemLinkCollection itemlinks, Dictionary<string, CtrlAction> CtrlActions)
            : base(xmlNode, itemlinks,CtrlActions)
        {
            this.ItemChecked = NodeAttr.GetOrDefaultNodeAttrValue(xmlNode, Checked, "false");
        }


        protected override BarItem CreateBarItem()
        {
            this.BarItem = new BarCheckItem();
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

            //这个可以另写属性修改，不管了
            (this.BarItem as BarCheckItem).CheckBoxVisibility = CheckBoxVisibility.BeforeText;
            (this.BarItem as BarCheckItem).Checked = this.ItemChecked == "true";
            return this.BarItem;
        }
    }
}
