using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_StageClear : UI_Popup
{
    enum Buttons
    {
        StatButton1,
        StatButton2,
        StatButton3,
    }

    enum Texts
    {
        Stat1,
        Stat2,
        Stat3,
        StatValue1,
        StatValue2,
        StatValue3,
        StatButtonText1,
        StatButtonText2,
        StatButtonText3,
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

        GetButton((int)Buttons.StatButton1).gameObject.BindEvent(StatButton1Clicked);
        GetButton((int)Buttons.StatButton2).gameObject.BindEvent(StatButton2Clicked);
        GetButton((int)Buttons.StatButton3).gameObject.BindEvent(StatButton3Clicked);

        if (Managers.Data.UserData.user.gender == "male")
        {
            GetText((int)Texts.Stat1).text = Define.maleStat[0];
            GetText((int)Texts.StatButtonText1).text = Define.maleStat[0];
            GetText((int)Texts.Stat2).text = Define.maleStat[1];
            GetText((int)Texts.StatButtonText2).text = Define.maleStat[1];
            GetText((int)Texts.Stat3).text = Define.maleStat[2];
            GetText((int)Texts.StatButtonText3).text = Define.maleStat[2];

        }
        else if (Managers.Data.UserData.user.gender == "female")
        {
            GetText((int)Texts.Stat1).text = Define.femaleStat[0];
            GetText((int)Texts.StatButtonText1).text = Define.femaleStat[0];
            GetText((int)Texts.Stat2).text = Define.femaleStat[1];
            GetText((int)Texts.StatButtonText2).text = Define.femaleStat[1];
            GetText((int)Texts.Stat3).text = Define.femaleStat[2];
            GetText((int)Texts.StatButtonText3).text = Define.femaleStat[2];
        }
        else
            Debug.Log("Failed to bind Text : Managers.Data.UserData.user.gender");

        GetText((int)Texts.StatValue1).text = Managers.Data.UserData.user.stat1.ToString();
        GetText((int)Texts.StatValue2).text = Managers.Data.UserData.user.stat2.ToString();
        GetText((int)Texts.StatValue3).text = Managers.Data.UserData.user.stat3.ToString();
    }

    public void StatButton1Clicked(PointerEventData data)
    {
        Managers.Data.UserData.user.stat1++;
        Managers.Data.UserData.user.prevStat = 1;
        Managers.Sound.Play("Button", Define.Sound.Effect);
        Managers.Data.PrintLog();
        Managers.UI.ShowPopupUI<UI_CheckStat>();
    }

    public void StatButton2Clicked(PointerEventData data)
    {
        Managers.Data.UserData.user.stat2++;
        Managers.Data.UserData.user.prevStat = 2;
        Managers.Sound.Play("Button", Define.Sound.Effect);
        Managers.Data.PrintLog();
        Managers.UI.ShowPopupUI<UI_CheckStat>();
    }

    public void StatButton3Clicked(PointerEventData data)
    {
        Managers.Data.UserData.user.stat3++;
        Managers.Data.UserData.user.prevStat = 3;
        Managers.Sound.Play("Button", Define.Sound.Effect);
        Managers.Data.PrintLog();
        Managers.UI.ShowPopupUI<UI_CheckStat>();
    }
}
