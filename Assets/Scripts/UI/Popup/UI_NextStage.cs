using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_NextStage : UI_Popup
{
    enum Buttons
    {
        OkayButton,
        CancleButton,
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.OkayButton).gameObject.BindEvent(OkayButtonClicked);
        GetButton((int)Buttons.CancleButton).gameObject.BindEvent(CancleButtonClicked);
    }

    public void OkayButtonClicked(PointerEventData data)
    {
        Managers.Data.UserData.user.stage++;
        Managers.Data.SaveUserDataToJson(Managers.Data.UserData);
        Time.timeScale = 1;
        Managers.Scene.LoadScene(Define.Scene.Game);
        Managers.Sound.Play("Button", Define.Sound.Effect);
    }

    public void CancleButtonClicked(PointerEventData data)
    {
        Managers.Data.UserData.user.stage++;
        Time.timeScale = 1;
        ClosePopupUI();
        Managers.Scene.LoadScene(Define.Scene.Lobby);
        Managers.Sound.Play("Button", Define.Sound.Effect);
    }
}
