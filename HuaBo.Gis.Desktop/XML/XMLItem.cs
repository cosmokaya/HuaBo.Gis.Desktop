using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using HuaBo.Gis.Interfaces;

namespace HuaBo.Gis.Desktop.XML
{
    public class XMLItem
    {

        #region xml文件的标签名和属性名
        /// <summary>
        /// 标题
        /// </summary>
        public static string Text = "text";
        /// <summary>
        /// 是否可见
        /// </summary>
        public static string Visible = "visible";
        /// <summary>
        /// 是否默认选中,如果不是button或者checkbutton类型的，设置为""
        /// </summary>
        public static string Checked = "checked";
        /// <summary>
        /// Dll的路径
        /// </summary>
        public static string DllPath = "dllpath";
        /// <summary>
        /// 绑定的类
        /// </summary>
        public static string BindClass = "class";
        /// <summary>
        /// Item的类型，比如BarButtonItem等，不可更改
        /// </summary>
        public static string ItemType = "type";
        /// <summary>
        /// RibbonStyle
        /// </summary>
        public static string RibbonStyle = "style";
        /// <summary>
        /// 图片路径
        /// </summary>
        public static string Image = "image";
        /// <summary>
        /// 是否开启新组
        /// </summary>
        public static string BeginGroup = "begin";
        #endregion

        private string ItemName { get; set; }
        private string ItemText { get; set; }
        public string ItemVisible { get; set; }
        public string ItemChecked { get; set; }
        public string ItemDllPath { get; set; } //可以去掉
        public string ItemBindClass { get; set; }
        public string ItemItemType { get; set; }
        public string ItemRibbonStyle { get; set; }
        public string ItemImage { get; set; }
        public string ItemBeginGroup { get; set; }
        public XmlNode XmlNode { get; set; }

        public XMLItem(XmlNode xmlNode)
        { XmlNode = xmlNode; }

        public static XMLItem GetXMLItem(XmlNode xmlNode)
        {
            XMLItem item = new XMLItem(xmlNode);
            item.ItemName = xmlNode.Name;
            item.ItemText = NodeAttr.GetSetNodeAttrValue(xmlNode, Text, Guid.NewGuid() + "");
            item.ItemVisible = NodeAttr.GetSetNodeAttrValue(xmlNode, Visible, "true");
            //单独讨论
            item.ItemChecked = NodeAttr.GetSetNodeAttrValue(xmlNode, Checked, "");
            //remove
            item.ItemDllPath = NodeAttr.GetSetNodeAttrValue(xmlNode, DllPath, "");
            //
            item.ItemBindClass = NodeAttr.GetSetNodeAttrValue(xmlNode, BindClass, "");
            item.ItemItemType = NodeAttr.GetSetNodeAttrValue(xmlNode, ItemType);
            item.ItemRibbonStyle = NodeAttr.GetSetNodeAttrValue(xmlNode, RibbonStyle, "normal");
            item.ItemImage = NodeAttr.GetSetNodeAttrValue(xmlNode, Image, "");
            item.ItemBeginGroup = NodeAttr.GetSetNodeAttrValue(xmlNode, BeginGroup, "false");
            return item;
        }

        /// <summary>
        /// BarItem的初始化...
        /// </summary>
        /// <param name="xmlItem"></param>
        /// <param name="ctrlAction"></param>
        /// <param name="itemlinks">包含BarItem的父类</param>
        /// <returns></returns>
        public static BarItem CreateBarItem(XmlNode itemNode, RibbonControl ribbon, BarItemLinkCollection itemlinks, Dictionary<string, CtrlAction> ctrlActions)
        {
            System.Reflection.Assembly asmb = System.Reflection.Assembly.LoadFrom(@"./DevExpress.XtraBars.v14.1.dll");
            XMLItem xmlItem = XMLItem.GetXMLItem(itemNode);
            Type t = asmb.GetType(xmlItem.ItemItemType);
            BarItem result = Activator.CreateInstance(t) as BarItem;

            if (result != null)
            {
                bool isBeginGroup = xmlItem.ItemBeginGroup == "true";
                itemlinks.Add(result, isBeginGroup);
                result.Name = Guid.NewGuid() + "";
                result.Caption = xmlItem.ItemText;
                result.Visibility = xmlItem.ItemVisible != "false" ? BarItemVisibility.Always : BarItemVisibility.Never;
                result.RibbonStyle = xmlItem.ItemRibbonStyle == "large" ? RibbonItemStyles.Large : RibbonItemStyles.Default;
                CtrlAction ctrlAction = ctrlActions.ContainsKey(xmlItem.ItemBindClass) ? ctrlActions[xmlItem.ItemBindClass] : null;
                if (ctrlAction != null)
                {
                    ctrlAction.BarItem = result;
                    result.Tag = ctrlAction;
                    if ((result as BarEditItem) != null)
                    {
                        (result as BarEditItem).EditValueChanged += (m, n) => { ctrlAction.Run(); };
                    }
                    else
                    {
                        result.ItemClick += (m, n) => { ctrlAction.Run(); };
                    }
                }

                try
                {
                    Bitmap bitMap = new Bitmap(xmlItem.ItemImage);
                    result.Glyph = bitMap;
                }
                catch (Exception)
                {
                }
                switch (xmlItem.ItemName)
                {
                    case XMLItemName.Button://普通button,但是带checked属性
                        if (xmlItem.ItemChecked != "")
                        {
                            (result as BarButtonItem).ButtonStyle = BarButtonStyle.Check;
                            if (xmlItem.ItemChecked == "true")
                            {
                                (result as BarButtonItem).Down = true;
                            }
                            else
                                (result as BarButtonItem).Down = false;
                        }
                        break;
                    case XMLItemName.ButtonCheckDropDown://判断如果有子菜单的情况 XMLItemName.ButtonCheckDropDown,暂时用不到，等换了新版本再说
                        break;
                    case XMLItemName.ButtonDropDown:
                        (result as BarButtonItem).ButtonStyle = BarButtonStyle.DropDown;
                        PopupMenu popup = new PopupMenu();
                        popup.Ribbon = ribbon;
                        popup.BeforePopup += (m, n) =>
                        {
                            foreach (BarItemLink linkItem in popup.ItemLinks)
                            {
                                BarItem popupItem = linkItem.Item;
                                CtrlAction ctrl = popupItem.Tag as CtrlAction;
                                if (ctrl != null)
                                {
                                    popupItem.Enabled = ctrl.Enable();
                                    if ((popupItem as BarCheckItem) != null)
                                    {
                                        (popupItem as BarCheckItem).Checked = ctrl.Check() == CheckState.Checked;
                                    }
                                    if ((popupItem as BarButtonItem) != null)
                                    {
                                        (popupItem as BarButtonItem).Down = ctrl.Check() == CheckState.Checked;
                                    }
                                }
                            }
                        };
                        ((result as BarButtonItem)).DropDownControl = popup;

                        foreach (XmlNode dropItemNode in itemNode.ChildNodes)
                        {
                            XMLItem xmlDropItem = XMLItem.GetXMLItem(dropItemNode);
                            BarItem barDropItem = XMLItem.CreateBarItem(dropItemNode, ribbon, popup.ItemLinks, ctrlActions);
                        }
                        break;
                    case XMLItemName.ButtonDropDownAct:
                        (result as BarButtonItem).ButtonStyle = BarButtonStyle.DropDown;
                        (result as BarButtonItem).ActAsDropDown = true;
                        break;
                    case XMLItemName.ButtonGroup:
                        break;
                    case XMLItemName.Check:
                        if (xmlItem.ItemChecked != "")
                        {
                            (result as BarCheckItem).CheckBoxVisibility = CheckBoxVisibility.BeforeText;
                        }
                        break;
                    case XMLItemName.ComboBoxEdit:
                        BarEditItem comboBoxEdit = result as BarEditItem;
                        RepositoryItemComboBox repository = new RepositoryItemComboBox();
                        //测试
                        repository.Items.AddRange(new object[] { "cccc", "eeee" });
                        repository.TextEditStyle = TextEditStyles.DisableTextEditor;
                        comboBoxEdit.Edit = repository;
                        comboBoxEdit.EditValue = repository.Items[0].ToString();
                        comboBoxEdit.Width = 100;
                        break;
                    case XMLItemName.ListItem:
                        break;
                    case XMLItemName.RibbonGallery:
                        break;
                    case XMLItemName.SkinRibbonGallery:
                        SkinHelper.InitSkinGallery((result as SkinRibbonGalleryBarItem));
                        break;
                    case XMLItemName.StaticText:
                        break;
                    case XMLItemName.TextEdit:
                        BarEditItem textEditItem = result as BarEditItem;
                        RepositoryItemTextEdit repositoryText = new RepositoryItemTextEdit();
                        textEditItem.Edit = repositoryText;
                        break;
                    case XMLItemName.ToggleSwtich:
                        break;
                    default: break;
                }

            }
            return result;
        }
    }
}
