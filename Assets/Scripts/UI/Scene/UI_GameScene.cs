using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_GameScene : UI_Scene
{
    GameObject _player = null;
    PlayerController pc = null;
    public float countdownSeconds = 30;
    // int healthCursor;

    enum Buttons { JumpButton, PauseButton }
    enum Images { Letter1, Letter2, Letter3, Clock }
    enum Texts { TimeText, }
    public Image[] healthUI;

    public void SetPlayer(GameObject player) { _player = player; }

    void Start()
    {
        Init();
    }

    void Update()
    {
        countdownSeconds -= Time.deltaTime;
        var span = new TimeSpan(0, 0, (int)countdownSeconds);
        GetText((int)Texts.TimeText).text = span.ToString(@"mm\:ss");

        switch (pc._health)
        {
            case 0:
                healthUI[0].gameObject.SetActive(true);
                healthUI[1].gameObject.SetActive(false);
                healthUI[2].gameObject.SetActive(false);
                break;
            case 1:
                healthUI[0].gameObject.SetActive(true);
                healthUI[1].gameObject.SetActive(false);
                healthUI[2].gameObject.SetActive(false);
                break;
            case 2:
                healthUI[0].gameObject.SetActive(true);
                healthUI[1].gameObject.SetActive(true);
                healthUI[2].gameObject.SetActive(false);
                break;
            case 3:
                healthUI[0].gameObject.SetActive(true);
                healthUI[1].gameObject.SetActive(true);
                healthUI[2].gameObject.SetActive(true);
                break;
        }
    }

    public override void Init()
    {
        base.Init();
        pc = _player.GetComponent<PlayerController>();

        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));
        Bind<Text>(typeof(Texts));
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
        Managers.UI.ShowPopupUI<UI_Pause>();
    }
}
