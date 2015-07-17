using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuaBo.Gis.Desktop;
using HuaBo.Gis.Interfaces;

namespace HuaBo.Gis.Plugins
{

    [Export(typeof(CtrlAction))]
    public class NewSceneWindowAction : CtrlAction
    {

        public override void Run()
        {
            string sceneName = GisApp.ActiveApp.SelectNode.Name;

            //打开场景，假如说已经打开，则激活
            //否则，直接新建这个场景
            var document = GisApp.ActiveApp.FormMain.DocumentManager.View.Documents.Where(s =>
                s.Caption == sceneName && (s.Form as IFormScene) != null
            ).FirstOrDefault();

            IFormScene formScene = null;
            if (document != null)
            {
                formScene = document.Form as IFormScene;
                GisApp.ActiveApp.FormMain.DocumentManager.View.ActivateDocument(document.Control);
            }
            else
            {
                formScene = GisApp.ActiveApp.CreateFormScene(sceneName);
            }
            formScene.SceneControl.Scene.Workspace = GisApp.ActiveApp.Workspace;
            formScene.SceneControl.Scene.Open(sceneName);
        }
    }
}
