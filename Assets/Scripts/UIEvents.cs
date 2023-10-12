using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEvents : MonoBehaviour
{
    private static UIEvents instance;
    public static UIEvents Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIEvents>();
            }
            return instance;
        }
        private set
        {
            instance = value;
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

    public void ShowDefaultUI(){
        if(CanvasManagerGamePlay.Instance == null)return;
        CanvasManagerGamePlay.Instance.DefaultUI.gameObject.SetActive(true);
    }

    public void ShowWinUI(){
        Timer.instance.Pause();
        LevelController.Instance.paritcleSystemWin.Play();
        Invoke("ActiveWinUI", 0.5f);
    }
    private void ActiveWinUI()
    {
        if (CanvasManagerGamePlay.Instance == null) return;
        CanvasManagerGamePlay.Instance.WinUI.gameObject.SetActive(true);
    }
}
