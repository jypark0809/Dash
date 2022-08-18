using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_CheckStat : UI_Popup
{
    enum Buttons
    {
        OkayButton,
        CancleButton,
    }

    enum Texts
    {
        StatText,
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

        switch (PlayerPrefs.GetInt("prevStat"))
        {
            case 1:
                if (Managers.Data.UserData.user.gender == "male")
                    GetText((int)Texts.StatText).text = Define.maleStat[0];
                else if (Managers.Data.UserData.user.gender == "female")
                    GetText((int)Texts.StatText).text = Define.femaleStat[0];
                else
                    Debug.Log("Failed to load Userdata : UI_CheckStat.cs");
                break;
            case 2:
                if (Managers.Data.UserData.user.gender == "male")
                    GetText((int)Texts.StatText).text = Define.maleStat[1];
                else if (Managers.Data.UserData.user.gender == "female")
                    GetText((int)Texts.StatText).text = Define.femaleStat[1];
                else
                    Debug.Log("Failed to load Userdata : UI_CheckStat.cs");
                break;
            case 3:
                if (Managers.Data.UserData.user.gender == "male")
                    GetText((int)Texts.StatText).text = Define.maleStat[2];
                else if (Managers.Data.UserData.user.gender == "female")
                    GetText((int)Texts.StatText).text = Define.femaleStat[2];
                else
                    Debug.Log("Failed to load Userdata : UI_CheckStat.cs");
                break;
            default:
                Debug.Log("Parsing Error : UI_CheakStat");
                break;
        }
        
    }

    public void OkayButtonClicked(PointerEventData data)
    {
        if (Managers.Data.UserData.user.stage < 9)
        {
            Managers.Data.UserData.user.stage++;
            Managers.Data.SaveUserDataToJson(Managers.Data.UserData);
            Managers.Data.PrintLog();
            Managers.Sound.Play("Button", Define.Sound.Effect);
            ClosePopupUI();
            Managers.UI.ShowPopupUI<UI_NextStage>();
        }
        else
        {
            Managers.Data.SaveUserDataToJson(Managers.Data.UserData); // 최종 스텟 저장
            Managers.Data.PrintLog();
            Managers.Sound.Play("Button", Define.Sound.Effect);
            ClosePopupUI();
            Time.timeScale = 1;
            Managers.Scene.LoadScene(Define.Scene.Ending);
        }
    }

    public void CancleButtonClicked(PointerEventData data)
    {
        switch (PlayerPrefs.GetInt("prevStat"))
        {
            case 1:
                Managers.Data.UserData.user.stat1--;
                break;
            case 2:
                Managers.Data.UserData.user.stat2--;
                break;
            case 3:
                Managers.Data.UserData.user.stat3--;
                break;
            default:
                Debug.Log("Fail to Save Json : Managers.Data.UserData.user.prevStat");
                break;
        }
        Managers.Data.PrintLog();
        Managers.Sound.Play("Button", Define.Sound.Effect);
        ClosePopupUI();
    }
}
