using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleAuto : MonoBehaviour
{
    public Transform transformText;
    private void OnEnable()
    {
        transformText.transform.DOScale(new Vector3(1.25f, 1.25f, 1.25f), 1.25f)
           .SetEase(Ease.InOutQuad)
           .SetLoops(-1, LoopType.Yoyo);
    }
}
