using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Shop : UI_Popup
{
    private RewardedAd rewardedAd;
    // string adUnitTestId = "ca-app-pub-3940256099942544/5224354917";
    string adUnitId = "ca-app-pub-3940256099942544/5224354917";
    public Toggle _toggleLoveLetter, _toggleRuby;

    enum Buttons
    {
        CloseButton,
        Item_Letter1,
        Item_Letter2,
        Item_Ruby1,
        Item_Ruby2,
        Item_Ruby3,
        Item_Ruby4,
    }

    enum Texts
    {
        AmberText,
        RubyText,
    }

    enum Images
    {
        LoveLetterPanel,
        RubyPanel,
        CostumePanel,
        TickekPanel,
    }

    void Awake()
    {
        _toggleLoveLetter.onValueChanged.AddListener(OnLoveLetterToggleValueChangedEvent);
        _toggleRuby.onValueChanged.AddListener(OnRubyToggleValueChangedEvent);
    }

    void Start()
    {
        Init();

        this.rewardedAd = new RewardedAd(adUnitId);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);

        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Images));

        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(CloseButtonClicked);
        GetButton((int)Buttons.Item_Letter1).gameObject.BindEvent(LetterItem1Clicked);
        GetButton((int)Buttons.Item_Letter2).gameObject.BindEvent(LetterItem2Clicked);
        GetButton((int)Buttons.Item_Ruby1).gameObject.BindEvent(CloseRubyItem1Clicked);
        GetButton((int)Buttons.Item_Ruby2).gameObject.BindEvent(CloseRubyItem2Clicked);
        GetButton((int)Buttons.Item_Ruby3).gameObject.BindEvent(CloseRubyItem3Clicked);
        GetButton((int)Buttons.Item_Ruby4).gameObject.BindEvent(CloseRubyItem4Clicked);

        if (PlayerPrefs.GetInt("extrahealth") == 1)
            GetButton((int)Buttons.Item_Letter2).interactable = false;
        if (PlayerPrefs.GetInt("extrahealth") == 2)
            GetButton((int)Buttons.Item_Letter1).interactable = false;

        GetText((int)Texts.AmberText).text = Managers.Data.UserData.user.amber.ToString();
        GetText((int)Texts.RubyText).text = Managers.Data.UserData.user.ruby.ToString();

        GetImage((int)Images.CostumePanel).gameObject.SetActive(false);
        GetImage((int)Images.RubyPanel).gameObject.SetActive(false);
        GetImage((int)Images.TickekPanel).gameObject.SetActive(false);
    }

    public void CloseButtonClicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);
        ClosePopupUI();
    }

    #region Tab Toggle
    public void OnLoveLetterToggleValueChangedEvent(bool boolean)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);
        GetImage((int)Images.LoveLetterPanel).gameObject.SetActive(boolean);
    }

    public void OnRubyToggleValueChangedEvent(bool boolean)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);
        GetImage((int)Images.RubyPanel).gameObject.SetActive(boolean);
    }
    #endregion

    #region LoveLetter

    public void LetterItem1Clicked(PointerEventData data)
    {
        if (GetButton((int)Buttons.Item_Letter1).interactable == true)
        {
            Managers.Sound.Play("Button", Define.Sound.Effect);
            Managers.Data.UserData.user.amber -= 80;                        // Decrease Amber
            GetButton((int)Buttons.Item_Letter2).interactable = false;      // Lock another Button
            PlayerPrefs.SetInt("extrahealth", 1);                           // Set Character Health
        }
        else
        {
            Debug.Log("interactable unchecked");
        }
        
    }

    public void LetterItem2Clicked(PointerEventData data)
    {
        if (GetButton((int)Buttons.Item_Letter2).interactable == true)
        {
            GetButton((int)Buttons.Item_Letter1).interactable = false;
            Managers.Sound.Play("Button", Define.Sound.Effect);
            Managers.Data.UserData.user.ruby -= 90;
            PlayerPrefs.SetInt("extrahealth", 2);
        }
        else
        {
            Debug.Log("interactable unchecked");
        }
    }

    #endregion

    #region Ruby Panel

    public void CloseRubyItem1Clicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);
        this.GetComponent<Canvas>().sortingOrder = -1;
        UserChoseToWatchAd();
    }

    public void CloseRubyItem2Clicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);

        if(Managers.Data.UserData.user.ruby >= 50)
        {
            Managers.Data.UserData.user.ruby -= 50;
            Managers.Data.UserData.user.amber += 100;
            UpdateData();
            Managers.Data.SaveUserDataToJson(Managers.Data.UserData);
        }
        else
        {
            Debug.Log("Not enough ruby");
        }
    }

    public void CloseRubyItem3Clicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);
        // 인앱 결제
    }

    public void CloseRubyItem4Clicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);
        // 인앱 결제
    }

    #endregion

    void UpdateData()
    {
        GetText((int)Texts.AmberText).text = Managers.Data.UserData.user.amber.ToString();
        GetText((int)Texts.RubyText).text = Managers.Data.UserData.user.ruby.ToString();
    }

    #region Admob

    public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received");
    }

    // 광고가 종료되었을 때
    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        ReloadAd();
    }

    // 광고를 끝까지 시청하였을 때
    public void HandleUserEarnedReward(object sender, Reward args)
    {
        Managers.Data.UserData.user.ruby += 50;
        UpdateData();
        Managers.Data.SaveUserDataToJson(Managers.Data.UserData);
    }

    private void UserChoseToWatchAd()
    {
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
        }
    }

    public RewardedAd ReloadAd()
    {
        rewardedAd = new RewardedAd(adUnitId);

        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        AdRequest request = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(request);
        return rewardedAd;
    }

    #endregion
}
