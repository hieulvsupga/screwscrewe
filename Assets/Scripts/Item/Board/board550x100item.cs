using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class board550x100item : Board_Item
{
    public override void DetermineCenterPoint(Slot_board_Item a){
        float dist = Vector3.Distance(a.transform.position, transform.position);
        Debug.Log(transform.position.y+"heheh"+a.transform.position.y);
        if(transform.position.y < a.transform.position.y){
            Debug.Log("heheheheehehsboard550x100item"+dist);
            transform.position = new Vector3(a.transform.position.x,a.transform.position.y-dist,transform.position.z);
            if(transform.eulerAngles.z<=270 && transform.eulerAngles.z>=265){
                transform.rotation = Quaternion.Euler(0,0,-90);
            }
        }
    }
}
