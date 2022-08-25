using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_StageClear : UI_Popup
{
    public Sprite[] _sprites;
    bool _isTicket;

    enum Buttons
    {
        StatButton1,
        StatButton2,
        StatButton3,
        OkayButton,
        CancleButton,
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
        RemainStatText,
        PrevStatText,
    }

    enum Images
    {
        StatImage1,
        StatImage2,
        StatImage3,
        CheckPanel,
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

        Managers.Data.UserData.user.extraStat++;
        _isTicket = (Managers.Data.UserData.user.extraStat == 9);

        GetButton((int)Buttons.StatButton1).gameObject.BindEvent(StatButton1Clicked);
        GetButton((int)Buttons.StatButton2).gameObject.BindEvent(StatButton2Clicked);
        GetButton((int)Buttons.StatButton3).gameObject.BindEvent(StatButton3Clicked);
        GetButton((int)Buttons.OkayButton).gameObject.BindEvent(OkayButtonClicked);
        GetButton((int)Buttons.CancleButton).gameObject.BindEvent(CancleButtonClicked);
        GetImage((int)Images.CheckPanel).gameObject.SetActive(false);
        UpdateUserStat();

        if (Managers.Data.UserData.user.gender == "male")
        {
            GetText((int)Texts.Stat1).text = Define.maleStat[0];
            GetText((int)Texts.StatButtonText1).text = Define.maleStat[0];
            GetText((int)Texts.Stat2).text = Define.maleStat[1];
            GetText((int)Texts.StatButtonText2).text = Define.maleStat[1];
            GetText((int)Texts.Stat3).text = Define.maleStat[2];
            GetText((int)Texts.StatButtonText3).text = Define.maleStat[2];
            GetImage((int)Images.StatImage1).sprite = _sprites[0];
            GetImage((int)Images.StatImage1).SetNativeSize();
            GetImage((int)Images.StatImage2).sprite = _sprites[1];
            GetImage((int)Images.StatImage2).SetNativeSize();
            GetImage((int)Images.StatImage3).sprite = _sprites[2];
            GetImage((int)Images.StatImage3).SetNativeSize();

        }
        else if (Managers.Data.UserData.user.gender == "female")
        {
            GetText((int)Texts.Stat1).text = Define.femaleStat[0];
            GetText((int)Texts.StatButtonText1).text = Define.femaleStat[0];
            GetText((int)Texts.Stat2).text = Define.femaleStat[1];
            GetText((int)Texts.StatButtonText2).text = Define.femaleStat[1];
            GetText((int)Texts.Stat3).text = Define.femaleStat[2];
            GetText((int)Texts.StatButtonText3).text = Define.femaleStat[2];
            GetImage((int)Images.StatImage1).sprite = _sprites[3];
            GetImage((int)Images.StatImage1).SetNativeSize();
            GetImage((int)Images.StatImage2).sprite = _sprites[4];
            GetImage((int)Images.StatImage2).SetNativeSize();
            GetImage((int)Images.StatImage3).sprite = _sprites[5];
            GetImage((int)Images.StatImage3).SetNativeSize();
        }
        else
            Debug.Log("Failed to bind Text : UI_StageClear.cs");
    }

    public void StatButton1Clicked(PointerEventData data)
    {
        PlayerPrefs.SetInt("prevStat", 1);
        setPrevStat(PlayerPrefs.GetInt("prevStat"));
        GetImage((int)Images.CheckPanel).gameObject.SetActive(true);
        Managers.Sound.Play("Button", Define.Sound.Effect);
    }

    public void StatButton2Clicked(PointerEventData data)
    {
        PlayerPrefs.SetInt("prevStat", 2);
        setPrevStat(PlayerPrefs.GetInt("prevStat"));
        GetImage((int)Images.CheckPanel).gameObject.SetActive(true);
        Managers.Sound.Play("Button", Define.Sound.Effect);
    }

    public void StatButton3Clicked(PointerEventData data)
    {
        PlayerPrefs.SetInt("prevStat", 3);
        setPrevStat(PlayerPrefs.GetInt("prevStat"));
        GetImage((int)Images.CheckPanel).gameObject.SetActive(true);
        Managers.Sound.Play("Button", Define.Sound.Effect);
    }

    public void OkayButtonClicked(PointerEventData data)
    {
        // 9 스테이지 클리어 했을 때
        if (Managers.Data.UserData.user.stage == 9 && StatSum() == 8 && _isTicket == false)
        {
            switch (PlayerPrefs.GetInt("prevStat"))
            {
                case 1:
                    Managers.Data.UserData.user.stat1++;
                    break;
                case 2:
                    Managers.Data.UserData.user.stat2++;
                    break;
                case 3:
                    Managers.Data.UserData.user.stat3++;
                    break;
            }

            Managers.Data.UserData.user.extraStat--;
            Managers.Data.SaveUserDataToJson(Managers.Data.UserData); // 최종 스텟 저장
            Managers.Sound.Play("Button", Define.Sound.Effect);
            ClosePopupUI();
            Time.timeScale = 1;
            Managers.Scene.LoadScene(Define.Scene.Ending);
        }
        // 1~8 스테이지 클리어 했을 때
        else if (Managers.Data.UserData.user.stage < 9)
        {
            switch (PlayerPrefs.GetInt("prevStat"))
            {
                case 1:
                    Managers.Data.UserData.user.stat1++;
                    break;
                case 2:
                    Managers.Data.UserData.user.stat2++;
                    break;
                case 3:
                    Managers.Data.UserData.user.stat3++;
                    break;
            }

            Managers.Data.UserData.user.extraStat--;
            Managers.Sound.Play("Button", Define.Sound.Effect);
            UpdateUserStat();
            GetImage((int)Images.CheckPanel).gameObject.SetActive(false);
            
            if (Managers.Data.UserData.user.extraStat == 0)
            {
                ClosePopupUI();
                Managers.UI.ShowPopupUI<UI_NextStage>();
            }
        }
        // 돌파권 썼을 때
        else if (Managers.Data.UserData.user.stage == 9 && _isTicket)
        {
            switch (PlayerPrefs.GetInt("prevStat"))
            {
                case 1:
                    Managers.Data.UserData.user.stat1++;
                    break;
                case 2:
                    Managers.Data.UserData.user.stat2++;
                    break;
                case 3:
                    Managers.Data.UserData.user.stat3++;
                    break;
            }

            Managers.Data.UserData.user.extraStat--;
            Managers.Data.SaveUserDataToJson(Managers.Data.UserData);
            Managers.Sound.Play("Button", Define.Sound.Effect);
            UpdateUserStat();
            GetImage((int)Images.CheckPanel).gameObject.SetActive(false);

            if (Managers.Data.UserData.user.extraStat == 0)
            {
                ClosePopupUI();
                Managers.UI.ShowPopupUI<UI_SelectNpc>();
            }
        }
    }

    public void CancleButtonClicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);
        GetImage((int)Images.CheckPanel).gameObject.SetActive(false);
    }

    void setPrevStat(int prevStat)
    {
        switch (prevStat)
        {
            case 1:
                if (Managers.Data.UserData.user.gender == "male")
                    GetText((int)Texts.PrevStatText).text = Define.maleStat[0];
                else if (Managers.Data.UserData.user.gender == "female")
                    GetText((int)Texts.PrevStatText).text = Define.femaleStat[0];
                else
                    Debug.Log("Failed to load Userdata : UI_CheckStat.cs");
                break;
            case 2:
                if (Managers.Data.UserData.user.gender == "male")
                    GetText((int)Texts.PrevStatText).text = Define.maleStat[1];
                else if (Managers.Data.UserData.user.gender == "female")
                    GetText((int)Texts.PrevStatText).text = Define.femaleStat[1];
                else
                    Debug.Log("Failed to load Userdata : UI_CheckStat.cs");
                break;
            case 3:
                if (Managers.Data.UserData.user.gender == "male")
                    GetText((int)Texts.PrevStatText).text = Define.maleStat[2];
                else if (Managers.Data.UserData.user.gender == "female")
                    GetText((int)Texts.PrevStatText).text = Define.femaleStat[2];
                else
                    Debug.Log("Failed to load Userdata : UI_CheckStat.cs");
                break;
            default:
                Debug.Log("Parsing Error : UI_CheakStat");
                break;
        }
    }

    void UpdateUserStat()
    {
        GetText((int)Texts.StatValue1).text = Managers.Data.UserData.user.stat1.ToString();
        GetText((int)Texts.StatValue2).text = Managers.Data.UserData.user.stat2.ToString();
        GetText((int)Texts.StatValue3).text = Managers.Data.UserData.user.stat3.ToString();
        GetText((int)Texts.PrevStatText).text = GetText((int)Texts.RemainStatText).text = "남은 스텟 포인트 : " + Managers.Data.UserData.user.extraStat;
    }

    int StatSum()
    {
        return Managers.Data.UserData.user.stat1 + Managers.Data.UserData.user.stat2 + Managers.Data.UserData.user.stat3;
    }
}
