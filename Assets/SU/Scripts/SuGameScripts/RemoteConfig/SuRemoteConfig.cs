using Firebase;
using Firebase.Extensions;
using Firebase.RemoteConfig;
using GoogleMobileAds.Common;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SuRemoteConfig : BaseSUUnit
{
    //[DisableInEditorMode]
    //private bool initCompleted = false;


    [DisableInEditorMode]
    public bool fetchComplete = false;
    System.Collections.Generic.Dictionary<string, object> RemoteDict;
    public List<RemoteConfigDataModule> RemoteData;
    public override void Init()
    {  
        //initCompleted = false;

        if (SuGame.haveDependencies == true)
        {
#if SUGAME_VALIDATED
            InitRemoteConfig();
#endif
        }
        else
        {
            Debug.Log("Không có dependencies");
        }


    }
    public static Action OnFetchComplete;


#if SUGAME_VALIDATED
       void Awake()
        {     
        RemoteDict = new System.Collections.Generic.Dictionary<string, object>
        {

        };
        foreach (RemoteConfigDataModule _item in RemoteData)
        {
            RemoteDict.Add(_item.Name.ToString(), PlayerPrefs.GetString(_item.Name.ToString(), _item.defaultValue));
        }      
        
    }

    private void InitRemoteConfig()
    {
       if(RemoteDict == null)
        {
            RemoteDict = new System.Collections.Generic.Dictionary<string, object>
            {

            };
            foreach (RemoteConfigDataModule _item in RemoteData)
            {
                RemoteDict.Add(_item.Name.ToString(), PlayerPrefs.GetString(_item.Name.ToString(), _item.defaultValue));
            }
        }
        Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(RemoteDict).ContinueWithOnMainThread(task =>
        {
            //initCompleted = true;
            FetchDataAsync();
        });


    }
    Task FetchDataAsync()
    {
        Utiliti.Log(this, "Fetching data...");
        Task fetchTask = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.FetchAsync(TimeSpan.Zero);
        return fetchTask.ContinueWithOnMainThread(FetchComplete);
    }


    void DisplayAllKeys()
    {
        System.Collections.Generic.IEnumerable<string> keys = FirebaseRemoteConfig.DefaultInstance.Keys;
        foreach (string key in keys)
        {
            Utiliti.Log(this, "    " + key + ":" + FirebaseRemoteConfig.DefaultInstance.GetValue(key).StringValue.ToString());
            if (RemoteDict.ContainsKey(key))
            {
                RemoteDict[key] = FirebaseRemoteConfig.DefaultInstance.GetValue(key).StringValue;
                
            }
            else
            {
                RemoteDict.Add(key, FirebaseRemoteConfig.DefaultInstance.GetValue(key).StringValue);
            }
            PlayerPrefs.SetString(key, RemoteDict[key].ToString());
        }
        Utiliti.Log(null, "GetKeysByPrefix(\"config_test_s\"):");
        keys = FirebaseRemoteConfig.DefaultInstance.GetKeysByPrefix("config_test_s");
        foreach (string key in keys)
        {
            Debug.Log("    " + key);
        }
        fetchComplete = true;

    }



    void FetchComplete(Task fetchTask)
    {
        FirebaseRemoteConfig.DefaultInstance.ActivateAsync().ContinueWithOnMainThread(task =>
        {
            if (fetchTask.IsCanceled)
            {
                Utiliti.Log(null, "Fetch canceled.");
    
            }
            else if (fetchTask.IsFaulted)
            {
                Utiliti.Log(null, "Fetch encountered an error.");
            }
            else if (fetchTask.IsCompleted)
            {
                Utiliti.Log(null, "Fetch completed successfully!");
                //FirebaseRemoteConfig.ActivateFetched();
            }
            var info = FirebaseRemoteConfig.DefaultInstance.Info;
            switch (info.LastFetchStatus)
            {
                case Firebase.RemoteConfig.LastFetchStatus.Success:
                    Utiliti.Log(null, string.Format("Remote data loaded and ready (last fetch time {0}).",
                        info.FetchTime));
                    
                    MobileAdsEventExecutor.ExecuteInUpdate(() =>
                    {
                        // chạy action vào frame tiếp theo ở main thread tránh trường hợp các hàm action có lỗi sẽ không chạy , gây treo hoặc crash game
                        DisplayAllKeys();
                        OnFetchComplete?.Invoke();
                    });


                    break;
                case Firebase.RemoteConfig.LastFetchStatus.Failure:
                    switch (info.LastFetchFailureReason)
                    {
                        case Firebase.RemoteConfig.FetchFailureReason.Error:
                            Utiliti.Log(null, "Fetch failed for unknown reason");
                            break;
                        case Firebase.RemoteConfig.FetchFailureReason.Throttled:
                            Utiliti.Log(null, "Fetch throttled until " + info.ThrottledEndTime);
                          
                            break;
                    }
                    break;
                case Firebase.RemoteConfig.LastFetchStatus.Pending:
                    Utiliti.Log(null, "Latest Fetch call still pending.");
                  
                    break;
            }


        });

    }

    public bool HaveKey(RemoteConfigName keyName)
    {
        return HaveKey(keyName.ToString());
    }

    public bool HaveKey(string keyName)
    {
        return RemoteDict.ContainsKey(keyName);
    }

    // lấy giá trị string 
    public string GetStringValue(string name)
    {
        if (RemoteDict.ContainsKey(name))
        {
            return RemoteDict[name].ToString();
        }
        else
        {
            return "";
        }
    }
    public string GetStringValue(RemoteConfigName name)
    {
        string sName = name.ToString();
        return GetStringValue(sName);
    }

    // lấy giá trị int , nếu data sai thì trả về 0;
    public int GetIntValue(string name)
    {
        if (RemoteDict.ContainsKey(name))
        {
            if (int.TryParse(RemoteDict[name].ToString(), out int vl))
            {
                return vl;
            }
            return 0;
        }
        else
        {
            return 0;
        }
    }
    public int GetIntValue(RemoteConfigName name)
    {
        string sName = name.ToString();
        return GetIntValue(sName);
    }

    // lấy giá trị float
    public float GetFloatValue(string name)
    {
        if (RemoteDict.ContainsKey(name))
        {
            System.Globalization.NumberStyles style = System.Globalization.NumberStyles.Float;
            System.Globalization.CultureInfo cul = System.Globalization.CultureInfo.CurrentCulture;
            if (float.TryParse(RemoteDict[name].ToString(), style, cul, out float vl))
            {
                return vl;
            }
            return 0;
        }
        else
        {
            return 0;
        }
    }
    public float GetFloatValue(RemoteConfigName name)
    {
        string sName = name.ToString();
        return GetFloatValue(sName);
    }

    // lấy giá trị bool , nếu không có thì trả về false
    public bool GetBoolValue(string name)
    {
        if (RemoteDict.ContainsKey(name))
        {
            string value = RemoteDict[name].ToString();
            if (value == "1" || value == "true")
            {
                return true;
            }
            return false;
        }
        return false;
    }
    public bool GetBoolValue(RemoteConfigName name)
    {
        string sName = name.ToString();
        return GetBoolValue(sName);
    }

    // lấy giá trị json theo type 
    public T GetJsonValueAsType<T>(string name)
    {
        if (RemoteDict.ContainsKey(name))
        {
            try
            {
                T value = JsonUtility.FromJson<T>(RemoteDict[name].ToString());
                return value;
            }
            catch
            {
                // data không phải json 
                return default;
            }
        }
        return default;
        // default == giá trị null;
    }
    public T GetJsonValueAsType<T>(RemoteConfigName name)
    {
        string sName = name.ToString();
        return GetJsonValueAsType<T>(sName);
    }
#endif
}


[System.Serializable]
public struct RemoteConfigDataModule
{
#if SUGAME_VALIDATED
    public RemoteConfigName Name;
    // nếu giá trị là float , thì dùng định dạng A,B chứ không dùng định dạng A.B
    public string defaultValue;
#endif
}
