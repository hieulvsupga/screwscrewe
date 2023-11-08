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



    //public LoadDataBase loadDataBase;
    public Transform MainLevelSetupCreateMap;

    public ScreenShotCamera screenshotcamera;

    public Transform HelpHandTurtorial;
    public TxTScaleAuto TxtTur;
    public void StartGame()
    {           
        LoadDataBase.Instance.LoadLevelGame(ButtonLevel.GetLevelString());
        CanvasManagerGamePlay.Instance.IngameUI.gameObject.SetActive(true);
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
            if (LoadDataBase.checkgameloadingRun == false)
            {
                CleanMap();
                Controller.Instance.rootlevel?.ClearRoot(() =>
                {
                    LoadDataBase.Instance.LoadLevelGame(ButtonLevel.GetLevelString());
                });
            }
            
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
        if (LoadDataBase.checkgameloadingRun == true)
        {         
            return;
        }
        CleanMap();
        Controller.Instance.nailLayerController.ClearLayer();
        Controller.Instance.rootlevel?.ClearRoot(() => {
            if (ButtonLevel.LevelPlaying == "")
            {
                LoadDataBase.Instance.LoadLevelGame(ButtonLevel.GetLevelString());
                Debug.Log("CHAY 1");
            }
            else
            {
                LoadDataBase.Instance.LoadLevelGame(ButtonLevel.LevelPlaying);
                Debug.Log("CHAY 2");
            }
        }); 
    }

    public void ResetLevel2()
    {
        //CleanMap();
        //Controller.Instance.nailLayerController.ClearLayer();
        //Controller.Instance.rootlevel?.ClearRoot(() =>
        //{
        //    for (int i = 0; i < Controller.Instance.transform.childCount; i++)
        //    {
        //        Destroy(Controller.Instance.transform.GetChild(i).gameObject);
        //    }
        //    if (ButtonLevel.LevelPlaying == "")
        //    {
        //        LoadDataBase.Instance.LoadLevelGame2(ButtonLevel.GetLevelString());
        //        Debug.Log("CHAY 1");
        //    }
        //    else
        //    {
        //        LoadDataBase.Instance.LoadLevelGame2(ButtonLevel.LevelPlaying);
        //        Debug.Log("CHAY 2");
        //    }
        //});
    }

    public void NextLevelNotDacbiet()
    {
        if (LoadDataBase.checkgameloadingRun == true)
        {      
            return;
        }
        if (Background.statusbg == "dacbiet")
        {
            Controller.Instance.background_ui.Notdacbiet();
        }
        CleanMap();
        Controller.Instance.LevelIDInt++;
        Controller.Instance.nailLayerController.ClearLayer();
        Controller.Instance.rootlevel.ClearRoot(() =>
        {
            LoadDataBase.Instance.LoadLevelGame(ButtonLevel.GetLevelString());
        });
    
        //Debug.Log(Controller.Instance.LevelIDInt+"eiiiiiiiiiiiiii");
        //Controller.Instance.nailLayerController.ClearLayer();
        //loadDataBase.LoadLevelGame(ButtonLevel.GetLevelString());
    }


    public void ActiveActionUser()
    {
        checkActionUser?.Invoke();
    }
   


}