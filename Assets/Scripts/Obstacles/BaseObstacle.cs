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

    void SetActiceOff()
    {
        if (transform.position.x < -20)
        {
            gameObject.SetActive(false);
        }
    }
}
