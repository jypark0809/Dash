using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Hint : UI_Popup
{
    GameScene _gameScene;

    public Sprite[] _npcSprites;
    int _scriptIndex = 0;

    Hint _hint;
    string _targetLine;
    public int _charPerSecend;
    float interval;
    int _index;
    bool _isType;
    int hintId;

    enum Images
    {
        Panel,
        CursurImage,
        NpcImage,
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

        _gameScene = (GameScene)Managers.Scene.CurrentScene;
        GetHintId();
        interval = 1.0f / _charPerSecend;
        Managers.Data.HintDict.TryGetValue(hintId, out _hint);

        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Images));

        GetImage((int)Images.Panel).gameObject.BindEvent(PanelImageClicked);
        GetImage((int)Images.CursurImage).gameObject.SetActive(false);

        if (PlayerPrefs.GetInt("isAccessFirst") == 0)
        {
            GetText((int)Texts.NameText).text = "주인공"; // npc 이름
        }
        else
        {
            GetText((int)Texts.NameText).text = Define.npcName[_hint.scripts[_scriptIndex].npcId]; // npc 이름
        }
        SetLine(_hint.scripts[_scriptIndex]); // 대사, npc 이미지

        Managers.Game._player.GetComponent<Animator>().speed = 0;
        _gameScene.StopScrolling();
    }

    public void PanelImageClicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);

        if (_isType == false)
            _scriptIndex++;

        if (_scriptIndex < _hint.scripts.Length)
        {
            if (PlayerPrefs.GetInt("isAccessFirst") == 0)
            {
                GetText((int)Texts.NameText).text = "주인공"; // npc 이름
            }
            else
            {
                GetText((int)Texts.NameText).text = Define.npcName[_hint.scripts[_scriptIndex].npcId]; // npc 이름
            }
            SetLine(_hint.scripts[_scriptIndex]);
        }
        else
        {
            // 힌트 스크립트 끝났을 때
            Managers.Game._player.GetComponent<Animator>().speed = 1;
            _gameScene.StartScrolling();
            ClosePopupUI();
        }
    }

    void SetLine(Script script)
    {
        // npc 이미지
        GetImage((int)Images.NpcImage).sprite = _npcSprites[script.imageId];
        GetImage((int)Images.NpcImage).SetNativeSize();

        // 스크립트
        if (_isType)
        {
            StopCoroutine(Typing());
            GetText((int)Texts.ScriptText).text = _targetLine;
            EndTyping();
        }
        else
        {
            // script string 보간
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

    void GetHintId()
    {
        switch(Managers.Data.UserData.user.stage)
        {
            case 0:
                if (Managers.Data.UserData.user.gender == "female")
                {
                    hintId = 0;
                }
                else if (Managers.Data.UserData.user.gender == "male")
                {
                    hintId = 0;
                }
                break;
            case 1:
                if (Managers.Data.UserData.user.gender == "female")
                {
                    hintId = Random.Range(44, 51); // 44~50
                }
                else if (Managers.Data.UserData.user.gender == "male")
                {
                    hintId = Random.Range(14, 21); // 14~20
                }
                break;

            case 2:
                if (Managers.Data.UserData.user.gender == "female")
                {
                    hintId = Random.Range(54, 61); // 54~60
                }
                else if (Managers.Data.UserData.user.gender == "male")
                {
                    hintId = Random.Range(25, 31); // 25~30
                }
                break;

            case 3:
                if (Managers.Data.UserData.user.gender == "female")
                {
                    hintId = Random.Range(31, 34); // 31~33
                }
                else if (Managers.Data.UserData.user.gender == "male")
                {
                    hintId = Random.Range(1, 5); // 1~4
                }
                break;

            case 4:
                if (Managers.Data.UserData.user.gender == "female")
                {
                    hintId = Random.Range(34, 41); // 34~40
                }
                else if (Managers.Data.UserData.user.gender == "male")
                {
                    hintId = Random.Range(25, 31); // 25~30
                }
                break;

            case 5:
                if (Managers.Data.UserData.user.gender == "female")
                {
                    hintId = Random.Range(54, 61); // 54~60
                }
                else if (Managers.Data.UserData.user.gender == "male")
                {
                    hintId = Random.Range(5, 11); // 5~10
                }
                break;

            case 6:
                if (Managers.Data.UserData.user.gender == "female")
                {
                    hintId = Random.Range(41, 44); // 41~43
                }
                else if (Managers.Data.UserData.user.gender == "male")
                {
                    hintId = Random.Range(11, 14); // 11~13
                }
                break;

            case 7:
                if (Managers.Data.UserData.user.gender == "female")
                {
                    hintId = Random.Range(34, 41); // 34~40
                }
                else if (Managers.Data.UserData.user.gender == "male")
                {
                    hintId = Random.Range(5, 11); // 5~10
                }
                break;

            case 8:
                if (Managers.Data.UserData.user.gender == "female")
                {
                    hintId = Random.Range(44, 51); // 44~50
                }
                else if (Managers.Data.UserData.user.gender == "male")
                {
                    hintId = Random.Range(14, 21); // 14~20
                }
                break;

            case 9:
                if (Managers.Data.UserData.user.gender == "female")
                {
                    hintId = Random.Range(51, 54); // 51~53
                }
                else if (Managers.Data.UserData.user.gender == "male")
                {
                    hintId = Random.Range(21, 25); // 21~24
                }
                break;
        }
    }
}
