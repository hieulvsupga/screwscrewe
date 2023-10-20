using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testrotate : MonoBehaviour
{
    public Transform a;
    // Start is called before the first frame update
    void Start()
    {
        a.transform.SetParent(transform.parent);
        transform.SetParent(a);
        a.rotation = Quaternion.Euler(0, 0, -15);
        this.transform.SetParent(a.parent);
        a.transform.SetParent(this.transform);
        Debug.Log("co chay");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
