using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SelectNpc : UI_Popup
{
    int _npcId;
    int _stat1, _stat2, _stat3;

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

        _npcId = PlayerPrefs.GetInt("npcId");

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
        {
            PlayerPrefs.SetInt("npcId", 1);
            _npcId = 1;
        }
        else if (Managers.Data.UserData.user.gender == "female")
        {
            PlayerPrefs.SetInt("npcId", 4);
            _npcId = 4;
        }
        else
            Debug.Log("Failed to save PlayerPrefs of npcId : UI_SelectNpc.cs");
        branchEnding();
        Managers.Data.ClearAllStage();
        ClosePopupUI();
        Managers.Sound.Play("Button", Define.Sound.Effect);
        Managers.UI.ShowPopupUI<UI_Scripts>();
    }

    public void NpcButton2ButtonClicked(PointerEventData data)
    {
        if (Managers.Data.UserData.user.gender == "male")
        {
            PlayerPrefs.SetInt("npcId", 2);
            _npcId = 2;
        }
        else if (Managers.Data.UserData.user.gender == "female")
        {
            PlayerPrefs.SetInt("npcId", 5);
            _npcId = 5;
        }
        else
            Debug.Log("Failed to save PlayerPrefs of npcId : UI_SelectNpc.cs");
        branchEnding();
        Managers.Data.ClearAllStage();
        ClosePopupUI();
        Managers.Sound.Play("Button", Define.Sound.Effect);
        Managers.UI.ShowPopupUI<UI_Scripts>();
    }

    public void NpcButton3ButtonClicked(PointerEventData data)
    {
        if (Managers.Data.UserData.user.gender == "male")
        {
            PlayerPrefs.SetInt("npcId", 3);
            _npcId = 3;
        }
        else if (Managers.Data.UserData.user.gender == "female")
        {
            PlayerPrefs.SetInt("npcId", 6);
            _npcId = 6;
        }
        else
            Debug.Log("Failed to save PlayerPrefs of npcId : UI_SelectNpc.cs");
        branchEnding();
        Managers.Data.ClearAllStage();
        ClosePopupUI();
        Managers.Sound.Play("Button", Define.Sound.Effect);
        Managers.UI.ShowPopupUI<UI_Scripts>();
    }

    void branchEnding()
    {
        _stat1 = Managers.Data.UserData.user.stat1;
        _stat2 = Managers.Data.UserData.user.stat2;
        _stat3 = Managers.Data.UserData.user.stat3;

        switch (_npcId)
        {
            // ����� : sta1 > sta2 > stat3
            case 1:
                if (_stat1 >= _stat2 && _stat1 > _stat3)
                {
                    if (_stat2 > _stat3)
                    {
                        PlayerPrefs.SetInt("endingId", 1);
                        SaveCollection(GetHappyEndingId(_npcId));
                    }
                    else
                    {
                        PlayerPrefs.SetInt("endingId", 2);
                        SaveCollection(GetBadEndingId(_npcId));
                    }

                }
                else
                {
                    PlayerPrefs.SetInt("endingId", 2);
                    SaveCollection(GetBadEndingId(_npcId));
                }
                break;

            //  �Ҳ�ģ�� : stat3 > stat1 > stat2
            case 2:
                if (_stat3 >= _stat1 && _stat3 > _stat2)
                {
                    if (_stat1 > _stat2)
                    {
                        PlayerPrefs.SetInt("endingId", 3);
                        SaveCollection(GetHappyEndingId(_npcId));
                    }
                    else
                    {
                        PlayerPrefs.SetInt("endingId", 4);
                        SaveCollection(GetBadEndingId(_npcId));
                    }
                }
                else
                {
                    PlayerPrefs.SetInt("endingId", 4);
                    SaveCollection(GetBadEndingId(_npcId));
                }
                break;

            //  �Ĺ� : stat2 > stat3 > stat1
            case 3:
                if (_stat2 >= _stat3 && _stat2 > _stat1)
                {
                    if (_stat3 > _stat1)
                    {
                        PlayerPrefs.SetInt("endingId", 5);
                        SaveCollection(GetHappyEndingId(_npcId));
                    }
                    else
                    {
                        PlayerPrefs.SetInt("endingId", 6);
                        SaveCollection(GetBadEndingId(_npcId));
                    }
                }
                else
                {
                    PlayerPrefs.SetInt("endingId", 6);
                    SaveCollection(GetBadEndingId(_npcId));
                }
                break;

            // �л�ȸ�� : stat1 > stat2 > stat3
            case 4:
                if (_stat1 >= _stat2 && _stat1 > _stat3)
                {
                    if (_stat2 > _stat3)
                    {
                        PlayerPrefs.SetInt("endingId", 7);
                        SaveCollection(GetHappyEndingId(_npcId));
                    }
                    else
                    {
                        PlayerPrefs.SetInt("endingId", 8);
                        SaveCollection(GetBadEndingId(_npcId));
                    }
                }
                else
                {
                    PlayerPrefs.SetInt("endingId", 8);
                    SaveCollection(GetBadEndingId(_npcId));
                }
                break;

            // ü������ : stat3 > stat1 > stat2
            case 5:
                if (_stat3 >= _stat1 && _stat3 > _stat2)
                {
                    if (_stat1 > _stat2)
                    {
                        PlayerPrefs.SetInt("endingId", 9);
                        SaveCollection(GetHappyEndingId(_npcId));
                    }
                    else
                    {
                        PlayerPrefs.SetInt("endingId", 10);
                        SaveCollection(GetBadEndingId(_npcId));
                    }
                }
                else
                {
                    PlayerPrefs.SetInt("endingId", 10);
                    SaveCollection(GetBadEndingId(_npcId));
                }

                break;

            // �������� : stat2 > stat3 > stat1
            case 6:
                if (_stat2 >= _stat3 && _stat2 > _stat1)
                {
                    if (_stat3 > _stat1)
                    {
                        PlayerPrefs.SetInt("endingId", 11);
                        SaveCollection(GetHappyEndingId(_npcId));
                    }
                    else
                    {
                        PlayerPrefs.SetInt("endingId", 12);
                        SaveCollection(GetBadEndingId(_npcId));
                    }
                }
                else
                {
                    PlayerPrefs.SetInt("endingId", 12);
                    SaveCollection(GetBadEndingId(_npcId));
                }
                break;

            default:
                Debug.Log("Failed to load script data : UI_Scripts.cs");
                break;
        }
    }

    int GetHappyEndingId(int npcId)
    {
        return (npcId * 2 - 1); // 1, 3, 5, 7, 9
    }

    int GetBadEndingId(int npcId)
    {
        return (npcId * 2);
    }

    void SaveCollection(int endingIndex)
    {
        Managers.Data.UserData.user.ending[endingIndex - 1] = true;
        Managers.Data.SaveUserDataToJson(Managers.Data.UserData);
    }
}
