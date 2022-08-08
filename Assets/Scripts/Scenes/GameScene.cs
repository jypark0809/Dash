using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    GameObject player;
    PlayerController pc;
    MapController mc1;
    MapController mc2;
    AudioClip audioClip;

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        UI_GameScene gameScene = Managers.UI.ShowSceneUI<UI_GameScene>();
        Managers.Sound.Play("bgmtest", Define.Sound.Bgm);

        if (Managers.Data.UserData.user.gender == "male")
            player = Managers.Game.Spawn(Define.WorldObject.Player, "male");
        else if (Managers.Data.UserData.user.gender == "female")
            player = Managers.Game.Spawn(Define.WorldObject.Player, "female");
        else
            Debug.Log("Failed to load character : GameScene.cs");

        pc = player.GetOrAddComponent<PlayerController>();
        gameScene.SetPlayer(player);

        GameObject stage = Managers.Game.Spawn(Define.WorldObject.Stage, $"Stages/Stage_{Managers.Data.UserData.user.stage}");
        mc1 = GameObject.Find("SubBackground Group").GetComponent<MapController>();
        mc2 = GameObject.Find("Background Group").GetComponent<MapController>();

        pc.move = stage.GetComponent<Move>();
        pc.mapControllers[0] = mc1;
        pc.mapControllers[1] = mc2;

        UserData user = Managers.Data.UserData;
    }

    private void Update()
    {
        if(pc._state == Define.PlayerState.Clear)
        {
            mc1._speed = 0;
            mc2._speed = 0;
            pc.move._speed = 0;
        }
    }

    public override void Clear()
    {

    }
}
