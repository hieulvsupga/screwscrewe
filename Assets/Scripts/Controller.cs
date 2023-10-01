using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Controller : MonoBehaviour
{
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

    public int LoadDataIndex = 0;
    public NailSpawner nailSpawner;
    // Start is called before the first frame update
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
    public void LoadLevel(){
        StartCoroutine(LoadAsset());
    }
    private IEnumerator LoadAsset()
    {
        while (LoadDataIndex < 1 && LoadDataIndex>=0)
        {
            yield return null;
        }
        SceneManager.LoadScene("GamePlay");
    }
}
