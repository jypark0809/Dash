using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : BaseObstacle
{
    public int _range = 5;
    float screenRightWorldPos;

    public override void Init()
    {
        _anim = GetComponent<Animator>();
        screenRightWorldPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;
    }

    void Update()
    {
        if (transform.position.x - Managers.Game._player.transform.position.x < _range)
        {
            _anim.Play("Spider");
        }
    }
}
