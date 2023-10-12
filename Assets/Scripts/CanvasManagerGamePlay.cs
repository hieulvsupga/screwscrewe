using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class CanvasManagerGamePlay : MonoBehaviour
{

    private static CanvasManagerGamePlay instance;
    public static CanvasManagerGamePlay Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CanvasManagerGamePlay>();
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }
    public void Home(){
        Controller.Instance.rootlevel.ClearRoot();
        SceneManager.LoadScene("Level");
        Controller.Instance.nailLayerController.ClearLayer();
    }
    public TextLevel textLevel;
    public Transform DefaultUI;
    public Transform HintUI;
    public Transform WinUI;
    public LevelDacbiet levelDacbietUI;

    public void Test2()
    {
        Nail_Item nail_Item = Controller.Instance.nailSpawner._pool.Get();
        Nail_Item nail_Item3 = Controller.Instance.nailSpawner._pool.Get();
        Nail_Item nail_Item4 = Controller.Instance.nailSpawner._pool.Get();
    }


    public void Test3()
    {
        LevelController.Instance.loadDataBase.LoadLevelGame(ButtonLevel.GetLevelString());
    }
}
