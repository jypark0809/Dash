using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Goal : UI_Popup
{
    enum Buttons
    {
        OkayButton,
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
    }

    public void OkayButtonClicked(PointerEventData data)
    {
        Managers.Data.UserData.user.amber += 10;
        Managers.Data.SaveUserDataToJson(Managers.Data.UserData);
        Managers.Data.PrintLog();
        ClosePopupUI();
        Managers.UI.ShowPopupUI<UI_StageClear>();
    }
}
