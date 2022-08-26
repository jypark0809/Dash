using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ChangeNickname : UI_Popup
{
    enum Texts
    {
        NicknameText,
    }

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

        Bind<Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.OkayButton).gameObject.BindEvent(OkayButtonClicked);
        GetButton((int)Buttons.CancleButton).gameObject.BindEvent(CancleButtonClicked);
    }

    public void OkayButtonClicked(PointerEventData data)
    {
        Managers.Data.UserData.user.nickname = GetText((int)Texts.NicknameText).text;
        Managers.Data.SaveUserDataToJson(Managers.Data.UserData);
        ClosePopupUI();
    }

    public void CancleButtonClicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);
        ClosePopupUI();
    }
}
