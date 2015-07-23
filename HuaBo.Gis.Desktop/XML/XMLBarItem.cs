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

        public XMLBarItem(XmlNode xmlNode)
        { XmlNode = xmlNode; }

        public static XMLBarItem GetXMLItem(XmlNode xmlNode)
        {
            XMLBarItem item = new XMLBarItem(xmlNode);
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
            XMLBarItem xmlItem = XMLBarItem.GetXMLItem(itemNode);
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
                    //一共在2个地方重新刷新了CtrlAction的Form属性。1,Popopu弹出前2.按钮点击前（暂定）
                    if ((result as BarEditItem) != null)
                    {
                        (result as BarEditItem).EditValueChanged += (m, n) =>
                        {
                            //ctrlAction.Form = GisApp.ActiveApp.FormMain.ActiveForm;
                            ctrlAction.Run();
                        };
                    }
                    else
                    {
                        result.ItemClick += (m, n) =>
                        {
                            //ctrlAction.Form = GisApp.ActiveApp.FormMain.ActiveForm;
                            ctrlAction.Run();
                        };
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
                    case XMLCommandType.Button://普通button,但是带checked属性
                        CreateBarButtonItem(result, xmlItem);
                        break;
                    case XMLCommandType.ButtonCheckDropDown://判断如果有子菜单的情况 XMLItemName.ButtonCheckDropDown,暂时用不到，等换了新版本再说
                        CreateBarButtonCheckDropItem(result, xmlItem);
                        break;
                    case XMLCommandType.ButtonDropDown:
                        CreateBarButtonDrop(result, xmlItem, ribbon, ctrlActions);
                        break;
                    case XMLCommandType.ButtonDropDownAct:
                        CreateBarDropActItem(result, xmlItem);
                        break;
                    case XMLCommandType.ButtonGroup:
                        break;
                    case XMLCommandType.Check:
                        CreateBarCheckItem(result, xmlItem);
                        break;
                    case XMLCommandType.ComboBoxEdit:
                        CreateBarComboBoxEditItem(result, xmlItem);
                        break;
                    case XMLCommandType.RibbonGallery:
                        break;
                    case XMLCommandType.SkinRibbonGallery:
                        CreateSkinRibbonGalleryBarItem(result, xmlItem);
                        break;
                    case XMLCommandType.StaticText:
                        break;
                    case XMLCommandType.TextEdit:
                        CreateBarTextEditItem(result, xmlItem);
                        break;
                    case XMLCommandType.ToggleSwtich:
                        break;
                    default: break;
                }

            }
            return result;
        }

        //普通的Button，但是包含Check类型
        private static BarButtonItem CreateBarButtonItem(BarItem barItem, XMLBarItem xmlItem)
        {
            BarButtonItem result = barItem as BarButtonItem;
            try
            {
                result = barItem as BarButtonItem;
                if (xmlItem.ItemChecked != "")
                {
                    result.ButtonStyle = BarButtonStyle.Check;
                    if (xmlItem.ItemChecked == "true")
                        result.Down = true;
                    else
                        result.Down = false;
                }
            }
            catch (Exception)
            {
                result = barItem as BarButtonItem;
            }

            return result;
        }

        //14.2的按钮，以后写
        private static BarButtonItem CreateBarButtonCheckDropItem(BarItem barItem, XMLBarItem xmlItem)
        {
            return null;
        }

        private static BarButtonItem CreateBarButtonDrop(BarItem barItem, XMLBarItem xmlItem, RibbonControl ribbon, Dictionary<string, CtrlAction> ctrlActions)
        {
            BarButtonItem result = barItem as BarButtonItem;
            try
            {
                result.ButtonStyle = BarButtonStyle.DropDown;
                PopupMenu popup = XMLPopupMenu.CreatePopupMenu(xmlItem.XmlNode, ribbon);
                result.DropDownControl = popup;

                foreach (XmlNode dropItemNode in xmlItem.XmlNode.ChildNodes)
                {
                    BarItem barDropItem = XMLBarItem.CreateBarItem(dropItemNode, ribbon, popup.ItemLinks, ctrlActions);
                }
            }
            catch (Exception)
            {
                result = barItem as BarButtonItem;
            }
            return result;
        }

        //应该有下拉的，还没写
        private static BarButtonItem CreateBarDropActItem(BarItem barItem, XMLBarItem xmlItem)
        {
            BarButtonItem result = barItem as BarButtonItem;
            try
            {
                result.ButtonStyle = BarButtonStyle.DropDown;
                result.ActAsDropDown = true;
            }
            catch (Exception)
            {
                result = barItem as BarButtonItem;
            }
            return result;
        }

        private static BarButtonGroup CreateBarButtonGroup(BarItem barItem, XMLBarItem xmlItem)
        {
            return null;
        }

        private static BarCheckItem CreateBarCheckItem(BarItem barItem, XMLBarItem xmlItem)
        {
            BarCheckItem result = barItem as BarCheckItem;
            try
            {
                result.CheckBoxVisibility = CheckBoxVisibility.BeforeText;
            }
            catch (Exception)
            {
                result = barItem as BarCheckItem;
            }
            return result;
        }

        private static BarEditItem CreateBarComboBoxEditItem(BarItem barItem, XMLBarItem xmlItem)
        {
            BarEditItem result = barItem as BarEditItem;
            try
            {
                RepositoryItemComboBox repository = new RepositoryItemComboBox();
                //todo：未完全完成
                repository.Items.AddRange(new object[] { "cccc", "eeee" });
                repository.TextEditStyle = TextEditStyles.DisableTextEditor;
                result.Edit = repository;
                result.EditValue = repository.Items[0].ToString();
                result.Width = 100;
            }
            catch (Exception)
            {
                result = barItem as BarEditItem;
            }
            return result;
        }

        private static BarEditItem CreateBarTextEditItem(BarItem barItem, XMLBarItem xmlItem)
        {
            BarEditItem result = barItem as BarEditItem;
            try
            {
                RepositoryItemTextEdit repositoryText = new RepositoryItemTextEdit();
                result.Edit = repositoryText;
            }
            catch (Exception)
            {
                result = barItem as BarEditItem;
            }
            return result;
        }

        private static RibbonGalleryBarItem CreateRibbonGalleryBarItem(BarItem barItem, XMLBarItem xmlItem)
        {
            return null;
        }

        private static SkinRibbonGalleryBarItem CreateSkinRibbonGalleryBarItem(BarItem barItem, XMLBarItem xmlItem)
        {
            SkinRibbonGalleryBarItem result = barItem as SkinRibbonGalleryBarItem;
            try
            {
                SkinHelper.InitSkinGallery(result);
            }
            catch (Exception)
            {
                GisApp.ActiveApp.Output.Warning("SkinRibbonGalleryBarItem初始化失败！");
            }
            return result;
        }
        private static BarStaticItem CreateBarStaticItem(BarItem barItem, XMLBarItem xmlItem)
        {
            return null;
        }


    }
}
