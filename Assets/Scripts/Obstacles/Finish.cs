using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    float _startPos;
    float _playerPos = -4;
    float _stageDistance;

    void Start()
    {
        _startPos = transform.position.x;
        _stageDistance = _startPos - _playerPos;
    }

    public float CalculateDistance()
    {
        float remainDistance = transform.position.x - _playerPos;
        float curPos = _stageDistance - remainDistance;

        return curPos / _stageDistance;
    }
}
