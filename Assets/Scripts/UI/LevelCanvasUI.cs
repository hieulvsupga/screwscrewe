using EnhancedUI.EnhancedScroller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCanvasUI : MonoBehaviour
{
    public EnhancedScroller enhancedscroller_Level;
    void Start()
    {
        Debug.Log("co chay ma 2");
        GetIndexJumpLevel(PlayerPrefs.GetInt("Playinglevel"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetIndexJumpLevel(int level)
    {
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
