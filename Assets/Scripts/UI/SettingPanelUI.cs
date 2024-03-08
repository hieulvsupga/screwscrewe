using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingPanelUI : MonoBehaviour
{
   public static int SoundCheck = 0;
   public static int musicCheck = 0;
   public static int MusicCheck
    {
        set {
            musicCheck = value;
            if(musicCheck == 1)
            {
                AudioController.Instance.StartSoundBackGround();
            }
            else
            {
                AudioController.Instance.EndSoundBackGround();
            }
        }
        get
        {
            return musicCheck;
        }
    }
   public void OpenRate()
   {
        gameObject.SetActive(false);
        CanvasGameIn1.Instance.RatePanel.gameObject.SetActive(true);
   }

    public void HomeLevel()
    {
        gameObject.SetActive(false);
        if (SceneManager.GetActiveScene().name == "Level") return;
        Controller.Instance.nailLayerController.ClearLayer();
        Controller.Instance.rootlevel?.ClearRoot(() =>
        {
            CanvasManagerGamePlay.Instance.IngameUI.gameObject.SetActive(false);
            Timer.instance.uiText.gameObject.SetActive(false);
            CanvasManagerGamePlay.Instance.SelectLevelUI.HienCanvasLevel();
        });
    }
}
