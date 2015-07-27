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

namespace HuaBo.Gis.Desktop
{
    public class XMLBarItem
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
        /// 绑定的类
        /// </summary>
        public static string Class = "class";
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
        internal string ItemText { get; set; }
        internal string ItemVisible { get; set; }
        internal string ItemBindClass { get; set; }
        internal string ItemRibbonStyle { get; set; }
        internal string ItemImage { get; set; }
        internal string ItemBeginGroup { get; set; }
        public XmlNode XmlNode { get; set; }

        public BarItem BarItem { get; set; }

        public BarItemLinkCollection ItemLinks { get; set; }

        internal Dictionary<string, CtrlAction> CtrlActions { get; set; }

        internal RibbonControl Ribbon { get; set; }

        public XMLBarItem(XmlNode xmlNode, BarItemLinkCollection itemlinks, Dictionary<string, CtrlAction> ctrlActions)
        {
            XmlNode = xmlNode;
            ItemLinks = itemlinks;
            CtrlActions = ctrlActions;

            this.ItemName = xmlNode.Name;
            this.ItemText = NodeAttr.GetOrDefaultNodeAttrValue(xmlNode, Text, Guid.NewGuid() + "");
            this.ItemVisible = NodeAttr.GetOrDefaultNodeAttrValue(xmlNode, Visible, "true");
            this.ItemBindClass = NodeAttr.GetOrDefaultNodeAttrValue(xmlNode, Class, "");
            this.ItemRibbonStyle = NodeAttr.GetOrDefaultNodeAttrValue(xmlNode, RibbonStyle, "normal");
            this.ItemImage = NodeAttr.GetOrDefaultNodeAttrValue(xmlNode, Image, "");
            this.ItemBeginGroup = NodeAttr.GetOrDefaultNodeAttrValue(xmlNode, BeginGroup, "false");

        }

        public static BarItem CreateBarItem(XmlNode xmlNode, BarItemLinkCollection itemlinks, Dictionary<string, CtrlAction> ctrlActions)
        {
            XMLBarItem xmlBarItem = new XMLBarItem(xmlNode, itemlinks, ctrlActions);
            switch (xmlNode.Name.ToLower())
            {
                case XMLCommandType.Button:
                    xmlBarItem = new XMLBarButtonItem(xmlNode, itemlinks, ctrlActions);
                    break;
                case XMLCommandType.ButtonCheck:
                    xmlBarItem = new XMLBarButtonCheckItem(xmlNode, itemlinks, ctrlActions);
                    break;
                //case XMLCommandType.ButtonCheckDropDown:
                //    xmlBarItem = new XMLBarButtonItem(xmlNode, itemlinks);
                //    break;
                case XMLCommandType.ButtonDropDown:
                    xmlBarItem = new XMLBarButtonDropDownItem(xmlNode, itemlinks, ctrlActions);
                    break;
                //case XMLCommandType.ButtonDropDownAct:
                //    xmlBarItem = new XMLBarButtonItem(xmlNode, itemlinks);
                //    break;
                //case XMLCommandType.ButtonGroup:
                //    xmlBarItem = new XMLBarButtonItem(xmlNode, itemlinks);
                //    break;
                case XMLCommandType.Check:
                    xmlBarItem = new XMLBarCheckItem(xmlNode, itemlinks, ctrlActions);
                    break;
                case XMLCommandType.ComboBoxEdit:
                    xmlBarItem = new XMLComboBoxEditItem(xmlNode, itemlinks, ctrlActions);
                    break;
                //case XMLCommandType.RibbonGallery:
                //    xmlBarItem = new XMLBarButtonItem(xmlNode, itemlinks);
                //    break;
                case XMLCommandType.SkinRibbonGallery:
                    xmlBarItem = new XMLSkinRibbonGalleryItem(xmlNode, itemlinks, ctrlActions);
                    break;
                //case XMLCommandType.StaticText:
                //    xmlBarItem = new XMLBarButtonItem(xmlNode, itemlinks);
                //    break;
                case XMLCommandType.TextEdit:
                    xmlBarItem = new XMLTextEditItem(xmlNode, itemlinks, ctrlActions);
                    break;
                //case XMLCommandType.ToggleSwtich:
                //    xmlBarItem = new XMLBarButtonItem(xmlNode, itemlinks);
                //    break;
                default:
                    break;
            }

            xmlBarItem.BarItem = xmlBarItem.CreateBarItem();
            return xmlBarItem.BarItem;
        }


        protected virtual BarItem CreateBarItem()
        {
            return null;
        }

        /// <summary>
        /// BarItem的初始化...
        /// </summary>
        /// <param name="xmlItem"></param>
        /// <param name="ctrlAction"></param>
        /// <param name="itemlinks">包含BarItem的父类</param>
        /// <returns></returns>
        //public static BarItem CreateBarItem(XmlNode itemNode, RibbonControl ribbon, BarItemLinkCollection itemlinks, Dictionary<string, CtrlAction> ctrlActions)
        //{
        //    System.Reflection.Assembly asmb = System.Reflection.Assembly.LoadFrom(@"./DevExpress.XtraBars.v14.1.dll");
        //    XMLBarItem xmlItem = XMLBarItem.GetXMLItem(itemNode);
        //    Type t = asmb.GetType(xmlItem.ItemItemType);
        //    BarItem result = Activator.CreateInstance(t) as BarItem;

        //    if (result != null)
        //    {
        //        bool isBeginGroup = xmlItem.ItemBeginGroup == "true";
        //        itemlinks.Add(result, isBeginGroup);
        //        result.Name = Guid.NewGuid() + "";
        //        result.Caption = xmlItem.ItemText;
        //        result.Visibility = xmlItem.ItemVisible != "false" ? BarItemVisibility.Always : BarItemVisibility.Never;
        //        result.RibbonStyle = xmlItem.ItemRibbonStyle == "large" ? RibbonItemStyles.Large : RibbonItemStyles.Default;
        //        CtrlAction ctrlAction = ctrlActions.ContainsKey(xmlItem.ItemBindClass) ? ctrlActions[xmlItem.ItemBindClass] : null;
        //        if (ctrlAction != null)
        //        {
        //            ctrlAction.BarItem = result;
        //            result.Tag = ctrlAction;
        //            //一共在2个地方重新刷新了CtrlAction的Form属性。1,Popopu弹出前2.按钮点击前（暂定）
        //            if ((result as BarEditItem) != null)
        //            {
        //                (result as BarEditItem).EditValueChanged += (m, n) =>
        //                {
        //                    ctrlAction.Run();
        //                };
        //            }
        //            else
        //            {
        //                result.ItemClick += (m, n) =>
        //                {
        //                    ctrlAction.Run();
        //                };
        //            }
        //        }

        //        try
        //        {
        //            Bitmap bitMap = new Bitmap(xmlItem.ItemImage);
        //            result.Glyph = bitMap;
        //        }
        //        catch (Exception)
        //        {
        //        }
        //        switch (xmlItem.ItemName)
        //        {
        //            case XMLCommandType.Button://普通button,但是带checked属性
        //                CreateBarButtonItem(result, xmlItem);
        //                break;
        //            case XMLCommandType.ButtonCheckDropDown://判断如果有子菜单的情况 XMLItemName.ButtonCheckDropDown,暂时用不到，等换了新版本再说
        //                CreateBarButtonCheckDropItem(result, xmlItem);
        //                break;
        //            case XMLCommandType.ButtonDropDown:
        //                CreateBarButtonDrop(result, xmlItem, ribbon, ctrlActions);
        //                break;
        //            case XMLCommandType.ButtonDropDownAct:
        //                CreateBarDropActItem(result, xmlItem);
        //                break;
        //            case XMLCommandType.ButtonGroup:
        //                break;
        //            case XMLCommandType.Check:
        //                CreateBarCheckItem(result, xmlItem);
        //                break;
        //            case XMLCommandType.ComboBoxEdit:
        //                CreateBarComboBoxEditItem(result, xmlItem);
        //                break;
        //            case XMLCommandType.RibbonGallery:
        //                break;
        //            case XMLCommandType.SkinRibbonGallery:
        //                CreateSkinRibbonGalleryBarItem(result, xmlItem);
        //                break;
        //            case XMLCommandType.StaticText:
        //                break;
        //            case XMLCommandType.TextEdit:
        //                CreateBarTextEditItem(result, xmlItem);
        //                break;
        //            case XMLCommandType.ToggleSwtich:
        //                break;
        //            default: break;
        //        }

        //    }
        //    return result;
        //}


        //普通的Button，但是包含Check类型
        //private static BarButtonItem CreateBarButtonItem(BarItem barItem, XMLBarItem xmlItem)
        //{
        //    BarButtonItem result = barItem as BarButtonItem;
        //    try
        //    {
        //        result = barItem as BarButtonItem;
        //        if (xmlItem.ItemChecked != "")
        //        {
        //            result.ButtonStyle = BarButtonStyle.Check;
        //            if (xmlItem.ItemChecked == "true")
        //                result.Down = true;
        //            else
        //                result.Down = false;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        result = barItem as BarButtonItem;
        //    }

        //    return result;
        //}

        //14.2的按钮，以后写
        //private static BarButtonItem CreateBarButtonCheckDropItem(BarItem barItem, XMLBarItem xmlItem)
        //{
        //    return null;
        //}

        //private static BarButtonItem CreateBarButtonDrop(BarItem barItem, XMLBarItem xmlItem, RibbonControl ribbon, Dictionary<string, CtrlAction> ctrlActions)
        //{
        //    BarButtonItem result = barItem as BarButtonItem;
        //    try
        //    {
        //        result.ButtonStyle = BarButtonStyle.DropDown;
        //        PopupMenu popup = XMLPopupMenu.CreatePopupMenu(xmlItem.XmlNode, ribbon);
        //        result.DropDownControl = popup;

        //        foreach (XmlNode dropItemNode in xmlItem.XmlNode.ChildNodes)
        //        {
        //            BarItem barDropItem = XMLBarItem.CreateBarItem(dropItemNode, ribbon, popup.ItemLinks, ctrlActions);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        result = barItem as BarButtonItem;
        //    }
        //    return result;
        //}

        ////应该有下拉的，还没写
        //private static BarButtonItem CreateBarDropActItem(BarItem barItem, XMLBarItem xmlItem)
        //{
        //    BarButtonItem result = barItem as BarButtonItem;
        //    try
        //    {
        //        result.ButtonStyle = BarButtonStyle.DropDown;
        //        result.ActAsDropDown = true;
        //    }
        //    catch (Exception)
        //    {
        //        result = barItem as BarButtonItem;
        //    }
        //    return result;
        //}

        //private static BarButtonGroup CreateBarButtonGroup(BarItem barItem, XMLBarItem xmlItem)
        //{
        //    return null;
        //}

        //private static BarCheckItem CreateBarCheckItem(BarItem barItem, XMLBarItem xmlItem)
        //{
        //    BarCheckItem result = barItem as BarCheckItem;
        //    try
        //    {
        //        result.CheckBoxVisibility = CheckBoxVisibility.BeforeText;
        //    }
        //    catch (Exception)
        //    {
        //        result = barItem as BarCheckItem;
        //    }
        //    return result;
        //}

        //private static BarEditItem CreateBarComboBoxEditItem(BarItem barItem, XMLBarItem xmlItem)
        //{
        //    BarEditItem result = barItem as BarEditItem;
        //    try
        //    {
        //        RepositoryItemComboBox repository = new RepositoryItemComboBox();
        //        //todo：未完全完成
        //        repository.Items.AddRange(new object[] { "cccc", "eeee" });
        //        repository.TextEditStyle = TextEditStyles.DisableTextEditor;
        //        result.Edit = repository;
        //        result.EditValue = repository.Items[0].ToString();
        //        result.Width = 100;
        //    }
        //    catch (Exception)
        //    {
        //        result = barItem as BarEditItem;
        //    }
        //    return result;
        //}

        //private static BarEditItem CreateBarTextEditItem(BarItem barItem, XMLBarItem xmlItem)
        //{
        //    BarEditItem result = barItem as BarEditItem;
        //    try
        //    {
        //        RepositoryItemTextEdit repositoryText = new RepositoryItemTextEdit();
        //        result.Edit = repositoryText;
        //    }
        //    catch (Exception)
        //    {
        //        result = barItem as BarEditItem;
        //    }
        //    return result;
        //}

        //private static RibbonGalleryBarItem CreateRibbonGalleryBarItem(BarItem barItem, XMLBarItem xmlItem)
        //{
        //    return null;
        //}

        //private static SkinRibbonGalleryBarItem CreateSkinRibbonGalleryBarItem(BarItem barItem, XMLBarItem xmlItem)
        //{
        //    SkinRibbonGalleryBarItem result = barItem as SkinRibbonGalleryBarItem;
        //    try
        //    {
        //        SkinHelper.InitSkinGallery(result);
        //    }
        //    catch (Exception)
        //    {
        //        GisApp.ActiveApp.Output.Warning("SkinRibbonGalleryBarItem初始化失败！");
        //    }
        //    return result;
        //}
        //private static BarStaticItem CreateBarStaticItem(BarItem barItem, XMLBarItem xmlItem)
        //{
        //    return null;
        //}


    }
}
