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
        LevelController.Instance.ResetLevel();
        gameObject.SetActive(false);
    }

    public void NextLevel(){
        LevelController.Instance.NextLevelNotDacbiet();
        gameObject.SetActive(false);
    }
}
