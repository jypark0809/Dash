using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerEx
{
    PlayerController _player;
    public PlayerController Player { get { return _player; } set { _player = value; } }

    StageController _stage;
    public StageController Stage { get { return _stage; } set { _stage = value; } }

    MapController _bgMain;
    public MapController MainBG { get { return _bgMain; } set { _bgMain = value; } }

    MapController _bgSub;
    public MapController SubBG { get { return _bgSub; } set { _bgSub = value; } }

    public GameObject SpawnPlayer(string path, Transform parent = null)
    {
        GameObject go = Managers.Resource.Instantiate(path, parent);
        _player = go.GetOrAddComponent<PlayerController>();
        return go;
    }

    public GameObject SpawnStage(string path, Transform parent = null)
    {
        GameObject go = Managers.Resource.Instantiate(path, parent);
        _stage = go.GetOrAddComponent<StageController>();
        return go;
    }

    public GameObject SpawnBackgroundMap(string path, Define.WorldObject type, Transform parent = null)
    {
        GameObject go = Managers.Resource.Instantiate(path, parent);

        if (type == Define.WorldObject.MainBG)
        {
            _bgMain = go.GetOrAddComponent<MapController>();
        }
        else if (type == Define.WorldObject.SubBG)
        {
            _bgSub = go.GetOrAddComponent<MapController>();
        }
        
        return go;
    }

    public Define.WorldObject GetWorldObjectType(GameObject go)
    {
        BaseController bc = go.GetComponent<BaseController>();
        if (bc == null)
            return Define.WorldObject.Unknown;

        return bc.WorldObjectType;
    }

    public void Despawn(GameObject go)
    {
        Define.WorldObject type = GetWorldObjectType(go);
        switch (type)
        {
            case Define.WorldObject.Player:
                {
                    if (_player == go)
                        _player = null;
                }
                break;
        }
        Managers.Resource.Destroy(go);
    }

    public void StopMapScrolling(MapController mc)
    {
        mc.Speed = 0;
    }
}
