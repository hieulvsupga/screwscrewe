using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ButtonLevel : MonoBehaviour
{
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
        PlayerPrefs.SetString("levelstart", $"Assets/_GameAssets/data_{level}.json");
        SceneManager.LoadScene("GamePlay");
    }
}
