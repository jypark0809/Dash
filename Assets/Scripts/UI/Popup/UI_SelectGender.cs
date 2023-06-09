using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SelectGender : UI_Popup
{
    enum Buttons
    {
        MaleButton,
        FemaleButton,
        CloseButton,
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.MaleButton).gameObject.BindEvent(MaleButtonClicked);
        GetButton((int)Buttons.FemaleButton).gameObject.BindEvent(FemaleButtonClicked);
        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(CloseButtonClicked);
    }

    public void MaleButtonClicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);
        Managers.Game.SaveData.gender = "male";
        Managers.UI.ShowPopupUI<UI_CheckGender>();
    }

    public void FemaleButtonClicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);
        Managers.Game.SaveData.gender = "female";
        Managers.UI.ShowPopupUI<UI_CheckGender>();
    }

    public void CloseButtonClicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);
        ClosePopupUI();
    }
}
