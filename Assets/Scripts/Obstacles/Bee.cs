using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : BaseObstacle
{
    float screenRightWorldPos;
    public int _speed = 3;

    public override void Init()
    {
        Debug.Log("screenRightWorldPos : " + Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);
        _anim = GetComponent<Animator>();
        screenRightWorldPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;
    }

    void Update()
    {
        if (transform.position.x < screenRightWorldPos)
        {
            transform.Translate(Vector2.left * _speed * Time.deltaTime);
            _anim.Play("Bee");
        }
    }
}
