using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextLevel : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI textLevel;
   
    void Start()
    {
        textLevel.text = "Level " + Controller.Instance.LevelIDInt;
    }
}
