using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchBtn : MonoBehaviour
{
    public Sprite[] sprites;
    public Image imageView;
    public bool checkActive = false;
    public RectTransform rectTransform;
    public void ClickButton()
    {
        checkActive = !checkActive;
        if (checkActive)
        {
            imageView.sprite = sprites[0];
            rectTransform.anchoredPosition = new Vector2(57, -7.5f);
        }
        else
        {
            imageView.sprite = sprites[1];
            rectTransform.anchoredPosition = new Vector2(-57, -7.5f);
        }
    }

}
