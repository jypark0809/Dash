using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : ObstacleController
{
    public float _range = 11.6f;
    public int _speed = 3;

    public override void Init()
    {
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (transform.position.x - Managers.Object.Player.transform.position.x < _range)
        {
            transform.Translate(Vector2.left * _speed * Time.deltaTime);
            _anim.Play("Bee");
        }
    }
}
