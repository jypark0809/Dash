using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SelectNpc : UI_Popup
{
    public Sprite[] _sprites;

    enum Buttons
    {
        NpcButton1,
        NpcButton2,
        NpcButton3,
    }

    enum Texts
    {
        Stat1,
        Stat2,
        Stat3,
        StatValue1,
        StatValue2,
        StatValue3,
        NpcText1,
        NpcText2,
        NpcText3,
    }

    enum Images
    {
        NpcImage1,
        NpcImage2,
        NpcImage3,
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
        Bind<Image>(typeof(Images));

        GetButton((int)Buttons.NpcButton1).gameObject.BindEvent(NpcButton1ButtonClicked);
        GetButton((int)Buttons.NpcButton2).gameObject.BindEvent(NpcButton2ButtonClicked);
        GetButton((int)Buttons.NpcButton3).gameObject.BindEvent(NpcButton3ButtonClicked);

        if (Managers.Data.UserData.user.gender == "male")
        {
            GetText((int)Texts.Stat1).text = Define.maleStat[0];
            GetText((int)Texts.Stat2).text = Define.maleStat[1];
            GetText((int)Texts.Stat3).text = Define.maleStat[2];
            GetText((int)Texts.NpcText1).text = Define.maleTarget[0];
            GetText((int)Texts.NpcText2).text = Define.maleTarget[1];
            GetText((int)Texts.NpcText3).text = Define.maleTarget[2];
            GetImage((int)Images.NpcImage1).sprite = _sprites[0];
            GetImage((int)Images.NpcImage1).SetNativeSize();
            GetImage((int)Images.NpcImage2).sprite = _sprites[1];
            GetImage((int)Images.NpcImage2).SetNativeSize();
            GetImage((int)Images.NpcImage3).sprite = _sprites[2];
            GetImage((int)Images.NpcImage3).SetNativeSize();

        }
        else if (Managers.Data.UserData.user.gender == "female")
        {
            GetText((int)Texts.Stat1).text = Define.femaleStat[0];
            GetText((int)Texts.Stat2).text = Define.femaleStat[1];
            GetText((int)Texts.Stat3).text = Define.femaleStat[2];
            GetText((int)Texts.NpcText1).text = Define.femaleTarget[0];
            GetText((int)Texts.NpcText2).text = Define.femaleTarget[1];
            GetText((int)Texts.NpcText3).text = Define.femaleTarget[2];
            GetImage((int)Images.NpcImage1).sprite = _sprites[3];
            GetImage((int)Images.NpcImage1).SetNativeSize();
            GetImage((int)Images.NpcImage2).sprite = _sprites[4];
            GetImage((int)Images.NpcImage2).SetNativeSize();
            GetImage((int)Images.NpcImage3).sprite = _sprites[5];
            GetImage((int)Images.NpcImage3).SetNativeSize();
        }
        else
            Debug.Log("Failed to bind Text : UI_SelectNpc.cs");

        GetText((int)Texts.StatValue1).text = Managers.Data.UserData.user.stat1.ToString();
        GetText((int)Texts.StatValue2).text = Managers.Data.UserData.user.stat2.ToString();
        GetText((int)Texts.StatValue3).text = Managers.Data.UserData.user.stat3.ToString();
    }

    public void NpcButton1ButtonClicked(PointerEventData data)
    {
        if (Managers.Data.UserData.user.gender == "male")
            PlayerPrefs.SetInt("npcId", 1);
        else if (Managers.Data.UserData.user.gender == "female")
            PlayerPrefs.SetInt("npcId", 4);
        else
            Debug.Log("Failed to save PlayerPrefs of npcId : UI_SelectNpc.cs");
        ClosePopupUI();
        Managers.Sound.Play("Button", Define.Sound.Effect);
        Managers.UI.ShowPopupUI<UI_Scripts>();
    }

    public void NpcButton2ButtonClicked(PointerEventData data)
    {
        if (Managers.Data.UserData.user.gender == "male")
            PlayerPrefs.SetInt("npcId", 2);
        else if (Managers.Data.UserData.user.gender == "female")
            PlayerPrefs.SetInt("npcId", 5);
        else
            Debug.Log("Failed to save PlayerPrefs of npcId : UI_SelectNpc.cs");
        ClosePopupUI();
        Managers.Sound.Play("Button", Define.Sound.Effect);
        Managers.UI.ShowPopupUI<UI_Scripts>();
    }

    public void NpcButton3ButtonClicked(PointerEventData data)
    {
        if (Managers.Data.UserData.user.gender == "male")
            PlayerPrefs.SetInt("npcId", 3);
        else if (Managers.Data.UserData.user.gender == "female")
            PlayerPrefs.SetInt("npcId", 6);
        else
            Debug.Log("Failed to save PlayerPrefs of npcId : UI_SelectNpc.cs");
        ClosePopupUI();
        Managers.Sound.Play("Button", Define.Sound.Effect);
        Managers.UI.ShowPopupUI<UI_Scripts>();
    }
}
