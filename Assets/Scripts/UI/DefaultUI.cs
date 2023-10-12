using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DefaultUI : MonoBehaviour
{
    // public void Show(){
    //     UIEvents.Instance.ShowDefaultUI();
    // }

    public void ReplayBtn(){
        Controller.Instance.rootlevel.ClearRoot();
        Controller.Instance.nailLayerController.ClearLayer();
        LevelController.Instance.loadDataBase.LoadLevelGame(ButtonLevel.GetLevelString());
        ControllPlayGame.Instance.targetNail = null;
        gameObject.SetActive(false);
    }

    public void NextLevel(){
        Controller.Instance.LevelIDInt++;
        Controller.Instance.rootlevel.ClearRoot();
        Controller.Instance.nailLayerController.ClearLayer();
        LevelController.Instance.loadDataBase.LoadLevelGame(ButtonLevel.GetLevelString());
        ControllPlayGame.Instance.targetNail = null;
        gameObject.SetActive(false);
    }
}
