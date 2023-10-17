using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class board550x100item : Board_Item
{
    public override void DetermineCenterPoint(Slot_board_Item a) {       
        if (transform.eulerAngles.z <= 275 && transform.eulerAngles.z >= 265)
        {
            float dist = Vector3.Distance(a.transform.position, transform.position);        
            if (transform.position.y < a.transform.position.y)
            {             
                transform.position = new Vector3(a.transform.position.x, a.transform.position.y - dist, transform.position.z);
                transform.rotation = Quaternion.Euler(0, 0, -90);
            }
        }
        else if (transform.eulerAngles.z <= 4 && transform.eulerAngles.z >= 4)
        {
            float dist = Vector3.Distance(a.transform.position, transform.position);         
            if (transform.position.x > a.transform.position.x)
            {
                transform.position = new Vector3(a.transform.position.x + dist, a.transform.position.y, transform.position.z);
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else if (transform.eulerAngles.z <= 185 && transform.eulerAngles.z >= 175)
        {
            float dist = Vector3.Distance(a.transform.position, transform.position);
            if (transform.position.x < a.transform.position.x)
            {
                transform.position = new Vector3(a.transform.position.x - dist, a.transform.position.y, transform.position.z);
                transform.rotation = Quaternion.Euler(0, 0, 180);
            }
        }
    }
}
