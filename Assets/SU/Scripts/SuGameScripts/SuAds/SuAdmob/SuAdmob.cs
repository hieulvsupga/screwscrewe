using GoogleMobileAds;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using GoogleMobileAds.Ump.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class SuAdmob : BaseSuAds
{
    private bool OnFetchRemoteCompleteBool = false;
#if UNITY_EDITOR
    private void OnValidate()
    {
       
        BannerID.IsTest = IsTest;
        InterstitialID.IsTest = IsTest;
        RewardVideoID.IsTest = IsTest;
        AppOpenID.IsTest = IsTest;
        BannerID.Android_Test = "ca-app-pub-3940256099942544/6300978111";
        BannerID.IOS_Test = "ca-app-pub-3940256099942544/2934735716";
        RewardVideoID.Android_Test = "ca-app-pub-3940256099942544/5224354917";
        RewardVideoID.IOS_Test = "ca-app-pub-3940256099942544/1712485313";
        InterstitialID.Android_Test = "ca-app-pub-3940256099942544/1033173712";
        InterstitialID.IOS_Test = "ca-app-pub-3940256099942544/4411468910";
        AppOpenID.Android_Test = "ca-app-pub-3940256099942544/3419835294";
        AppOpenID.IOS_Test = "ca-app-pub-3940256099942544/5662855259";
    }
#endif


    private void Awake()
    {

        //RewardVideoID.Android = "faefawefaewfawefawefawefawe";
        SuRemoteConfig.OnFetchComplete += OnFetchRemoteComplete;
        //if (!OnFetchRemoteCompleteBool)
        //{
        //    RewardVideoID.Android = SuGame.Get<SuRemoteConfig>().GetJsonValueAsType<Banner_id>(RemoteConfigName.rewarded_id).id;
        //    InterstitialID.Android = SuGame.Get<SuRemoteConfig>().GetJsonValueAsType<Interstitial_id>(RemoteConfigName.interstitial_id).id;
        //    AppOpenID.Android = SuGame.Get<SuRemoteConfig>().GetJsonValueAsType<Ads_open_id>(RemoteConfigName.ads_open_id).id;
        //    BannerID.Android = SuGame.Get<SuRemoteConfig>().GetJsonValueAsType<Banner_id>(RemoteConfigName.banner_id).id;
        //}


        Inited = false;

        MobileAds.RaiseAdEventsOnUnityMainThread = true;
        MobileAds.Initialize((initStatus) =>
        {
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {

                Dictionary<string, AdapterStatus> map = initStatus.getAdapterStatusMap();

                foreach (KeyValuePair<string, AdapterStatus> keyValuePair in map)
                {
                    string className = keyValuePair.Key;
                    AdapterStatus status = keyValuePair.Value;

                    switch (status.InitializationState)
                    {
                        case AdapterState.NotReady:
                            Utiliti.Log(this, "Adapter: " + className + " not ready.");                      
                            break;
                        case AdapterState.Ready:
                            // The adapter was successfully initialized.
                            Utiliti.Log(this, "Adapter: " + className + " is initialized.");                       
                            break;
                    }
                }
                InitAll();
                var debugSettings = new ConsentDebugSettings
                {
                    // Geography appears as in EEA for debug devices.
                    DebugGeography = DebugGeography.EEA,
                    TestDeviceHashedIds = new List<string>()
                    {
                     // máy test bên vid
                    "F19951C4862CADADA35417145B00AE98"
                    }
                };

                ConsentRequestParameters request = new ConsentRequestParameters
                {
                    TagForUnderAgeOfConsent = false,
#if UNITY_EDITOR
                    // chỉ trên editor mới chạy debug
                    ConsentDebugSettings = debugSettings
#endif
                };

                // Check the current consent information status.
                ConsentInformation.Update(request, OnConsentInfoUpdated);
            });
        });
    }

    private void Start()
    {
        if (!OnFetchRemoteCompleteBool)
        {

            string rewardid = SuGame.Get<SuRemoteConfig>().GetJsonValueAsType<Rewarded_id>(RemoteConfigName.rewarded_id).id;
            string interid = SuGame.Get<SuRemoteConfig>().GetJsonValueAsType<Interstitial_id>(RemoteConfigName.interstitial_id).id;
            string appopenid = SuGame.Get<SuRemoteConfig>().GetJsonValueAsType<Ads_open_id>(RemoteConfigName.ads_open_id).id;
            string bannerid = SuGame.Get<SuRemoteConfig>().GetJsonValueAsType<Banner_id>(RemoteConfigName.banner_id).banner_small;
            string bannerid2 = SuGame.Get<SuRemoteConfig>().GetJsonValueAsType<Banner_id>(RemoteConfigName.banner_id).banner_big;
            if (rewardid != null && rewardid != "")
            {
                RewardVideoID.Android = rewardid;
            }
            if (interid != null && interid != "")
            {
                InterstitialID.Android = interid;
            }
            if (appopenid != null && appopenid != "")
            {
                AppOpenID.Android = appopenid;
            }
            if (bannerid != null && bannerid != "")
            {
                BannerID.Android = bannerid;
            }
            if(bannerid2 != null && bannerid2 != "")
            {
               BannerID.Android2 = bannerid2;
            }
        }
    }

    //hieusua
    private void OnFetchRemoteComplete()
    {
    
        OnFetchRemoteCompleteBool = true;
        string rewardid = SuGame.Get<SuRemoteConfig>().GetJsonValueAsType<Rewarded_id>(RemoteConfigName.rewarded_id).id;
        string interid = SuGame.Get<SuRemoteConfig>().GetJsonValueAsType<Interstitial_id>(RemoteConfigName.interstitial_id).id;
        string appopenid = SuGame.Get<SuRemoteConfig>().GetJsonValueAsType<Ads_open_id>(RemoteConfigName.ads_open_id).id;
        string bannerid = SuGame.Get<SuRemoteConfig>().GetJsonValueAsType<Banner_id>(RemoteConfigName.banner_id).banner_small;
        string bannerid2 = SuGame.Get<SuRemoteConfig>().GetJsonValueAsType<Banner_id>(RemoteConfigName.banner_id).banner_big;
        // Debug.Log(bannerid2 + "-----------------------------------------------------------------------------------" +bannerid);
        if (rewardid != null && rewardid != "")
        {
            RewardVideoID.Android = rewardid;
        }
        if (interid != null && interid != "")
        {
            InterstitialID.Android = interid;
        }
        if (appopenid != null && appopenid != "")
        {
            AppOpenID.Android = appopenid;
        }
        if (bannerid != null && bannerid != "")
        {
            BannerID.Android = bannerid;
        }
        if (bannerid2 != null && bannerid2 != "")
        {
            BannerID.Android2 = bannerid2;
        }
    }










    //---------- Consent
    private void OnConsentInfoUpdated(FormError obj)
    {
        if (ConsentInformation.IsConsentFormAvailable())
        {
            LoadConsentForm();
        }
    }

    private ConsentForm _consentForm;

    void LoadConsentForm()
    {
        // Loads a consent form.
        ConsentForm.Load(OnLoadConsentForm);
    }

    void OnLoadConsentForm(ConsentForm consentForm, FormError error)
    {
        if (error != null)
        {
            // Handle the error.
            UnityEngine.Debug.LogError(error);
            return;
        }

        // The consent form was loaded.
        // Save the consent form for future requests.
        _consentForm = consentForm;
        if (ConsentInformation.ConsentStatus == ConsentStatus.Required)
        {
            _consentForm.Show(OnShowForm);
        }
        // You are now ready to show the form.
    }

    void OnShowForm(FormError error)
    {
        if (error != null)
        {
            // Handle the error.
            UnityEngine.Debug.LogError(error);
            return;
        }

        // Handle dismissal by reloading form.
        LoadConsentForm();
    }



    // --------- Banner --------------------------------
    BannerView _bannerView;

    //hieu
    BannerView _bannerViewLarge;

    public override void InitBanner()
    {
        CreateBannerView();
        LoadBanner();
        HideBanner();

        CreateBannerView2();
        LoadBanner2();
        HideBanner2();
    }

    public override bool HaveReadyBanner
    {
        get
        {
            return _bannerView != null;
        }
    }
    public override bool HaveReadyBanner2
    {
        get
        {
            return _bannerViewLarge != null;
        }
    }
    void CreateBannerView()
    {
        string id = BannerID.ID;
        if (_bannerView != null)
        {
            _bannerView.Destroy();
        }

        AdSize adaptiveSize =
                AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);

        _bannerView = new BannerView(id, adaptiveSize, AdPosition.Bottom);
       
        //Utiliti.Log(this, "Create banner view with id : " + id);
        RegisterBannerEvents();
    }

    void CreateBannerView2()
    {
        string id = BannerID.ID2;
        if (_bannerViewLarge != null)
        {
            _bannerViewLarge.Destroy();
        }
        //AdSize adSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
        _bannerViewLarge = new BannerView(id, AdSize.MediumRectangle, AdPosition.Bottom);
        RegisterBannerEvents2();
    }

    public override void LoadBanner()
    {
        if (_bannerView == null)
        {
            CreateBannerView();


        }
        var adRequest = new AdRequest();
        _bannerView.LoadAd(adRequest);
    }

    void LoadBanner2()
    {
        if(_bannerViewLarge == null)
        {
            CreateBannerView2();
        }
        var adRequest = new AdRequest();
        _bannerViewLarge.LoadAd(adRequest);
    }


    public override void RegisterBannerEvents()
    {
        _bannerView.OnBannerAdLoaded += () =>
        {
         
            Utiliti.Log(this, "Load banner success");
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {
                NeedRequestBannerOnUserAction = false;

                //if (UIManager.Instance.SelectAddMoveUI.activeInHierarchy || UIManager.Instance.CompleteLevelUI.gameObject.activeInHierarchy && Controller.Instance.Use_Banner_Big)
                //{
                    SuGame.Get<SuAds>().HideBanner();
                //}
                //else
                //{
                //    if (!UIManager.Instance.LoadingBar.activeInHierarchy)
                //    {
                //        Controller.Instance.CheckNoAds();
                //    }
                //    SuAdsEventListener.instance.OnBannerLoaded();
                //}                
            });
        };

        _bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {
                NeedRequestBannerOnUserAction = true;
                Utiliti.Log(this, "Load banner failed : " + error.ToString());

                SuAdsEventListener.instance.OnBannerFailedToLoad(new SuAdsAdError()
                {
                    errorInfo = JsonUtility.ToJson(error)
                });
            });
        };


        _bannerView.OnAdPaid += (AdValue adValue) =>
        {
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {
                string classname = "";
                if (_bannerView != null)
                {
                    if (_bannerView.GetResponseInfo() != null)
                    {
                        classname = _bannerView.GetResponseInfo().GetMediationAdapterClassName();
                    }
                }

                // gọi action onPaid
                SuAdsAdValue _adValue = new SuAdsAdValue()
                {
                    Network = classname,
                    Valuemicros = adValue.Value,
                    Value = adValue.Value / 1000000F,
                    Precision = adValue.Precision.ToString(),
                    CurrencyCode = adValue.CurrencyCode,
                    actionShowAds = ActionShowAds.Banner_Impression,
                    Ad_Format = "banner",
                    Mediation_Platform = AdsNetwork.admob,
                    UnitID = BannerID.ID
                };
                SuAdsEventListener.instance.OnBannerPaid(_adValue);

            });
        };

        _bannerView.OnAdImpressionRecorded += () =>
        {
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {

                // gọi action inImpression
                SuAdsEventListener.instance.OnBannerImpression();
            });
        };

        _bannerView.OnAdClicked += () =>
        {
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {

                // gọi action on Ad Click
                SuAdsEventListener.instance.OnBannerClick();
            });
        };

        _bannerView.OnAdFullScreenContentOpened += () =>
        {
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {

                // gọi action on aD open
                SuAdsEventListener.instance.OnBannerOpen();
            });
        };

        _bannerView.OnAdFullScreenContentClosed += () =>
        {
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {

                // gọi action onAD Close
                SuAdsEventListener.instance.OnBannerClose();
            });
        };
    }


    public override void RegisterBannerEvents2()
    {
        _bannerViewLarge.OnBannerAdLoaded += () =>
        {

            Utiliti.Log(this, "Load banner success");
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {
                NeedRequestBannerOnUserAction2 = false;
                //if (UIManager.Instance.SelectAddMoveUI.activeInHierarchy == false && UIManager.Instance.CompleteLevelUI.gameObject.activeInHierarchy==false && Controller.Instance.Use_Banner_Big)
                //{
                //    SuGame.Get<SuAds>().HideBanner2();
                //}
                //else
                //{
                    //if (!UIManager.Instance.LoadingBar.activeInHierarchy)
                    //{
                    //    Controller.Instance.CheckNoAds();
                    //}
                    SuAdsEventListener.instance.OnBannerLoaded();
                //}
            });
        };

        _bannerViewLarge.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {
                NeedRequestBannerOnUserAction2 = true;
                Utiliti.Log(this, "Load banner failed : " + error.ToString());

                SuAdsEventListener.instance.OnBannerFailedToLoad(new SuAdsAdError()
                {
                    errorInfo = JsonUtility.ToJson(error)
                });
            });
        };


        _bannerViewLarge.OnAdPaid += (AdValue adValue) =>
        {
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {
                string classname = "";
                if (_bannerViewLarge != null)
                {
                    if (_bannerViewLarge.GetResponseInfo() != null)
                    {
                        classname = _bannerViewLarge.GetResponseInfo().GetMediationAdapterClassName();
                    }
                }
                // gọi action onPaid
                SuAdsAdValue _adValue = new SuAdsAdValue()
                {
                    Network = classname,
                    Valuemicros = adValue.Value,
                    Value = adValue.Value / 1000000F,
                    Precision = adValue.Precision.ToString(),
                    CurrencyCode = adValue.CurrencyCode,
                    actionShowAds = ActionShowAds.Banner_Impression,
                    Ad_Format = "banner",
                    Mediation_Platform = AdsNetwork.admob,
                    UnitID = BannerID.ID2
                };
                SuAdsEventListener.instance.OnBannerPaid(_adValue);

            });
        };

        _bannerViewLarge.OnAdImpressionRecorded += () =>
        {
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {

                // gọi action inImpression
                SuAdsEventListener.instance.OnBannerImpression();
            });
        };

        _bannerViewLarge.OnAdClicked += () =>
        {
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {

                // gọi action on Ad Click
                SuAdsEventListener.instance.OnBannerClick();
            });
        };

        _bannerViewLarge.OnAdFullScreenContentOpened += () =>
        {
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {

                // gọi action on aD open
                SuAdsEventListener.instance.OnBannerOpen();
            });
        };

        _bannerViewLarge.OnAdFullScreenContentClosed += () =>
        {
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {

                // gọi action onAD Close
                SuAdsEventListener.instance.OnBannerClose();
            });
        };
    }


    public override void HideBanner()
    {
        if (_bannerView != null)
        {
            _bannerView.Hide();
            IsBannerShowing = false;
            SuAdsEventListener.instance.OnBannerHide();
        }
    }
    public override void HideBanner2()
    {
        if (_bannerViewLarge != null)
        {
            _bannerViewLarge.Hide();
        }
        SuAdsEventListener.instance.OnBannerHide();
    }

    public override void ShowBanner()
    {
        if (_bannerView != null)
        {
            _bannerView.Show();
            IsBannerShowing = true;
            SuAdsEventListener.instance.OnBannerShow();
        }
    }

    public override void ShowBanner2()
    {
        if (_bannerViewLarge != null)
        {
            _bannerViewLarge.Show();
            SuAdsEventListener.instance.OnBannerShow2();
        }
    }


    public override void RequestBannerOnUserAction()
    {
        Utiliti.Log(this, "Load lại banner khi user có hành động");
        //if (!IsRemoveAds && (NeedRequestBannerOnUserAction || IsBannerShowing == false) && (DateTime.Now - LastTimeRequestBannerOnUserAction).Seconds >= 60)
        if (PlayerPrefs.GetInt("NoAds")==0 && (NeedRequestBannerOnUserAction || IsBannerShowing == false) && (DateTime.Now - LastTimeRequestBannerOnUserAction).TotalSeconds >= 60)
        {
            LastTimeRequestBannerOnUserAction = DateTime.Now;
            LoadBanner();
        }
    }


    private DateTime LastTimeRequestBannerOnUserAction2;
    public override void RequestBannerOnUserAction2()
    {
        if (PlayerPrefs.GetInt("NoAds") == 0 && (NeedRequestBannerOnUserAction2) && (DateTime.Now - LastTimeRequestBannerOnUserAction2).TotalSeconds >= 60)
        {
            LastTimeRequestBannerOnUserAction2 = DateTime.Now;
            LoadBanner2();
        }
    }


    //-------------------------------------------------------------- Interstitial
    InterstitialAd _interstitial;

    public override bool HaveReadyInterstitial
    {
        get
        {
            return _interstitial != null && _interstitial.CanShowAd();
        }
    }



    public override void InitInterstitial()
    {
        LoadInterstitial();
    }
    public override void LoadInterstitial()
    {

        //hieu
        //SuGame.Get<SuAnalytics>().LogEvent(EventName.tracking_intertitial_call,
        //    new Param(ParaName.level_id, PlayerPrefs.GetInt("Playinglevel")),
        //    new Param(ParaName.mode_id, Controller.Instance.CheckDifficuleLevel(PlayerPrefs.GetInt("Playinglevel")).ToString()),
        //    new Param(ParaName.loading_id, Controller.Instance.uuid)                        
        //);
        
        SuGame.Get<SuAnalytics>().LogEvent(EventName.tracking_intertitial_call
           // ,                     
           //new Param(ParaName.level, AnalyticsHieu.level()),
           //new Param(ParaName.highest_level, AnalyticsHieu.highest_level(LevelManager.Instance.LevelIDInt)),
           //new Param(ParaName.move_left, AnalyticsHieu.move_left()),
           //new Param(ParaName.coins, AnalyticsHieu.coins()),
           //new Param(ParaName.current_skin, AnalyticsHieu.current_skin()),
           //new Param(ParaName.current_trail, AnalyticsHieu.current_trail()),
           //new Param(ParaName.current_touch, AnalyticsHieu.current_touch())
         );


        if (_interstitial != null)
        {
            _interstitial.Destroy();
            _interstitial = null;
        }
        var adRequest = new AdRequest();
        string id = InterstitialID.ID;

        InterstitialAd.Load(id, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    MobileAdsEventExecutor.ExecuteInUpdate(() =>
                    {
                        NeedRequestInterstitialOnUserAction = true;
                        SuAdsEventListener.instance.OnInterstitialFailedToLoad(new SuAdsAdError()
                        {
                            errorInfo = JsonUtility.ToJson(error)
                        });
                    });
                    return;
                }


                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {
                    NeedRequestInterstitialOnUserAction = false;
                    LastTimeInterstitialLoaded = DateTime.Now;
                    SuAdsEventListener.instance.OnInterstitialLoaded();
                    _interstitial = ad;
                    RegisterInterstitialEvents();
                    Utiliti.Log(this, "Interstitial ad loaded with response : "
                          + ad.GetResponseInfo());
                });

            });
    }
    public override void RegisterInterstitialEvents()
    {
        
        _interstitial.OnAdPaid += (AdValue adValue) =>
        {
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {
                // interstitial paid
                // gọi action onPaid
                string classname = "";
                if (_interstitial != null)
                {
                    if (_interstitial.GetResponseInfo() != null)
                    {
                        classname = _interstitial.GetResponseInfo().GetMediationAdapterClassName();
                    }
                }
                SuAdsAdValue _adValue = new SuAdsAdValue()
                {
                    Network = classname,
                    Valuemicros = adValue.Value,
                    Value = adValue.Value / 1000000F,
                    Precision = adValue.Precision.ToString(),
                    CurrencyCode = adValue.CurrencyCode,
                    actionShowAds = ActionShowAdsName,
                    Ad_Format = "inter",
                    Mediation_Platform = AdsNetwork.admob,
                    UnitID = InterstitialID.ID
                };
                SuAdsEventListener.instance.OnInterstitialPaid(_adValue);
            });
        };
        // Raised when an impression is recorded for an ad.
        _interstitial.OnAdImpressionRecorded += () =>
        {
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {

                // gọi action on Ad Click
                SuAdsEventListener.instance.OnInterstitialImpression();

            });
        };
        // Raised when a click is recorded for an ad.
        _interstitial.OnAdClicked += () =>
        {
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {

                // gọi action on Ad Click
                SuAdsEventListener.instance.OnInterstitialClick();
            });
        };
        // Raised when an ad opened full screen content.
        _interstitial.OnAdFullScreenContentOpened += () =>
        {
            SuGame.Get<SuAds>().LockAppOpenAds = true;
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {

                // gọi action on open

                SuAdsEventListener.instance.OnInterstitialShow();
            });
        };
        // Raised when the ad closed full screen content.
        _interstitial.OnAdFullScreenContentClosed += () =>
        {
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {
                OnInterstitialCloseAction?.Invoke();
                SuAdsEventListener.instance.OnInterstititalClose();
                LoadInterstitial();
            });
        };
        // Raised when the ad failed to open full screen content.
        _interstitial.OnAdFullScreenContentFailed += (AdError error) =>
        {
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {
                LoadInterstitial();
                SuAdsEventListener.instance.OnInterstitialFailedToShow(new SuAdsAdError()
                {
                    errorInfo = JsonUtility.ToJson(error)
                });
            });
        };
    }

    public override void RequestInterstitialOnUserAction()
    {
        if (!IsRemoveAds && (!HaveReadyInterstitial || NeedRequestInterstitialOnUserAction) && (DateTime.Now - LastTimeRequestInterstitialOnUserAction).TotalSeconds >= 60)
        {
            LastTimeRequestInterstitialOnUserAction = DateTime.Now;
            LoadInterstitial();
        }
    }

    public override void ShowInterstitial(Action onClose, ActionShowAds actionShowAdsName)
    {
        if (IsRemoveAds)
        {
            onClose?.Invoke();
            return;
        }
        if (!HaveReadyInterstitial)
        {
            onClose?.Invoke();
            return;
        }

        OnInterstitialCloseAction = onClose;
        ActionShowAdsName = actionShowAdsName;
        Invoke(nameof(ShowInterDelay), 0.5F);
    }

    void ShowInterDelay()
    {
        _interstitial.Show();
    }

    // ------ Reward Video ----------------------------------------------------------------------------------------------------------------
    RewardedAd _rewardVideo;

    public override void InitRewardVideo()
    {
        LoadRewardVideo();
    }

    public override void LoadRewardVideo()
    {
        if (_rewardVideo != null)
        {
            _rewardVideo.Destroy();
            _rewardVideo = null;
        }


        var adRequest = new AdRequest();

        string id = RewardVideoID.ID;
        // send the request to load the ad.
        RewardedAd.Load(id, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    // Debug.Log("Load video lỗi " + e.LoadAdError.GetMessage());

                    MobileAdsEventExecutor.ExecuteInUpdate(() =>
                    {
                        NeedRequestRewardVideoOnUserAction = true;
                        SuAdsEventListener.instance.OnRewardVideoFailedToLoad(new SuAdsAdError()
                        {
                            errorInfo = JsonUtility.ToJson(error)
                        });
                    });
                    return;
                }
                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {
                    LastTimeRewardVideoLoaded = DateTime.Now;
                    NeedRequestRewardVideoOnUserAction = false;
                    _rewardVideo = ad;
                    RegisterRewardVideoEvents();
                    SuAdsEventListener.instance.OnRewardVideoLoaded();
                });
            });
    }
    public override void RegisterRewardVideoEvents()
    {
        _rewardVideo.OnAdPaid += (AdValue adValue) =>
        {
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {
                string classname = "";
                if (_rewardVideo != null)
                {
                    if (_rewardVideo.GetResponseInfo() != null)
                    {
                        classname = _rewardVideo.GetResponseInfo().GetMediationAdapterClassName();
                    }
                }
                SuAdsAdValue _adValue = new SuAdsAdValue()
                {
                    Network = classname,
                    Valuemicros = adValue.Value,
                    Value = adValue.Value / 1000000F,
                    Precision = adValue.Precision.ToString(),
                    CurrencyCode = adValue.CurrencyCode,
                    actionShowAds = ActionShowAdsName,
                    Ad_Format = "rewarded",
                    Mediation_Platform = AdsNetwork.admob,
                    UnitID = RewardVideoID.ID
                };
                SuAdsEventListener.instance?.OnRewardVideoPaid(_adValue);
            });
        };
        // Raised when an impression is recorded for an ad.
        _rewardVideo.OnAdImpressionRecorded += () =>
        {
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {

            });
        };
        // Raised when a click is recorded for an ad.
        _rewardVideo.OnAdClicked += () =>
        {
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {
                SuAdsEventListener.instance.OnRewardVideoClick();
            });
        };
        // Raised when an ad opened full screen content.
        _rewardVideo.OnAdFullScreenContentOpened += () =>
        {
            SuGame.Get<SuAds>().LockAppOpenAds = true;
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {

                SuAdsEventListener.instance.OnRewardVideoShow();
            });
        };
        // Raised when the ad closed full screen content.
        _rewardVideo.OnAdFullScreenContentClosed += () =>
        {
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {
                if (OnRewardVideoCloseAction != null && canRewardByVideo == true)
                {
                    OnRewardVideoCloseAction?.Invoke();
                    OnRewardVideoCloseAction = null;
                }
                else
                {
                    OnRewardVideoCloseAction = null;
                    canRewardByVideo = false;
                }
                LoadRewardVideo();
                //SuAdsEventListener.instance.OnRewardVideoClose();
            });
        };
        // Raised when the ad failed to open full screen content.
        _rewardVideo.OnAdFullScreenContentFailed += (AdError error) =>
        {
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {
                LoadRewardVideo();
                SuAdsEventListener.instance.OnRewardVideoFailedToShow(new SuAdsAdError()
                {
                    errorInfo = JsonUtility.ToJson(error)
                });
            });
        };
    }



    public override bool HaveReadyRewardVideo
    {
        get
        {
            return _rewardVideo != null && _rewardVideo.CanShowAd();
        }
    }

    public override void RequestRewardVideoOnUserAction()
    {
        if ((!HaveReadyRewardVideo || NeedRequestRewardVideoOnUserAction) && (DateTime.Now - LastTimeRequestRewardVideoOnUserAction).TotalSeconds >= 60)
        {
            LastTimeRequestRewardVideoOnUserAction = DateTime.Now;
            LoadRewardVideo();
        }
    }




    bool canRewardByVideo = false;
    public override void ShowRewardVideo(Action onClose, Action onNoAds, ActionShowAds actionShowAdsName)
    {
        if (!HaveReadyRewardVideo)
        {
            onNoAds?.Invoke();
            return;
        }
        OnRewardVideoCloseAction = onClose;
        Invoke(nameof(ShowRewardVideoDelay), 0.5F);
    }

    void ShowRewardVideoDelay()
    {
        LastTimeShowRewardVideo = DateTime.Now;
        _rewardVideo.Show((rw) =>
        {
            canRewardByVideo = true;
            SuAdsEventListener.instance.OnRewardVideoReward();
        });
    }

    // --------------------- APP OPEN -------------------------------------------------------
    AppOpenAd _appOpen;
    public override void InitAppOpen()
    {
        LoadAppOpen();
    }



    public override void LoadAppOpen()
    {
        if (_appOpen != null)
        {
            _appOpen.Destroy();
            _appOpen = null;
        }
        string id = AppOpenID.ID;
        var adRequest = new AdRequest();
        AppOpenAd.Load(id, adRequest,
            (AppOpenAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("app open ad failed to load an ad " +
                                   "with error : " + error);
                    MobileAdsEventExecutor.ExecuteInUpdate(() =>
                    {
                        NeedRequestAppOpenOnUserAction = true;
                        SuAdsEventListener.instance.OnAppOpenFailedToLoad(new SuAdsAdError()
                        {
                            errorInfo = JsonUtility.ToJson(error)
                        });
                    });

                    return;
                }
                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {
                 
                    Utiliti.Log(this, "App open ad loaded with response : "
                          + ad.GetResponseInfo());
                    NeedRequestAppOpenOnUserAction = false;
                    _appOpen = ad;
                    RegisterAppOpenEvents();
                    SuAdsEventListener.instance.OnAppOpenLoaded();
                });

            });
    }

    public override void RegisterAppOpenEvents()
    {
       
        _appOpen.OnAdPaid += (AdValue adValue) =>
        {
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {
                string classname = "";
                if (_appOpen != null)
                {
                    if (_appOpen.GetResponseInfo() != null)
                    {
                        classname = _appOpen.GetResponseInfo().GetMediationAdapterClassName();
                    }
                }
                SuAdsAdValue _adValue = new SuAdsAdValue()
                {
                    Network = classname,
                    Valuemicros = adValue.Value,
                    Value = adValue.Value / 1000000F,
                    Precision = adValue.Precision.ToString(),
                    CurrencyCode = adValue.CurrencyCode,
                    actionShowAds = ActionShowAds.BackToGame,
                    Ad_Format = "rewarded",
                    Mediation_Platform = AdsNetwork.admob,
                    UnitID = AppOpenID.ID
                };
                SuAdsEventListener.instance.OnAppOpenPaid(_adValue);
            });
        };
        // Raised when an impression is recorded for an ad.
        _appOpen.OnAdImpressionRecorded += () =>
        {
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {
                SuAdsEventListener.instance.OnAppOpenImpression();
            });
        };
        // Raised when a click is recorded for an ad.
        _appOpen.OnAdClicked += () =>
        {
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {
                SuAdsEventListener.instance.OnAppOpenClick();
            });
        };
        // Raised when an ad opened full screen content.
        _appOpen.OnAdFullScreenContentOpened += () =>
        {
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {
                SuGame.Get<SuAds>().LockAppOpenAds = true;
                SuAdsEventListener.instance.OnAppOpenShow();

            });
        };
        // Raised when the ad closed full screen content.
        _appOpen.OnAdFullScreenContentClosed += () =>
        {
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {
                SuAdsEventListener.instance.OnAppOpenClose();
                OnAppOpenCloseAction?.Invoke();
                LoadAppOpen();
            });
        };
        // Raised when the ad failed to open full screen content.
        _appOpen.OnAdFullScreenContentFailed += (AdError error) =>
        {
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {
                SuAdsEventListener.instance.OnAppOpenFailedToShow(new SuAdsAdError()
                {
                    errorInfo = JsonUtility.ToJson(error)
                });
                LoadAppOpen();
            });
        };
    }

    public override bool HaveReadyAppOpen
    {
        get
        {
            return _appOpen != null && _appOpen.CanShowAd();
        }
    }



    public override void ShowAppOpen(Action onClose, Action onNoAds, ActionShowAds actionShowAdsName)
    {
        if (HaveReadyAppOpen)
        {
            OnAppOpenCloseAction = onClose;
            ActionShowAdsName = actionShowAdsName;
            //Invoke(nameof(ShowAppOpenDelay), 0.5F);
            ShowAppOpenDelay();
        }
        else
        {
            onNoAds?.Invoke();
        }
    }

    void ShowAppOpenDelay()
    {
        LastTimeShowAppOpen = DateTime.Now;
        _appOpen.Show();
    }

    public override void RequestAppOpenOnUserAction()
    {
        if (!IsRemoveAds && (!HaveReadyAppOpen || NeedRequestAppOpenOnUserAction) && (DateTime.Now - LastTimeRequestAppOpenOnUserAction).TotalSeconds >= 60)
        {
            LastTimeRequestAppOpenOnUserAction = DateTime.Now;
            LoadAppOpen();
        }
    }
}
