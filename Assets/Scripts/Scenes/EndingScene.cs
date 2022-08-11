using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Ending;
        Managers.UI.ShowPopupUI<UI_SelectNpc>();
    }

    public override void Clear()
    {

    }
}
