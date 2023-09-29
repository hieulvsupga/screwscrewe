using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
namespace SUGA.SuGameEditor
{
    public class SuGameEditor : MonoBehaviour
    {
        public static void RemoveSymbol(string symbolName)
        {
            string[] symbols;
            PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, out symbols);
            if (symbols != null)
            {
                int index = Array.IndexOf(symbols, symbolName);
                if (index >= 0)
                {
                    // có synbol,remove nó
                    symbols = symbols.Where(e => e != symbolName).ToArray();
                    // set lại vào playersetting
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, symbols);
                }
            }
        }

        public static bool HaveSymbol(string symbolName)
        {
            string[] symbols;
            PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, out symbols);
            if (symbols != null)
            {
                int index = Array.IndexOf(symbols, symbolName);
                if (index >= 0)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public static void AddSymbol(string symbolName)
        {
            string[] symbols;
            PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, out symbols);
            if (symbols != null)
            {
                int index = Array.IndexOf(symbols, symbolName);
                if (index == -1)
                {
                    // không có synbol, thêm vào 
                    symbols = symbols.Concat(new string[] { symbolName }).ToArray();
                    // set lại vào playersetting
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, symbols);
                }
            }
        }
    }
}
