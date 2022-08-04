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
    }

    public void MaleButtonClicked(PointerEventData data)
    {
        Managers.Data.UserData.user.gender = "male";
        Managers.Data.SaveUserDataToJson(Managers.Data.UserData);
        Managers.Scene.LoadScene(Define.Scene.Game);
    }

    public void FemaleButtonClicked(PointerEventData data)
    {
        Managers.Data.UserData.user.gender = "female";
        Managers.Data.SaveUserDataToJson(Managers.Data.UserData);
        Managers.Scene.LoadScene(Define.Scene.Game);
    }
}
