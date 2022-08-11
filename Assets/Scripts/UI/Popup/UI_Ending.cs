using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Ending : UI_Popup
{
    enum Buttons { LobbyButton, }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.LobbyButton).gameObject.BindEvent(LobbyButtonClicked);
    }

    public void LobbyButtonClicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);
        Managers.Scene.LoadScene(Define.Scene.Lobby);
    }
}
