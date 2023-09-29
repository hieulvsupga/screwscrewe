using Sirenix.OdinInspector;
using System;
using UnityEngine;
public abstract class BaseSuAds : MonoBehaviour
{


    [HideInInspector]
    public bool Inited;
    public bool IsRemoveAds
    {
        get
        {
            return PlayerPrefs.GetInt("IsRemoveAds", 0) == 1;
        }
        set
        {
            PlayerPrefs.SetInt("IsRemoveAds", value ? 1 : 0);
        }
    }
    [HideInInspector]
    public ActionShowAds ActionShowAdsName;

    public void InitAll()
    {
        if (EnableBanner && !IsRemoveAds)
        {
            InitBanner();
        }
        if (EnableInterstitial && !IsRemoveAds)
        {
            InitInterstitial();
        }
        if (EnableRewardVideo && !IsRemoveAds)
        {
            InitRewardVideo();
        }
        if (EnableAppOpen && !IsRemoveAds)
        {
            InitAppOpen();
        }
    }


    public AdsNetwork Network;
    public bool IsTest;
    //---------------- Banner ------------------
    [Header("* Banner ----------------------------------------------------------------------------------------")]

    public bool EnableBanner;
    [ShowIf(@"EnableBanner")]
    public AdsUnit BannerID;
    [HideInInspector]
    public bool IsBannerShowing;
    public abstract bool HaveReadyBanner { get; }
    public abstract bool HaveReadyBanner2 { get; }
    public abstract void InitBanner();
    public abstract void LoadBanner();
    public abstract void RegisterBannerEvents();
    public abstract void RegisterBannerEvents2();
    public abstract void ShowBanner();
    public abstract void ShowBanner2();
    public abstract void HideBanner();
    public abstract void HideBanner2();
    [HideInInspector]
    public bool NeedRequestBannerOnUserAction = false;
    [HideInInspector]
    public bool NeedRequestBannerOnUserAction2 = false;
    public DateTime LastTimeRequestBannerOnUserAction;    
    public abstract void RequestBannerOnUserAction();
    public abstract void RequestBannerOnUserAction2();

    // ------------ Interstitial --------------------
    [Space(20)]
    [Header("* Interstitial ----------------------------------------------------------------------------------------")]
    public bool EnableInterstitial;
    public Action OnInterstitialCloseAction;
    [ShowIf(@"EnableInterstitial")]
    public AdsUnit InterstitialID;
    [HideInInspector]
    public abstract bool HaveReadyInterstitial { get; }
    [HideInInspector]
    public DateTime LastTimeShowInterstitial, LastTimeRequestInterstitialOnUserAction;
    public DateTime LastTimeInterstitialLoaded;
    public abstract void InitInterstitial();
    public abstract void LoadInterstitial();
    public abstract void RegisterInterstitialEvents();
    public abstract void ShowInterstitial(Action onClose, ActionShowAds actionShowAdsName);
    [HideInInspector]
    public bool NeedRequestInterstitialOnUserAction = false;
    public abstract void RequestInterstitialOnUserAction();

    // ------------ RewardVide --------------------
    [Space(20)]
    [Header("* RewardVideo ----------------------------------------------------------------------------------------")]
    public bool EnableRewardVideo;
    public Action OnRewardVideoCloseAction;
    [ShowIf(@"EnableRewardVideo")]
    public AdsUnit RewardVideoID;
    [HideInInspector]
    public abstract bool HaveReadyRewardVideo { get; }
    [HideInInspector]
    public DateTime LastTimeShowRewardVideo, LastTimeRequestRewardVideoOnUserAction;
    public DateTime LastTimeRewardVideoLoaded;
    public abstract void InitRewardVideo();
    public abstract void LoadRewardVideo();
    public abstract void RegisterRewardVideoEvents();
    public abstract void ShowRewardVideo(Action onClose, Action onNoAds, ActionShowAds actionShowAdsName);
    [HideInInspector]
    public bool NeedRequestRewardVideoOnUserAction = false;
    public abstract void RequestRewardVideoOnUserAction();


    // ------------ AppOpen 
    [Space(20)]
    [Header("* AppOpen ----------------------------------------------------------------------------------------")]
    public bool EnableAppOpen;
    public Action OnAppOpenCloseAction;
    [ShowIf(@"EnableAppOpen")]
    public AdsUnit AppOpenID;
    [HideInInspector]
    public abstract bool HaveReadyAppOpen { get; }
    [HideInInspector]
    public DateTime LastTimeShowAppOpen, LastTimeRequestAppOpenOnUserAction;
    public DateTime LastTimeAppOpenLoaded;
    public abstract void InitAppOpen();
    public abstract void LoadAppOpen();
    public abstract void RegisterAppOpenEvents();
    public abstract void ShowAppOpen(Action onClose, Action onNoAds, ActionShowAds actionShowAdsName);
    [HideInInspector]
    public bool NeedRequestAppOpenOnUserAction = false;
    public abstract void RequestAppOpenOnUserAction();
}

public enum AdsNetwork
{
    admob,
    max,
    ironsource
}
