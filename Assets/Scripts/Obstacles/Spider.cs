using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : BaseObstacle
{
    public int _range = 5;
    public int _speed = 5;
    Vector3 _destPos;

    public override void Init()
    {
        _destPos = new Vector2(0, transform.position.y - 3);
        _anim = GetComponent<Animator>();
        _anim.Play("Spider");
    }

    void Update()
    {
        if (transform.position.x - Managers.Game._player.transform.position.x < _range)
        {
            if ( Mathf.Abs(_destPos.y - transform.position.y) < 0.05f)
            {
                transform.position = new Vector3(transform.position.x, _destPos.y);
            }
            else
            {
                transform.Translate(Vector2.down * _speed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("collide!");
        }
    }
}
