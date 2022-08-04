using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Ending;
        UI_Ending endingScene = Managers.UI.ShowSceneUI<UI_Ending>();
    }

    public override void Clear()
    {

    }
}
