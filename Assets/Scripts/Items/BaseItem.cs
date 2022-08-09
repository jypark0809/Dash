using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseItem : MonoBehaviour
{
    public Define.WorldObject WorldObjectType { get; protected set; } = Define.WorldObject.Item;
    protected Animator _anim;

    void Awake()
    {
        Init();
    }

    public abstract void Init();
}
