using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using DevExpress.XtraBars.Ribbon;

namespace HuaBo.Gis.Desktop
{
    public class XMLPageGroup
    {
        /// <summary>
        /// 标签名
        /// </summary>
        public const string Name = "group";
        /// <summary>
        /// 标题
        /// </summary>
        public static string Text = "text";
        /// <summary>
        /// 是否可见
        /// </summary>
        public static string Visible = "visible";
        public string GroupText { get; set; }

        public string GroupVisible { get; set; }
        public XmlNode XmlNode { get; set; }
        private XMLPageGroup()
        { }
        public XMLPageGroup(XmlNode xmlNode)
        { XmlNode = xmlNode; }

        public static XMLPageGroup GetXMLPageGroup(XmlNode xmlNode)
        {
            XMLPageGroup page = new XMLPageGroup(xmlNode);
            if (xmlNode.Name != Name)
            {
                page = null;
            }
            else
            {
                page.GroupText = NodeAttr.GetOrDefaultNodeAttrValue(xmlNode, Text, Guid.NewGuid() + "");
                page.GroupVisible = NodeAttr.GetOrDefaultNodeAttrValue(xmlNode, Visible, "true");
            }
            return page;
        }

        public static RibbonPageGroup CreatePageGroup(XmlNode xmlNode)
        {
            RibbonPageGroup pageGroup = new RibbonPageGroup();
            pageGroup.ShowCaptionButton = false;
            if (XMLManager.GetNodeType(xmlNode) != XMLNodeType.PageGroup)
            {
                pageGroup = null;
            }
            else
            {
                XMLPageGroup xmlpageGroup = GetXMLPageGroup(xmlNode);
                pageGroup.Text = xmlpageGroup.GroupText;
                bool result = false;
                if (bool.TryParse(xmlpageGroup.GroupText, out result))
                {
                    pageGroup.Visible = result;
                }
            }
            return pageGroup;
        }

    }
}
