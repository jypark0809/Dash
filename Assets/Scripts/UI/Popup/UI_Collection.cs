using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Collection : UI_Popup
{
    int _curPage = 1;
    public Toggle _toggle1, _toggle2;

    enum Buttons
    {
        CloseButton,
        RightButton,
        LeftButton,
        EndingButton1,
        EndingButton2,
        EndingButton3,
        EndingButton4,
        EndingButton5,
        EndingButton6,
        EndingButton7,
        EndingButton8,
        EndingButton9,
        EndingButton10,
        EndingButton11,
        EndingButton12,
    }

    enum Texts
    {
        PageText,
    }

    enum GameObjects
    {
        MaleHappyEnding,
        MaleBadEnding,
        FemaleHappyEnding,
        FemaleBadEnding,
        LockGroup1,
        LockGroup2,
        LockGroup3,
        LockGroup4,
        LockGroup5,
        LockGroup6,
        LockGroup7,
        LockGroup8,
        LockGroup9,
        LockGroup10,
        LockGroup11,
        LockGroup12,
    }

    void Awake()
    {
        _toggle1.onValueChanged.AddListener(OnMaleToggleValueChangedEvent);
        _toggle2.onValueChanged.AddListener(OnFemaleToggleValueChangedEvent);
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));

        GetObject((int)GameObjects.MaleBadEnding).SetActive(false);
        GetObject((int)GameObjects.FemaleHappyEnding).SetActive(false);
        GetObject((int)GameObjects.FemaleBadEnding).SetActive(false);

        BindUserEndingData();

        // 엔딩 버튼 초기화
        GetButton((int)Buttons.EndingButton1).gameObject.BindEvent(EndingButton1Clicked);
        GetButton((int)Buttons.EndingButton2).gameObject.BindEvent(EndingButton2Clicked);
        GetButton((int)Buttons.EndingButton3).gameObject.BindEvent(EndingButton3Clicked);
        GetButton((int)Buttons.EndingButton4).gameObject.BindEvent(EndingButton4Clicked);
        GetButton((int)Buttons.EndingButton5).gameObject.BindEvent(EndingButton5Clicked);
        GetButton((int)Buttons.EndingButton6).gameObject.BindEvent(EndingButton6Clicked);
        GetButton((int)Buttons.EndingButton7).gameObject.BindEvent(EndingButton7Clicked);
        GetButton((int)Buttons.EndingButton8).gameObject.BindEvent(EndingButton8Clicked);
        GetButton((int)Buttons.EndingButton9).gameObject.BindEvent(EndingButton9Clicked);
        GetButton((int)Buttons.EndingButton10).gameObject.BindEvent(EndingButton10Clicked);
        GetButton((int)Buttons.EndingButton11).gameObject.BindEvent(EndingButton11Clicked);
        GetButton((int)Buttons.EndingButton12).gameObject.BindEvent(EndingButton12Clicked);

        // 화살표 버튼 초기화
        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(CloseButtonClicked);
        GetButton((int)Buttons.RightButton).gameObject.BindEvent(RightButtonClicked);
        GetButton((int)Buttons.LeftButton).gameObject.BindEvent(LeftButtonClicked);
        GetButton((int)Buttons.LeftButton).gameObject.SetActive(false);
    }

    public void OnMaleToggleValueChangedEvent(bool boolean)
    {
        GetObject((int)GameObjects.MaleHappyEnding).gameObject.SetActive(boolean);
        GetObject((int)GameObjects.FemaleBadEnding).gameObject.SetActive(false);
        _curPage = 1;
        GetText((int)Texts.PageText).text = _curPage.ToString() + " / 2";
        GetButton((int)Buttons.RightButton).gameObject.SetActive(true);
        GetButton((int)Buttons.LeftButton).gameObject.SetActive(false);
    }

    public void OnFemaleToggleValueChangedEvent(bool boolean)
    {
        GetObject((int)GameObjects.FemaleHappyEnding).gameObject.SetActive(boolean);
        GetObject((int)GameObjects.MaleBadEnding).gameObject.SetActive(false);
        _curPage = 1;
        GetText((int)Texts.PageText).text = _curPage.ToString() + " / 2";
        GetButton((int)Buttons.RightButton).gameObject.SetActive(true);
        GetButton((int)Buttons.LeftButton).gameObject.SetActive(false);
    }

    public void CloseButtonClicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);
        ClosePopupUI();
    }

    public void RightButtonClicked(PointerEventData data)
    {
        _curPage++;
        GetText((int)Texts.PageText).text = _curPage.ToString() + " / 2";
        GetButton((int)Buttons.RightButton).gameObject.SetActive(false);
        GetButton((int)Buttons.LeftButton).gameObject.SetActive(true);
        if (_toggle1.isOn)
        {
            GetObject((int)GameObjects.MaleHappyEnding).SetActive(false);
            GetObject((int)GameObjects.MaleBadEnding).SetActive(true);
        }
        else
        {
            GetObject((int)GameObjects.FemaleHappyEnding).SetActive(false);
            GetObject((int)GameObjects.FemaleBadEnding).SetActive(true);
        }
        Managers.Sound.Play("Button", Define.Sound.Effect);
    }

    public void LeftButtonClicked(PointerEventData data)
    {
        _curPage--;
        GetText((int)Texts.PageText).text = _curPage.ToString() + " / 2";
        GetButton((int)Buttons.RightButton).gameObject.SetActive(true);
        GetButton((int)Buttons.LeftButton).gameObject.SetActive(false);
        if (_toggle1.isOn)
        {
            GetObject((int)GameObjects.MaleHappyEnding).SetActive(true);
            GetObject((int)GameObjects.MaleBadEnding).SetActive(false);
        }
        else
        {
            GetObject((int)GameObjects.FemaleHappyEnding).SetActive(true);
            GetObject((int)GameObjects.FemaleBadEnding).SetActive(false);
        }
        Managers.Sound.Play("Button", Define.Sound.Effect);
    }

    public void EndingButton1Clicked(PointerEventData data)
    {
        PlayerPrefs.SetInt("endingId", 1);
        Managers.Scene.LoadScene(Define.Scene.ReplayEnding);
    }

    public void EndingButton2Clicked(PointerEventData data)
    {
        PlayerPrefs.SetInt("endingId", 2);
        Managers.Scene.LoadScene(Define.Scene.ReplayEnding);
    }

    public void EndingButton3Clicked(PointerEventData data)
    {
        PlayerPrefs.SetInt("endingId", 3);
        Managers.Scene.LoadScene(Define.Scene.ReplayEnding);
    }

    public void EndingButton4Clicked(PointerEventData data)
    {
        PlayerPrefs.SetInt("endingId", 4);
        Managers.Scene.LoadScene(Define.Scene.ReplayEnding);
    }

    public void EndingButton5Clicked(PointerEventData data)
    {
        PlayerPrefs.SetInt("endingId", 5);
        Managers.Scene.LoadScene(Define.Scene.ReplayEnding);
    }

    public void EndingButton6Clicked(PointerEventData data)
    {
        PlayerPrefs.SetInt("endingId", 6);
        Managers.Scene.LoadScene(Define.Scene.ReplayEnding);
    }

    public void EndingButton7Clicked(PointerEventData data)
    {
        PlayerPrefs.SetInt("endingId", 7);
        Managers.Scene.LoadScene(Define.Scene.ReplayEnding);
    }

    public void EndingButton8Clicked(PointerEventData data)
    {
        PlayerPrefs.SetInt("endingId", 8);
        Managers.Scene.LoadScene(Define.Scene.ReplayEnding);
    }

    public void EndingButton9Clicked(PointerEventData data)
    {
        PlayerPrefs.SetInt("endingId", 9);
        Managers.Scene.LoadScene(Define.Scene.ReplayEnding);
    }

    public void EndingButton10Clicked(PointerEventData data)
    {
        PlayerPrefs.SetInt("endingId", 10);
        Managers.Scene.LoadScene(Define.Scene.ReplayEnding);
    }

    public void EndingButton11Clicked(PointerEventData data)
    {
        PlayerPrefs.SetInt("endingId", 11);
        Managers.Scene.LoadScene(Define.Scene.ReplayEnding);
    }

    public void EndingButton12Clicked(PointerEventData data)
    {
        PlayerPrefs.SetInt("endingId", 12);
        Managers.Scene.LoadScene(Define.Scene.ReplayEnding);
    }

    void BindUserEndingData()
    {
        GetObject((int)GameObjects.LockGroup1).SetActive(!Managers.Data.UserData.user.ending[0]);
        GetObject((int)GameObjects.LockGroup2).SetActive(!Managers.Data.UserData.user.ending[1]);
        GetObject((int)GameObjects.LockGroup3).SetActive(!Managers.Data.UserData.user.ending[2]);
        GetObject((int)GameObjects.LockGroup4).SetActive(!Managers.Data.UserData.user.ending[3]);
        GetObject((int)GameObjects.LockGroup5).SetActive(!Managers.Data.UserData.user.ending[4]);
        GetObject((int)GameObjects.LockGroup6).SetActive(!Managers.Data.UserData.user.ending[5]);
        GetObject((int)GameObjects.LockGroup7).SetActive(!Managers.Data.UserData.user.ending[6]);
        GetObject((int)GameObjects.LockGroup8).SetActive(!Managers.Data.UserData.user.ending[7]);
        GetObject((int)GameObjects.LockGroup9).SetActive(!Managers.Data.UserData.user.ending[8]);
        GetObject((int)GameObjects.LockGroup10).SetActive(!Managers.Data.UserData.user.ending[9]);
        GetObject((int)GameObjects.LockGroup11).SetActive(!Managers.Data.UserData.user.ending[10]);
        GetObject((int)GameObjects.LockGroup12).SetActive(!Managers.Data.UserData.user.ending[11]);
    }
}
