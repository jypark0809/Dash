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
    public float Ratio { get; set; }

    enum Buttons
    {
        JumpButton,
        PauseButton
    }

    enum Images
    {
        Letter1,
        Letter2,
        Letter3,
        Bar_Position,
        MalePinImage,
        FemalePinImage,
    }
    enum Texts
    {
        StegeDigitText,
    }

    public Image[] healthUI;

    public void SetPlayer(GameObject player) { _player = player; }

    void Start()
    {
        Init();
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

        GetText((int)Texts.StegeDigitText).text = CalStage();

        if (Managers.Data.UserData.user.gender == "male")
            GetImage((int)Images.FemalePinImage).gameObject.SetActive(false);

        if (Managers.Data.UserData.user.gender == "female")
            GetImage((int)Images.MalePinImage).gameObject.SetActive(false);
    }

    void Update()
    {
        switch (pc._health)
        {
            case 0:
                healthUI[0].gameObject.SetActive(false);
                healthUI[1].gameObject.SetActive(false);
                healthUI[2].gameObject.SetActive(false);
                healthUI[3].gameObject.SetActive(false);
                healthUI[4].gameObject.SetActive(false);
                break;
            case 1:
                healthUI[0].gameObject.SetActive(true);
                healthUI[1].gameObject.SetActive(false);
                healthUI[2].gameObject.SetActive(false);
                healthUI[3].gameObject.SetActive(false);
                healthUI[4].gameObject.SetActive(false);
                break;
            case 2:
                healthUI[0].gameObject.SetActive(true);
                healthUI[1].gameObject.SetActive(true);
                healthUI[2].gameObject.SetActive(false);
                healthUI[3].gameObject.SetActive(false);
                healthUI[4].gameObject.SetActive(false);
                break;
            case 3:
                healthUI[0].gameObject.SetActive(true);
                healthUI[1].gameObject.SetActive(true);
                healthUI[2].gameObject.SetActive(true);
                healthUI[3].gameObject.SetActive(false);
                healthUI[4].gameObject.SetActive(false);
                break;
            case 4:
                healthUI[0].gameObject.SetActive(true);
                healthUI[1].gameObject.SetActive(true);
                healthUI[2].gameObject.SetActive(true);
                healthUI[3].gameObject.SetActive(true);
                healthUI[4].gameObject.SetActive(false);
                break;
            case 5:
                healthUI[0].gameObject.SetActive(true);
                healthUI[1].gameObject.SetActive(true);
                healthUI[2].gameObject.SetActive(true);
                healthUI[3].gameObject.SetActive(true);
                healthUI[4].gameObject.SetActive(true);
                break;
        }

        GetImage((int)Images.Bar_Position).rectTransform.sizeDelta = new Vector2(Ratio * 400, 30);
        GetImage((int)Images.MalePinImage).rectTransform.anchoredPosition = new Vector2(Ratio * 400 - 200, 0);
        GetImage((int)Images.FemalePinImage).rectTransform.anchoredPosition = new Vector2(Ratio * 400 - 200, 0);
    }

    public void JumpButtonClicked(PointerEventData data)
    {
        if ( pc._state != Define.PlayerState.Clear && pc._state != Define.PlayerState.Die)
        {
            pc._isJump = true;
            pc._state = Define.PlayerState.Jump;
            if (pc._jumpCount > 0)
            {
                pc.GetComponent<Rigidbody2D>().velocity = Vector2.up * pc._jumpPower;
                pc._jumpCount--;
                Managers.Sound.Play("Jump", Define.Sound.Effect);
            }
        }
    }

    public void PauseButtonClicked(PointerEventData data)
    {
        Time.timeScale = 0;

        Managers.Sound.Play("Button", Define.Sound.Effect);
        Managers.Sound.Pause();

        // TODO : UI
        Managers.UI.ShowPopupUI<UI_Pause>();
    }

    string CalStage()
    {
        int upperStage = (Managers.Data.UserData.user.stage + 2) / 3;
        int lowerStage = (Managers.Data.UserData.user.stage + 2) % 3 + 1;
        return upperStage.ToString() + "-" + lowerStage.ToString();
    }
}
