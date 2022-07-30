using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_LobbyScene : UI_Scene
{
    enum Buttons { OptionButton, HelpButton, ShopButton, AchievementButton, PlayButton }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        GetButton((int)Buttons.OptionButton).gameObject.BindEvent(OptionButtonClicked);
        GetButton((int)Buttons.HelpButton).gameObject.BindEvent(HelpButtonClicked);
        GetButton((int)Buttons.ShopButton).gameObject.BindEvent(ShopButtonClicked);
        GetButton((int)Buttons.AchievementButton).gameObject.BindEvent(AchievementButtonClicked);
        GetButton((int)Buttons.PlayButton).gameObject.BindEvent(PlayButtonClicked);

    }

    public void OptionButtonClicked(PointerEventData data)
    {
        // �ɼ� Popup UI
        Debug.Log("OptionButtonClicked");
    }

    public void HelpButtonClicked(PointerEventData data)
    {
        // ���� Popup UI
        Debug.Log("HelpButtonClicked");
    }

    public void ShopButtonClicked(PointerEventData data)
    {
        // ���� Popup UI
        Debug.Log("ShopButtonClicked");
    }

    public void AchievementButtonClicked(PointerEventData data)
    {
        // ���� Popup UI
        Debug.Log("AchievementButtonClicked");
    }

    public void PlayButtonClicked(PointerEventData data)
    {
        // TODO
        Debug.Log("PlayButtonClicked");
    }
}
