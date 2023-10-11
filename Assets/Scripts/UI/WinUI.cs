using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinUI : MonoBehaviour
{
    public void ReplayBtn(){
        Controller.Instance.rootlevel.ClearRoot();
        Controller.Instance.nailLayerController.ClearLayer();
        Controller.Instance.LoadLevel();
        gameObject.SetActive(false);
    }

    public void NextLevel(){
        Controller.Instance.LevelIDInt++;
        Controller.Instance.rootlevel.ClearRoot();
        Controller.Instance.nailLayerController.ClearLayer();
        Controller.Instance.LoadLevel();
        gameObject.SetActive(false);
    }
}
