using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teacher : BaseObstacle
{
    public bool isFight = false;
    public float _range = 15f;
    public int _speed = 5;

    public override void Init()
    {
        _anim = GetComponent<Animator>();
        _anim.Play("Teacher");
    }

    void Update()
    {
        if (transform.position.x - Managers.Game._player.transform.position.x < _range && isFight == false)
        {
            transform.Translate(Vector2.left * _speed * Time.deltaTime);
            _anim.speed = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isFight = true;
            _anim.speed = 0;
        }
    }
}
