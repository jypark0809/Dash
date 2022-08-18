using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ReplayEnding : UI_Popup
{
    public Sprite[] _sprites;

    int _scriptIndex = 0;
    Ending _ending;
    string _targetLine;
    public int _charPerSecend;
    float interval;
    int _index;
    bool _isType;

    enum Images
    {
        Panel,
        CursurImage,
        CharImage_1,
        CharImage_2,
    }

    enum Texts
    {
        ScriptText,
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        interval = 1.0f / _charPerSecend;
        Managers.Data.EndingDict.TryGetValue(PlayerPrefs.GetInt("endingId"), out _ending);

        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Images));

        GetImage((int)Images.Panel).gameObject.BindEvent(PanelImageClicked);
        GetImage((int)Images.CursurImage).gameObject.SetActive(false);
        GetImage((int)Images.CharImage_1).gameObject.SetActive(false);
        GetImage((int)Images.CharImage_2).gameObject.SetActive(false);

        SetLine(_ending.scripts[_scriptIndex]);
    }

    public void PanelImageClicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);

        if (_isType == false)
            _scriptIndex++;

        if (_scriptIndex < _ending.scripts.Length)
        {
            SetLine(_ending.scripts[_scriptIndex]);
        }
        else
        {
            Managers.UI.ShowPopupUI<UI_Ending>();
        }
    }

    void SetLine(Script script)
    {
        // 이미지
        if (script.npcId == 0)
        {
            GetImage((int)Images.CharImage_1).gameObject.SetActive(true);
            GetImage((int)Images.CharImage_2).gameObject.SetActive(false);
        }
        else if (script.npcId == 7)
        {
            GetImage((int)Images.CharImage_1).gameObject.SetActive(false);
            GetImage((int)Images.CharImage_2).gameObject.SetActive(false);
        }
        else
        {
            GetImage((int)Images.CharImage_1).gameObject.SetActive(false);
            GetImage((int)Images.CharImage_2).gameObject.SetActive(true);
        }

        // 스크립트
        if (_isType)
        {
            StopCoroutine(Typing());
            GetText((int)Texts.ScriptText).text = _targetLine;
            EndTyping();
        }
        else
        {
            _targetLine = script.line;
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
