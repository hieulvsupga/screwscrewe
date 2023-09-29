using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
using SUGA.SuGameEditor;
#endif
using UnityEngine;

public class ValidateSuGame : UnityEditor.AssetModificationProcessor
{
    const string DefineSymbolValidated = "SUGAME_VALIDATED";
#if UNITY_EDITOR

    static void OnWillCreateAsset(string assetName)
    {
        Debug.Log("OnWillCreateAsset is being called with the following asset: " + assetName + ".");
    }

    static AssetDeleteResult OnWillDeleteAsset(string path, RemoveAssetOptions opt)
    {        
        string pathParent = Directory.GetParent(path).Name;
        if (pathParent == "EnumValues")
        {
            return AssetDeleteResult.FailedDelete;
        }
        return AssetDeleteResult.DidNotDelete;
    }


    [InitializeOnLoadMethod]
    static void InitWhenLoad()
    {
        bool haveActionShowAds = ValidateActionShowAds();
        bool haveRemoteConfigName = ValidateRemoteConfigName();
        bool haveAnalyticsEventName = ValidateAnalyticsEventName();
        if (!haveActionShowAds || !haveRemoteConfigName || !haveAnalyticsEventName)
        {
            SuGameEditor.RemoveSymbol(DefineSymbolValidated);
        }
        else
        {
            SuGameEditor.AddSymbol(DefineSymbolValidated);
        }
    }


    static bool ValidateRemoteConfigName()
    {
        string[] res = System.IO.Directory.GetFiles(Application.dataPath, "RemoteConfigName.cs", SearchOption.AllDirectories);
        if (res.Length == 0)
        {
            Debug.Log("Không tồn tại file RemoteConfigName.cs");
            // không tồn tại file 
            // tạo file và thêm symbols 

            string[] pathToTemplate = System.IO.Directory.GetFiles(Application.dataPath, "RemoteConfigNameTemplate.txt", SearchOption.AllDirectories);
            if (pathToTemplate != null && pathToTemplate.Length > 0)
            {
                string sTemplate = File.ReadAllText(pathToTemplate[0]);
                string pathToCreateScript = Directory.GetParent(pathToTemplate[0]).Parent.Parent.FullName + "/EnumValues/RemoteConfigName.cs";
                Debug.Log("Path to create script là " + pathToCreateScript);
                if (!File.Exists(pathToCreateScript))
                {
                    // Create a file to write to.    
                    Debug.Log("Create Script RemoteConfigName.cs");
                    File.WriteAllText(pathToCreateScript, sTemplate);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            // co ton tai
            return true;
        }
    }

    static bool ValidateAnalyticsEventName()
    {
        string[] res = System.IO.Directory.GetFiles(Application.dataPath, "AnalyticsEventName.cs", SearchOption.AllDirectories);
        if (res.Length == 0)
        {
            Debug.Log("Không tồn tại file AnalyticsEventName.cs");
            // không tồn tại file 
            // tạo file và thêm symbols 

            string[] pathToTemplate = System.IO.Directory.GetFiles(Application.dataPath, "AnalyticsEventNameTemplate.txt", SearchOption.AllDirectories);
            if (pathToTemplate != null && pathToTemplate.Length > 0)
            {
                string sTemplate = File.ReadAllText(pathToTemplate[0]);
                string pathToCreateScript = Directory.GetParent(pathToTemplate[0]).Parent.Parent.FullName + "/EnumValues/AnalyticsEventName.cs";
                Debug.Log("Path to create script là " + pathToCreateScript);
                if (!File.Exists(pathToCreateScript))
                {
                    // Create a file to write to.    
                    Debug.Log("Create Script AnalyticsEventName.cs");
                    File.WriteAllText(pathToCreateScript, sTemplate);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            // co ton tai
            return true;
        }
    }

    static bool ValidateActionShowAds()
    {
        string[] res = System.IO.Directory.GetFiles(Application.dataPath, "ActionShowAds.cs", SearchOption.AllDirectories);
        if (res.Length == 0)
        {
            Debug.Log("Không tồn tại file ActionShowAds.cs");
            // không tồn tại file 
            // tạo file và thêm symbols 

            string[] pathToTemplate = System.IO.Directory.GetFiles(Application.dataPath, "ActionShowAdsTemplate.txt", SearchOption.AllDirectories);
            if (pathToTemplate != null && pathToTemplate.Length > 0)
            {
                string sTemplate = File.ReadAllText(pathToTemplate[0]);
                string pathToCreateScript = Directory.GetParent(pathToTemplate[0]).Parent.Parent.FullName + "/EnumValues/ActionShowAds.cs";
                if (!File.Exists(pathToCreateScript))
                {
                    // Create a file to write to.                   
                    File.WriteAllText(pathToCreateScript, sTemplate);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            // co ton tai
            return true;
        }
    }




#endif
}
