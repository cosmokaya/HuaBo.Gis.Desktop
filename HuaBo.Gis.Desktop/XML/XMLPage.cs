using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using DevExpress.XtraBars.Ribbon;

namespace HuaBo.Gis.Desktop
{
    public class XMLPage
    {
        /// <summary>
        /// 标签名
        /// </summary>
        public const string Name = "page";
        /// <summary>
        /// 标题
        /// </summary>
        public static string Text = "text";
        /// <summary>
        /// 是否可见
        /// </summary>
        public static string Visible = "visible";


        public string PageText { get; set; }
        public string PageVisible { get; set; }
        public XmlNode XmlNode { get; set; }
        private XMLPage()
        { }
        public XMLPage(XmlNode xmlNode)
        { XmlNode = xmlNode; }

        private static XMLPage GetXMLPage(XmlNode xmlNode)
        {
            XMLPage page = new XMLPage(xmlNode);
            if (XMLManager.GetNodeType(xmlNode) != XMLNodeType.Page)
            {
                page = null;
            }
            else
            {
                page.PageText = NodeAttr.GetOrDefaultNodeAttrValue(xmlNode, Text, Guid.NewGuid() + "");
                page.PageVisible = NodeAttr.GetOrDefaultNodeAttrValue(xmlNode, Visible, "true");
            }
            return page;
        }

        //XMLPage xmlpage
        public static RibbonPage CreatePage(XmlNode xmlNode)
        {
            RibbonPage page = new RibbonPage();
            if (XMLManager.GetNodeType(xmlNode) != XMLNodeType.Page)
            {
                page = null;
            }
            else
            {
                XMLPage xmlpage = GetXMLPage(xmlNode);
                page.Text = xmlpage.PageText;
                bool result = false;
                if (bool.TryParse(xmlpage.PageVisible, out result))
                {
                    page.Visible = result;
                }
            }
            return page;
        }
    }
}
