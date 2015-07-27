using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HuaBo.Gis.Desktop
{
    public class NodeAttr
    {
        /// <summary>
        /// 得到某个XMLNode的值，如果该属性没有，则添加，并赋值为value
        /// </summary>
        /// <param name="xmlNode"></param>
        /// <param name="attrName"></param>
        /// <returns></returns>
        public static string GetOrDefaultNodeAttrValue(XmlNode xmlNode, string attrName, string value = "")
        {
            string result = "";
            if (xmlNode.Attributes[attrName] == null)
            {
                XmlElement xmlElement = xmlNode as XmlElement;
                xmlElement.SetAttribute(attrName, value);
            }
            else
            {
                result = xmlNode.Attributes[attrName].Value;
            }
            return result;
        }

        /// <summary>
        /// 获取xmlNode某属性的值
        /// </summary>
        /// <param name="xmlNode"></param>
        /// <param name="attrName"></param>
        /// <returns></returns>
        public static string GetAttrValue(XmlNode xmlNode, string attrName)
        {
            string result = "";
            if (xmlNode.Attributes[attrName] == null)
            {
            }
            else
            {
                result = xmlNode.Attributes[attrName].Value;
            }
            return result;
        }
    }
}
