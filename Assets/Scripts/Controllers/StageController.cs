using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : BaseController
{
    public float _speed = 4;

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        WorldObjectType = Define.WorldObject.Stage;
    }

    void Update()
    {
        transform.Translate(Vector2.left * _speed * Time.deltaTime);
    }
}
