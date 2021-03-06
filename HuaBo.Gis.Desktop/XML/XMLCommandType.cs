﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuaBo.Gis.Desktop
{
    /// <summary>
    /// 其实完全可以根据接口转换来确定是啥类型
    /// </summary>
    public class XMLCommandType
    {
        //普通的Item
        public const string Button = "button";
        //普通的Item
        public const string ButtonCheck = "btncheck";
        //有下拉菜单的，也能自己点击
        public const string ButtonDropDown = "dropdown";
        //有下拉菜单的,但是只用来显示下拉菜单，不能自己点击
        public const string ButtonDropDownAct = "dropdownact";
        //有下拉菜单的,只用来显示下拉菜单，能自己点击，并且能check（14.2）
        public const string ButtonCheckDropDown = "checkdropdown";
        //CheckItem
        public const string Check = "check";
        //ToggleSwtichItem
        public const string ToggleSwtich = "toggle";
        public const string StaticText = "static";
        public const string RibbonGallery = "gallery";
        public const string ButtonGroup = "btngroup";
        public const string SkinRibbonGallery = "skin";
        //Combo编辑
        public const string ComboBoxEdit = "comboedit";
        //文本编辑
        public const string TextEdit = "textedit";

        public const string RepositoryItem = "item";

        /// <summary>
        /// ribbon
        /// </summary>
        public const string Ribbon = "ribbon";
        /// <summary>
        /// 是否开启新组
        /// </summary>
        public const string DockPanels = "dockpanels";
        /// <summary>
        /// 是否开启新组
        /// </summary>
        public const string PopupMenus = "popupmenus";

    }
}
