using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[System.Serializable]
public class AdsUnit
{
    [HideInInspector]
    public bool IsTest = false;
    [ShowIf("@this.IsTest == false")]
    public string Android, IOS, Android2;
    [ShowIf("@this.IsTest == true")]
    [DisableInEditorMode]
    public string Android_Test, IOS_Test;
    public string ID
    {
        get
        {
            if (IsTest)
            {
                return Application.platform == RuntimePlatform.IPhonePlayer ? IOS_Test : Android_Test;
            }
            else
                return Application.platform == RuntimePlatform.IPhonePlayer ? IOS : Android;
        }
    }

    

    //hieu
    public string ID2
    {
        get
        {
            if (IsTest)
            {
                return Application.platform == RuntimePlatform.IPhonePlayer ? IOS_Test : Android_Test;
            }
            else
                return Application.platform == RuntimePlatform.IPhonePlayer ? IOS : Android2;
        }
    }
}
