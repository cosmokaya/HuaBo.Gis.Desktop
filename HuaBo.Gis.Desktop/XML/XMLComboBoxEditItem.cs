using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;

namespace HuaBo.Gis.Desktop
{
    internal class XMLComboBoxEditItem : XMLBarItem
    {

        public XMLComboBoxEditItem(System.Xml.XmlNode xmlNode, DevExpress.XtraBars.BarItemLinkCollection itemlinks, Dictionary<string, CtrlAction> CtrlActions)
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

            RepositoryItemComboBox repository = new RepositoryItemComboBox();
            //todo：未完全完成
            for (int i = 0; i < this.XmlNode.ChildNodes.Count; i++)
            {
                object data = XMLRepositoryItem.GetValue(this.XmlNode.ChildNodes[i]);
                if (data.ToString() != "")
                {
                    repository.Items.Add(data);
                }
            }
            repository.TextEditStyle = TextEditStyles.DisableTextEditor;
            item.Edit = repository;
            item.EditValue = repository.Items[0].ToString();
            item.Width = 100;
            return this.BarItem;
        }
    }
}
