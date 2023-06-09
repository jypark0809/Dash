using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : BaseController
{
    protected Animator _anim;
    int _damage;
    Coroutine _coDamage;

    public override void Init()
    {
        base.Init();
        ObjectType = Define.ObjectType.Obstacle;
        _damage = 1;
    }

    public void SetActiceOff()
    {
        if (transform.position.x < -20)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController target = collision.gameObject.GetComponent<PlayerController>();

        if (target == null)
            return;

        if (_coDamage != null)
            StopCoroutine(_coDamage);

        _coDamage = StartCoroutine(CoDoDamage(target));
    }

    IEnumerator CoDoDamage(PlayerController target)
    {
        Managers.Sound.Play("Obstacle", Define.Sound.Effect);
        target.OnDamaged(this, _damage);

        yield return new WaitForSeconds(1.5f);

        target.gameObject.layer = 7;
        target.Sprite.color = new Color(1, 1, 1, 1f);
    }
}
