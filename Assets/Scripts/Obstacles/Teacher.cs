using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teacher : BaseObstacle
{
    public int _range = 15;
    public int _speed = 5;

    public override void Init()
    {
        _anim = GetComponent<Animator>();
        
    }

    void Update()
    {
        if (transform.position.x - Managers.Game._player.transform.position.x < _range)
        {
            transform.Translate(Vector2.left * _speed * Time.deltaTime);
            _anim.Play("Teacher");
        }
    }
}
