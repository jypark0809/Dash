using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Option : UI_Popup
{
    public Slider _bgmSlider, _effectSlider;
    public Toggle _vibrateOn, _vibrateOff;

    enum Buttons
    {
        CloseButton,
        DeveloperButton,
        InstagramButton,
        ChangeNicknameButton
    }

    void Awake()
    {
        _vibrateOn.onValueChanged.AddListener(OnToggleValueChangedEvent);
        _vibrateOff.onValueChanged.AddListener(OffToggleValueChangedEvent);
    }

    void Start()
    {
        Init();
        _bgmSlider.onValueChanged.AddListener(delegate { BgmValueChangeCheck(); });
        _effectSlider.onValueChanged.AddListener(delegate { EffectValueChangeCheck(); });
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(CloseButtonClicked);
        GetButton((int)Buttons.DeveloperButton).gameObject.BindEvent(DeveloperButtonClicked);
        GetButton((int)Buttons.InstagramButton).gameObject.BindEvent(InstagramButtonClicked);
        GetButton((int)Buttons.ChangeNicknameButton).gameObject.BindEvent(ChangeNicknameButtonClicked);
        _bgmSlider.value = PlayerPrefs.GetFloat("BgmVolume");
        _effectSlider.value = PlayerPrefs.GetFloat("EffectVolume");
    }

    public void OnToggleValueChangedEvent(bool boolean)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);
        PlayerPrefs.SetInt("vibrate", 1);
    }

    public void OffToggleValueChangedEvent(bool boolean)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);
        PlayerPrefs.SetInt("vibrate", 0);
    }

    public void BgmValueChangeCheck()
    {
        Managers.Sound.SetVolume((int)Define.Sound.Bgm, _bgmSlider.value);
        PlayerPrefs.SetFloat("BgmVolume", _bgmSlider.value);
    }

    public void EffectValueChangeCheck()
    {
        Managers.Sound.SetVolume((int)Define.Sound.Effect, _effectSlider.value);
        PlayerPrefs.SetFloat("EffectVolume", _effectSlider.value);
    }

    public void CloseButtonClicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);
        ClosePopupUI();
    }

    public void ChangeNicknameButtonClicked(PointerEventData data)
    {
        Managers.Sound.SetVolume((int)Define.Sound.Bgm, _bgmSlider.value);
        Managers.UI.ShowPopupUI<UI_ChangeNickname>("UI_Nickname");
    }

    public void DeveloperButtonClicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);
        Managers.UI.ShowPopupUI<UI_Developer>();
    }

    public void InstagramButtonClicked(PointerEventData data)
    {
        Application.OpenURL("https://www.instagram.com/dash_game_official/");
    }
}
