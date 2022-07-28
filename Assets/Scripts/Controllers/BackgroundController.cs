using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MovingObject
{
    public int startIndex;
    public int endIndex;
    public int count;
    public Transform[] sprites;
    Vector3 respawnPos;

    void Start()
    {
        respawnPos = new Vector3(count * 15.2f, 0, 0);
    }

    void Update()
    {
        Moving();
        RespawnMap();
    }

    void RespawnMap()
    {
        // end [0] [] [] [] [] [] [] [7] start
        if (sprites[endIndex].position.x < -15.2)
        {
            // Sprite 재사용
            sprites[endIndex].localPosition = sprites[endIndex].localPosition + respawnPos;

            // 커서 인덱스 갱신
            startIndex = endIndex;
            endIndex = (endIndex + 1) % count;
        }
    }
}
