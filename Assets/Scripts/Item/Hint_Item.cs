using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Hint_Item : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI textIdHint;
    public void SetUpTextIdHint(string str)
    {
        textIdHint.text = str;
    }
}
