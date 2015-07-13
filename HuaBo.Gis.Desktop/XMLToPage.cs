using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraBars.Ribbon;
using HuaBo.Gis.Desktop.XML;
using HuaBo.Gis.Interfaces;

namespace HuaBo.Gis.Desktop
{
    public class XMLToPage
    {
        private static RibbonControl Ribbon;
        private static XmlDocument m_doc;
        private static Dictionary<string, CtrlAction> m_ctrlActions;

        /// <summary>
        /// 所有Page添加到Ribbon中
        /// </summary>
        /// <param name="ribbon"></param>
        /// <param name="doc"></param>
        /// <param name="ctrlActions"></param>
        public static void Parse(RibbonControl ribbon, XmlDocument doc, Dictionary<string, CtrlAction> ctrlActions)
        {
            Ribbon = ribbon;
            m_doc = doc;
            m_ctrlActions = ctrlActions;

            CreatePageNoForm(m_doc.DocumentElement);
        }

        /// <summary>
        /// 创建非基于窗口的Page，这种都不要放到Category下面
        /// </summary>
        private static void CreatePageNoForm(XmlElement rootElement)
        {
            foreach (XmlNode item in rootElement.ChildNodes)
            {
                if (XMLManager.GetNodeType(item) == XMLNodeType.Ribbon)
                {
                    //page-pagegroup-item-dropdown
                    foreach (XmlNode pageNode in item.ChildNodes)
                    {
                        RibbonPage page = CreatePage(pageNode);
                    }
                }
            }
        }

        public static RibbonPage CreatePage(XmlNode pageNode)
        {
            RibbonPage page = XMLPage.CreatePage(pageNode);
            if (page == null) { return null; }
            Ribbon.Pages.Add(page);//必须在这个时候就加载进来，不然会出现问题。。
            CreateGroupAndItems(page, pageNode);

            return page;
        }


        /// <summary>
        /// 创建Page下面的内容
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageNode"></param>
        /// <param name="form"></param>
        /// <param name="isRun"></param>
        private static void CreateGroupAndItems(RibbonPage page, XmlNode pageNode)
        {
            //每个page对应的插件
            //2.pagegroup-item-dropdown
            foreach (XmlNode groupnode in pageNode.ChildNodes)
            {
                RibbonPageGroup group = XMLPageGroup.CreatePageGroup(groupnode);
                if (group == null) { continue; }
                page.Groups.Add(group);

                foreach (XmlNode itemNode in groupnode.ChildNodes)
                {
                    //先解析此Item，如果标签名是Items，则转换为
                    XMLItem.CreateBarItem(itemNode, Ribbon, group.ItemLinks, m_ctrlActions);
                }
            }
        }



    }
}
