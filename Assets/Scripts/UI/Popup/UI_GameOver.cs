using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_GameOver : UI_Popup
{
    enum Buttons { RetryButton, StopButton, }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.RetryButton).gameObject.BindEvent(RetryButtonClicked);
        GetButton((int)Buttons.StopButton).gameObject.BindEvent(StopButtonClicked);
    }

    public void RetryButtonClicked(PointerEventData data)
    {
        Time.timeScale = 1;
        Managers.Sound.Play("Button", Define.Sound.Effect);
        Managers.Scene.LoadScene(Define.Scene.Game);
    }

    public void StopButtonClicked(PointerEventData data)
    {
        Time.timeScale = 1;
        Managers.Sound.Play("Button", Define.Sound.Effect);
        Managers.Scene.LoadScene(Define.Scene.Lobby);
    }
}
