using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_GameScene : UI_Scene
{
    PlayerController _player;
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
        Letter4,
        Letter5,
        Bar_Position,
        MalePinImage,
        FemalePinImage,
    }
    enum Texts
    {
        StegeText,
        StegeDigitText,
    }

    public Image[] healthUI;

    void Awake()
    {
        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));
        Bind<Text>(typeof(Texts));
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        _player = Managers.Object.Player;

        GetButton((int)Buttons.JumpButton).gameObject.BindEvent(JumpButtonClicked, Define.UIEvent.PointerDown);
        GetButton((int)Buttons.PauseButton).gameObject.BindEvent(PauseButtonClicked);

        if (PlayerPrefs.GetInt("isAccessFirst") == 0)
        {
            healthUI[0].gameObject.SetActive(false);
            healthUI[1].gameObject.SetActive(false);
            healthUI[2].gameObject.SetActive(false);
            GetButton((int)Buttons.PauseButton).gameObject.SetActive(false);
            GetText((int)Texts.StegeText).gameObject.SetActive(false);
            GetText((int)Texts.StegeDigitText).gameObject.SetActive(false);
        }
        else
        {
            GetText((int)Texts.StegeDigitText).text = SetStageNumber();
        }

        if (Managers.Game.SaveData.gender == "male")
            GetImage((int)Images.FemalePinImage).gameObject.SetActive(false);
        else if (Managers.Game.SaveData.gender == "female")
            GetImage((int)Images.MalePinImage).gameObject.SetActive(false);
        else
            GetImage((int)Images.FemalePinImage).gameObject.SetActive(false);
    }

    void Update()
    {
        if(Managers.Object.Finish != null && Managers.Object.Player.State != Define.PlayerState.Arrive)
        {
            Ratio = Managers.Object.Finish.CalculateDistance();
        }

        GetImage((int)Images.Bar_Position).rectTransform.sizeDelta = new Vector2(Ratio * 400, 30);
        GetImage((int)Images.MalePinImage).rectTransform.anchoredPosition = new Vector2(Ratio * 400 - 200, 0);
        GetImage((int)Images.FemalePinImage).rectTransform.anchoredPosition = new Vector2(Ratio * 400 - 200, 0);
    }

    public void JumpButtonClicked(PointerEventData data)
    {
        if (_player.State != Define.PlayerState.Arrive && _player.State != Define.PlayerState.Die)
        {
            _player._isJump = true;
            _player.State = Define.PlayerState.Jump;
            if (_player._jumpCount > 0)
            {
                _player._jumpCount--;
                _player.GetComponent<Rigidbody2D>().velocity = Vector2.up * _player._jumpPower;
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

    string SetStageNumber()
    {
        int upperStage = (Managers.Game.SaveData.stage + 2) / 3;
        int lowerStage = (Managers.Game.SaveData.stage + 2) % 3 + 1;
        return upperStage.ToString() + "-" + lowerStage.ToString();
    }

    public void SetHeartUI(int hp)
    {
        switch (hp)
        {
            case 5:
                GetImage((int)Images.Letter1).gameObject.SetActive(true);
                GetImage((int)Images.Letter2).gameObject.SetActive(true);
                GetImage((int)Images.Letter3).gameObject.SetActive(true);
                GetImage((int)Images.Letter4).gameObject.SetActive(true);
                GetImage((int)Images.Letter5).gameObject.SetActive(true);
                break;
            case 4:
                GetImage((int)Images.Letter1).gameObject.SetActive(true);
                GetImage((int)Images.Letter2).gameObject.SetActive(true);
                GetImage((int)Images.Letter3).gameObject.SetActive(true);
                GetImage((int)Images.Letter4).gameObject.SetActive(true);
                GetImage((int)Images.Letter5).gameObject.SetActive(false); 
                break;
            case 3:
                GetImage((int)Images.Letter1).gameObject.SetActive(true);
                GetImage((int)Images.Letter2).gameObject.SetActive(true);
                GetImage((int)Images.Letter3).gameObject.SetActive(true);
                GetImage((int)Images.Letter4).gameObject.SetActive(false);
                GetImage((int)Images.Letter5).gameObject.SetActive(false);
                break;
            case 2:
                GetImage((int)Images.Letter1).gameObject.SetActive(true);
                GetImage((int)Images.Letter2).gameObject.SetActive(true);
                GetImage((int)Images.Letter3).gameObject.SetActive(false);
                GetImage((int)Images.Letter4).gameObject.SetActive(false);
                GetImage((int)Images.Letter5).gameObject.SetActive(false);
                break;
            case 1:
                GetImage((int)Images.Letter1).gameObject.SetActive(true);
                GetImage((int)Images.Letter2).gameObject.SetActive(false);
                GetImage((int)Images.Letter3).gameObject.SetActive(false);
                GetImage((int)Images.Letter4).gameObject.SetActive(false);
                GetImage((int)Images.Letter5).gameObject.SetActive(false);
                break;
            case 0:
                GetImage((int)Images.Letter1).gameObject.SetActive(false);
                GetImage((int)Images.Letter2).gameObject.SetActive(false);
                GetImage((int)Images.Letter3).gameObject.SetActive(false);
                GetImage((int)Images.Letter4).gameObject.SetActive(false);
                GetImage((int)Images.Letter5).gameObject.SetActive(false);
                break;
        }
    }
}
