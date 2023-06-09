using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : BaseScene
{
    PlayerController _male;
    PlayerController _female;

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Lobby;
        UI_LobbyScene lobbyScene = Managers.UI.ShowSceneUI<UI_LobbyScene>();
        Managers.Sound.Play("LobbyScene", Define.Sound.Bgm);

        // Set Characters
        _male = Managers.Object.SpawnPlayer("Player/Male");
        _male.transform.Translate(1f, 0, 0);
        _female = Managers.Object.SpawnPlayer("Player/Female");
        _female.transform.Translate(-0.5f, 0, 0);
    }

    private void Update()
    {
        #if UNITY_ANDROID
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        #endif
    }



    public override void Clear()
    {

    }
}
