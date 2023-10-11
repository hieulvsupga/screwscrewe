using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ButtonLevel : MonoBehaviour
{
    public TextMeshProUGUI textLevel;
    public Image backgroudImage;
    public Image statusImage;
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
        Controller.Instance.LevelIDInt = level;
        Controller.Instance.LoadLevel();
    }
    public static string GetLevelString()
    {
        return $"Assets/_GameAssets/data_{Controller.Instance.LevelIDInt}.json";
    }

    public void Actived()
    {
    
    }

    public void Activing()
    {
    
    }

    public void AwaitActive()
    { 
    }
}
