﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class board275x100 : Board_Item
{
    //test 14 - 6
    public override void AutoRotate(Slot_board_Item slotboardItem, Slot_board_Item findanySlotBoardIteminboard, Slot_Item slotItem)
    {
        Slot_board_Item parentt = FindOtherSlotBoard(slotboardItem);
        Vector2 dirboard = findanySlotBoardIteminboard.transform.position - slotItem.transform.position;
        Vector2 dir3 = findanySlotBoardIteminboard.transform.position - slotboardItem.transform.position;
        findanySlotBoardIteminboard.transform.SetParent(this.transform.parent);
        this.transform.SetParent(findanySlotBoardIteminboard.transform);
        float a = findanySlotBoardIteminboard.transform.rotation.eulerAngles.z + Vector2.Angle(dir3, dirboard);
        //Debug.Log(slotItem.transform.position.y + "=======================" + slotboardItem.transform.position.y);
        float bb = slotItem.transform.position.y - slotboardItem.transform.position.y;

        //Debug.Log(bb + "fffffffffffffff");
        //if (bb > 0)
        //{
        //    Debug.Log("TANG");
        //    findanySlotBoardIteminboard.transform.rotation = Quaternion.Euler(0, 0, findanySlotBoardIteminboard.transform.rotation.eulerAngles.z + Vector2.Angle(dir3, dirboard));
        //}
        //else
        //{
        //    Debug.Log("gIAM");
        //    findanySlotBoardIteminboard.transform.rotation = Quaternion.Euler(0, 0, findanySlotBoardIteminboard.transform.rotation.eulerAngles.z - Vector2.Angle(dir3, dirboard));
        //}


        Vector3 axis = Vector3.Cross(dir3, dirboard);
        float angle = Vector3.Angle(dir3, dirboard);
        Quaternion rotation = Quaternion.AngleAxis(angle, axis);

        // Áp dụng phép quay cho đường thẳng a
        findanySlotBoardIteminboard.transform.rotation = rotation * findanySlotBoardIteminboard.transform.rotation;








        //float dotProduct = Vector3.Dot(dirboard.normalized, dir3.normalized);
        //float angle = Mathf.Acos(dotProduct) * Mathf.Rad2Deg;
        //float anglethuduoc = Vector2.Angle(dir3, dirboard);
        //Debug.Log(anglethuduoc + "=====================================");
        //Debug.Log(findanySlotBoardIteminboard.transform.localEulerAngles.z);
        //Debug.Log(findanySlotBoardIteminboard.transform.eulerAngles.z);
        //Vector3 rotEuler = findanySlotBoardIteminboard.transform.eulerAngles;


        //Quaternion rotation2 = Quaternion.FromToRotation(dirboard, dir3);
        //Vector3 axis;
        //float angle2;
        //rotation2.ToAngleAxis(out angle2, out axis);












        ////if (angle2 > 0)
        ////{
        ////    findanySlotBoardIteminboard.transform.eulerAngles = rotEuler + new Vector3(0, 0, anglethuduoc);
        ////}
        ////else
        ////{
        ////    findanySlotBoardIteminboard.transform.eulerAngles = rotEuler - new Vector3(0, 0, anglethuduoc);
        ////}
        //if (angle > 0)
        //{
        //    //findanySlotBoardIteminboard.transform.rotation = Quaternion.Euler(0, 0, -anglethuduoc);
        //    //Debug.Log("tang");
        //    findanySlotBoardIteminboard.transform.eulerAngles = rotEuler - new Vector3(0, 0, anglethuduoc);
        //}
        //else
        //{
        //    //findanySlotBoardIteminboard.transform.rotation = Quaternion.Euler(0, 0, +anglethuduoc);
        //    //Debug.Log("giam");
        //    findanySlotBoardIteminboard.transform.eulerAngles = rotEuler + new Vector3(0, 0, anglethuduoc);
        //}
        //Debug.Log(findanySlotBoardIteminboard.transform.eulerAngles.z+"uonke");












        //if (this.transform.eulerAngles.z < 90)
        //{
        //    if (this.transform.eulerAngles.z < 60f)
        //    {
        //        findanySlotBoardIteminboard.transform.rotation = Quaternion.Euler(0, 0, findanySlotBoardIteminboard.transform.rotation.eulerAngles.z + Vector2.Angle(dir3, dirboard));
        //        Debug.Log("tang");
        //    }
        //    else
        //    {
        //        findanySlotBoardIteminboard.transform.rotation = Quaternion.Euler(0, 0, findanySlotBoardIteminboard.transform.rotation.eulerAngles.z - Vector2.Angle(dir3, dirboard));
        //        Debug.Log("giam");
        //    }
        //}
        //else
        //{
        //    if (this.transform.eulerAngles.z < 300f)
        //    {
        //        findanySlotBoardIteminboard.transform.rotation = Quaternion.Euler(0, 0, findanySlotBoardIteminboard.transform.rotation.eulerAngles.z + Vector2.Angle(dir3, dirboard));
        //        Debug.Log("tang");
        //    }
        //    else
        //    {
        //        findanySlotBoardIteminboard.transform.rotation = Quaternion.Euler(0, 0, findanySlotBoardIteminboard.transform.rotation.eulerAngles.z - Vector2.Angle(dir3, dirboard));
        //        Debug.Log("giam");
        //    }
        //}
        this.transform.SetParent(findanySlotBoardIteminboard.transform.parent);
        findanySlotBoardIteminboard.transform.SetParent(transform);
    }
}
