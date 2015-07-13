using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuaBo.Gis.Interfaces
{
    public interface IForm
    {
        // 摘要:
        //     获取或者设置窗口的激活状态。
        //bool Actived { get; set; }
        //
        // 摘要:
        //     获取窗口能否被保存的状态。 一个布尔值，表示是否保存，true 表示该窗口能被保存，false 表示该窗口不能被保存。
        //
        // 返回结果:
        //     一个布尔值，表示是否保存，true 表示该窗口能被保存，false 表示该窗口不能被保存。
        //bool CanSave { get; }
        //
        // 摘要:
        //     获取或设置窗口是否全屏显示。
        //bool FullScreen { get; set; }
        //
        // 摘要:
        //     获取或设置窗口是否需要保存。
        //bool NeedSave { get; set; }
        //
        // 摘要:
        //     获取浮动窗口的状态栏。
        //IStatusBar StatusBar { get; }

        // 摘要:
        //     在单击地图、布局、场景保存按钮后，触发该保存事件。
        //event EventHandler Saved;
        //
        // 摘要:
        //     在单击地图、布局、场景保存按钮时，触发该保存事件。
        //event SavingEventHandler Saving;

        // 摘要:
        //     将窗体中的内容进行保存，如地图窗口中的内容保存到工作空间中，三维窗口中的内容保存到场景中等等。
        //
        // 返回结果:
        //     保存成功返回 true；否则返回 false。
        //bool Save();
        //
        // 摘要:
        //     将窗体内容进行保存。
        //
        // 参数:
        //   notify:
        //     是否提示保存。true 表示弹出对话框进行提示，false 表示不进行提示。
        //
        //   newWindow:
        //     判断窗体是否为新窗体，ture 表示是新窗体，false 表示不是新窗体。
        //
        // 返回结果:
        //     保存成功返回 true；否则返回 false。
        //bool Save(bool notify, bool newWindow);
        //
        // 摘要:
        //     将窗口中的内容另存到新的工作空间中。
        //
        // 返回结果:
        //     另存成功返回 true；否则返回 false。
        //bool SaveAs();
    }
}
