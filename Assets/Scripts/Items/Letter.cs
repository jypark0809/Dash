using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Letter : ItemController
{
    public override void Init()
    {
        _anim = GetComponent<Animator>();
        _anim.Play("Item_Fly");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();

        if (player == null)
            return;

        Managers.Sound.Play("Item", Define.Sound.Effect);

        if (player.Hp < player.MaxHp && player.State != Define.PlayerState.Die)
            player.Hp++;

        gameObject.SetActive(false);
    }
}
