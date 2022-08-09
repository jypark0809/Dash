using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : BaseObstacle
{
    public int _speed = 3;

    public override void Init()
    {
        _anim = GetComponent<Animator>();
        _anim.Play("Bee");
    }

    void Update()
    {
            transform.Translate(Vector2.left * _speed * Time.deltaTime);
    }
}
