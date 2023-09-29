using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;
//using static Reporter;
using System.Numerics;
using GoogleMobileAds.Api;
using UnityEngine.UIElements;

public class SuAds : BaseSUUnit
{

    //hieu
    public delegate void MyEventHandler(string flag);
    public event MyEventHandler MyEventRewardVideo;
    public event MyEventHandler MyEventInter;

    static SuAdsSaveData _adsSaveData;
    public static SuAdsSaveData AdsSaveData
    {
        get
        {
            return _adsSaveData;
        }
        set
        {
            _adsSaveData = value;

        }
    }
    public static bool IsRemoveAds
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
    [DisableInPlayMode]
    [DisableInEditorMode]
    public bool LockAppOpenAds;   
    List<AdsNetwork> Networks;
    public Open_Ad_Config openAdData;
    public Inter_Ad_Config interData;
    public Ads_Network_Config NetworkData;
    public List<BaseSuAds> ListAdsControl;

    
    private void Awake()
    {
        string saveDataString = PlayerPrefs.GetString("AdsSaveData");
        if (!string.IsNullOrEmpty(saveDataString))
        {
            AdsSaveData = JsonUtility.FromJson<SuAdsSaveData>(saveDataString);
        }
        else
        {
            AdsSaveData = new SuAdsSaveData()
            {
                AppOpenCount = 0,
                AppOpenRevenue = 0,
                BannerCount = 0,
                BannerRevenue = 0,
                InterstitialCount = 0,
                InterstitialRevenue = 0,
                RewardedVideoCount = 0,
                RewardedVideoRevenue = 0
            };
            PlayerPrefs.Save();
        }       
        ListAdsControl = GetComponentsInChildren<BaseSuAds>(true).ToList();
        SuRemoteConfig.OnFetchComplete += OnFetchComplete;
    }

    private void OnFetchComplete()
    {
        Networks = new List<AdsNetwork>();
        NetworkData = SuGame.Get<SuRemoteConfig>().GetJsonValueAsType<Ads_Network_Config>(RemoteConfigName.ads_network_config);
        if (NetworkData.ads_networks == null || NetworkData.ads_networks.Count == 0)
        {
            Networks.Add(AdsNetwork.admob);
        }
        else
        {
            for (int i = 0; i < NetworkData.ads_networks.Count; i++)
            {
                string nw = NetworkData.ads_networks[i].ToLower();
                switch (nw)
                {
                    case "admob":
                        Networks.Add(AdsNetwork.admob);
                        break;
                    case "max":
                        Networks.Add(AdsNetwork.max);
                        break;
                    case "ironsource":
                        Networks.Add(AdsNetwork.ironsource);
                        break;
                }
            }
        }
        // sắp xếp lại list ads control 
        List<BaseSuAds> ListAdsControlTemp = new List<BaseSuAds>();
        for (int i = 0; i < Networks.Count; i++)
        {
            BaseSuAds adControl = ListAdsControl.Find(x => x.Network == Networks[i]);
            if (adControl != null)
            {
                ListAdsControlTemp.Add(adControl);
                adControl.gameObject.SetActive(true);
            }
        }
        ListAdsControl = ListAdsControlTemp;
        openAdData = SuGame.Get<SuRemoteConfig>().GetJsonValueAsType<Open_Ad_Config>(RemoteConfigName.open_ad_config);
        interData = SuGame.Get<SuRemoteConfig>().GetJsonValueAsType<Inter_Ad_Config>(RemoteConfigName.inter_ad_config);
    }

    public bool HaveReadyInterstitial
    {
        get
        {
            /*
            if ((DateTime.Now - lastTimeInterstitialLoaded).TotalHours >= 1)
            {
                return false;
            }
            */
            for (int i = 0; i < ListAdsControl.Count; i++)
            {
                if (ListAdsControl[i].HaveReadyInterstitial)
                    return true;
            }
            return false;
        }
    }

    public bool HaveReadyRewardVideo
    {
        get
        {
            /*
            if ((DateTime.Now - lastTimeVideoLoaded).TotalHours >= 1)
            {
                return false;
            }
            */
            for (int i = 0; i < ListAdsControl.Count; i++)
            {
                if (ListAdsControl[i].HaveReadyRewardVideo)
                    return true;
            }
            return false;
        }
    }


    public bool HaveReadyAppOpen
    {
        get
        {
            for (int i = 0; i < ListAdsControl.Count; i++)
            {
                if (ListAdsControl[i].HaveReadyAppOpen)
                    return true;
            }
            return false;
        }
    }


    public void HideBanner()
    {       
        for (int i = 0; i < ListAdsControl.Count; i++)
        {      
            ListAdsControl[i].HideBanner();
        }
    }
    public void HideBanner2()
    {
        for (int i = 0; i < ListAdsControl.Count; i++)
        {
            ListAdsControl[i].HideBanner2();
        }
    }

    public void ShowBanner()
    {      
        if (ListAdsControl.Count > 0)
        {
            if (ListAdsControl[0] != null && ListAdsControl[0].HaveReadyBanner)
            {
                ListAdsControl[0].ShowBanner();
            }
           
        }
    }


    public void ShowBanner2()
    {
        if (ListAdsControl.Count > 0)
        {
            if (ListAdsControl[0] != null && ListAdsControl[0].HaveReadyBanner2)
            {
                ListAdsControl[0].ShowBanner2();
            }
           
        }
    }


    public void RequestAppOpenOnUserAction()
    {
        for (int i = 0; i < ListAdsControl.Count; i++)
        {
            ListAdsControl[i].RequestAppOpenOnUserAction();
        }
    }

    public void RequestBannerOnUserAction()
    {
        for (int i = 0; i < ListAdsControl.Count; i++)
        {
            ListAdsControl[i].RequestBannerOnUserAction();
        }
    }

    public void RequestBannerOnUserAction2()
    {
        for (int i = 0; i < ListAdsControl.Count; i++)
        {
            ListAdsControl[i].RequestBannerOnUserAction2();
        }
    }




    public void RequestInterstitialOnUserAction()
    {
        for (int i = 0; i < ListAdsControl.Count; i++)
        {
            ListAdsControl[i].RequestInterstitialOnUserAction();
        }
    }

    public void RequestRewardVideoOnUserAction()
    {
        for (int i = 0; i < ListAdsControl.Count; i++)
        {
            ListAdsControl[i].RequestRewardVideoOnUserAction();
        }
    }
    public void ShowAppOpen(Action onClose, Action onNoAds, ActionShowAds actionShowAdsName)
    {
        for (int i = 0; i < ListAdsControl.Count; i++)
        {
            BaseSuAds adControl = ListAdsControl[i];
            if (adControl.HaveReadyInterstitial)
            {
                adControl.ShowAppOpen(onClose, onNoAds, actionShowAdsName);
                return;
            }
            else
            {
                //Debug.Log("adcontrol " + adControl.network + " không có interstitial");
            }
        }
        RequestAppOpenOnUserAction();
        onNoAds?.Invoke();
    }

    DateTime timeShowInter;
    public void ShowInterstitial(Action onClose, ActionShowAds actionShowAdsName)
    {
        //Debug.Log((DateTime.Now - timeShowInter).TotalSeconds + "show time");
        if ((DateTime.Now - timeShowInter).TotalSeconds < 60)
        {
            onClose?.Invoke();
            return;
        }
        Utiliti.Log(null, "Show interstitial , tìm kiếm adControl có inter");
        for (int i = 0; i < ListAdsControl.Count; i++)
        {
            BaseSuAds adControl = ListAdsControl[i];
            if (adControl.HaveReadyInterstitial)
            {
                timeShowInter = DateTime.Now;
               

                //UIManager.Instance.StartLoadingIcon(() =>
                //{
                    adControl.ShowInterstitial(() =>
                    {
                        //UIManager.Instance.loadingIcon.gameObject.SetActive(false);
                        onClose.Invoke();
                        MyEventInter.Invoke(actionShowAdsName.ToString());             
                    }, actionShowAdsName);               
                //});
                return;
            }
            else
            {       
            }
        }     
        // nếu không adcontrol nào có interstitial thì onclose
        Debug.Log("Không có adcontrol nào có interstitial");
        onClose?.Invoke();
        RequestInterstitialOnUserAction();
      
    }
   
    public void ShowRewardVideo(Action onClose, Action onNoAds, ActionShowAds actionShowAdsName)
    {
        for (int i = 0; i < ListAdsControl.Count; i++)
        {
            BaseSuAds adControl = ListAdsControl[i];
            if (adControl.HaveReadyRewardVideo)
            {
                //UIManager.Instance.StartLoadingIcon(() =>
                //{
                    adControl.ShowRewardVideo(() =>
                    {
                        //UIManager.Instance.loadingIcon.gameObject.SetActive(false);
                        onClose.Invoke();
                        MyEventRewardVideo.Invoke(actionShowAdsName.ToString());
                    }, onNoAds, actionShowAdsName);
               // });
                //AnalysReward();
                return;
            }
            else
            {
                //Debug.Log(adControl.network + " không có reward video");
            }
        }
        Debug.Log("Không mạng nào có video");
        RequestRewardVideoOnUserAction();
        onNoAds?.Invoke();
    }

    public override void Init()
    {

    }


    // app open 
    DateTime TimeStartPause;
    DateTime LastTimeShowAppOpenAd;
    private void OnApplicationFocus(bool focus)
    {
        PlayerPrefs.SetString("AdsSaveData", JsonUtility.ToJson(AdsSaveData));
        PlayerPrefs.Save();
        switch (focus)
        {
            case false:
                TimeStartPause = DateTime.Now;           
                Utiliti.Log(this, "App vào background");
                break;
            case true:
                Invoke("OnApplicationFocus2",0.5f);
                break;
        }

    }
    public void OnApplicationFocus2()
    {
        Utiliti.Log(this, "APp vào forege ground");
        //Debug.Log("Lock App open " + LockAppOpenAds);
        if (LockAppOpenAds == false && HaveReadyAppOpen)
        {
            //Debug.Log((DateTime.Now - TimeStartPause).TotalSeconds+"hehehehe"+ openAdData.on);
            if (openAdData.on && (DateTime.Now - TimeStartPause).TotalSeconds >= openAdData.time_in_background_show && (DateTime.Now - LastTimeShowAppOpenAd).TotalSeconds >= openAdData.capping_time)
            {
                //Debug.Log("Show app open");
                
                //ShowAppOpen(null, null, ActionShowAds.BackToGame);

                //UIManager.Instance.StartLoadingIcon(() =>
                //{
                    ShowAppOpen(() =>
                    {
                       // UIManager.Instance.loadingIcon.gameObject.SetActive(false);
                       // Debug.Log("co chay");
                    }, null, ActionShowAds.BackToGame);
                    
                //});
                return;
            }
        }
        else if (LockAppOpenAds)
        {
            //Debug.Log(" set lockappopen = false");
            LockAppOpenAds = false;
        }
    }

    ////analysme
    //private const string datareward = "count_reward_data";
    //private const string interstitial
    //private int countReward = 0;
    //private void Start()
    //{
    //    countReward = PlayerPrefs.GetInt(datareward);
    //}

    //public void AnalysReward()
    //{
    //    countReward++;
    //    PlayerPrefs.SetInt(datareward,countReward);
    //    switch (countReward)
    //    {
    //        case 1:
    //            SuGame.Get<SuAnalytics>().LogEvent(EventName.Rewarded_1);
    //            break;
    //        case 2:
    //            SuGame.Get<SuAnalytics>().LogEvent(EventName.Rewarded_2);
    //            break;
    //        case 3:
    //            SuGame.Get<SuAnalytics>().LogEvent(EventName.Rewarded_3);
    //            break;
    //        case 4:
    //            SuGame.Get<SuAnalytics>().LogEvent(EventName.Rewarded_4);
    //            break;
    //        case 5:
    //            SuGame.Get<SuAnalytics>().LogEvent(EventName.Rewarded_5);
    //            break;
    //        case 6:
    //            SuGame.Get<SuAnalytics>().LogEvent(EventName.Rewarded_6);
    //            break;
    //        case 7:
    //            SuGame.Get<SuAnalytics>().LogEvent(EventName.Rewarded_7);
    //            break;
    //    }
    //}


}
