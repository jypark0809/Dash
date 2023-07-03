using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : ObstacleController
{
    public int _range = 7;
    float screenRightWorldPos;

    public override void Init()
    {
        base.Init();
        _anim = GetComponent<Animator>();
        screenRightWorldPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;
    }

    void Update()
    {
        if (transform.position.x - Managers.Object.Player.transform.position.x < _range)
        {
            _anim.Play("Spider");
        }

        SetActiceOff();
    }
}
