using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HuaBo.Gis.Desktop
{
    internal class XMLRepositoryItem : XMLBase
    {
        static string ItemValue = "value";
        //暂时没用
        static string ItemText = "text";
        //static string ItemIndex = "index";//直接根据顺序排序就OK了
        //public object Value { get; set; }
        private XMLRepositoryItem()
        {
        }

        public static object GetValue(XmlNode xmlNode)
        {
            object result = null;
            if (xmlNode.Name.ToLower() == XMLCommandType.RepositoryItem)
            {
                result = NodeAttr.GetOrDefaultNodeAttrValue(xmlNode, ItemValue, "");
            }
            return result;
        }

    }
}
