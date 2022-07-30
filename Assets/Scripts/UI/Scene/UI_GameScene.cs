using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_GameScene : UI_Scene
{
    GameObject _player = null;
    PlayerController pc = null;

    enum Buttons { JumpButton, PauseButton }
    enum Images { Letter1, Letter2, Letter3 }
    public Image[] healthUI;

    public void SetPlayer(GameObject player) { _player = player; }

    void Start()
    {
        Init();
    }

    void Update()
    {
        switch (pc._health)
        {
            case 0:
                healthUI[0].color = new Color(1, 0, 0, 1);
                healthUI[1].color = new Color(1, 0, 0, 1);
                healthUI[2].color = new Color(1, 0, 0, 1);
                break;
            case 1:
                healthUI[0].color = new Color(0, 0, 0, 1);
                healthUI[1].color = new Color(1, 0, 0, 1);
                healthUI[2].color = new Color(1, 0, 0, 1);
                break;
            case 2:
                healthUI[0].color = new Color(0, 0, 0, 1);
                healthUI[1].color = new Color(0, 0, 0, 1);
                healthUI[2].color = new Color(1, 0, 0, 1);
                break;
            case 3:
                healthUI[0].color = new Color(0, 0, 0, 1);
                healthUI[1].color = new Color(0, 0, 0, 1);
                healthUI[2].color = new Color(0, 0, 0, 1);
                break;
        }
    }

    public override void Init()
    {
        base.Init();
        pc = _player.GetComponent<PlayerController>();

        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));
        GetButton((int)Buttons.JumpButton).gameObject.BindEvent(JumpButtonClicked);
        GetButton((int)Buttons.PauseButton).gameObject.BindEvent(PauseButtonClicked);

        healthUI = new Image[3];
        healthUI[0] = GetImage((int)Images.Letter1);
        healthUI[1] = GetImage((int)Images.Letter2);
        healthUI[2] = GetImage((int)Images.Letter3);
    }

    public void JumpButtonClicked(PointerEventData data)
    {
        pc._isJump = true;
        pc._state = Define.PlayerState.Jump;
        if (pc._jumpCount > 0)
        {
            pc.GetComponent<Rigidbody2D>().velocity = Vector2.up * pc._jumpPower;
            pc._jumpCount--;
        }
    }

    public void PauseButtonClicked(PointerEventData data)
    {
        Time.timeScale = 0;

        // TODO : UI
    }

    public void HealthDown()
    {

    }
}
