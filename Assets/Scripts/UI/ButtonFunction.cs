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
                Slot_Item.flag = 1;
                HomeBtn();
                break;
            case "SettingBtn":
                Slot_Item.flag = 1;
                SettingBtn();
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
            case "ResetBtn":
                AudioController.Instance.PlayClip("clean");
                ResetBtn();
                break;
        }
    }

    public void HomeBtn(){
        Controller.Instance.nailLayerController.ClearLayer();
        Controller.Instance.rootlevel?.ClearRoot(() =>
        {
            //SceneManager.LoadScene("Level");
            CanvasManagerGamePlay.Instance.IngameUI.gameObject.SetActive(false);
            Timer.instance.uiText.gameObject.SetActive(false);
            CanvasManagerGamePlay.Instance.SelectLevelUI.HienCanvasLevel();
        });
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
        Controller.Instance.TimeRemotetoController(0);
        CanvasManagerGamePlay.Instance.HintUI.gameObject.SetActive(true);
    }

    public void BackHintUI()
    {
        // for (int i = 0; i < Controller.Instance.rootlevel.listHint.Count; i++)
        // {
        //     Controller.Instance.rootlevel.listHint[i].gameObject.SetActive(false);
        // }   
        Controller.Instance.TimeRemotetoController(1);
        CanvasManagerGamePlay.Instance.HintUI.gameObject.SetActive(false);

    }

    public void NextLevel(){
        LevelController.Instance.NextLevelNotDacbiet();
    }

    public void SettingBtn(){
        Controller.Instance.TimeRemotetoController(0);
        CanvasGameIn1.Instance.SettingPanel.gameObject.SetActive(true);
    }
    

    public void ResetBtn()
    {
        //Controller.Instance.rootlevel.ClearRoot();
        //Controller.Instance.nailLayerController.ClearLayer();
        LevelController.Instance.ResetLevel();
    }
}
