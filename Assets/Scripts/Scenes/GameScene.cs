using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        UI_GameScene gs = Managers.UI.ShowSceneUI<UI_GameScene>();

        GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "Player");
        PlayerController pc = player.GetOrAddComponent<PlayerController>();
        gs.SetPlayer(player);

        GameObject stage = Managers.Game.Spawn(Define.WorldObject.Map, "Stages/Stage_1_1"); // TODO : Stage¿¡ µû¸¥ ¸Ê Spawn
        MapController mc1 = GameObject.Find("Class Group").GetComponent<MapController>();
        MapController mc2 = GameObject.Find("Hall Group").GetComponent<MapController>();

        pc.move = stage.GetComponent<Move>();
        pc.mapControllers[0] = mc1;
        pc.mapControllers[1] = mc2;
        
    }

    public override void Clear()
    {

    }
}
