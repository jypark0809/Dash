using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : BaseController
{
    protected Animator _anim;
    protected SpriteRenderer _sprite;

    public override void Init()
    {
        base.Init();
        ObjectType = Define.ObjectType.Item;
    }
}
