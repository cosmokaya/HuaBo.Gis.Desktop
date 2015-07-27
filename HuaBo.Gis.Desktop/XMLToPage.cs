using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraBars.Ribbon;
using HuaBo.Gis.Interfaces;

namespace HuaBo.Gis.Desktop
{
    public class XMLToPage
    {
        private static RibbonControl Ribbon;
        private XmlNode m_xmlNode;
        private Dictionary<string, CtrlAction> m_ctrlActions;

        static XMLToPage()
        {
            Ribbon = GisApp.ActiveApp.FormMain.RibbonView;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlNode">XmlNode的子节点必须全部为Page</param>
        /// <param name="ctrlActions"></param>
        public XMLToPage(XmlNode xmlNode, Dictionary<string, CtrlAction> ctrlActions)
        {
            if (Ribbon == null)
            {
                System.Windows.Forms.MessageBox.Show("先给静态变量Ribbon赋值！");
            }

            if (XMLManager.GetNodeType(xmlNode) == XMLNodeType.Ribbon || XMLManager.GetNodeType(xmlNode) == XMLNodeType.PageCategory)
            {
                m_xmlNode = xmlNode;
            }


            m_ctrlActions = ctrlActions;
        }

        /// <summary>
        /// 创建非基于窗口的Page，这种都不要放到Category下面
        /// </summary>
        public void CreateRibbonOrCategory(RibbonPageCategory category = null)
        {
            //page-pagegroup-item-dropdown
            foreach (XmlNode pageNode in m_xmlNode)
            {
                if (XMLManager.GetNodeType(pageNode) == XMLNodeType.Page)
                {
                    RibbonPage page = CreatePage(pageNode);
                    if (category != null)
                    {
                        category.Pages.Add(page);
                    }
                    CreateGroupAndItems(page, pageNode);
                }
            }

        }

        public static RibbonPage CreatePage(XmlNode pageNode)
        {
            RibbonPage page = XMLPage.CreatePage(pageNode);
            if (page == null) { return null; }
            Ribbon.Pages.Add(page);//必须在这个时候就加载进来，不然会出现问题。。
            return page;
        }


        /// <summary>
        /// 创建Page下面的内容
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageNode"></param>
        /// <param name="form"></param>
        /// <param name="isRun"></param>
        private void CreateGroupAndItems(RibbonPage page, XmlNode pageNode)
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
                    //todo:test
                    BarItem barItem = XMLBarItem.CreateBarItem(itemNode, group.ItemLinks, m_ctrlActions);
                }
            }
        }



    }
}
