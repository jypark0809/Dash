using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float _speed = 3;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector2.left * _speed * Time.deltaTime);
    }
}
