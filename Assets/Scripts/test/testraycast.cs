using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testraycast : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Vector2.right+"ffff");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 20);
        Debug.DrawLine(transform.position, transform.position - transform.up, Color.green);
    }
}
