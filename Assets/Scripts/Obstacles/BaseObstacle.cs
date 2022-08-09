using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseObstacle : MonoBehaviour
{
    public Define.WorldObject WorldObjectType { get; protected set; } = Define.WorldObject.Obstacle;
    protected Animator _anim;

    void Awake()
    {
        Init();
    }

    public abstract void Init();

    public void SetActiceOff()
    {
        if (transform.position.x < -20)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Managers.Sound.Play("Itemtest", Define.Sound.Effect);
        }
    }
}
