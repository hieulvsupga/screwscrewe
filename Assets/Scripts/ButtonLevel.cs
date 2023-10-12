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
        SetUp();
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

    //private void Start() {
    //    if(Level < Controller.Instance.LevelIDInt){
    //        Actived();
    //    }else if (Level == Controller.Instance.LevelIDInt){
    //        Activing();
    //    }else{
    //        AwaitActive();
    //    }
    //}
    public void SetUp()
    {
        if (Level < Controller.Instance.LevelIDInt)
        {
            Actived();
        }
        else if (Level == Controller.Instance.LevelIDInt)
        {
            Activing();
        }
        else
        {
            AwaitActive();
        }
    }

    public void Actived()
    {
        backgroudImage.sprite = ResourcesLevel.Instance.spriteBackgroudLevel[0];
        statusImage.sprite = ResourcesLevel.Instance.statusLevelButtonLevel[0];
        statusImage.enabled = true;
    }

    public void Activing()
    {
        backgroudImage.sprite = ResourcesLevel.Instance.spriteBackgroudLevel[1];
        statusImage.enabled = false;
    }

    public void AwaitActive()
    { 
        backgroudImage.sprite = ResourcesLevel.Instance.spriteBackgroudLevel[2];
        statusImage.sprite = ResourcesLevel.Instance.statusLevelButtonLevel[1];
        statusImage.enabled = true;
    }
}
