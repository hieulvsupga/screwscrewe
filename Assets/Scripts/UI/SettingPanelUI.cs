using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingPanelUI : MonoBehaviour
{
   public void OpenRate()
   {
        gameObject.SetActive(false);
        CanvasGameIn1.Instance.RatePanel.gameObject.SetActive(true);
   }

    public void HomeLevel()
    {
        gameObject.SetActive(false);
        if (SceneManager.GetActiveScene().name == "Level") return;
        Controller.Instance.rootlevel.ClearRoot();
        SceneManager.LoadScene("Level");
        Controller.Instance.nailLayerController.ClearLayer();
    }
}
