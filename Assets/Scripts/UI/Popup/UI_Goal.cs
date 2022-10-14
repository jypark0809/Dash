using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using System;

public class UI_Goal : UI_Popup
{
    private RewardedAd rewardedAd;

    // 테스트 = "ca-app-pub-3940256099942544/5224354917";
    // Admob = "ca-app-pub-1206779307721674/3264417737";
    string adUnitId = "ca-app-pub-1206779307721674/3264417737";

    enum Buttons
    {
        OkayButton,
        AdRewardButton,
    }

    enum GameObjects
    {
        Effect,
    }

    void Start()
    {
        Init();

        #region InitAdmob
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
        #endregion
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.OkayButton).gameObject.BindEvent(OkayButtonClicked);
        GetButton((int)Buttons.AdRewardButton).gameObject.BindEvent(OnRewardButtonClicked);
    }

    public void OkayButtonClicked(PointerEventData data)
    {
        Managers.Data.UserData.user.amber += 20;
        Managers.Data.SaveUserDataToJson(Managers.Data.UserData);
        Managers.Sound.Play("Button", Define.Sound.Effect);
        ClosePopupUI();
        Managers.UI.ShowPopupUI<UI_StageClear>();
    }

    public void OnRewardButtonClicked(PointerEventData data)
    {
        Managers.Sound.Play("Button", Define.Sound.Effect);
        if (GetButton((int)Buttons.AdRewardButton).interactable)
        {
            this.GetComponent<Canvas>().sortingOrder = -1;
            UserChoseToWatchAd();
        }
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received");
    }

    // ����� ����Ǿ��� ��
    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        ReloadAd();
    }

    // ����� ������ ��û�Ͽ��� ��
    public void HandleUserEarnedReward(object sender, Reward args)
    {
        Managers.Data.UserData.user.ruby += 15;
        Managers.Data.SaveUserDataToJson(Managers.Data.UserData);
        Managers.UI.ShowPopupUI<UI_ConfirmAdReward>();
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

    public void disableAdReward()
    {
        GetButton((int)Buttons.AdRewardButton).interactable = false;
    }
}
