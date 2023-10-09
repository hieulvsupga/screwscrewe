using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;
public static class Utiliti
{
   public static string SetCoinsText(int value)
   {     
        string formattedNumber = "";
        if (value >= 1000000000)
        {
            return "Max";
        }
        if (value >= 1000000)
        {
            float roundedNumber = Mathf.Round(value / 100000) / 10;
            formattedNumber = roundedNumber.ToString("F1") + "M";
        }
        else if (value >= 10000)
        {
            float roundedNumber = Mathf.Round(value / 100) / 10;
            formattedNumber = roundedNumber.ToString("F1") + "K";
        }
        else
        {
            formattedNumber = value.ToString();
        }

        return formattedNumber;
    }
    static int GetFirstDigitFromNumber(int value)
    {
        return int.Parse(value.ToString()[0].ToString());
    }
    public static int RoundOff(this int i)
    {
         return ((int)Math.Round(i / 10.0)) * 10;
    }
    public static int GetCurrentLODIndex(LODGroup lodGroup)
    {
        LOD[] lods = lodGroup.GetLODs();
        for (int i = 0; i < lods.Length; i++)
        {
            LOD lod = lods[i];
            if (lod.renderers.Length > 0 && lod.renderers[0].isVisible)
                return i;
        }
        return -1;
    }


    public static bool IsNetworkConnected()
    {
        return Application.internetReachability != NetworkReachability.NotReachable;
    }

    public static void DeLog(Object myObj, string prefix, string msg)
    {
#if UNITY_EDITOR
        Debug.Log($"{msg}");
#endif
    }

    public static void Log(this Object myObj, string msg)
    {
      // DeLog(myObj, "", msg);
    }
    public static Sprite ConvertToSprite(this Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
    }
   

   

}
