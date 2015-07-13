using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace HuaBo.Gis.Desktop.XML
{
    public class XMLDockPanel
    {
        public const string Name = "dockpanel";
        public static string DockStyleStr = "style";
        /// <summary>
        /// 标题
        /// </summary>
        public static string Text = "text";
        /// <summary>
        /// 是否可见
        /// </summary>
        public static string Visible = "visible";
        /// <summary>
        /// 绑定的窗体
        /// </summary>
        public static string Form = "form";
        /// <summary>
        /// Dll的路径
        /// </summary>
        public static string DllPath = "dllpath";
        /// <summary>
        /// 绑定的类
        /// </summary>
        public static string BindControl = "control";
        /// <summary>
        /// float时的位置
        /// </summary>
        public static string FloatLocation = "float";
        /// <summary>
        /// float时的Size
        /// </summary>
        public static string Size = "size";

        private string ItemName { get; set; }
        private string ItemDockStyle { get; set; }
        public string ItemVisible { get; set; }
        public string ItemText { get; set; }
        public string ItemForm { get; set; }
        public string ItemDllPath { get; set; } //可以去掉
        public string ItemBindControl { get; set; }
        public string ItemFloatLocation { get; set; }
        public string ItemSize { get; set; }
        public XmlNode XmlNode { get; set; }

        public XMLDockPanel(XmlNode xmlNode)
        { XmlNode = xmlNode; }

        public static XMLDockPanel GetXMLDockPanel(XmlNode xmlNode)
        {
            XMLDockPanel item = new XMLDockPanel(xmlNode);
            item.ItemName = xmlNode.Name;
            item.ItemDockStyle = NodeAttr.GetSetNodeAttrValue(xmlNode, DockStyleStr, "");
            item.ItemText = NodeAttr.GetSetNodeAttrValue(xmlNode, Text, Guid.NewGuid() + "");
            item.ItemVisible = NodeAttr.GetSetNodeAttrValue(xmlNode, Visible, "true");
            item.ItemForm = NodeAttr.GetSetNodeAttrValue(xmlNode, Form, "");
            //
            item.ItemDllPath = NodeAttr.GetSetNodeAttrValue(xmlNode, DllPath, "");
            item.ItemBindControl = NodeAttr.GetSetNodeAttrValue(xmlNode, BindControl);
            item.ItemFloatLocation = NodeAttr.GetSetNodeAttrValue(xmlNode, FloatLocation, "0,0");
            item.ItemSize = NodeAttr.GetSetNodeAttrValue(xmlNode, Size, "0,0");
            return item;
        }


        public static DockPanel CreateDockPanel(XMLDockPanel xmlItem, DockManager dockManager)
        {
            DockPanel dockPanel = null;
            dockPanel = AddDockPanel(GetDockStyleFormXml(xmlItem.ItemDockStyle), dockManager);
            if (dockPanel.Dock == DockingStyle.Float)
            {
                int x = Convert.ToInt32(xmlItem.ItemFloatLocation.Split(",".ToCharArray()).ToList()[0]);
                int y = Convert.ToInt32(xmlItem.ItemFloatLocation.Split(",".ToCharArray()).ToList()[1]);
                dockPanel.FloatLocation = new System.Drawing.Point(x, y);
                int width = Convert.ToInt32(xmlItem.ItemSize.Split(",".ToCharArray()).ToList()[0]);
                int height = Convert.ToInt32(xmlItem.ItemSize.Split(",".ToCharArray()).ToList()[1]);
                dockPanel.Size = new System.Drawing.Size();
            }
            else
            {
                dockPanel.FloatLocation = dockPanel.Location;
                dockPanel.FloatSize = dockPanel.Size;
            }

            dockPanel.Name = xmlItem.ItemBindControl;
            dockPanel.Text = xmlItem.ItemText;
            dockPanel.Visibility = xmlItem.ItemVisible == "true" ? DockVisibility.Visible : DockVisibility.Hidden;
            //dockPanel.Tag = new { Form = xmlItem.ItemForm, Dll = xmlItem.ItemDllPath, Control = xmlItem.ItemBindControl };

            dockPanel.ControlContainer.Controls.Add(CreateControl(xmlItem.ItemBindControl, xmlItem.ItemDllPath));
            return dockPanel;
        }

        //todo:这个还是要自动解析的，因为所有控件都是没有参数的构造函数。以后在考虑
        internal static XtraUserControl CreateControl(string controlName, string dllPath, object data = null)
        {
            System.Reflection.Assembly asmb = System.Reflection.Assembly.LoadFrom(dllPath);
            Type t = asmb.GetType(controlName);
            XtraUserControl result = null;
            if (data == null)
            {
                //result = Activator.CreateInstance<XtraUserControl>();
                result = Activator.CreateInstance(t) as XtraUserControl;
            }
            else
            {
                result = Activator.CreateInstance(t, new object[] { data }) as XtraUserControl;
            }
            result.Dock = DockStyle.Fill;
            return result;
        }


        public static DockPanel AddDockPanel(DockingStyle dockingStyle, DockManager dockManager)
        {
            DockPanel dockpanel = null;
            DockPanel firstPanel = null;
            try
            {
                for (Int32 i = 0; i < dockManager.RootPanels.Count; i++)
                {
                    if (dockManager.RootPanels[i].Dock == dockingStyle)
                    {
                        firstPanel = dockManager.RootPanels[i];
                        break;
                    }
                }
                if (firstPanel != null)
                {
                    if (firstPanel.Dock == DockingStyle.Left)
                    {
                        dockpanel = firstPanel.AddPanel();
                        dockpanel.Dock = DockingStyle.Fill;
                        firstPanel.Dock = DockingStyle.Fill;
                        dockpanel.DockTo(firstPanel);
                    }
                    else if (firstPanel.Dock != DockingStyle.Float)
                    {
                        dockpanel = firstPanel.AddPanel();
                        dockpanel.Dock = DockingStyle.Fill;
                        firstPanel.Dock = DockingStyle.Fill;
                        dockpanel.DockAsTab(firstPanel);
                    }
                }
                else
                {
                    dockpanel = dockManager.AddPanel(dockingStyle);
                    //设定默认大小
                    //if (dockingStyle == DockingStyle.Left)
                    //{ dockpanel.Width = 250; }
                    //else
                    //{ dockpanel.Width = 300; }

                    //dockpanel.FloatSize = new Size(300, 600);
                }
            }
            catch (Exception ex)
            {
            }
            return dockpanel;
        }


        /// <summary>
        /// 根据style的text获取
        /// top,bottom,left,right,float
        /// </summary>
        /// <param name="style"></param>
        /// <returns></returns>
        private static DockingStyle GetDockStyleFormXml(string style)
        {
            DockingStyle dockingStyle = DockingStyle.Float;
            switch (style)
            {
                case "float":
                    dockingStyle = DockingStyle.Float;
                    break;
                case "top":
                    dockingStyle = DockingStyle.Top;
                    break;
                case "bottom":
                    dockingStyle = DockingStyle.Bottom;
                    break;
                case "left":
                    dockingStyle = DockingStyle.Left;
                    break;
                case "right":
                    dockingStyle = DockingStyle.Right;
                    break;
                default:
                    dockingStyle = DockingStyle.Float;
                    break;
            }
            return dockingStyle;
        }
    }
}
