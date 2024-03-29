using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_CheckGender : UI_Popup
{
    enum Buttons
    {
        OkayButton,
        CancleButton,
    }

    enum Texts
    {
        GenderText,
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

        GetButton((int)Buttons.OkayButton).gameObject.BindEvent(OkayButtonClicked);
        GetButton((int)Buttons.CancleButton).gameObject.BindEvent(CancleButtonClicked);

        if (Managers.Game.SaveData.gender == "male")
        {
            GetText((int)Texts.GenderText).text = "남자";
            GetText((int)Texts.GenderText).color = new Color(0, 0, 1);
        }
        else if (Managers.Game.SaveData.gender == "female")
        {
            GetText((int)Texts.GenderText).text = "여자";
            GetText((int)Texts.GenderText).color = new Color(1, 0, 0);
        }
        else
            Debug.Log("Failed to Load Gender : UI_CheckGender.cs");
    }

    public void OkayButtonClicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);
        Managers.UI.CloseAllPopupUI();
        if (Managers.Game.SaveData.round == 0)
        {
            Managers.UI.ShowPopupUI<UI_Nickname>("UI_Nickname");
        }
        else
        {
            Managers.UI.ShowPopupUI<UI_Difficulty>();
        }
    }

    public void CancleButtonClicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);
        Managers.Game.SaveData.gender = "unselected";
        ClosePopupUI();
    }
}
