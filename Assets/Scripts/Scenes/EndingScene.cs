using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Ending;

        if (Managers.Data.UserData.user.extraStat == 8)
        {
            Managers.UI.ShowPopupUI<UI_StageClear>();
        }
        else
        {
            Managers.UI.ShowPopupUI<UI_SelectNpc>();
        }
    }

    public override void Clear()
    {

    }
}
