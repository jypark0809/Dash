using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    UI_GameScene _gameSceneUI;
    PlayerController _player;
    StageController _stage;
    MapController _bgMain;
    MapController _bgSub;
    Finish _finish;

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        // Sound
        Managers.Sound.Play("GameScene", Define.Sound.Bgm);

        // UI
        _gameSceneUI = Managers.UI.ShowSceneUI<UI_GameScene>();

        // Player
        if (Managers.Game.SaveData.gender == "male")
        {
            switch (PlayerPrefs.GetInt("maleCostume"))
            {
                case 0:
                    _player = Managers.Object.SpawnPlayer("Player/Male");
                    break;
                case 1:
                    _player = Managers.Object.SpawnPlayer("Player/Male_Custume1");
                    break;
            }
        }
        else if (Managers.Game.SaveData.gender == "female")
        {
            switch (PlayerPrefs.GetInt("femaleCostume"))
            {
                case 0:
                    _player = Managers.Object.SpawnPlayer("Player/Female");
                    break;
                case 1:
                    _player = Managers.Object.SpawnPlayer("Player/Female_Custume1");
                    break;
            }
        }
        else
        {
            // Tutorial
            _player = Managers.Object.SpawnPlayer("Player/Male");
        }
            
        _player.name = "Player";

        // Stage
        LoadStage();

        // Finish Object
        GameObject finishObj = GameObject.Find("Finish");
        _finish = finishObj.GetComponent<Finish>();
        Managers.Object.Finish = _finish;

        // Map
        LoadMap();

        // 각 스테이지에 해당하는 Tutorial/Prologue/Hint 스크립트 UI 생성
        if (Managers.Game.SaveData.stage == 1 && PlayerPrefs.GetInt("isAccessFirst") != 0)
        {
            if (Managers.Game.SaveData.gender == "male")
                Managers.UI.ShowPopupUI<UI_Prologue>("UI_Prologue_FeMale");
            else if (Managers.Game.SaveData.gender == "female")
                Managers.UI.ShowPopupUI<UI_Prologue>("UI_Prologue_Male");
        }
        else if (Managers.Game.SaveData.stage != 1 && PlayerPrefs.GetInt("isAccessFirst") != 0)
        {
            Managers.UI.ShowPopupUI<UI_Hint>();
        }

        if (PlayerPrefs.GetInt("isAccessFirst") == 0)
        {
            PlayerPrefs.SetInt("Tutorial", 0);
            Managers.UI.ShowPopupUI<UI_Hint>();
        }
    }

    void LoadStage()
    {
        if (PlayerPrefs.GetInt("isAccessFirst") == 0)
        {
            _stage = Managers.Object.SpawnStage($"Stages/Stage_0");
        }
        else
        {
            switch (PlayerPrefs.GetInt("difficulty"))
            {
                case 0:
                    _stage = Managers.Object.SpawnStage($"Stages/Easy_Stage_{Managers.Game.SaveData.stage}");
                    break;
                case 1:
                    _stage = Managers.Object.SpawnStage($"Stages/Normal_Stage_{Managers.Game.SaveData.stage}");
                    break;
                case 2:
                    _stage = Managers.Object.SpawnStage($"Stages/Hard_Stage_{Managers.Game.SaveData.stage}");
                    break;
            }
        }
    }

    void LoadMap()
    {
        if (Managers.Game.SaveData.stage == 0 || Managers.Game.SaveData.stage == 1 ||
            Managers.Game.SaveData.stage == 2 || Managers.Game.SaveData.stage == 3)
        {
            _bgMain = Managers.Object.SpawnBackgroundMap("BackGround/MainMap_Stage1", Define.ObjectType.MainBG);
            _bgSub = Managers.Object.SpawnBackgroundMap("BackGround/SubMap_Stage1", Define.ObjectType.SubBG);
        }
        else if ((Managers.Game.SaveData.stage == 4 || Managers.Game.SaveData.stage == 5 ||
            Managers.Game.SaveData.stage == 6))
        {
            _bgMain = Managers.Object.SpawnBackgroundMap("BackGround/MainMap_Stage2", Define.ObjectType.MainBG);
            _bgSub = Managers.Object.SpawnBackgroundMap("BackGround/SubMap_Stage2", Define.ObjectType.SubBG);
        }
        else if ((Managers.Game.SaveData.stage == 7 || Managers.Game.SaveData.stage == 8 ||
            Managers.Game.SaveData.stage == 9))
        {
            _bgMain = Managers.Object.SpawnBackgroundMap("BackGround/MainMap_Stage3", Define.ObjectType.MainBG).GetComponent<MapController>();
            _bgSub = Managers.Object.SpawnBackgroundMap("BackGround/SubMap_Stage3", Define.ObjectType.SubBG).GetComponent<MapController>();
        }
    }

    public void RestartGame()
    {
        _bgMain.Speed = 4;
        _bgSub.Speed = 3;
        _stage.Speed = 4;
        _player.GetComponent<Animator>().speed = 1;
    }

    public void StopGame()
    {
        _bgMain.Speed = 0;
        _bgSub.Speed = 0;
        _stage.Speed = 0;
        _player.GetComponent<Animator>().speed = 0;
    }

    public void StageClear()
    {
        _player.State = Define.PlayerState.Arrive;
        _player.Anim.SetBool("isJump", false);

        // 맵만 멈추고 플레이어는 이동
        StopGame();
        _player.GetComponent<Animator>().speed = 1;
        StartCoroutine(CoStageClear());
    }

    IEnumerator CoStageClear()
    {
        float tick = 0;
        while (true)
        {
            _player.transform.position += Vector3.right * 6.0f * Time.deltaTime;
            tick += Time.deltaTime;
            if (tick > 2.5f)
                break;
            yield return null;
        }

        if (PlayerPrefs.GetInt("isAccessFirst") == 0)
        {
            Managers.Scene.LoadScene(Define.Scene.Lobby);
            PlayerPrefs.SetInt("isAccessFirst", 1);
            PlayerPrefs.SetInt("Tutorial", 0);
        }
        else
        {
            Time.timeScale = 0;
            Managers.Sound.Clear();
            Managers.Sound.Play("StageClear", Define.Sound.Effect);
            Managers.UI.ShowPopupUI<UI_Goal>();
        }
    }

    public void GameOver()
    {
        StartCoroutine(CoGameOver());
    }

    IEnumerator CoGameOver()
    {
        StopGame();

        _player.gameObject.layer = 8; // PlayerDamaged Layer
        _player.Sprite.color = new Color(1, 1, 1, 0.4f);

        yield return new WaitForSeconds(1f);

        Time.timeScale = 0;
        Managers.Sound.Clear();
        Managers.Sound.Play("GameOver", Define.Sound.Effect);
        Managers.UI.ShowPopupUI<UI_GameOver>();
    }

    public override void Clear()
    {

    }
}
