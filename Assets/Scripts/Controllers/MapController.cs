using UnityEngine;

public class MapController : BaseController
{
    public float _speed;
    public float Speed { get { return _speed; } set { _speed = value; } }
    public Transform[] spritesPosition;

    int count;
    int endIndex;
    Vector3 respawnPos;
    const float MAP_HORIZONTAL_SIZE = 15.2f;
    const float LIMIT_POS_X = -30.4f;

    public override void Init()
    {
        count = spritesPosition.Length;
        respawnPos = new Vector3(count * MAP_HORIZONTAL_SIZE, 0, 0);
    }

    void Update()
    {
        transform.Translate(Vector2.left * _speed * Time.deltaTime);
        RespawnMap();
    }

    void RespawnMap()
    {
        // end
        // [0] [] [] [] [] [] [] []
        if (spritesPosition[endIndex].position.x < LIMIT_POS_X)
        {
            spritesPosition[endIndex].localPosition = spritesPosition[endIndex].localPosition + respawnPos;
            endIndex = (endIndex + 1) % count;
        }
    }
}
