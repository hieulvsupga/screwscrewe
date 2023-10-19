using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TxTScaleAuto : MonoBehaviour
{
    public Transform transformText;
    public void SetUp()
    {    
        transformText.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 1f)
            .SetEase(Ease.InOutQuad)
            .SetLoops(-1, LoopType.Yoyo);
    }
    //private void OnEnable()
    //{
       
    //    transformText.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 1f)
    //        .SetEase(Ease.InOutQuad)
    //        .SetLoops(-1, LoopType.Yoyo);
    //}
}
