using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Hint : UI_Popup
{
    public Sprite[] _npcSprites;
    int _scriptIndex = 0;

    Hint _hint;
    string _targetLine;
    public int _charPerSecend;
    float interval;
    int _index;
    bool _isType;

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

        interval = 1.0f / _charPerSecend;
        Managers.Data.HintDict.TryGetValue(GetHintId(), out _hint);

        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Images));

        GetImage((int)Images.Panel).gameObject.BindEvent(PanelImageClicked);
        GetImage((int)Images.CursurImage).gameObject.SetActive(false);

        GetText((int)Texts.NameText).text = Define.npcName[_hint.scripts[_scriptIndex].npcId]; // npc 이름
        SetLine(_hint.scripts[_scriptIndex]); // 대사, npc 이미지
    }

    public void PanelImageClicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);

        if (_isType == false)
            _scriptIndex++;

        if (_scriptIndex < _hint.scripts.Length)
        {
            GetText((int)Texts.NameText).text = Define.npcName[_hint.scripts[_scriptIndex].npcId];
            SetLine(_hint.scripts[_scriptIndex]);
        }
        else
        {
            Managers.UI.ShowPopupUI<UI_Ending>();
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

    int GetHintId()
    {
        int hintId = 0;
        return hintId;
    }
}
