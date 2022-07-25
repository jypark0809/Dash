using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public float speed;
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
        transform.Translate(Vector2.left * speed * Time.deltaTime, 0);

        // end [0] [] [] [] [] [] [] [7] start
        if (sprites[endIndex].position.x < -15.2)
        {
            // Sprite ����
            sprites[endIndex].localPosition = sprites[endIndex].localPosition + respawnPos;

            // Ŀ�� �ε��� ����
            startIndex = endIndex;
            endIndex = (endIndex + 1) % count;
        }
    }
}
