using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    UI_GameScene _gameSceneUI;
    GameObject _player;
    PlayerController _playerController;
    GameObject _stage;
    MapController _bgMain;
    MapController _bgSub;
    Finish finish;

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        // Sound
        Managers.Sound.Play("GameScene", Define.Sound.Bgm);

        // Load UI
        _gameSceneUI = Managers.UI.ShowSceneUI<UI_GameScene>();

        // Load Player Prefabs
        if (Managers.Data.UserData.user.gender == "male")
        {
            switch (PlayerPrefs.GetInt("maleCostume"))
            {
                case 0:
                    _player = Managers.Game.SpawnPlayer("Player/Male");
                    break;
                case 1:
                    _player = Managers.Game.SpawnPlayer("Player/Male_Custume1");
                    break;
            }
        }
        else if (Managers.Data.UserData.user.gender == "female")
        {
            switch (PlayerPrefs.GetInt("femaleCostume"))
            {
                case 0:
                    _player = Managers.Game.SpawnPlayer("Player/Female");
                    break;
                case 1:
                    _player = Managers.Game.SpawnPlayer("Player/Female_Custume1");
                    break;
            }
        }
        else
            _player = Managers.Game.SpawnPlayer("Player/Male");

        _player.name = "Player";
        _playerController = Managers.Game.Player;

        // Load Stage
        LoadStage();

        // Load Finish Object
        GameObject finishObj = GameObject.Find("Finish");
        finish = finishObj.GetComponent<Finish>();
        
        // Load Background
        if (Managers.Data.UserData.user.stage == 0 || Managers.Data.UserData.user.stage == 1 ||
            Managers.Data.UserData.user.stage == 2 || Managers.Data.UserData.user.stage == 3)
        {
            _bgMain = Managers.Game.SpawnBackgroundMap("BackGround/MainMap_Stage1", Define.WorldObject.MainBG).GetComponent<MapController>();
            _bgSub = Managers.Game.SpawnBackgroundMap("BackGround/SubMap_Stage1", Define.WorldObject.SubBG).GetComponent<MapController>();
        }
        else if ((Managers.Data.UserData.user.stage == 4 || Managers.Data.UserData.user.stage == 5 ||
            Managers.Data.UserData.user.stage == 6))
        {
            _bgMain = Managers.Game.SpawnBackgroundMap("BackGround/MainMap_Stage2", Define.WorldObject.MainBG).GetComponent<MapController>();
            _bgSub = Managers.Game.SpawnBackgroundMap("BackGround/SubMap_Stage2", Define.WorldObject.SubBG).GetComponent<MapController>();
        }
        else if ((Managers.Data.UserData.user.stage == 7 || Managers.Data.UserData.user.stage == 8 ||
            Managers.Data.UserData.user.stage == 9))
        {
            _bgMain = Managers.Game.SpawnBackgroundMap("BackGround/MainMap_Stage3", Define.WorldObject.MainBG).GetComponent<MapController>();
            _bgSub = Managers.Game.SpawnBackgroundMap("BackGround/SubMap_Stage3", Define.WorldObject.SubBG).GetComponent<MapController>();
        }

        _playerController.mapControllers[0] = _bgMain;
        _playerController.mapControllers[1] = _bgSub;

        

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
        if(_playerController._state != Define.PlayerState.Clear)
        {
            _gameSceneUI.Ratio = finish.CalculateDistance();
        }
        else
        {
            // Background �� Stage ��ֹ� ��ũ�Ѹ� ����
            StopScrolling();
        }
    }

    public override void Clear()
    {

    }

    public void StopScrolling()
    {
        _bgMain._speed = 0;
        _bgSub._speed = 0;
        Managers.Game.Stage.Speed = 0;
    }

    public void StartScrolling()
    {
        _bgMain._speed = 4;
        _bgSub._speed = 3;
        Managers.Game.Stage.Speed = 4;
    }

    void LoadStage()
    {
        if (PlayerPrefs.GetInt("isAccessFirst") == 0)
        {
            _stage = Managers.Game.SpawnStage($"Stages/Stage_0");
        }
        else
        {
            switch(PlayerPrefs.GetInt("difficulty"))
            {
                case 0:
                    _stage = Managers.Game.SpawnStage($"Stages/Easy_Stage_{Managers.Data.UserData.user.stage}");
                    break;
                case 1:
                    _stage = Managers.Game.SpawnStage($"Stages/Normal_Stage_{Managers.Data.UserData.user.stage}");
                    break;
                case 2:
                    _stage = Managers.Game.SpawnStage($"Stages/Hard_Stage_{Managers.Data.UserData.user.stage}");
                    break;
            }
        }
    }
}
