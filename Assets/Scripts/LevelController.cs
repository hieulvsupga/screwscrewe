using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public List<int> LevelDacbiet = new List<int>(){10,20,30};
    public ParticleSystem paritcleSystemWin;
    private static LevelController instance;
    public static LevelController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LevelController>();
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }



    public LoadDataBase loadDataBase;
    public Transform MainLevelSetupCreateMap;

    public ScreenShotCamera screenshotcamera;
    //public RootLevel rootlevel;
    void Start()
    {
        // Physics2D.IgnoreLayerCollision(17, 17, false);    


        //rootlevel = new RootLevel();
        //loadDataBase.LoadLevelGame("Assets/_GameAssets/data_2.json");
        loadDataBase.LoadLevelGame(ButtonLevel.GetLevelString());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NextLevelGame()
    {
        StartCoroutine(NextLevel());
    }

    public IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(0);
        Controller.Instance.LevelIDInt++;
        if(LevelDacbiet.Contains(Controller.Instance.LevelIDInt)){
            CanvasManagerGamePlay.Instance.levelDacbietUI.gameObject.SetActive(true);
        }else{
            CleanMap();
            Controller.Instance.rootlevel.ClearRoot();
            loadDataBase.LoadLevelGame(ButtonLevel.GetLevelString());
        }
        Controller.Instance.nailLayerController.ClearLayer();
        Debug.Log("hehehehehe");
    } 

    public void CleanMap()
    {
        ControllPlayGame.Instance.targetNail = null;
    }
}