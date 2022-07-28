using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public float _speed;

    void Start()
    {

    }

    protected void Moving()
    {
        transform.Translate(Vector2.left * _speed * Time.deltaTime);
    }
}
