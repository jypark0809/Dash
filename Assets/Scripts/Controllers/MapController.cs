using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : BaseController
{
    public float _speed;
    public int startIndex;
    public int endIndex;
    public int count;
    public Transform[] spritesPosition;
    Vector3 respawnPos;

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        WorldObjectType = Define.WorldObject.Map;
        respawnPos = new Vector3(count * 15.2f, 0, 0);
    }

    void Update()
    {
        transform.Translate(Vector2.left * _speed * Time.deltaTime);
        RespawnMap();
    }

    void RespawnMap()
    {
        // end [0] [] [] [] [] [] [] [7] start
        if (spritesPosition[endIndex].position.x < -30.4)
        {
            // Sprite ����
            spritesPosition[endIndex].localPosition = spritesPosition[endIndex].localPosition + respawnPos;

            // Ŀ�� �ε��� ����
            startIndex = endIndex;
            endIndex = (endIndex + 1) % count;
        }
    }
}
