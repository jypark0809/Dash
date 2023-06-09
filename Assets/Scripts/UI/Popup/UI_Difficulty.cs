using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Difficulty : UI_Popup
{
    enum Buttons
    {
        CloseButton,
        EasyButton,
        NomalButton,
        HardButton
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.EasyButton).gameObject.BindEvent(OnEasyButtonClicked);
        GetButton((int)Buttons.NomalButton).gameObject.BindEvent(OnNomalButtonClicked);
        GetButton((int)Buttons.HardButton).gameObject.BindEvent(OnHardButtonClicked);
        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(OnCloseButtonClicked);
    }

    public void OnEasyButtonClicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);
        PlayerPrefs.SetInt("difficulty", 0);
        ClosePopupUI();
        Managers.UI.ShowPopupUI<UI_Ticket>();
    }

    public void OnNomalButtonClicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);
        PlayerPrefs.SetInt("difficulty", 1);
        ClosePopupUI();
        Managers.UI.ShowPopupUI<UI_Ticket>();
    }

    public void OnHardButtonClicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);
        PlayerPrefs.SetInt("difficulty", 2);
        ClosePopupUI();
        Managers.UI.ShowPopupUI<UI_Ticket>();
    }

    public void OnCloseButtonClicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);
        Managers.Game.SaveData.gender = "unselected";
        ClosePopupUI();
    }
}
