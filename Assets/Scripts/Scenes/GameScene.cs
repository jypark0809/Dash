using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    UI_GameScene gameScene;
    GameObject player;
    GameObject stage;
    PlayerController pc;
    MapController mc1;
    MapController mc2;
    Finish finish;

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        // Sound
        Managers.Sound.Play("GameScene", Define.Sound.Bgm);

        // Load UI
        gameScene = Managers.UI.ShowSceneUI<UI_GameScene>();

        // Load Player
        // TODO : 다른 함수로 빼기
        if (Managers.Data.UserData.user.gender == "male")
            player = Managers.Game.Spawn(Define.WorldObject.Player, "male");
        else if (Managers.Data.UserData.user.gender == "female")
            player = Managers.Game.Spawn(Define.WorldObject.Player, "female");
        else
            Debug.Log("Failed to load character : GameScene.cs");
        player.name = "Player";

        pc = player.GetOrAddComponent<PlayerController>();

        // Load Stage
        stage = Managers.Game.Spawn(Define.WorldObject.Stage, $"Stages/Stage_{Managers.Data.UserData.user.stage}");

        // Load Finish Object
        GameObject finishObj = GameObject.Find("Finish");
        finish = finishObj.GetComponent<Finish>();
        
        // Load Background
        mc1 = GameObject.Find("MainMap Group").GetComponent<MapController>();
        mc2 = GameObject.Find("SubMap Group").GetComponent<MapController>();
        pc.stageController = stage.GetComponent<StageController>();
        pc.mapControllers[0] = mc1;
        pc.mapControllers[1] = mc2;

        // Load Data
        UserData user = Managers.Data.UserData;
    }

    private void Update()
    {
        if(pc._state != Define.PlayerState.Clear)
        {
            gameScene.Ratio = finish.CalculateDistance();
        }
        else
        {
            // Background 및 Stage 장애물 스크롤링 정지
            mc1._speed = 0;
            mc2._speed = 0;
            pc.stageController._speed = 0;
        }
    }

    public override void Clear()
    {

    }
}
