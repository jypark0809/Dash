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
    Script _script;

    string _targetLine; // dictionary에서 꺼내온 대사 1줄
    public int _charPerSecend;
    float interval;
    int _index;
    bool _isType;

    enum Images
    {
        Panel,
        CursurImage,
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

        interval = 1.0f / _charPerSecend;
        _npcId = PlayerPrefs.GetInt("npcId");
        LoadUserData();
        branchEnding();

        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Images));

        GetImage((int)Images.Panel).gameObject.BindEvent(PanelImageClicked);
        GetImage((int)Images.CursurImage).gameObject.SetActive(false);

        SetLine(_script.lines[_scriptIndex]);
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
            Managers.Data.ScriptDict.TryGetValue(12, out _script);
            return;
        }

        switch (_npcId)
        {
            // 모범생 : sta1 > sta2 > stat3
            case 0:
                if (_stat1 > _stat2 && _stat1 > _stat3)
                {
                    if (_stat2 > _stat3)
                        Managers.Data.ScriptDict.TryGetValue(GetHappyEndingId(_npcId), out _script);
                    else
                        Managers.Data.ScriptDict.TryGetValue(GetBadEndingId(_npcId), out _script);
                }
                else
                    Managers.Data.ScriptDict.TryGetValue(GetBadEndingId(_npcId), out _script);

                break;

            //  소꿉친구 : stat3 > stat1 > stat2
            case 1:
                if (_stat3 > _stat1 && _stat3 > _stat2)
                {
                    if (_stat1 > _stat2)
                        Managers.Data.ScriptDict.TryGetValue(GetHappyEndingId(_npcId), out _script);
                    else
                        Managers.Data.ScriptDict.TryGetValue(GetBadEndingId(_npcId), out _script);
                }
                else
                    Managers.Data.ScriptDict.TryGetValue(GetBadEndingId(_npcId), out _script);

                break;

            //  후배 : stat2 > stat3 > stat1
            case 2:
                if (_stat2 > _stat1 && _stat2 > _stat3)
                {
                    if (_stat3 > _stat1)
                        Managers.Data.ScriptDict.TryGetValue(GetHappyEndingId(_npcId), out _script);
                    else
                        Managers.Data.ScriptDict.TryGetValue(GetBadEndingId(_npcId), out _script);
                }
                else
                    Managers.Data.ScriptDict.TryGetValue(GetBadEndingId(_npcId), out _script);

                break;

            // 학생회장 : stat1 > stat2 > stat3
            case 3:
                if (_stat1 > _stat2 && _stat1 > _stat3)
                {
                    if (_stat2 > _stat3)
                        Managers.Data.ScriptDict.TryGetValue(GetHappyEndingId(_npcId), out _script);
                    else
                        Managers.Data.ScriptDict.TryGetValue(GetBadEndingId(_npcId), out _script);
                }
                else
                    Managers.Data.ScriptDict.TryGetValue(GetBadEndingId(_npcId), out _script);

                break;

            // 체육부장 : stat3 > stat1 > stat2
            case 4:
                if (_stat3 > _stat1 && _stat3 > _stat2)
                {
                    if (_stat1 > _stat2)
                        Managers.Data.ScriptDict.TryGetValue(GetHappyEndingId(_npcId), out _script);
                    else
                        Managers.Data.ScriptDict.TryGetValue(GetBadEndingId(_npcId), out _script);
                }
                else
                    Managers.Data.ScriptDict.TryGetValue(GetBadEndingId(_npcId), out _script);

                break;

            // 선도부장 : stat2 > stat3 > stat1
            case 5:
                if (_stat2 > _stat1 && _stat2 > _stat3)
                {
                    if (_stat3 > _stat1)
                        Managers.Data.ScriptDict.TryGetValue(GetHappyEndingId(_npcId), out _script);
                    else
                        Managers.Data.ScriptDict.TryGetValue(GetBadEndingId(_npcId), out _script);
                }
                else
                    Managers.Data.ScriptDict.TryGetValue(GetBadEndingId(_npcId), out _script);

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

        if (_isType == false)
            _scriptIndex++;

        if (_scriptIndex < _script.lines.Length)
        {
            SetLine(_script.lines[_scriptIndex]);
        }
        else
        {
            Managers.Data.InitData();
            Managers.UI.ShowPopupUI<UI_Ending>();
        }
    }

    void SetLine(string line)
    {
        if (_isType)
        {
            StopCoroutine(Typing());
            GetText((int)Texts.ScriptText).text = _targetLine;
            EndTyping();
        }
        else
        {
            _targetLine = line;
            StartCoroutine(StartTyping());
        }
    }

    IEnumerator StartTyping()
    {
        GetText((int)Texts.ScriptText).text = "";
        _index = 0;
        GetImage((int)Images.CursurImage).gameObject.SetActive(false);
        _isType = true;

        yield return new WaitForSeconds(interval);
        StartCoroutine(Typing());
    }

    IEnumerator Typing()
    {
        if (GetText((int)Texts.ScriptText).text == _targetLine)
        {
            EndTyping();
            yield break;
        }

        GetText((int)Texts.ScriptText).text += _targetLine[_index];
        

        //Sound
        if (_targetLine[_index] != ' ' || _targetLine[_index] != '.')
            Managers.Sound.Play("Keyboard", Define.Sound.Effect);

        _index++;

        yield return new WaitForSeconds(interval);
        StartCoroutine(Typing());
    }

    void EndTyping()
    {
        GetImage((int)Images.CursurImage).gameObject.SetActive(true);
        _isType = false;
    }
}
