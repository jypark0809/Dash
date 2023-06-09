using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    float _playerPos = -4;
    float _finishPos;
    float _stageDistance;

    void Start()
    {
        _finishPos = transform.position.x;
        _stageDistance = _finishPos - _playerPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController pc = collision.GetComponent<PlayerController>();

        if (pc == null)
            return;

        (Managers.Scene.CurrentScene as GameScene).StageClear();
    }

    public float CalculateDistance()
    {
        float remainDistance = transform.position.x - _playerPos;
        float curPos = _stageDistance - remainDistance;

        return curPos / _stageDistance;
    }
}
