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
    SpriteRenderer _sprite;

    public override void Init()
    {
        WorldObjectType = Define.WorldObject.Player;
        mapControllers = new MapController[2];
        _state = Define.PlayerState.Run;
        _anim = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
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
            case Define.PlayerState.Clear:
                StageClear();
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            if (_state != Define.PlayerState.Die)
            {
                // 발판 충돌로직
                _jumpCount = 2;
                _isJump = false;
                _state = Define.PlayerState.Run;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            OnDamaged();

            // 표지판
            if (collision.gameObject.name == "Signage")
            {
                DecreaseHealth(1);
            }

            // 벌
            if (collision.gameObject.name == "Bee")
            {
                DecreaseHealth(2);
            }

            // 뜀틀
            if (collision.gameObject.name == "VaultingHorse")
            {
                DecreaseHealth(1);
            }

            // 사물함
            if (collision.gameObject.name == "Locker")
            {
                DecreaseHealth(1);
            }

            // 압정
            if (collision.gameObject.name == "Pin")
            {
                StartCoroutine(SpeedDown());
                DecreaseHealth(1);
            }

            // 책
            if (collision.gameObject.name == "Books")
            {
                DecreaseHealth(1);
            }

            // 거미
            if (collision.gameObject.name == "Spider")
            {
                DecreaseHealth(1);
            }

            // 학주
            if (collision.gameObject.name == "Teacher")
            {
                DecreaseHealth(3);
            }
        }

        if (collision.gameObject.tag == "Item")
        {
            if (collision.gameObject.name == "Letter")
            {
                if (_health < 3 && _state != Define.PlayerState.Die)
                    _health++;
            }

            if (collision.gameObject.name == "Coffee")
            {
                StartCoroutine(SpeedUp());
            }

            if (collision.gameObject.name == "JumpingShoes")
            {
                _jumpCount++;
            }

            collision.gameObject.SetActive(false);
        }

        if (collision.gameObject.tag == "Finish")
        {
            Managers.Data.PrintLog();
            _state = Define.PlayerState.Clear;
        }
    }

    void DecreaseHealth(int count)
    {
        _health -= count;
        if (_health <= 0)
        {
            _health = 0;
            _state = Define.PlayerState.Die;
        }
    }

    void OnDamaged()
    {
        gameObject.layer = 8; // PlayerDamaged
        _sprite.color = new Color(1, 1, 1, 0.4f);

        // animation

        StartCoroutine(DamageRecovered());
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
        StartCoroutine(GameOver());
    }

    public void StageClear()
    {
        transform.position += Vector3.right * 6.0f * Time.deltaTime;
        StartCoroutine(StageClearUI());
    }

    IEnumerator SpeedUp()
    {
        foreach(MapController item in mapControllers)
            item._speed = 8;
        move._speed = 8;

        yield return new WaitForSeconds(3f);

        foreach (MapController item in mapControllers)
            item._speed = 4;
        move._speed = 4;
    }

    IEnumerator SpeedDown()
    {
        foreach (MapController item in mapControllers)
            item._speed = 2;
        move._speed = 2;

        yield return new WaitForSeconds(1.5f);

        foreach (MapController item in mapControllers)
            item._speed = 4;
        move._speed = 4;
    }

    IEnumerator DamageRecovered()
    {
        yield return new WaitForSeconds(1.5f);

        gameObject.layer = 7;
        _sprite.color = new Color(1, 1, 1, 1);
    }

    IEnumerator StageClearUI()
    {
        yield return new WaitForSeconds(2.5f);
        Time.timeScale = 0;
        Managers.Sound.Clear();
        Managers.Sound.Play("StageClear", Define.Sound.Effect);
        Managers.UI.ShowPopupUI<UI_Goal>();
    }

    IEnumerator GameOver()
    {
        gameObject.layer = 8; // PlayerDamaged
        mapControllers[0]._speed = 0;
        mapControllers[1]._speed = 0;
        move._speed = 0;
        _anim.speed = 0f;

        yield return new WaitForSeconds(1f);
        Time.timeScale = 0;
        Managers.Sound.Clear();
        Managers.Sound.Play("GameOver", Define.Sound.Effect);
        Managers.UI.ShowPopupUI<UI_GameOver>();
    }
}
