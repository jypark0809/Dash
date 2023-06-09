using System.Collections;
using UnityEngine;
using static Define;

public class PlayerController : BaseController
{
    public int _jumpCount = 2;
    public int JumpCount { get { return _jumpCount; } set { _jumpCount = value; } }
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
    public int MaxHp { get { return _maxHp; } set { _maxHp = value; } }
    public bool _isFight = false;

    public GameObject Shield;
    public PlayerState State { get; set; } = PlayerState.Run;

    public Animator Anim { get; set; }
    public SpriteRenderer Sprite { get; set; }

    public override void Init()
    {
        base.Init();
        ObjectType = ObjectType.Player;
        Anim = GetComponent<Animator>();
        Sprite = GetComponent<SpriteRenderer>();
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
        switch (State)
        {
            case Define.PlayerState.Run:
                UpdateRun();
                break;
            case Define.PlayerState.Jump:
                UpdateJump();
                break;
        }
    }

    void UpdateRun()
    {
        Anim.SetBool("isJump", false);
    }

    void UpdateJump()
    {
        Anim.SetBool("isJump", true);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            if (State != Define.PlayerState.Die && State != Define.PlayerState.Arrive)
            {
                State = Define.PlayerState.Run;
                _jumpCount = 2;
                _isJump = false;
            }
        }
    }

    public void OnDamaged(ObstacleController obstacle, int damage)
    {
        if (PlayerPrefs.GetInt("vibrate") == 1)
            Vibration.Vibrate((long)200);

        gameObject.layer = 8; // PlayerDamaged Layer
        Sprite.color = new Color(1, 1, 1, 0.4f);

        if (Shield.activeSelf)
        {
            Shield.SetActive(false);
            return;
        }

        Hp -= damage;
        if (Hp <= 0)
        {
            Hp = 0;
            OnDead();
        }
    }

    void OnDead()
    {
        State = Define.PlayerState.Die;
        (Managers.Scene.CurrentScene as GameScene).GameOver();
    }
}
