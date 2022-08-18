using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : BaseScene
{
    GameObject _male;
    GameObject _female;

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Lobby;
        UI_LobbyScene lobbyScene = Managers.UI.ShowSceneUI<UI_LobbyScene>();
        Managers.Sound.Play("LobbyScene", Define.Sound.Bgm);

        // Set Characters
        _male = Managers.Game.Spawn(Define.WorldObject.Player, "male");
        _male.transform.Translate(1f, 0, 0);
        _female = Managers.Game.Spawn(Define.WorldObject.Player, "female");
        _female.transform.Translate(-0.5f, 0, 0);

        // TODO : 테스트
        // Dictionary<int, Script> dict = Managers.Data.EndingDict;
    }

    public override void Clear()
    {

    }

    private void Update()
    {
    }
}
