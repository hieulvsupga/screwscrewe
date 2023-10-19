using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SwitchBtn : MonoBehaviour
{
    public Sprite[] sprites;
    public Image imageView;
    public bool checkActive = false;
    public RectTransform rectTransform;
    private void Start()
    {
        switch (gameObject.name)
        {
            case "SFXButton":
                int m = PlayerPrefs.GetInt("Sound");
                if( m ==0)
                {
                    imageView.sprite = sprites[1];
                    rectTransform.anchoredPosition = new Vector2(-57, -7.5f);
                 
                }
                else
                {
                    imageView.sprite = sprites[0];
                    rectTransform.anchoredPosition = new Vector2(57, -7.5f);
                  
                }
                break;
            case "MusicButton":

                break;
        }
    }
    public void ClickButton()
    {
        checkActive = !checkActive;
        if (checkActive)
        {
            imageView.sprite = sprites[0];
            rectTransform.anchoredPosition = new Vector2(57, -7.5f);
            switch (gameObject.name)
            {
                case "SFXButton":
                    SettingPanelUI.SoundCheck = 1;
                    PlayerPrefs.SetInt("Sound", 1);
                    break;
                case "MusicButton":
                    
                    break;
            }
        }
        else
        {
            imageView.sprite = sprites[1];
            rectTransform.anchoredPosition = new Vector2(-57, -7.5f);
            switch (gameObject.name)
            {
                case "SFXButton":
                    SettingPanelUI.SoundCheck = 0;
                    PlayerPrefs.SetInt("Sound", 0);
                    break;
                case "MusicButton":
                    
                    break;
            }
        }
    }

}
