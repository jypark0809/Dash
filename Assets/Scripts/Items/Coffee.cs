using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffee : BaseItem
{
    public override void Init()
    {
        _anim = GetComponent<Animator>();
        _anim.Play("Item_Fly");
    }
}
