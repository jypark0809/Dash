using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    GameObject player;
    PlayerController pc;
    MapController mc1;
    MapController mc2;

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        UI_GameScene gs = Managers.UI.ShowSceneUI<UI_GameScene>();

        player = Managers.Game.Spawn(Define.WorldObject.Player, "Player");
        pc = player.GetOrAddComponent<PlayerController>();
        gs.SetPlayer(player);

        GameObject stage = Managers.Game.Spawn(Define.WorldObject.Stage, "Stages/Stage_3_1"); // 스테이지를 생성한다.
        mc1 = GameObject.Find("Class Group").GetComponent<MapController>();
        mc2 = GameObject.Find("Hall Group").GetComponent<MapController>();

        pc.move = stage.GetComponent<Move>();
        pc.mapControllers[0] = mc1;
        pc.mapControllers[1] = mc2;
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
