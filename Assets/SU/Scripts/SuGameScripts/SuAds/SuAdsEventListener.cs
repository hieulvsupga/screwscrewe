using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SuAdsEventListener : MonoBehaviour, ISuAdsEventListener
{
    List<ISuAdsEventListener> Listenners;
    public static SuAdsEventListener instance;
    void Awake()
    {
        instance = this;
        Listenners = FindObjectsOfType<MonoBehaviour>().OfType<ISuAdsEventListener>().ToList();
        if (Listenners.Contains(this))
        {
            Listenners.Remove(this);
        }
    }
    public void OnBannerClick()
    {
        Listenners.ForEach(item => item.OnBannerClick());
    }

    public void OnBannerClose()
    {
        Listenners.ForEach(item => item.OnBannerClose());
    }

    public void OnBannerFailedToLoad(SuAdsAdError adError)
    {
        Listenners.ForEach(item => item.OnBannerFailedToLoad(adError));
    }

    public void OnBannerHide()
    {        
        Listenners.ForEach(item => item.OnBannerHide());
    }

    public void OnBannerImpression()
    {
        Listenners.ForEach(item => item.OnBannerImpression());
    }

    public void OnBannerLoaded()
    {
        Listenners.ForEach(item => item.OnBannerLoaded());
    }

    public void OnBannerOpen()
    {
        Listenners.ForEach(item => item.OnBannerOpen());
    }

    public void OnBannerPaid(SuAdsAdValue adValue)
    {
        Listenners.ForEach(item => item.OnBannerPaid(adValue));
    }

    public void OnBannerShow()
    {
        Listenners.ForEach(item => item.OnBannerShow());
    }
    public void OnBannerShow2()
    {
        Listenners.ForEach(item => item.OnBannerShow2());
    }

    public void OnInterstitialClick()
    {
        Listenners.ForEach(item => item.OnInterstitialClick());
    }

    public void OnInterstitialFailedToLoad(SuAdsAdError adError)
    {
        Listenners.ForEach(item => item.OnInterstitialFailedToLoad(adError));
    }

    public void OnInterstitialImpression()
    {
        Listenners.ForEach(item => item.OnInterstitialImpression());
    }

    public void OnInterstitialLoaded()
    {
        Listenners.ForEach(item => item.OnInterstitialLoaded());
    }

    public void OnInterstitialPaid(SuAdsAdValue adValue)
    {
        Listenners.ForEach(item => item.OnInterstitialPaid(adValue));
    }

    public void OnInterstitialShow()
    {
        Listenners.ForEach(item => item.OnInterstitialShow());
    }

    public void OnInterstititalClose()
    {
        Listenners.ForEach(item => item.OnInterstititalClose());
    }

    public void OnRewardVideoClick()
    {
        Listenners.ForEach(item => item.OnRewardVideoClick());
    }

    public void OnRewardVideoClose()
    {
        Listenners.ForEach(item => item.OnRewardVideoClose());
    }

    public void OnRewardVideoFailedToLoad(SuAdsAdError adError)
    {
        Listenners.ForEach(item => item.OnRewardVideoFailedToLoad(adError));
    }

    public void OnRewardVideoImpression()
    {
        Listenners.ForEach(item => item.OnRewardVideoImpression());
    }

    public void OnRewardVideoLoaded()
    {
        Listenners.ForEach(item => item.OnRewardVideoLoaded());
    }

    public void OnRewardVideoPaid(SuAdsAdValue adValue)
    {
        Listenners.ForEach(item => item.OnRewardVideoPaid(adValue));
    }

    public void OnRewardVideoReward()
    {
        Listenners.ForEach(item => item.OnRewardVideoReward());
    }

    public void OnRewardVideoShow()
    {
        Listenners.ForEach(item => item.OnRewardVideoShow());
    }

    public void OnInterstitialFailedToShow(SuAdsAdError adError)
    {
        Listenners.ForEach(item => item.OnInterstitialFailedToShow(adError));
    }

    public void OnRewardVideoFailedToShow(SuAdsAdError adError)
    {
        Listenners.ForEach(item => item.OnRewardVideoFailedToShow(adError));
    }

    public void OnAppOpenShow()
    {
        Listenners.ForEach(item => item.OnAppOpenShow());
    }

    public void OnAppOpenClose()
    {
        Listenners.ForEach(item => item.OnAppOpenClose());
    }

    public void OnAppOpenLoaded()
    {
        Listenners.ForEach(item => item.OnAppOpenLoaded());
    }

    public void OnAppOpenFailedToLoad(SuAdsAdError adError)
    {
        Listenners.ForEach(item => item.OnAppOpenFailedToLoad(adError));
    }

    public void OnAppOpenPaid(SuAdsAdValue adValue)
    {
        Listenners.ForEach(item => item.OnAppOpenPaid(adValue));
    }

    public void OnAppOpenImpression()
    {
        Listenners.ForEach(item => item.OnAppOpenImpression());
    }

    public void OnAppOpenClick()
    {
        Listenners.ForEach(item => item.OnAppOpenClick());
    }

    public void OnAppOpenFailedToShow(SuAdsAdError adError)
    {
        Listenners.ForEach(item => item.OnAppOpenFailedToShow(adError));
    }
}
