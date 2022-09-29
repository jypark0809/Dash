using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Hint : UI_Popup
{
    GameScene _gameScene;

    public Sprite[] _npcSprites;
    string[] _npcName;
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
        switch(PlayerPrefs.GetInt("Tutorial"))
        {
            case 0:
                Managers.Data.HintDict.TryGetValue(hintId, out _hint);
                break;
            case 1:
                Managers.Data.HintDict.TryGetValue(91, out _hint);
                break;
            case 2:
                Managers.Data.HintDict.TryGetValue(92, out _hint);
                break;
            case 3:
                Managers.Data.HintDict.TryGetValue(93, out _hint);
                break;
            case 4:
                Managers.Data.HintDict.TryGetValue(94, out _hint);
                break;
            case 5:
                Managers.Data.HintDict.TryGetValue(95, out _hint);
                break;
            case 6:
                Managers.Data.HintDict.TryGetValue(96, out _hint);
                break;
            case 7:
                Managers.Data.HintDict.TryGetValue(97, out _hint);
                break;
            case 8:
                Managers.Data.HintDict.TryGetValue(98, out _hint);
                break;
        }
        _npcName = new string[]{
            Managers.Data.UserData.user.nickname,
        "백설", "차가윤", "고유미", "서새한", "채대성", "선도진", "오탁후", "남학생", "여학생", "선생님" };

        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Images));

        GetImage((int)Images.Panel).gameObject.BindEvent(PanelImageClicked);
        GetImage((int)Images.CursurImage).gameObject.SetActive(false);

        GetText((int)Texts.NameText).text = _npcName[_hint.scripts[_scriptIndex].npcId];
        SetLine(_hint.scripts[_scriptIndex]);

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
            GetText((int)Texts.NameText).text = _npcName[_hint.scripts[_scriptIndex].npcId];
            SetLine(_hint.scripts[_scriptIndex]);
        }
        else
        {
            Managers.Game._player.GetComponent<Animator>().speed = 1;
            _gameScene.StartScrolling();
            ClosePopupUI();
        }
    }

    void SetLine(Script script)
    {
        GetImage((int)Images.NpcImage).sprite = _npcSprites[script.imageId];
        GetImage((int)Images.NpcImage).SetNativeSize();

        if (_isType)
        {
            StopCoroutine(Typing());
            GetText((int)Texts.ScriptText).text = _targetLine;
            EndTyping();
        }
        else
        {
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
                    hintId = Random.Range(44, 47); // 44~46
                }
                else if (Managers.Data.UserData.user.gender == "male")
                {
                    hintId = Random.Range(14, 17); // 14~16
                }
                break;

            case 2:
                if (Managers.Data.UserData.user.gender == "female")
                {
                    hintId = Random.Range(54, 58); // 54~57
                }
                else if (Managers.Data.UserData.user.gender == "male")
                {
                    hintId = Random.Range(25, 28); // 25~27
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
                    hintId = Random.Range(34, 37); // 34~36
                }
                else if (Managers.Data.UserData.user.gender == "male")
                {
                    hintId = Random.Range(28, 31); // 28~30
                }
                break;

            case 5:
                if (Managers.Data.UserData.user.gender == "female")
                {
                    hintId = Random.Range(58, 61); // 58~60
                }
                else if (Managers.Data.UserData.user.gender == "male")
                {
                    hintId = Random.Range(5, 8); // 5~7
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
                    hintId = Random.Range(37, 41); // 37~40
                }
                else if (Managers.Data.UserData.user.gender == "male")
                {
                    hintId = Random.Range(8, 11); // 8~10
                }
                break;

            case 8:
                if (Managers.Data.UserData.user.gender == "female")
                {
                    hintId = Random.Range(47, 51); // 47~50
                }
                else if (Managers.Data.UserData.user.gender == "male")
                {
                    hintId = Random.Range(17, 21); // 17~20
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
