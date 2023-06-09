using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffee : ItemController
{
    Coroutine _coGetCanCoffe;

    public override void Init()
    {
        _anim = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        _anim.Play("Item_Fly");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();

        if (player == null)
            return;

        if (_coGetCanCoffe != null)
            StopCoroutine(_coGetCanCoffe);

        _coGetCanCoffe = StartCoroutine(CoGetCanCoffee(player));

        // Inactive Item
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sprite in sprites)
            sprite.enabled = false;
    }

    IEnumerator CoGetCanCoffee(PlayerController player)
    {
        Managers.Sound.Play("Item", Define.Sound.Effect);

        // Speed Up
        Managers.Object.MainBG.Speed = 8;
        Managers.Object.SubBG.Speed = 6;
        Managers.Object.Stage.Speed = 8;

        // Active Shield
        player.Shield.SetActive(true);

        yield return new WaitForSeconds(2f);

        // Speed Down
        Managers.Object.MainBG.Speed = 4;
        Managers.Object.SubBG.Speed = 3;
        Managers.Object.Stage.Speed = 4;

        // Inactive Shield
        player.Shield.SetActive(false);
    }
}
