using System.Collections;
using UnityEngine;

public class PlayerController : BaseController
{
    public float _jumpCount = 2;
    public float _jumpPower = 10.0f;
    public bool _isJump = false;
    int _hp = 3;
    public int Hp
    {
        get { return _hp;}
        set
        {
            _hp = value;

            if (_hp > _maxHp)
                _hp = _maxHp;

            if (Managers.Scene.CurrentScene as GameScene)
                (Managers.UI.SceneUI as UI_GameScene).SetHeartUI(_hp);
        }
    }
    int _maxHp;
    public bool _isFight = false;
    bool _isArrive = false;

    public GameObject _shieldObj;
    bool _shield = false;

    public Define.PlayerState _state;
    public MapController[] mapControllers;

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
        _maxHp = Hp + PlayerPrefs.GetInt("extrahealth");
        if (PlayerPrefs.GetInt("isAccessFirst") == 0)
        {
            Hp = 5;
        }
        else
        {
            Hp = _maxHp;
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
            if (collision.gameObject.name == "Signage")
            {
                DecreaseHealth(1);
            }

            if (collision.gameObject.name == "Bee")
            {
                DecreaseHealth(2);
            }

            if (collision.gameObject.name == "VaultingHorse")
            {
                DecreaseHealth(1);
            }

            if (collision.gameObject.name == "Locker")
            {
                DecreaseHealth(1);
            }

            if (collision.gameObject.name == "Pin")
            {
                StartCoroutine("SpeedDown");
                DecreaseHealth(1);
            }

            if (collision.gameObject.name == "Books")
            {
                DecreaseHealth(1);
            }

            if (collision.gameObject.name == "Spider")
            {
                DecreaseHealth(1);
            }

            if (collision.gameObject.name == "Teacher")
            {
                gameObject.layer = 8; // PlayerDamaged
                _sprite.color = new Color(1, 1, 1, 0.4f);
                Managers.UI.ShowPopupUI<UI_Teacher>();
                _state = Define.PlayerState.Fight;
                _teacher = collision.transform.GetComponent<Teacher>();

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
                if (Hp < _maxHp && _state != Define.PlayerState.Die)
                    Hp++;
            }

            if (collision.gameObject.name == "Coffee")
            {
                StartCoroutine("ActiveShield");
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
            _isArrive = true;
        }
    }

    void DecreaseHealth(int count)
    {
        if (PlayerPrefs.GetInt("vibrate") == 1)
            Vibration.Vibrate((long)200);

        Hp -= count;
        if (Hp <= 0)
        {
            Hp = 0;
            _state = Define.PlayerState.Die;
        }

        gameObject.layer = 8; // PlayerDamaged
        _sprite.color = new Color(1, 1, 1, 0.4f);

        StartCoroutine("DamageRecovered");
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
        if(_isArrive)
        {
            StartCoroutine("StageClearUI");
            _isArrive = false;
        }
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
        Managers.Game.Stage.Speed = 8;

        yield return new WaitForSeconds(2f);

        mapControllers[0]._speed = 4;
        mapControllers[1]._speed = 3;
        Managers.Game.Stage.Speed = 4;
    }

    IEnumerator SpeedDown()
    {
        StopCoroutine("SpeedUp");
        mapControllers[0]._speed = 2;
        mapControllers[1]._speed = 1.5f;
        Managers.Game.Stage.Speed = 2;

        yield return new WaitForSeconds(1.5f);

        mapControllers[0]._speed = 4;
        mapControllers[1]._speed = 3;
        Managers.Game.Stage.Speed = 4;
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

    IEnumerator ActiveShield()
    {
        _shield = true;
        _shieldObj.SetActive(true);
        gameObject.layer = 8; // PlayerDamaged

        yield return new WaitForSeconds(2f);

        gameObject.layer = 7;
        _shield = false;
        _shieldObj.SetActive(false);
    }

    void StopGame()
    {
        mapControllers[0]._speed = 0;
        mapControllers[1]._speed = 0;
        Managers.Game.Stage.Speed = 0;
        _anim.speed = 0f;
    }

    void rewindSpeed()
    {
        mapControllers[0]._speed = 4;
        mapControllers[1]._speed = 3;
        Managers.Game.Stage.Speed = 4;
        _anim.speed = 1f;
    }
}
