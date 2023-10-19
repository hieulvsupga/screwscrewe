using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelController : MonoBehaviour
{
    public List<IUIEffectItem> ListUIEffectControll = new List<IUIEffectItem>();
    public delegate void CheckActionUser();
    public event CheckActionUser checkActionUser;


    public List<int> LevelDacbiet = new List<int>(){9,19,29};
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

    public Transform HelpHandTurtorial;
    public TxTScaleAuto TxtTur;
    void Start()
    {
        // Physics2D.IgnoreLayerCollision(17, 17, false);    


        //rootlevel = new RootLevel();
        //loadDataBase.LoadLevelGame("Assets/_GameAssets/data_2.json");
        
        loadDataBase.LoadLevelGame(ButtonLevel.GetLevelString());
        Controller.Instance.background_ui.Notdacbiet();
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
    } 

    public void CleanMap()
    {
        ControllPlayGame.Instance.targetNail = null;

        //Debug.Log(ListUIEffectControll.Count+"so luong la");
        for(int i=0; i< ListUIEffectControll.Count; i++)
        {
            ListUIEffectControll[i].ResetPool();
        }
        ListUIEffectControll.Clear();
    }

    public void ResetLevel()
    {
        CleanMap();
        Controller.Instance.rootlevel.ClearRoot();
        Controller.Instance.nailLayerController.ClearLayer();
        loadDataBase.LoadLevelGame(ButtonLevel.GetLevelString());
       
    }

    public void NextLevelNotDacbiet()
    {
        if(Background.statusbg == "dacbiet")
        {
            Controller.Instance.background_ui.Notdacbiet();
        }

        CleanMap();
        Controller.Instance.LevelIDInt++;
        Controller.Instance.rootlevel.ClearRoot();
        Controller.Instance.nailLayerController.ClearLayer();
        loadDataBase.LoadLevelGame(ButtonLevel.GetLevelString());
      
    }


    public void ActiveActionUser()
    {
        checkActionUser?.Invoke();
    }
   


}