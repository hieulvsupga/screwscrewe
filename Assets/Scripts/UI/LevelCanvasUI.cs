using EnhancedUI.EnhancedScroller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCanvasUI : MonoBehaviour
{
    public EnhancedScroller enhancedscroller_Level;
    public static int flag = 0;

    void Start()
    {
        if(flag == 1) { return; }
        //GetIndexJumpLevel(PlayerPrefs.GetInt("Playinglevel"));
        GetIndexJumpLevel(Controller.Instance.LevelIDInt);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetIndexJumpLevel(int level)
    {
        flag = 1;
        int m = (int)Mathf.Floor(level / 5);
        if (level % 3 != 0)
        {

        }
        else
        {
            if (m > 0)
            {
                m -= 1;
            }
        }
        enhancedscroller_Level.jumpdesire = m;
    }
}
