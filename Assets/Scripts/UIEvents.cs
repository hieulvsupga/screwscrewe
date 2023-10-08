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
}
