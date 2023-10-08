using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonFunction : MonoBehaviour
{
    public void ClickFuction()
    {
        Debug.Log(gameObject.name);
        switch (gameObject.name)
        {
            case "HomeBtn":
                HomeBtn();
                break;
            case "SettingBtn":
                break;
            case "NoAdsBtn":
                break;
            case "AddTimeBtn":
                AddTimeBtn();
                break;
        }
    }

    public void HomeBtn(){
        Controller.Instance.rootlevel.ClearRoot();
        SceneManager.LoadScene("Level");
        Controller.Instance.nailLayerController.ClearLayer();
    }

    public void AddTimeBtn(){
        Timer.instance.IncreaseTime(20);
    }
}
