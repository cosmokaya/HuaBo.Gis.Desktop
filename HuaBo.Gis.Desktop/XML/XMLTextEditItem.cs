using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaBo.Gis.Desktop
{
    internal class XMLTextEditItem : XMLBarItem
    {

        public XMLTextEditItem(System.Xml.XmlNode xmlNode, DevExpress.XtraBars.BarItemLinkCollection itemlinks, Dictionary<string, CtrlAction> CtrlActions)
            : base(xmlNode, itemlinks, CtrlActions)
        {
        }

        protected override BarItem CreateBarItem()
        {
            BarEditItem item = new BarEditItem();
            this.BarItem = item;
            bool isBeginGroup = this.ItemBeginGroup == "true";
            this.ItemLinks.Add(item, isBeginGroup);
            item.Name = Guid.NewGuid() + "";
            item.Caption = this.ItemText;
            item.Visibility = this.ItemVisible != "false" ? BarItemVisibility.Always : BarItemVisibility.Never;
            item.RibbonStyle = this.ItemRibbonStyle == "large" ? RibbonItemStyles.Large : RibbonItemStyles.Default;
            item.Glyph = BitMapManager.GetBitMap(this.ItemImage);

            CtrlAction ctrlAction = this.CtrlActions.ContainsKey(this.ItemBindClass) ? CtrlActions[ItemBindClass] : null;
            if (ctrlAction != null)
            {
                ctrlAction.BarItem = item;
                item.Tag = ctrlAction;
                item.EditValueChanged += (m, n) =>
                        {
                            ctrlAction.Run();
                        };
            }

            RepositoryItemTextEdit repository = new RepositoryItemTextEdit();

            object data = XMLRepositoryItem.GetValue(this.XmlNode.ChildNodes[0]);
            if (data.ToString() != "")
            {
                item.EditValue = data;
            }
            item.Edit = repository;
            item.Width = 100;
            return this.BarItem;
        }
    }
}
