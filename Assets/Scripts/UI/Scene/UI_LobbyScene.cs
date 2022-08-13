using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_LobbyScene : UI_Scene
{
    enum Buttons
    {
        TipButton,
        TargetButton,
        ShopButton,
        EndingBookButton,
        OptionButton,
        PlayButton,
        InitButton,
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.TipButton).gameObject.BindEvent(TipButtonClicked);
        GetButton((int)Buttons.OptionButton).gameObject.BindEvent(OptionButtonClicked);
        GetButton((int)Buttons.TargetButton).gameObject.BindEvent(TargetButtonClicked);
        GetButton((int)Buttons.ShopButton).gameObject.BindEvent(ShopButtonClicked);
        GetButton((int)Buttons.EndingBookButton).gameObject.BindEvent(EndingBookButtonClicked);
        GetButton((int)Buttons.PlayButton).gameObject.BindEvent(PlayButtonClicked);
        GetButton((int)Buttons.InitButton).gameObject.BindEvent(InitButtonClicked);
    }

    public void TipButtonClicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);
        Managers.UI.ShowPopupUI<UI_Tip>();
    }

    public void OptionButtonClicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);
        Debug.Log("OptionButtonClicked");
    }

    public void TargetButtonClicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);
        Debug.Log("TargetButtonClicked");
    }

    public void ShopButtonClicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);
        Debug.Log("ShopButtonClicked");
    }

    public void EndingBookButtonClicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);
        Debug.Log("EndingBookButtonClicked");
    }

    public void PlayButtonClicked(PointerEventData data)
    {
        if (Managers.Data.UserData.user.stat1 + Managers.Data.UserData.user.stat2 + Managers.Data.UserData.user.stat3 < 9)
        {
            if (Managers.Data.UserData.user.gender == "unselected")
                Managers.UI.ShowPopupUI<UI_SelectGender>();
            else
            {
                Managers.Scene.LoadScene(Define.Scene.Game);
            }

            Managers.Sound.Play("Button", Define.Sound.Effect);
        }
        else
        {
            // 스텟이 9 이상이면 바로 EndingScene
            Managers.Data.PrintLog();
            Managers.Scene.LoadScene(Define.Scene.Ending);
            Managers.Sound.Play("Button", Define.Sound.Effect);
        }
    }

    public void InitButtonClicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);
        Managers.UI.ShowPopupUI<UI_Test>();
    }
}
