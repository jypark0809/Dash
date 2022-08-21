using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ReplayEnding : UI_Popup
{
    public Sprite[] _npcSprites;
    public Sprite[] _cutSceneSprites;

    int _scriptIndex = 0;
    int _cutSceneIndex;

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
        Managers.Data.EndingDict.TryGetValue(PlayerPrefs.GetInt("endingId"), out _ending);
        _cutSceneIndex = _ending.index;

        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Images));

        GetImage((int)Images.Panel).gameObject.BindEvent(PanelImageClicked);
        GetImage((int)Images.CursurImage).gameObject.SetActive(false);

        GetImage((int)Images.CutScene).sprite = _cutSceneSprites[_ending.endingId]; // ÄÆ¾À
        GetText((int)Texts.NameText).text = Define.npcName[_ending.scripts[_index].npcId]; // npc ÀÌ¸§
        SetLine(_ending.scripts[_scriptIndex]); // ´ë»ç, npc ÀÌ¹ÌÁö
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
        // ÄÆ¾À
        if (_scriptIndex == _cutSceneIndex)
            GetImage((int)Images.CutScene).GetComponent<Animator>().Play("CutSceneFadeIn");

        // npc ÀÌ¹ÌÁö
        GetImage((int)Images.NpcImage).sprite = _npcSprites[script.imageId];
        GetImage((int)Images.NpcImage).SetNativeSize();

        // ½ºÅ©¸³Æ®
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
}
