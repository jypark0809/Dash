using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager
{
    PlayerController _player;
    public PlayerController Player { get { return _player; } set { _player = value; } }

    StageController _stage;
    public StageController Stage { get { return _stage; } set { _stage = value; } }

    MapController _bgMain;
    public MapController MainBG { get { return _bgMain; } set { _bgMain = value; } }

    MapController _bgSub;
    public MapController SubBG { get { return _bgSub; } set { _bgSub = value; } }

    Finish _finish;
    public Finish Finish { get { return _finish; } set { _finish = value; } }

    public PlayerController SpawnPlayer(string path, Transform parent = null)
    {
        GameObject go = Managers.Resource.Instantiate(path, parent);
        _player = go.GetOrAddComponent<PlayerController>();
        return _player;
    }

    public StageController SpawnStage(string path, Transform parent = null)
    {
        GameObject go = Managers.Resource.Instantiate(path, parent);
        _stage = go.GetOrAddComponent<StageController>();
        return _stage;
    }

    public MapController SpawnBackgroundMap(string path, Define.ObjectType type, Transform parent = null)
    {
        GameObject go = Managers.Resource.Instantiate(path, parent);

        if (type == Define.ObjectType.MainBG)
        {
            _bgMain = go.GetOrAddComponent<MapController>();
            return _bgMain;
        }
        else if (type == Define.ObjectType.SubBG)
        {
            _bgSub = go.GetOrAddComponent<MapController>();
            return _bgSub;
        }

        return null;
    }

    public Define.ObjectType GetWorldObjectType(GameObject go)
    {
        BaseController bc = go.GetComponent<BaseController>();
        if (bc == null)
            return Define.ObjectType.Unknown;

        return bc.ObjectType;
    }

    public void Despawn(GameObject go)
    {
        Define.ObjectType type = GetWorldObjectType(go);
        switch (type)
        {
            case Define.ObjectType.Player:
                {
                    if (_player == go)
                        _player = null;
                }
                break;
        }
        Managers.Resource.Destroy(go);
    }
}
