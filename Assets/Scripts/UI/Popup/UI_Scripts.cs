using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Scripts : UI_Popup
{
    public Sprite[] _npcSprites;
    public Sprite[] _cutSceneSprites;

    int _scriptIndex = 0;
    int _cutSceneIndex;
    int _npcId;
    int _stat1, _stat2, _stat3;

    Ending _ending;
    string _targetLine; // dictionary���� ������ ��� 1��
    public int _charPerSecend;
    float interval;
    int _index; // �ؽ�Ʈ �ִϸ��̼�
    bool _isType;

    enum Images
    {
        Panel,
        CursurImage,
        NpcImage,
        CutScene,
    }

    enum Texts
    {
        ScriptText,
        NameText,
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
        _cutSceneIndex = _ending.index;

        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Images));

        GetImage((int)Images.Panel).gameObject.BindEvent(PanelImageClicked);
        GetImage((int)Images.CursurImage).gameObject.SetActive(false);

        Managers.Data.ClearAllStage();
        GetImage((int)Images.CutScene).sprite = _cutSceneSprites[_ending.endingId]; // �ƾ�
        GetText((int)Texts.NameText).text = Define.npcName[_ending.scripts[_index].npcId]; // npc �̸�
        SetLine(_ending.scripts[_scriptIndex]); // ���, npc �̹���
    }

    void LoadUserData()
    {
        _stat1 = Managers.Data.UserData.user.stat1;
        _stat2 = Managers.Data.UserData.user.stat2;
        _stat3 = Managers.Data.UserData.user.stat3;
    }

    // Ending �ḻ
    void branchEnding()
    {
        switch (_npcId)
        {
            // ����� : sta1 > sta2 > stat3
            case 1:
                if (_stat1 > _stat2 && _stat1 > _stat3)
                {
                    if (_stat2 > _stat3)
                    {
                        Managers.Data.EndingDict.TryGetValue(GetHappyEndingId(_npcId), out _ending);
                        SaveCollection(GetHappyEndingId(_npcId));
                    }
                    else
                    {
                        Managers.Data.EndingDict.TryGetValue(GetBadEndingId(_npcId), out _ending);
                        SaveCollection(GetBadEndingId(_npcId));
                    }
                        
                }
                else
                {
                    Managers.Data.EndingDict.TryGetValue(GetBadEndingId(_npcId), out _ending);
                    SaveCollection(GetBadEndingId(_npcId));
                }
                break;

            //  �Ҳ�ģ�� : stat3 > stat1 > stat2
            case 2:
                if (_stat3 > _stat1 && _stat3 > _stat2)
                {
                    if (_stat1 > _stat2)
                    {
                        Managers.Data.EndingDict.TryGetValue(GetHappyEndingId(_npcId), out _ending);
                        SaveCollection(GetHappyEndingId(_npcId));
                    }
                    else
                    {
                        Managers.Data.EndingDict.TryGetValue(GetBadEndingId(_npcId), out _ending);
                        SaveCollection(GetBadEndingId(_npcId));
                    }
                }
                else
                {
                    Managers.Data.EndingDict.TryGetValue(GetBadEndingId(_npcId), out _ending);
                    SaveCollection(GetBadEndingId(_npcId));
                }
                break;

            //  �Ĺ� : stat2 > stat3 > stat1
            case 3:
                if (_stat2 > _stat1 && _stat2 > _stat3)
                {
                    if (_stat3 > _stat1)
                    {
                        Managers.Data.EndingDict.TryGetValue(GetHappyEndingId(_npcId), out _ending);
                        SaveCollection(GetHappyEndingId(_npcId));
                    }
                    else
                    {
                        Managers.Data.EndingDict.TryGetValue(GetBadEndingId(_npcId), out _ending);
                        SaveCollection(GetBadEndingId(_npcId));
                    }
                }
                else
                {
                    Managers.Data.EndingDict.TryGetValue(GetBadEndingId(_npcId), out _ending);
                    SaveCollection(GetBadEndingId(_npcId));
                }
                break;

            // �л�ȸ�� : stat1 > stat2 > stat3
            case 4:
                if (_stat1 > _stat2 && _stat1 > _stat3)
                {
                    if (_stat2 > _stat3)
                    {
                        Managers.Data.EndingDict.TryGetValue(GetHappyEndingId(_npcId), out _ending);
                        SaveCollection(GetHappyEndingId(_npcId));
                    }
                    else
                    {
                        Managers.Data.EndingDict.TryGetValue(GetBadEndingId(_npcId), out _ending);
                        SaveCollection(GetBadEndingId(_npcId));
                    } 
                }
                else
                {
                    Managers.Data.EndingDict.TryGetValue(GetBadEndingId(_npcId), out _ending);
                    SaveCollection(GetBadEndingId(_npcId));
                }
                break;

            // ü������ : stat3 > stat1 > stat2
            case 5:
                if (_stat3 > _stat1 && _stat3 > _stat2)
                {
                    if (_stat1 > _stat2)
                    {
                        Managers.Data.EndingDict.TryGetValue(GetHappyEndingId(_npcId), out _ending);
                        SaveCollection(GetHappyEndingId(_npcId));
                    }
                    else
                    {
                        Managers.Data.EndingDict.TryGetValue(GetBadEndingId(_npcId), out _ending);
                        SaveCollection(GetBadEndingId(_npcId));
                    }
                }
                else
                {
                    Managers.Data.EndingDict.TryGetValue(GetBadEndingId(_npcId), out _ending);
                    SaveCollection(GetBadEndingId(_npcId));
                }

                break;

            // �������� : stat2 > stat3 > stat1
            case 6:
                if (_stat2 > _stat1 && _stat2 > _stat3)
                {
                    if (_stat3 > _stat1)
                    {
                        Managers.Data.EndingDict.TryGetValue(GetHappyEndingId(_npcId), out _ending);
                        SaveCollection(GetHappyEndingId(_npcId));
                    }
                    else
                    {
                        Managers.Data.EndingDict.TryGetValue(GetBadEndingId(_npcId), out _ending);
                        SaveCollection(GetBadEndingId(_npcId));
                    }
                }
                else
                {
                    Managers.Data.EndingDict.TryGetValue(GetBadEndingId(_npcId), out _ending);
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

    public void PanelImageClicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);

        if (_isType == false)
            _scriptIndex++;

        if (_scriptIndex < _ending.scripts.Length)
        {
            GetText((int)Texts.NameText).text = Define.npcName[_ending.scripts[_scriptIndex].npcId];
            SetLine(_ending.scripts[_scriptIndex]);
        }
        else
        {
            Managers.UI.ShowPopupUI<UI_Ending>();
        }
    }

    void SetLine(Script script)
    {
        // �ƾ�
        if (_scriptIndex == _cutSceneIndex)
            GetImage((int)Images.CutScene).GetComponent<Animator>().Play("CutSceneFadeIn");

        // npc �̹���
        GetImage((int)Images.NpcImage).sprite = _npcSprites[script.imageId];
        GetImage((int)Images.NpcImage).SetNativeSize();

        // ��ũ��Ʈ
        if (_isType)
        {
            StopCoroutine(Typing());
            GetText((int)Texts.ScriptText).text = _targetLine;
            EndTyping();
        }
        else
        {
            // script string ����
            _targetLine = string.Format(script.line, Managers.Data.UserData.user.nickname);
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

    void SaveCollection(int endingIndex)
    {
        Managers.Data.UserData.user.ending[endingIndex-1] = true;
        Managers.Data.SaveUserDataToJson(Managers.Data.UserData);
    }
}
