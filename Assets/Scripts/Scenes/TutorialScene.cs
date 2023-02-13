using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class TutorialScene : BaseScene
{
    GameObject _player;
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
        _player = Managers.Game.SpawnPlayer("Player/Male");
        _bgMain = Managers.Game.SpawnBackgroundMap("BackGround/MainMap_Stage1", Define.WorldObject.MainBG).GetComponent<MapController>();
        _bgSub = Managers.Game.SpawnBackgroundMap("BackGround/SubMap_Stage1", Define.WorldObject.SubBG).GetComponent<MapController>();

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
