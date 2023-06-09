using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class TitleScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Title;

        Managers.Data.Init();
        Managers.Sound.Init();
        Managers.Game.Init();

        Managers.Sound.Play("StageClear", Define.Sound.Effect);
        Managers.UI.ShowSceneUI<UI_TitleScene>();
    }

    public override void Clear()
    {

    }
}
