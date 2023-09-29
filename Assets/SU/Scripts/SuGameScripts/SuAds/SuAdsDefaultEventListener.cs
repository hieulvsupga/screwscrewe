using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuAdsDefaultEventListener : MonoBehaviour, ISuAdsEventListener
{
    public void OnAppOpenClick()
    {

    }

    public void OnAppOpenClose()
    {

    }

    public void OnAppOpenFailedToLoad(SuAdsAdError adError)
    {

    }

    public void OnAppOpenFailedToShow(SuAdsAdError adError)
    {

    }

    public void OnAppOpenImpression()
    {

    }

    public void OnAppOpenLoaded()
    {

    }

    public void OnAppOpenPaid(SuAdsAdValue adValue)
    {
        LogFirebaseInpression(EventName.paid_ad_impression_app_open, adValue);
    }

    public void OnAppOpenShow()
    {

    }

    public void OnBannerClick()
    {

    }

    public void OnBannerClose()
    {

    }

    public void OnBannerFailedToLoad(SuAdsAdError adError)
    {

    }

    public void OnBannerHide()
    {
       
    }

    public void OnBannerImpression()
    {

    }

    public void OnBannerLoaded()
    {

    }

    public void OnBannerOpen()
    {

    }

    public void OnBannerPaid(SuAdsAdValue adValue)
    {
        LogFirebaseInpression(EventName.paid_ad_impression_banner, adValue);
    }

    public void OnBannerShow()
    {
        //if(UIManager.Instance.SelectAddMoveUI.activeInHierarchy || UIManager.Instance.CompleteLevelUI.gameObject.activeInHierarchy && Controller.Instance.Use_Banner_Big)
        //{
        //    SuGame.Get<SuAds>().HideBanner();
        //}
    }
    public void OnBannerShow2()
    {
        //if (UIManager.Instance.SelectAddMoveUI.activeInHierarchy== false && UIManager.Instance.CompleteLevelUI.gameObject.activeInHierarchy==false && Controller.Instance.Use_Banner_Big)
        //{
        //    SuGame.Get<SuAds>().HideBanner2();
        //}
    }

    public void OnInterstitialClick()
    {

    }

    public void OnInterstitialFailedToLoad(SuAdsAdError adError)
    {

    }

    public void OnInterstitialFailedToShow(SuAdsAdError adError)
    {

    }

    public void OnInterstitialImpression()
    {

    }

    public void OnInterstitialLoaded()
    {

    }

    public void OnInterstitialPaid(SuAdsAdValue adValue)
    {
        LogFirebaseInpression(EventName.paid_ad_impression_interstitial, adValue);
    }

    public void OnInterstitialShow()
    {

    }

    public void OnInterstititalClose()
    {

    }

    public void OnRewardVideoClick()
    {

    }

    public void OnRewardVideoClose()
    {

    }

    public void OnRewardVideoFailedToLoad(SuAdsAdError adError)
    {

    }

    public void OnRewardVideoFailedToShow(SuAdsAdError adError)
    {

    }

    public void OnRewardVideoImpression()
    {

    }

    public void OnRewardVideoLoaded()
    {

    }

    public void OnRewardVideoPaid(SuAdsAdValue adValue)
    {
        LogFirebaseInpression(EventName.paid_ad_impression_video, adValue);
    }

    public void OnRewardVideoReward()
    {

    }

    public void OnRewardVideoShow()
    {

    }

    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    void LogFirebaseInpression(EventName eventName, SuAdsAdValue adValue)
    {
        Debug.Log("Log Paid Event");
        Firebase.Analytics.Parameter[] LTVParameters = {// Log ad value in micros.
            new Firebase.Analytics.Parameter("value", adValue.Value ),
            new Firebase.Analytics.Parameter("ad_platform",adValue.Mediation_Platform.ToString()),
            new Firebase.Analytics.Parameter("ad_format", adValue.Ad_Format ),
            new Firebase.Analytics.Parameter("currency", adValue.CurrencyCode ),
            new Firebase.Analytics.Parameter("precision",adValue.Precision),
            new Firebase.Analytics.Parameter("ad_unit_name", adValue.UnitID ),
            new Firebase.Analytics.Parameter("ad_source",adValue.Network)};
        Firebase.Analytics.FirebaseAnalytics.LogEvent(eventName.ToString(), LTVParameters);

        Firebase.Analytics.Parameter[] adrevenueparam = {// Log ad value in micros.
           // new Firebase.Analytics.Parameter("level", LevelManager.Instance.LevelIDInt),
            new Firebase.Analytics.Parameter("value", adValue.Value ),     
            new Firebase.Analytics.Parameter("ad_format", adValue.Ad_Format ),
            new Firebase.Analytics.Parameter("currency", adValue.CurrencyCode ),     
            };
        Firebase.Analytics.FirebaseAnalytics.LogEvent("ad_revenue_sdk", adrevenueparam);

        //SuGame.Get<SuAnalytics>().TrackPaidAdEvent(adValue.Value, adValue.Ad_Format.ToLower());
        // ri�ng cho MAX 
        // log firebase ad_impression
        if (adValue.Mediation_Platform == AdsNetwork.max)
        {
            var impressionParameters = new[] {
            new Firebase.Analytics.Parameter("ad_platform", "AppLovin"),
            new Firebase.Analytics.Parameter("ad_source", adValue.Network),
            new Firebase.Analytics.Parameter("ad_unit_name", adValue.UnitID),
            new Firebase.Analytics.Parameter("ad_format", adValue.Ad_Format),
            new Firebase.Analytics.Parameter("value", adValue.Value),
            new Firebase.Analytics.Parameter("currency", "USD"), // All AppLovin revenue is sent in USD
            };
            Firebase.Analytics.FirebaseAnalytics.LogEvent("ad_impression", impressionParameters);          
        }
        LogAdjustImpression(adValue);
        // log event 
        switch(eventName)
        {
            case EventName.paid_ad_impression_banner:
                SuAds.AdsSaveData.BannerCount++;
                SuAds.AdsSaveData.BannerRevenue += adValue.Value;
                SuDefaultEvent.LogEventBannerCount(SuAds.AdsSaveData.BannerCount);
                break;
            case EventName.paid_ad_impression_interstitial:
                SuAds.AdsSaveData.InterstitialCount++;
                SuAds.AdsSaveData.InterstitialRevenue += adValue.Value;
                SuDefaultEvent.LogEventInterCount(SuAds.AdsSaveData.InterstitialCount);
                break;
            case EventName.paid_ad_impression_video:
                SuAds.AdsSaveData.RewardedVideoCount++;
                SuAds.AdsSaveData.RewardedVideoRevenue += adValue.Value;
                SuDefaultEvent.LogEvenRewardedCount(SuAds.AdsSaveData.RewardedVideoCount);
                break;
            case EventName.paid_ad_impression_app_open:
                SuAds.AdsSaveData.AppOpenCount++;
                SuAds.AdsSaveData.AppOpenRevenue += adValue.Value;
                //SuDefaultEvent.LogEventBannerCount(SuAds.AdsSaveData.BannerCount);
                break;
        }


        //me
        if(SuAds.AdsSaveData.InterstitialCount + SuAds.AdsSaveData.BannerCount == 10)
        {
            SuGame.Get<SuAnalytics>().LogEvent(EventName.Banner_Inter_10);
            double revInterBanner10 = SuAds.AdsSaveData.InterstitialRevenue + SuAds.AdsSaveData.BannerRevenue;
            SuGame.Get<SuAnalytics>().LogEvent(EventName.af_purchase, new Param(ParaName.Revenue, revInterBanner10));

        }    
    }

    void LogAdjustImpression(SuAdsAdValue adInfo)
    {
        string network = adInfo.Network;
        double revenue = adInfo.Value;
        Dictionary<string, string> dataDict = new Dictionary<string, string>
                    {
                        { "Precision", adInfo.Precision },
                    };
        // log impression level revenue

        //hieu bat
        //string adSource = com.adjust.sdk.AdjustConfig.AdjustAdRevenueSourceAdMob;

        //switch (adInfo.Mediation_Platform)
        //{
        //    case AdsNetwork.admob:
        //        adSource = com.adjust.sdk.AdjustConfig.AdjustAdRevenueSourceAdMob;
        //        break;
        //    case AdsNetwork.max:
        //        adSource = com.adjust.sdk.AdjustConfig.AdjustAdRevenueSourceAppLovinMAX;
        //        break;
        //    case AdsNetwork.ironsource:
        //        adSource = com.adjust.sdk.AdjustConfig.AdjustAdRevenueSourceIronSource;
        //        break;
        //}


        SuGame.Get<SuAdjust>().LogRevenue(network, revenue, "USD", 1, adInfo.actionShowAds.ToString(), adInfo.UnitID, dataDict);

    }
}
