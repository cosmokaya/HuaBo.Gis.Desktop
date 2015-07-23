using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HuaBo.Gis.Desktop.XML
{
    public class XMLBarButtonItem : XMLBarItem
    {
        private XmlNode m_xmlNode;

        public XMLBarButtonItem(XmlNode xmlNode)
            : base(xmlNode)
        {
            m_xmlNode = xmlNode;
        }
    }
}
