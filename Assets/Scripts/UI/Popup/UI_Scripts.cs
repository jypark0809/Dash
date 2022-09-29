using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Scripts : UI_Popup
{
    public Sprite[] _npcSprites;
    public Sprite[] _cutSceneSprites;
    public Sprite[] _endingTextImage;
    string[] _endingName = { null, "어쨌든 백마탄 왕자", "이루지 못한 백마탄 왕자",
                                "진심이 담긴 명찰의 의미", "절교 엔딩",
                                "청력 손실", "집중이 부족한 탓",
                                "어쨌든 고백 성공", "조금 특이한 해바라기",
                                "국가대표 공식 커플 1호", "불협화음 공식 커플 1호",
                                "인형탈 알바", "교내 봉사 엔딩" };

    int _scriptIndex = 0;
    int _cutSceneIndex;

    Ending _ending;
    string _targetLine;
    public int _charPerSecend;
    float interval;
    int _index;
    bool _isType;

    enum Buttons
    {
        OkayButton,
    }

    enum Images
    {
        Panel,
        CursurImage,
        NpcImage,
        CutScene,
        Blocker,
        EndingImage,
    }

    enum Texts
    {
        ScriptText,
        NameText,
        EndingText,
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

        PlayEndingBgm();

        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Images));

        GetButton((int)Buttons.OkayButton).gameObject.BindEvent(OkayButtonClicked);
        GetImage((int)Images.Panel).gameObject.BindEvent(PanelImageClicked);
        GetImage((int)Images.CursurImage).gameObject.SetActive(false);
        GetImage((int)Images.Blocker).gameObject.SetActive(false);

        GetImage((int)Images.CutScene).sprite = _cutSceneSprites[_ending.endingId];
        GetText((int)Texts.NameText).text = Define.npcName[_ending.scripts[_scriptIndex].npcId];
        SetLine(_ending.scripts[_scriptIndex]);
    }

    public void OkayButtonClicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);
        Managers.Scene.LoadScene(Define.Scene.Lobby);
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
            GetImage((int)Images.Blocker).gameObject.SetActive(true);
            if (PlayerPrefs.GetInt("endingId") % 2 == 0)
            {
                GetImage((int)Images.EndingImage).sprite = _endingTextImage[1];
                GetImage((int)Images.EndingImage).SetNativeSize();
            }
            else
            {
                GetImage((int)Images.EndingImage).sprite = _endingTextImage[0];
                GetImage((int)Images.EndingImage).SetNativeSize();
            }
            GetText((int)Texts.EndingText).text = GetEndingName();
        }
    }

    void SetLine(Script script)
    {
        if (_scriptIndex == _cutSceneIndex)
            GetImage((int)Images.CutScene).GetComponent<Animator>().Play("CutSceneFadeIn");

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

    void PlayEndingBgm()
    {
        if (PlayerPrefs.GetInt("endingId") % 2 == 0)
        {
            Managers.Sound.Play("BadEnding", Define.Sound.Bgm);
        }
        else
        {
            Managers.Sound.Play("HappyEnding", Define.Sound.Bgm);
        }
    }

    string GetEndingName()
    {
        return _endingName[PlayerPrefs.GetInt("endingId")];
    }
}
