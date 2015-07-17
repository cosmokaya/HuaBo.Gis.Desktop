using DevExpress.XtraBars.Ribbon;
using HuaBo.Gis.Desktop.XML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HuaBo.Gis.Interfaces;

namespace HuaBo.Gis.Desktop
{
    public class XMLToPageCategory
    {
        public static RibbonPageCategory Create(string formType, string categoryName, XmlNode xmlNode)
        {
            RibbonPageCategory result = null;
            foreach (XmlNode item in xmlNode.ChildNodes)
            {
                if (XMLManager.GetNodeType(item) == XMLNodeType.Ribbon)
                {
                    //层层遍历
                    foreach (XmlNode categoryNode in item.ChildNodes)
                    {
                        if (XMLManager.GetNodeType(categoryNode) != XMLNodeType.PageCategory)
                        { continue; }
                        if (categoryNode.Attributes[XMLPageCategory.Form].Value != formType)
                        { continue; }
                        RibbonPageCategory category = XMLPageCategory.CreateCategory(categoryNode);
                        if (category == null) { continue; }

                        foreach (XmlNode pageNode in categoryNode.ChildNodes)
                        {
                            RibbonPage page = XMLToPage.CreatePage(pageNode);
                            if (page != null)
                            {
                                category.Pages.Add(page);
                            }
                        }
                        category.Text = categoryName;
                        result = category;
                        break;
                    }
                }
            }
            return result;
        }

    }
}
