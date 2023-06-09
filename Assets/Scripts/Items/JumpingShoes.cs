using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingShoes : ItemController
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

        player.JumpCount++;

        gameObject.SetActive(false);
    }
}
