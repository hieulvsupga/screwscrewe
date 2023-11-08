using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinUI : MonoBehaviour
{
    public void ReplayBtn(){
        if (LoadDataBase.checkgameloadingRun == true)
        {
            return;
        }
        Controller.Instance.nailLayerController.ClearLayer();
        Controller.Instance.rootlevel?.ClearRoot(() =>
        {
            LoadDataBase.Instance.LoadLevelGame(ButtonLevel.GetLevelString());
            gameObject.SetActive(false);
        });
    }

    public void NextLevel(){
        //Controller.Instance.LevelIDInt++;
        //Debug.Log(LevelController.Instance.LevelDacbiet.Contains(Controller.Instance.LevelIDInt) + "hehehehehehe");
        if(LevelController.Instance.LevelDacbiet.Contains(Controller.Instance.LevelIDInt)){
            //Controller.Instance.LevelIDInt++;
            CanvasManagerGamePlay.Instance.levelDacbietUI.gameObject.SetActive(true);
        }else{
            LevelController.Instance.NextLevelNotDacbiet();
            //ControllPlayGame.Instance.targetNail = null;
            //Controller.Instance.rootlevel.ClearRoot();
            //LevelController.Instance.loadDataBase.LoadLevelGame(ButtonLevel.GetLevelString());
        }     
       
       
        gameObject.SetActive(false);
    }
}
