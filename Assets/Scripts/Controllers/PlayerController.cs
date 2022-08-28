using System.Collections;
using UnityEngine;

public class PlayerController : BaseController
{
    public float _jumpCount = 2;
    public float _jumpPower = 10.0f;
    public bool _isJump = false;
    public int _health;
    int _maxHealth;
    public bool _isFight = false;

    public GameObject _shieldObj;
    bool _shield = false;

    public Define.PlayerState _state;
    public MapController[] mapControllers;
    public StageController stageController;

    Teacher _teacher;
    Animator _anim;
    SpriteRenderer _sprite;

    public override void Init()
    {
        WorldObjectType = Define.WorldObject.Player;
        mapControllers = new MapController[2];
        _state = Define.PlayerState.Run;
        _anim = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        _maxHealth = _health + PlayerPrefs.GetInt("extrahealth");
        if (PlayerPrefs.GetInt("isAccessFirst") == 0)
        {
            _health = 99;
        }
        else
        {
            _health = _maxHealth;
        }
        
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
            case Define.PlayerState.Fight:
                UpdateFight();
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
            if (_state != Define.PlayerState.Die && _state != Define.PlayerState.Clear && _state != Define.PlayerState.Fight)
            {
                // 발판 충돌로직
                _state = Define.PlayerState.Run;
                _jumpCount = 2;
                _isJump = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
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
                StartCoroutine("SpeedDown");
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
                gameObject.layer = 8; // PlayerDamaged
                _sprite.color = new Color(1, 1, 1, 0.4f);
                Managers.UI.ShowPopupUI<UI_Teacher>();
                // _isFight = true;
                _state = Define.PlayerState.Fight;
                _teacher = collision.transform.GetComponent<Teacher>();

                // 점프 중일 때 바닥에 닿아도 _jumpCount가 갱신되지 않음
                if (_isJump)
                {
                    _jumpCount = 2;
                    _isJump = false;
                }
            }
        }

        if (collision.gameObject.tag == "Item")
        {
            if (collision.gameObject.name == "Letter")
            {
                if (_health < _maxHealth && _state != Define.PlayerState.Die)
                    _health++;
            }

            if (collision.gameObject.name == "Coffee")
            {
                _shield = true;
                _shieldObj.SetActive(true);
                StartCoroutine("SpeedUp");
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
        if (PlayerPrefs.GetInt("vibrate") == 1)
            Vibration.Vibrate((long)200);

        gameObject.layer = 8; // PlayerDamaged
        _sprite.color = new Color(1, 1, 1, 0.4f);
        StartCoroutine("DamageRecovered");

        if (_shield == false)
        {
            _health -= count;
            if (_health <= 0)
            {
                _health = 0;
                _state = Define.PlayerState.Die;
            }
        }
        else
        {
            _shieldObj.SetActive(false);
            _shield = false;
        }
    }

    void UpdateRun()
    {
        _anim.SetBool("isJump", false);
    }

    void UpdateFight()
    {
        StopGame();
    }

    void UpdateJump()
    {
        _anim.SetBool("isJump", true);
    }

    void UpdateDie()
    {
        if (_teacher != null)
            _teacher.isFight = false;
        StartCoroutine("GameOver");
    }

    public void StageClear()
    {
        _anim.SetBool("isJump", false);
        transform.position += Vector3.right * 6.0f * Time.deltaTime;
        StartCoroutine("StageClearUI");
    }

    public void recovereDamage()
    {
        // _isFight = false;
        StartCoroutine("DamageRecovered");
        _state = Define.PlayerState.Run;
        rewindSpeed();
        _teacher.isFight = false;
        _teacher.GetComponent<Animator>().speed = 1;
    }

    IEnumerator SpeedUp()
    {
        StopCoroutine("SpeedDown");
        mapControllers[0]._speed = 8;
        mapControllers[1]._speed = 6;
        stageController._speed = 8;

        yield return new WaitForSeconds(2f);

        mapControllers[0]._speed = 4;
        mapControllers[1]._speed = 3;
        stageController._speed = 4;
    }

    IEnumerator SpeedDown()
    {
        StopCoroutine("SpeedUp");
        mapControllers[0]._speed = 2;
        mapControllers[1]._speed = 1.5f;
        stageController._speed = 2;

        yield return new WaitForSeconds(1.5f);

        mapControllers[0]._speed = 4;
        mapControllers[1]._speed = 3;
        stageController._speed = 4;
    }

    IEnumerator DamageRecovered()
    {
        yield return new WaitForSeconds(1.5f);

        gameObject.layer = 7;
        _sprite.color = new Color(1, 1, 1, 1);
    }

    IEnumerator StageClearUI()
    {
        if (PlayerPrefs.GetInt("isAccessFirst") == 0)
        {
            yield return new WaitForSeconds(2.5f);
            Managers.Scene.LoadScene(Define.Scene.Lobby);
            PlayerPrefs.SetInt("isAccessFirst",1);
            PlayerPrefs.SetInt("Tutorial", 0);
        }
        else
        {
            yield return new WaitForSeconds(2.5f);
            Time.timeScale = 0;
            Managers.Sound.Clear();
            Managers.Sound.Play("StageClear", Define.Sound.Effect);
            Managers.UI.ShowPopupUI<UI_Goal>();
        }
    }

    IEnumerator GameOver()
    {
        gameObject.layer = 8; // PlayerDamaged
        StopGame();

        yield return new WaitForSeconds(1f);
        Time.timeScale = 0;
        Managers.Sound.Clear();
        Managers.Sound.Play("GameOver", Define.Sound.Effect);
        Managers.UI.ShowPopupUI<UI_GameOver>();
    }

    void StopGame()
    {
        mapControllers[0]._speed = 0;
        mapControllers[1]._speed = 0;
        stageController._speed = 0;
        _anim.speed = 0f;
    }

    void rewindSpeed()
    {
        mapControllers[0]._speed = 4;
        mapControllers[1]._speed = 3;
        stageController._speed = 4;
        _anim.speed = 1f;
    }
}
