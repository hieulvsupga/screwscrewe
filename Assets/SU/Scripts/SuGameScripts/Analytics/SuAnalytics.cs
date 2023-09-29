using Firebase;
using Firebase.Analytics;
using GoogleMobileAds.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class SuAnalytics : BaseSUUnit
{
    static bool firebaseInitialized = false;
    public override void Init()
    {
        // call after check dependency success 
        firebaseInitialized = false;
        if (SuGame.haveDependencies == true)
        {
#if SUGAME_VALIDATED
            InitializeFirebaseAnalytics();
#endif
        }
    }
#if SUGAME_VALIDATED
    void LogExam()
    {
        // cách log 
        // ví dụ log event Level_Complete khi hoàn thành level 5 , đạt 3 sao 
        LogEvent(EventName.Level_Complete,
            new Param(ParaName.Level, 5),
            new Param(ParaName.Star_Number, 3));
    }






    void InitializeFirebaseAnalytics()
    {
        try
        {
            Utiliti.Log(this,"Enabling data collection.");
            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
            Utiliti.Log(this, "Set user properties.");
            // Set the user ID.
            string userID = "";
#if UNITY_ANDROID
            userID = SystemInfo.deviceUniqueIdentifier;
#elif UNITY_IOS
            userID = UnityEngine.iOS.Device.vendorIdentifier;
#endif
            FirebaseAnalytics.SetUserId(userID);
            firebaseInitialized = true;
            Utiliti.Log(this, "Init firebase analytics success ");
            SuGame.Get<SuAnalytics>().LogEvent(EventName.tracking_loading_start,
          //new Param(ParaName.level_id, AnalyticsHieu.level()),
          //new Param(ParaName.loading_id, Controller.Instance.uuid),
          new Param(ParaName.loading_utc, DateTime.Now.ToString())
          );

        }
        catch (System.Exception e)
        {
            Utiliti.Log(this, "init firebase analytics error : " + e.ToString());
        }
    }

    public void LogEvent(string eventName, params Param[] _params)
    {
        if (!firebaseInitialized)
        {
            Utiliti.Log(this, "Can't log");
            return;
        }
        if (_params == null)
        {
            FirebaseAnalytics.LogEvent(eventName.ToString());
            return;
        }
        double revenue = 0;
        List<Parameter> pr = new List<Parameter>();
        foreach (Param _pr in _params)
        {
            if (_pr.value is int)
            {
                pr.Add(new Parameter(_pr.paramName.ToString(), (uint)_pr.value));
            }
            else if (_pr.value is uint)
            {
                pr.Add(new Parameter(_pr.paramName.ToString(), (uint)_pr.value));
            }
            else if (_pr.value is string)
            {
                pr.Add(new Parameter(_pr.paramName.ToString(), _pr.value.ToString()));
            }
            else if (_pr.value is double)
            {
                pr.Add(new Parameter(_pr.paramName.ToString(), (double)_pr.value));
            }
            else if (_pr.value is float)
            {
                pr.Add(new Parameter(_pr.paramName.ToString(), (float)_pr.value));
            }
            else if (_pr.value is long)
            {
                pr.Add(new Parameter(_pr.paramName.ToString(), (long)_pr.value));
            }
            else
            {       
                Utiliti.Log(this, "special type : " + _pr.value.GetType());
                pr.Add(new Parameter(_pr.paramName.ToString(), _pr.value.ToString()));
            }
            if (_pr.paramName == ParaName.Revenue)
            {
                revenue = (double)_pr.value;
            }
        }

        Utiliti.Log(this, "**********************************************  Log event " + eventName);
        FirebaseAnalytics.LogEvent(eventName.ToString(), pr.ToArray());
        SuGame.Get<SuAdjust>().LogEvent(eventName, (float)revenue, _params);
    }


    public void LogEvent(EventName eventName, params Param[] _param)
    {
        if (!firebaseInitialized)
        {
           
            Utiliti.Log(this, "Can't log");
            return;
        }
        double revenue = 0;
        List<Parameter> pr = new List<Parameter>();
        foreach (Param _pr in _param)
        {
            if (_pr.value is int)
            {
                pr.Add(new Parameter(_pr.paramName.ToString(), (uint)_pr.value));
            }
            else if (_pr.value is uint)
            {
                pr.Add(new Parameter(_pr.paramName.ToString(), (uint)_pr.value));
            }
            else if (_pr.value is string)
            {
                pr.Add(new Parameter(_pr.paramName.ToString(), _pr.value.ToString()));
            }
            else if (_pr.value is double)
            {
                pr.Add(new Parameter(_pr.paramName.ToString(), (double)_pr.value));
            }
            else if (_pr.value is float)
            {
                pr.Add(new Parameter(_pr.paramName.ToString(), (float)_pr.value));
            }
            else if (_pr.value is long)
            {
                pr.Add(new Parameter(_pr.paramName.ToString(), (long)_pr.value));
            }
            else
            {
                Utiliti.Log(this, "Special type : " + _pr.value.GetType());
                pr.Add(new Parameter(_pr.paramName.ToString(), _pr.value.ToString()));
            }
            if (_pr.paramName == ParaName.Revenue)
            {
                revenue = (double)_pr.value;
            }
        }

        Utiliti.Log(this, "Log firebase event : " + eventName.ToString());
        FirebaseAnalytics.LogEvent(eventName.ToString(), pr.ToArray());
        SuGame.Get<SuAdjust>().LogEvent(eventName, (float)revenue, _param);
    }


    public static void LogEventLevelStart(int level, string level_mode)
    {
        if (firebaseInitialized)
        {
            Parameter[] LevelStartParameters = {
            new Parameter(FirebaseAnalytics.ParameterLevel, level),
            new Parameter("level_mode",level_mode)
            };
            FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelStart, LevelStartParameters);
        }
    }

    public static void LogEventLevelEnd(int level, string level_mode, bool isWin)
    {
        if (firebaseInitialized)
        {
            Parameter[] LevelEndParameters = {
            new Parameter(FirebaseAnalytics.ParameterLevel, level),
            new Parameter("level_mode",level_mode),
            new Parameter(FirebaseAnalytics.ParameterSuccess, isWin.ToString())
            };

            FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelEnd, LevelEndParameters);
        }
    }


    //--------------------- Nhóm log cho cc theo dõi tỉ lệ và thời điểm user bỏ ---------------------------

    public static void TrackPaidAdEvent(double value, string adFormat, int level)
    {
        Parameter[] AdRevenueParameters = {
        new Parameter(FirebaseAnalytics.ParameterValue, value),
        new Parameter(FirebaseAnalytics.ParameterCurrency, "USD"),
        new Parameter("ad_format", adFormat),
        // not required (these are for level analytics)
        new Parameter(FirebaseAnalytics.ParameterLevel, level)
    };

        FirebaseAnalytics.LogEvent("ad_revenue_sdk", AdRevenueParameters);
    }

    public static void LogEventIAP(decimal price, string currency, int level)
    {
        Parameter[] IAPRevenueParameters = {
        new Parameter(FirebaseAnalytics.ParameterLevel,level),
        new Parameter(FirebaseAnalytics.ParameterValue, price.ToString()),
        new Parameter( FirebaseAnalytics.ParameterCurrency, currency)
};

        FirebaseAnalytics.LogEvent("iap_sdk", IAPRevenueParameters);
    }

    /*
    public static void TrackingScreen(ScreenName screen_view)
    {
        DebugPro.Log("Track_Screen " + screen_view);
        LogEvent(EventName.tracking_screen,
            new Param(ParaName.level_id, GameManager.CurrentLevel),
            new Param(ParaName.mode_id, GameManager.CurrentGameMode.ToString()),
            new Param(ParaName.screen_view, screen_view.ToString()));
    }
    */


#endif
}

public class Param
{
#if SUGAME_VALIDATED
    public ParaName paramName;
    public object value;
    public Param(ParaName _name, uint _value)
    {
        paramName = _name;
        value = _value;
    }
    public Param(ParaName _name, string _value)
    {
        paramName = _name;
        value = _value;
    }
    public Param(ParaName _name, float _value)
    {
        paramName = _name;
        value = _value;
    }
    public Param(ParaName _name, double _value)
    {
        paramName = _name;
        value = _value;
    }
    public Param(ParaName _name, long _value)
    {
        paramName = _name;
        value = _value;
    }
#endif
}




