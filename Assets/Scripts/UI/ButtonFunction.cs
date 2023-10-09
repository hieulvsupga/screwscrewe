using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonFunction : MonoBehaviour
{
    public void ClickFuction()
    {
       
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
            case "HintBtn":
                HintBtn();
                break;
            case "BackHintUI":
                BackHintUI();
                break;
            case "NextLevel":
                NextLevel();
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

    public void HintBtn()
    {
        // for (int i = 0; i < Controller.Instance.rootlevel.listHint.Count; i++)
        // {
        //     Controller.Instance.rootlevel.listHint[i].gameObject.SetActive(true);
        // }
        CanvasManagerGamePlay.Instance.HintUI.gameObject.SetActive(true);
    }

    public void BackHintUI()
    {
        // for (int i = 0; i < Controller.Instance.rootlevel.listHint.Count; i++)
        // {
        //     Controller.Instance.rootlevel.listHint[i].gameObject.SetActive(false);
        // }   
        CanvasManagerGamePlay.Instance.HintUI.gameObject.SetActive(false);

    }

    public void NextLevel(){
        LevelController.Instance.NextLevelGame();
    }
}
