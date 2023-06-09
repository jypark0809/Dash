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

        GetText((int)Texts.AmberText).text = Managers.Game.SaveData.amber.ToString();
        GetText((int)Texts.RubyText).text = Managers.Game.SaveData.ruby.ToString();

        UI_LobbyScene lobbyUI = (UI_LobbyScene)Managers.UI.SceneUI;
        lobbyUI.SetActiveGoodsUI(false);
    }

    public void CloseButtonClicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);
        Managers.Game.SaveData.gender = "unselected";
        ClosePopupUI();
    }

    public void TicketItem1ButtonClicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);

        if (Managers.Game.SaveData.amber >= 120)
        {
            Managers.Game.SaveData.amber -= 120;
            Managers.Game.SaveData.extraStat = 3;
            Managers.Game.SaveData.stage = 4;
            Managers.Game.SaveGame();
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

        if (Managers.Game.SaveData.amber >= 240)
        {
            Managers.Game.SaveData.amber -= 240;
            Managers.Game.SaveData.extraStat = 6;
            Managers.Game.SaveData.stage = 7;
            Managers.Game.SaveGame();
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

        if (Managers.Game.SaveData.ruby >= 100)
        {
            Managers.Game.SaveData.ruby -= 100;
            Managers.Game.SaveData.extraStat = 8;
            Managers.Game.SaveData.stage = 9;
            Managers.Game.SaveGame();
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
        Managers.Game.SaveGame();
        Managers.Scene.LoadScene(Define.Scene.Game);
        ClosePopupUI();
    }
}
