using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayEndingScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.ReplayEnding;
        Managers.UI.ShowPopupUI<UI_ReplayEnding>();
    }

    public override void Clear()
    {
        
    }
}
