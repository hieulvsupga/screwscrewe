using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultUI : MonoBehaviour
{
    // public void Show(){
    //     UIEvents.Instance.ShowDefaultUI();
    // }

    public void ReplayBtn(){
        Controller.Instance.rootlevel.ClearRoot();
        Controller.Instance.nailLayerController.ClearLayer();
        Controller.Instance.LoadLevel();
        gameObject.SetActive(false);
    }

    public void NextLevel(){
        ButtonLevel.levelplaying++;
        Controller.Instance.rootlevel.ClearRoot();
        Controller.Instance.nailLayerController.ClearLayer();
        Controller.Instance.LoadLevel();
        gameObject.SetActive(false);
    }
}
