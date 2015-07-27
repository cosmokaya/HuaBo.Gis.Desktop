using DevExpress.XtraBars.Ribbon;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="formType"></param>
        /// <param name="categoryName"></param>
        /// <param name="xmlNode">输入xml的ribbon节点</param>
        /// <param name="CtrlActions"></param>
        /// <returns></returns>
        //public static RibbonPageCategory Create(string formType, string categoryName, XmlNode xmlNode, Dictionary<string, CtrlAction> CtrlActions)
        //{
        //    RibbonPageCategory result = null;

        //    if (XMLManager.GetNodeType(xmlNode) == XMLNodeType.Ribbon)
        //    {
        //        //层层遍历
        //        foreach (XmlNode categoryNode in xmlNode.ChildNodes)
        //        {
        //            if (XMLManager.GetNodeType(categoryNode) != XMLNodeType.PageCategory)
        //            { continue; }
        //            if (categoryNode.Attributes[XMLPageCategory.Form].Value != formType)
        //            { continue; }
        //            RibbonPageCategory category = XMLPageCategory.CreateCategory(categoryNode);
        //            if (category == null) { continue; }

        //            XMLToPage xmlToPage = new XMLToPage(categoryNode, CtrlActions);
        //            xmlToPage.CreateRibbonOrCategory(category);

        //            category.Text = categoryName;
        //            result = category;
        //            break;
        //        }
        //    }
        //    return result;
        //}

    }
}
