using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
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
        StartCoroutine(CreatePhysic2dforboard());
    }

    public IEnumerator CreatePhysic2dforboard()
    {
        yield return new WaitForSeconds(0);
        ButtonLevel.levelplaying++;
        CleanMap();
        Controller.Instance.rootlevel.ClearRoot();
        loadDataBase.LoadLevelGame(ButtonLevel.GetLevelString());
    } 

    public void CleanMap()
    {
        ControllPlayGame.Instance.targetNail = null;
    }
}