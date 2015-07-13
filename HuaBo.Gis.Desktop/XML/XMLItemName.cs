using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuaBo.Gis.Desktop.XML
{
    public class XMLItemName
    {
        //普通的Item
        public static string Button = "button";
        //有下拉菜单的
        public static string ButtonDropDown = "dropdown";
        //有下拉菜单的,但是只用来显示下拉菜单，不能自己点击
        public static string ButtonDropDownAct = "dropdownact";
        //有下拉菜单的,但是只用来显示下拉菜单，不能自己点击
        public static string ButtonCheckDropDown = "checkdropdown";
        //CheckItem
        public static string Check = "check";
        //ToggleSwtichItem
        public static string ToggleSwtich = "toggle";
        public static string StaticText = "static";
        public static string EditItem = "edit";
        public static string ListItem = "list";
        public static string RibbonGallery = "gallery";
        public static string ButtonGroup = "btngroup";
        public static string SkinRibbonGallery = "skin";
    }
}
