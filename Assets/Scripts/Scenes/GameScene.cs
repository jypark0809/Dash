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
            player = Managers.Game.Spawn(Define.WorldObject.Player, "male");
        player.name = "Player";

        pc = player.GetOrAddComponent<PlayerController>();

        // Load Stage
        if (PlayerPrefs.GetInt("isAccessFirst") == 0)
        {
            stage = Managers.Game.Spawn(Define.WorldObject.Stage, $"Stages/Stage_0");
        }
        else
        {
            stage = Managers.Game.Spawn(Define.WorldObject.Stage, $"Stages/Stage_{Managers.Data.UserData.user.stage}");
        }
        

        // Load Finish Object
        GameObject finishObj = GameObject.Find("Finish");
        finish = finishObj.GetComponent<Finish>();
        
        // Load Background
        if (Managers.Data.UserData.user.stage == 0 || Managers.Data.UserData.user.stage == 1 ||
            Managers.Data.UserData.user.stage == 2 || Managers.Data.UserData.user.stage == 3)
        {
            Managers.Resource.Instantiate("BackGround/MainMap_Stage1");
            Managers.Resource.Instantiate("BackGround/SubMap_Stage1");
            mc1 = GameObject.Find("MainMap_Stage1").GetComponent<MapController>();
            mc2 = GameObject.Find("SubMap_Stage1").GetComponent<MapController>();
        }
        else if ((Managers.Data.UserData.user.stage == 4 || Managers.Data.UserData.user.stage == 5 ||
            Managers.Data.UserData.user.stage == 6))
        {
            Managers.Resource.Instantiate("BackGround/MainMap_Stage2");
            Managers.Resource.Instantiate("BackGround/SubMap_Stage2");
            mc1 = GameObject.Find("MainMap_Stage2").GetComponent<MapController>();
            mc2 = GameObject.Find("SubMap_Stage2").GetComponent<MapController>();
        }
        else if ((Managers.Data.UserData.user.stage == 7 || Managers.Data.UserData.user.stage == 8 ||
            Managers.Data.UserData.user.stage == 9))
        {
            Managers.Resource.Instantiate("BackGround/MainMap_Stage3");
            Managers.Resource.Instantiate("BackGround/SubMap_Stage3");
            mc1 = GameObject.Find("MainMap_Stage3").GetComponent<MapController>();
            mc2 = GameObject.Find("SubMap_Stage3").GetComponent<MapController>();
        }

        // mc1 = GameObject.Find("MainMap Group").GetComponent<MapController>();
        // mc2 = GameObject.Find("SubMap Group").GetComponent<MapController>();
        pc.stageController = stage.GetComponent<StageController>();
        pc.mapControllers[0] = mc1;
        pc.mapControllers[1] = mc2;

        

        if (Managers.Data.UserData.user.stage == 1 && PlayerPrefs.GetInt("isAccessFirst") != 0)
        {
            if (Managers.Data.UserData.user.gender == "male")
                Managers.UI.ShowPopupUI<UI_Prologue>("UI_Prologue_FeMale");
            else if (Managers.Data.UserData.user.gender == "female")
                Managers.UI.ShowPopupUI<UI_Prologue>("UI_Prologue_Male");
        }
        else if (Managers.Data.UserData.user.stage != 1 && PlayerPrefs.GetInt("isAccessFirst") != 0)
        {
            Managers.UI.ShowPopupUI<UI_Hint>();
        }

        if (PlayerPrefs.GetInt("isAccessFirst") == 0)
        {
            PlayerPrefs.SetInt("Tutorial", 0);
            Managers.UI.ShowPopupUI<UI_Hint>();
        }
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
            StopScrolling();
        }
    }

    public override void Clear()
    {

    }

    public void StopScrolling()
    {
        mc1._speed = 0;
        mc2._speed = 0;
        pc.stageController._speed = 0;
    }

    public void StartScrolling()
    {
        mc1._speed = 4;
        mc2._speed = 3;
        pc.stageController._speed = 4;
    }
}
