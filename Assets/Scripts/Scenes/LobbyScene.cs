using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Lobby;
        UI_LobbyScene lobbyScene = Managers.UI.ShowSceneUI<UI_LobbyScene>();
    }

    public override void Clear()
    {

    }
}
