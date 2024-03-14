using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidCicleBoard : Board_Item
{
    public override bool Define_intersection(Vector3 positionM)
    {
        float distance = Vector3.Distance(transform.position, positionM);
        float radiuscicle = Vector3.Distance(transform.position, positionAnchor[0].position);
        //Mathf.Abs(positionAnchor[0].position.y);
        Debug.Log(radiuscicle +"hehhfehfawehfaiwhef" +distance);
        if (distance < radiuscicle)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
