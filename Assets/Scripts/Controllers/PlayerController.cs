using System.Collections;
using UnityEngine;

public class PlayerController : BaseController
{
    public float _jumpCount = 2;
    public float _jumpPower = 10.0f;
    public bool _isJump = false;
    public int _health = 3;

    public Define.PlayerState _state;

    public MapController[] mapControllers;
    public Move move;

    Animator _anim;

    public override void Init()
    {
        WorldObjectType = Define.WorldObject.Player;
        mapControllers = new MapController[2];
        _state = Define.PlayerState.Run;
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        switch (_state)
        {
            case Define.PlayerState.Die:
                UpdateDie();
                break;
            case Define.PlayerState.Run:
                UpdateRun();
                break;
            case Define.PlayerState.Jump:
                UpdateJump();
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            Debug.Log("Collide Platform");

            // 발판 충돌로직
            _jumpCount = 2;
            _isJump = false;
            _state = Define.PlayerState.Run;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            if (_health > 1)
            {
                _health--;

                // 피격 코루틴
            }
            else
            {
                _health--;
                _state = Define.PlayerState.Die;
            }

            if (collision.gameObject.name == "VaultingHorse")
            {
                Debug.Log("Collide VaultingHorse");
            }

            if (collision.gameObject.name == "Signage")
            {
                Debug.Log("Collide Signage");
            }

            if (collision.gameObject.name == "Pool")
            { 
                Debug.Log("Collide Pool");
                StartCoroutine(SpeedDown());
            }
        }

        if (collision.gameObject.tag == "Item")
        {
            if (collision.gameObject.name == "Letter")
            {
                // TODO
                Debug.Log("Collide Letter");
                _health++;
            }

            if (collision.gameObject.name == "Coffee")
            {
                // TODO : 코루틴 호출
                StartCoroutine(SpeedUp());
            }

            if (collision.gameObject.name == "JumpingShoes")
            {
                // TODO
                _jumpCount++;
            }

            collision.gameObject.SetActive(false);
        }

        if (collision.gameObject.tag == "Finish")
        {
            Time.timeScale = 0;
        }
    }

    void UpdateRun()
    {
        _anim.SetBool("isJump", false);
    }

    void UpdateJump()
    {
        _anim.SetBool("isJump", true);
    }

    void UpdateDie()
    {
        // Die
        Time.timeScale = 0;
    }

    IEnumerator SpeedUp()
    {
        foreach(MapController item in mapControllers)
            item._speed *= 2;
        move._speed *= 2;

        yield return new WaitForSeconds(3f);

        foreach (MapController item in mapControllers)
            item._speed /= 2;
        move._speed /= 2;
    }

    IEnumerator SpeedDown()
    {
        foreach (MapController item in mapControllers)
            item._speed /= 2;
        move._speed /= 2;

        yield return new WaitForSeconds(3f);

        foreach (MapController item in mapControllers)
            item._speed *= 2;
        move._speed *= 2;
    }

    IEnumerator OnDamage()
    {
        yield return null;
    }
}
