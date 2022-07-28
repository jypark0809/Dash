using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float _jumpCount = 2;
    public float _jumpPower = 10.0f;
    public bool _isJump = false;
    public MovingObject[] movingObjects;

    public Rigidbody2D _rigid;
    public BoxCollider2D _collider;
    Animator _anim;
    public enum PlayerState { Run, Jump, Die }
    public PlayerState _state = PlayerState.Run;

    void Start()
    {
        // Managers.Input.KeyAction -= Jump;
        // Managers.Input.KeyAction += Jump;

        _rigid = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        switch (_state)
        {
            case PlayerState.Die:
                UpdateDie();
                break;
            case PlayerState.Run:
                UpdateRun();
                break;
            case PlayerState.Jump:
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
            _state = PlayerState.Run;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            if (collision.gameObject.name == "VaultingHorse")
            {
                Debug.Log("Collide VaultingHorse");

                // 장애물 충돌 로직
            }
        }

        if (collision.gameObject.tag == "Item")
        {
            if (collision.gameObject.name == "Letter")
            {
                // TODO
                Debug.Log("Collide Letter");
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
        // 체력이 0이면 Die
    }

    IEnumerator SpeedUp()
    {
        foreach(MovingObject item in movingObjects)
            item._speed *= 2;

        yield return new WaitForSeconds(3f);

        foreach (MovingObject item in movingObjects)
            item._speed /= 2;
    }
}
