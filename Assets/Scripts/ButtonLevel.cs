using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ButtonLevel : MonoBehaviour
{
    public static int levelplaying;
    public TextMeshProUGUI textLevel;

    private int level;
    public int Level
    {
        set
        {
            level = value;
            textLevel.text = level.ToString();
        }
        get
        {
            return level;
        }
    }
    public void SetupButtonLevel(int _level)
    {
        Level = _level;
    }
    public void ClickLevel()
    {    
        levelplaying = level;
        Controller.Instance.LoadLevel();
    }
    public static string GetLevelString()
    {
        return $"Assets/_GameAssets/data_{levelplaying}.json";
    }
}
