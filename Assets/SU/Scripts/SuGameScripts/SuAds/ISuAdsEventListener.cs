using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISuAdsEventListener
{
    // banner 
    void OnBannerShow();
    void OnBannerShow2();
    void OnBannerHide();
    void OnBannerLoaded();
    void OnBannerFailedToLoad(SuAdsAdError adError);
    void OnBannerPaid(SuAdsAdValue adValue);
    void OnBannerClick();
    void OnBannerImpression();
    void OnBannerOpen();
    void OnBannerClose();
    // inter
    void OnInterstitialShow();
    void OnInterstititalClose();
    void OnInterstitialLoaded();
    void OnInterstitialFailedToLoad(SuAdsAdError adError);
    void OnInterstitialPaid(SuAdsAdValue adValue);
    void OnInterstitialImpression();
    void OnInterstitialClick();
    void OnInterstitialFailedToShow(SuAdsAdError adError);
    // reward
    void OnRewardVideoShow();
    void OnRewardVideoClose();
    void OnRewardVideoLoaded();
    void OnRewardVideoFailedToLoad(SuAdsAdError adError);
    void OnRewardVideoPaid(SuAdsAdValue adValue);
    void OnRewardVideoImpression();
    void OnRewardVideoClick();
    void OnRewardVideoReward();
    void OnRewardVideoFailedToShow(SuAdsAdError adError);
    // appOpen
    void OnAppOpenShow();
    void OnAppOpenClose();
    void OnAppOpenLoaded();
    void OnAppOpenFailedToLoad(SuAdsAdError adError);
    void OnAppOpenPaid(SuAdsAdValue adValue);
    void OnAppOpenImpression();
    void OnAppOpenClick();   
    void OnAppOpenFailedToShow(SuAdsAdError adError);

}
