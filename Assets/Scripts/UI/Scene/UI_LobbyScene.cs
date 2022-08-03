using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_LobbyScene : UI_Scene
{
    enum Buttons { TipButton, TargetButton, ShopButton, EndingBookButton, OptionButton,  PlayButton }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        GetButton((int)Buttons.OptionButton).gameObject.BindEvent(OptionButtonClicked);
        GetButton((int)Buttons.TargetButton).gameObject.BindEvent(TargetButtonButtonClicked);
        GetButton((int)Buttons.ShopButton).gameObject.BindEvent(ShopButtonClicked);
        GetButton((int)Buttons.EndingBookButton).gameObject.BindEvent(EndingBookButtonButtonClicked);
        GetButton((int)Buttons.PlayButton).gameObject.BindEvent(PlayButtonClicked);

    }

    public void OptionButtonClicked(PointerEventData data)
    {
        // 옵션 Popup UI
        Debug.Log("OptionButtonClicked");
    }

    public void TargetButtonButtonClicked(PointerEventData data)
    {
        // 도움말 Popup UI
        Debug.Log("HelpButtonClicked");
    }

    public void ShopButtonClicked(PointerEventData data)
    {
        // 상점 Popup UI
        Debug.Log("ShopButtonClicked");
    }

    public void EndingBookButtonButtonClicked(PointerEventData data)
    {
        // 도감 Popup UI
        Debug.Log("AchievementButtonClicked");
    }

    public void PlayButtonClicked(PointerEventData data)
    {
        // TODO
        Debug.Log("PlayButtonClicked");
    }
}
