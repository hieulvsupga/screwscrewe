using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDacbiet : MonoBehaviour
{

    public void NotDacbiet(){
        //LevelController.Instance.CleanMap();
        //Controller.Instance.rootlevel.ClearRoot();
        //LevelController.Instance.loadDataBase.LoadLevelGame(ButtonLevel.GetLevelString());
        LevelController.Instance.NextLevelNotDacbiet();
        gameObject.SetActive(false);
    }

    public void Dacbiet(){
        Controller.Instance.LevelIDInt++;
        Controller.Instance.background_ui.Dacbiet();
        LevelController.Instance.CleanMap();
        Controller.Instance.rootlevel?.ClearRoot();
        LevelController.Instance.loadDataBase.LoadLevelGame(ButtonLevel.GetLevelDacbietString());
        Controller.Instance.nailLayerController.ClearLayer();
        gameObject.SetActive(false);
    }
}
