using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Ticket : UI_Popup
{
    enum Buttons
    {
        CloseButton,
        TicketItem1,
        TicketItem2,
        TicketItem3,
        TicketItem4,
    }

    enum Texts
    {
        AmberText,
        RubyText,
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));

        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(CloseButtonClicked);
        GetButton((int)Buttons.TicketItem1).gameObject.BindEvent(TicketItem1ButtonClicked);
        GetButton((int)Buttons.TicketItem2).gameObject.BindEvent(TicketItem2ButtonClicked);
        GetButton((int)Buttons.TicketItem3).gameObject.BindEvent(TicketItem3ButtonClicked);
        GetButton((int)Buttons.TicketItem4).gameObject.BindEvent(TicketItem4ButtonClicked);

        GetText((int)Texts.AmberText).text = Managers.Data.UserData.user.amber.ToString();
        GetText((int)Texts.RubyText).text = Managers.Data.UserData.user.ruby.ToString();

        UI_LobbyScene lobbyUI = (UI_LobbyScene)Managers.UI._sceneUI;
        lobbyUI.SetActiveGoodsUI(false);
    }

    public void CloseButtonClicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);
        ClosePopupUI();
    }

    public void TicketItem1ButtonClicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);

        if (Managers.Data.UserData.user.amber >= 120)
        {
            Managers.Data.UserData.user.amber -= 120;
            Managers.Data.UserData.user.extraStat = 3;
            Managers.Data.UserData.user.stage = 4;
            Managers.Data.SaveUserDataToJson(Managers.Data.UserData);
            Managers.Scene.LoadScene(Define.Scene.Game);
            ClosePopupUI();
        }
        else
        {
            
        }
    }

    public void TicketItem2ButtonClicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);

        if (Managers.Data.UserData.user.amber >= 240)
        {
            Managers.Data.UserData.user.amber -= 240;
            Managers.Data.UserData.user.extraStat = 6;
            Managers.Data.UserData.user.stage = 7;
            Managers.Data.SaveUserDataToJson(Managers.Data.UserData);
            Managers.Scene.LoadScene(Define.Scene.Game);
            ClosePopupUI();
        }
        else
        {

        }
        
    }

    public void TicketItem3ButtonClicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);

        if (Managers.Data.UserData.user.amber >= 100)
        {
            Managers.Data.UserData.user.ruby -= 100;
            Managers.Data.UserData.user.extraStat = 8;
            Managers.Data.UserData.user.stage = 9;
            Managers.Data.SaveUserDataToJson(Managers.Data.UserData);
            Managers.Scene.LoadScene(Define.Scene.Ending);
            ClosePopupUI();
        }
        else
        {

        }
    }

    public void TicketItem4ButtonClicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);
        Managers.Scene.LoadScene(Define.Scene.Game);
        ClosePopupUI();
    }
}
