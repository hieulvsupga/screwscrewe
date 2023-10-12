using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDacbiet : MonoBehaviour
{

    public void NotDacbiet(){
        LevelController.Instance.CleanMap();
        Controller.Instance.rootlevel.ClearRoot();
        LevelController.Instance.loadDataBase.LoadLevelGame(ButtonLevel.GetLevelString());
        gameObject.SetActive(false);
    }

    public void Dacbiet(){
        LevelController.Instance.CleanMap();
        Controller.Instance.rootlevel.ClearRoot();
        LevelController.Instance.loadDataBase.LoadLevelGame(ButtonLevel.GetLevelDacbietString());
        gameObject.SetActive(false);
    }
}
