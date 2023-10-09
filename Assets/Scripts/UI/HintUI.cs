using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class HintUI : MonoBehaviour
{
    public Image spriteRendererhint;
    private void OnEnable() {
        spriteRendererhint.sprite = LevelController.Instance.screenshotcamera.spritescreenshot;
    }
}
