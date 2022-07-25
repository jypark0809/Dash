using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float _jumpCount = 2;
    public float _jumpSpeed = 4f;
    bool _isJump = false;

    Rigidbody2D _rigid;
    Animator _anim;
    public enum PlayerState { Run, Jump, Die }
    PlayerState _state = PlayerState.Run;

    void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    void Start()
    {
        // Managers.Input.KeyAction -= Jump;
        // Managers.Input.KeyAction += Jump;
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
            _jumpCount = 2;
            _isJump = false;
            _state = PlayerState.Run;
        }
    }

    public void Jump()
    {
        // 캐릭터 이동 관련 로직
        _isJump = true;
        _state = PlayerState.Jump;
        if (_jumpCount > 0)
        {
            _rigid.velocity = Vector2.up * _jumpSpeed;
            _jumpCount--;
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
}
