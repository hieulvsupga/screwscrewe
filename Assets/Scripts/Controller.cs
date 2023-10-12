using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Controller : MonoBehaviour
{
    private AsyncOperation async;
    private static Controller instance;
    public static Controller Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Controller>();
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }
    public Camera cameraMain;
    public RootLevel rootlevel;
    public int LoadDataIndex = 0;
    public NailSpawner nailSpawner;

    public NailLayerController nailLayerController;
    // Start is called before the first frame update



    private int levelInt;
    public int LevelIDInt
    {
        get
        {
            return levelInt;
        }
        set
        {
            levelInt = value;
            if (levelInt > PlayerPrefs.GetInt("Playinglevel"))
            {
                PlayerPrefs.SetInt("Playinglevel", levelInt);
            }
            if (CanvasManagerGamePlay.Instance != null)
            {
                CanvasManagerGamePlay.Instance.textLevel.textLevel.text = "Level " + levelInt;
            }
        }
    }
    private void Awake() {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            //DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        rootlevel = new RootLevel();
        levelInt = PlayerPrefs.GetInt("Playinglevel");
    }


    public void LoadLevel(){
        //StartCoroutine(LoadAsset());
        LoadLevelScene("GamePlay");
    }
    // private IEnumerator LoadAsset()
    // {
    //     while (LoadDataIndex < 1 && LoadDataIndex>=0)
    //     {
    //         yield return null;
    //     }
    //     SceneManager.LoadScene("GamePlay");
    // }

    public void LoadLevelScene(string namescene){
        StartCoroutine(LoadSceneAsync(namescene));
    }

    IEnumerator LoadSceneAsync (string sceneName){
        if(!string.IsNullOrEmpty(sceneName)){
            async = SceneManager.LoadSceneAsync(sceneName);
            while(!async.isDone && LoadDataIndex <17 && LoadDataIndex>=0){
                yield return 0;
            }
            
        }
    }
}
