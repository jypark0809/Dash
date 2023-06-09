using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class TutorialScene : BaseScene
{
    PlayerController _player;
    MapController _bgMain;
    MapController _bgSub;
    Finish finish;

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Tutorial;

        // Skip Tutorial
        if(PlayerPrefs.HasKey("isAccessFirst"))
            Managers.Scene.LoadScene(Define.Scene.Lobby);

        // Sound
        Managers.Sound.Play("GameScene", Define.Sound.Bgm);

        // Spawn Object
        _player = Managers.Object.SpawnPlayer("Player/Male");
        _bgMain = Managers.Object.SpawnBackgroundMap("BackGround/MainMap_Stage1", Define.ObjectType.MainBG).GetComponent<MapController>();
        _bgSub = Managers.Object.SpawnBackgroundMap("BackGround/SubMap_Stage1", Define.ObjectType.SubBG).GetComponent<MapController>();

        // Popup Script UI
        PlayerPrefs.SetInt("Tutorial", 0);
        Managers.UI.ShowPopupUI<UI_Hint>();

        GameObject finishObj = GameObject.Find("Finish");
        finish = finishObj.GetComponent<Finish>();
    }

    public override void Clear()
    {

    }
}
