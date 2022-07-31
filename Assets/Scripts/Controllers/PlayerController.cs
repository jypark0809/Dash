using System.Collections;
using UnityEngine;

public class PlayerController : BaseController
{
    // public float _gravity = 0.8f;
    // float _heightMovement;
    // bool _isGrounded;

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
        CheckPlatform();
        // UpdateHeight();

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

    //void UpdateHeight()
    //{
    //    _heightMovement += _gravity * Time.deltaTime * -1;
    //    transform.Translate(0, _heightMovement, 0);
    //}

    //void Land(float height)
    //{
    //    transform.position = new Vector3(transform.position.x, height, transform.position.z);
    //    _heightMovement = 0;
    //}

    void CheckPlatform()
    {
        Vector2 playerPos = new Vector2(transform.position.x, transform.position.y - 1.3f);
        Debug.DrawRay(playerPos, Vector2.down * 1f, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(playerPos, Vector2.down, 1f, LayerMask.GetMask("Platform"));
        if (hit)
        {
            // if (hit.rigidbody.simulated == false)
            // {
            //     hit.rigidbody.simulated = true;
            //     Debug.Log("rigidbody.simulated == false");
            // }
            // else
            //     Debug.Log("rigidbody.simulated == true");
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
