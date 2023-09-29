using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class SuDefaultEvent : MonoBehaviour
{
    EventName[] events;
    int OpenGameCount
    {
        get
        {
            return PlayerPrefs.GetInt("OpenGameCount", 0);
        }
        set
        {
            PlayerPrefs.SetInt("OpenGameCount", value);
        }
    }

    private void Awake()
    {
        events = (EventName[])System.Enum.GetValues(typeof(EventName));
        OpenGameCount++;
        Init();
    }
    IEnumerator LogEventDelay(EventName name, float delay)
    {
        Debug.Log("Log event " + name + " sau " + delay + " giây");
        yield return new WaitForSecondsRealtime(delay);
        SuGame.Get<SuAnalytics>().LogEvent(name);
    }

    void Init()
    {
        if (OpenGameCount == 1)
        {
            LogEventD0OnlineTime();
        }
    }

    void LogEventD0OnlineTime()
    {
        string pattern = @"^D0_\d+_Minutes$";
        for (int i = 0; i < events.Length; i++)
        {
            string name = events[i].ToString();
            if (Regex.IsMatch(name, pattern))
            {
                int minute = int.Parse(name.Remove(0, 3).Replace("_Minutes", ""));
                StartCoroutine(LogEventDelay(events[i], 60 * minute));
            }
        }
    }

    public static void LogEventBannerCount(uint count)
    {
        bool parse = System.Enum.TryParse("Banner_" + count,false,out EventName _eventName);
        if(parse)
        {
            SuGame.Get<SuAnalytics>().LogEvent(_eventName);
        }      
    }
    public static void LogEventInterCount(uint count)
    {
        bool parse = System.Enum.TryParse("Interstitial_" + count, false, out EventName _eventName);
        if (parse)
        {
            SuGame.Get<SuAnalytics>().LogEvent(_eventName);
        }
    }

    public static void LogEvenRewardedCount(uint count)
    {
        bool parse = System.Enum.TryParse("Rewarded_" + count, false, out EventName _eventName);
        if (parse)
        {
            SuGame.Get<SuAnalytics>().LogEvent(_eventName);
        }
    }
}
