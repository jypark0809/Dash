using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Pause : UI_Popup
{
    enum Buttons
    {
        KeepGoingButton,
        StopButton,
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.KeepGoingButton).gameObject.BindEvent(KeepGoingButtonClicked);
        GetButton((int)Buttons.StopButton).gameObject.BindEvent(StopButtonClicked);
    }

    public void KeepGoingButtonClicked(PointerEventData data)
    {
        // �������� ���ư���
        Managers.Sound.Play("Button", Define.Sound.Effect);
        Managers.Sound.RePlay();
        ClosePopupUI();
        Time.timeScale = 1;
    }

    public void StopButtonClicked(PointerEventData data)
    {
        // ���� �׸��ϱ�
        Time.timeScale = 1;
        Managers.Scene.LoadScene(Define.Scene.Lobby);
        Managers.Sound.Play("Button", Define.Sound.Effect);
    }
}
