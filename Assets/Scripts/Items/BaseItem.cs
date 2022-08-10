using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : MonoBehaviour
{
    public Define.WorldObject WorldObjectType { get; protected set; } = Define.WorldObject.Item;
    protected Animator _anim;

    void Awake()
    {
        Init();
    }

    public virtual void Init()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Managers.Sound.Play("Item", Define.Sound.Effect);
        }
    }
}
