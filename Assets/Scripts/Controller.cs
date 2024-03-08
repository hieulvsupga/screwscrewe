using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class Controller : MonoBehaviour
{

    public const int MAX_LEVEL = 100;

    public delegate void TimeDelegate(int a);
    public static event TimeDelegate TimeEvent;

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

    [Header("UI")]
    [Space(10)]
    public Background background_ui;

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
        levelInt = PlayerPrefs.GetInt("Playinglevel");
    }

    
    private void Start()
    {
        rootlevel = new RootLevel();
        //levelInt = PlayerPrefs.GetInt("Playinglevel");
    }


    public void StartLevel(){
        //LoadLevelScene("GamePlay");
        CanvasManagerGamePlay.Instance.SelectLevelUI.DisableLevel();
        LevelController.Instance.StartGame();
    }
    
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
    public void TimeRemotetoController(int a)
    {
        TimeEvent?.Invoke(a);
    }
    
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Vector3.zero, 30);
    }

    void DrawCircle(Vector3 center, float radius)
    {
        float theta = 0;
        float x = radius * Mathf.Cos(theta);
        float y = radius * Mathf.Sin(theta);
        Vector3 startPos = center + new Vector3(x, 0, y);
        Vector3 endPos = Vector3.zero;

        for (int i = 1; i <= 360; i++)
        {
            theta = (i * Mathf.PI) / 180;
            x = radius * Mathf.Cos(theta);
            y = radius * Mathf.Sin(theta);
            endPos = center + new Vector3(x, 0, y);
            Debug.DrawLine(startPos, endPos, Color.red);
            startPos = endPos;
        }
    }
}
