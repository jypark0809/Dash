using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : BaseObstacle
{
    public int _range = 6;
    public int _speed = 2;

    public override void Init()
    {

    }

    void Update()
    {
        if (transform.position.x - Managers.Game._player.transform.position.x < _range)
        {
            transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, 2.1f), 0.02f);
        }
    }
}
