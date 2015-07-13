using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using DevExpress.XtraBars.Ribbon;

namespace HuaBo.Gis.Desktop.XML
{
    public class XMLPageCategory
    {


        /// <summary>
        /// 标签名
        /// </summary>
        public const string Name = "category";
        /// <summary>
        /// 所属窗口
        /// </summary>
        public static string Form = "form";

        public XmlNode XmlNode { get; set; }
        public string CategoryForm { get; set; }

        private XMLPageCategory() { }
        public XMLPageCategory(XmlNode xmlNode)
        {
            XmlNode = xmlNode;
        }

        private static XMLPageCategory GetXMLCategory(XmlNode xmlNode)
        {
            XMLPageCategory category = new XMLPageCategory(xmlNode);
            if (XMLManager.GetNodeType(xmlNode) == XMLNodeType.PageCategory)
            {
                category = null;
            }
            else
            {
                category.CategoryForm = NodeAttr.GetSetNodeAttrValue(xmlNode, Form);
            }
            return category;
        }

        public static RibbonPageCategory CreateCategory(XmlNode xmlNode)
        {
            RibbonPageCategory pageCategory = new RibbonPageCategory();
            pageCategory.AutoStretchPageHeaders = true;
            if (XMLManager.GetNodeType(xmlNode) != XMLNodeType.PageCategory)
            {
                pageCategory = null;
            }
            else
            {
                //只是用来赋空的值而已
                GetXMLCategory(xmlNode);
            }
            return pageCategory;
        }

       
    }
}
