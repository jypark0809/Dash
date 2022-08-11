using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Scripts : UI_Popup
{
    // 모범생, 소꿉친구, 후배, 학생회장, 체육부장, 선도부장

    int _npcId;
    int _scriptIndex = 0;
    int _stat1, _stat2, _stat3;
    string[] _scripts;

    enum Images
    {
        Panel,
    }

    enum Texts
    {
        ScriptText
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        _npcId = PlayerPrefs.GetInt("npcId");
        LoadUserData();
        branchEnding();

        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Images));

        GetImage((int)Images.Panel).gameObject.BindEvent(PanelImageClicked);
        GetText((int)Texts.ScriptText).text = _scripts[_scriptIndex];
        _scriptIndex++;
    }

    void LoadUserData()
    {
        _stat1 = Managers.Data.UserData.user.stat1;
        _stat2 = Managers.Data.UserData.user.stat2;
        _stat3 = Managers.Data.UserData.user.stat3;
    }

    // Ending 결말
    void branchEnding()
    {
        if (_stat1 == 0 || _stat2 == 0 || _stat3 == 0)
        {
            Managers.Data.ScriptData.TryGetValue(12, out _scripts);
            return;
        }

        switch (_npcId)
        {
            // 모범생 : sta1 > sta2 > stat3
            case 0:
                if (_stat1 > _stat2 && _stat1 > _stat3)
                {
                    if (_stat2 > _stat3)
                        Managers.Data.ScriptData.TryGetValue(GetHappyEndingId(_npcId), out _scripts);
                    else
                        Managers.Data.ScriptData.TryGetValue(GetBadEndingId(_npcId), out _scripts);
                }
                else
                    Managers.Data.ScriptData.TryGetValue(GetBadEndingId(_npcId), out _scripts);

                break;

            //  소꿉친구 : stat3 > stat1 > stat2
            case 1:
                if (_stat3 > _stat1 && _stat3 > _stat2)
                {
                    if (_stat1 > _stat2)
                        Managers.Data.ScriptData.TryGetValue(GetHappyEndingId(_npcId), out _scripts);
                    else
                        Managers.Data.ScriptData.TryGetValue(GetBadEndingId(_npcId), out _scripts);
                }
                else
                    Managers.Data.ScriptData.TryGetValue(GetBadEndingId(_npcId), out _scripts);

                break;

            //  후배 : stat2 > stat3 > stat1
            case 2:
                if (_stat2 > _stat1 && _stat2 > _stat3)
                {
                    if (_stat3 > _stat1)
                        Managers.Data.ScriptData.TryGetValue(GetHappyEndingId(_npcId), out _scripts);
                    else
                        Managers.Data.ScriptData.TryGetValue(GetBadEndingId(_npcId), out _scripts);
                }
                else
                    Managers.Data.ScriptData.TryGetValue(GetBadEndingId(_npcId), out _scripts);

                break;

            // 학생회장 : stat1 > stat2 > stat3
            case 3:
                if (_stat1 > _stat2 && _stat1 > _stat3)
                {
                    if (_stat2 > _stat3)
                        Managers.Data.ScriptData.TryGetValue(GetHappyEndingId(_npcId), out _scripts);
                    else
                        Managers.Data.ScriptData.TryGetValue(GetBadEndingId(_npcId), out _scripts);
                }
                else
                    Managers.Data.ScriptData.TryGetValue(GetBadEndingId(_npcId), out _scripts);

                break;

            // 체육부장 : stat3 > stat1 > stat2
            case 4:
                if (_stat3 > _stat1 && _stat3 > _stat2)
                {
                    if (_stat1 > _stat2)
                        Managers.Data.ScriptData.TryGetValue(GetHappyEndingId(_npcId), out _scripts);
                    else
                        Managers.Data.ScriptData.TryGetValue(GetBadEndingId(_npcId), out _scripts);
                }
                else
                    Managers.Data.ScriptData.TryGetValue(GetBadEndingId(_npcId), out _scripts);

                break;

            // 선도부장 : stat2 > stat3 > stat1
            case 5:
                if (_stat2 > _stat1 && _stat2 > _stat3)
                {
                    if (_stat3 > _stat1)
                        Managers.Data.ScriptData.TryGetValue(GetHappyEndingId(_npcId), out _scripts);
                    else
                        Managers.Data.ScriptData.TryGetValue(GetBadEndingId(_npcId), out _scripts);
                }
                else
                    Managers.Data.ScriptData.TryGetValue(GetBadEndingId(_npcId), out _scripts);

                break;

            default:
                Debug.Log("Failed to load script data : UI_Scripts.cs");
                break;
        }
    }

    int GetHappyEndingId(int npcId)
    {
        return (npcId * 2);
    }

    int GetBadEndingId(int npcId)
    {
        return (npcId * 2) + 1;
    }

    public void PanelImageClicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);

        if (_scriptIndex < _scripts.Length)
        {
            GetText((int)Texts.ScriptText).text = _scripts[_scriptIndex];
            _scriptIndex++;
        }
        else
        {
            Managers.Data.InitData();
            Managers.UI.ShowPopupUI<UI_Ending>();
        }
    }
}
