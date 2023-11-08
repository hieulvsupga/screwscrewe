using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class board400x100 : Board_Item
{
    public override void AutoRotate(Slot_board_Item slotboardItem, Slot_board_Item findanySlotBoardIteminboard, Slot_Item slotItem)
    {
        // 42
        Slot_board_Item parentt = FindOtherSlotBoard(slotboardItem);
        Vector2 dirboard = findanySlotBoardIteminboard.transform.position - slotItem.transform.position;
        Vector2 dir3 = findanySlotBoardIteminboard.transform.position - slotboardItem.transform.position;
        findanySlotBoardIteminboard.transform.SetParent(this.transform.parent);
        this.transform.SetParent(findanySlotBoardIteminboard.transform);
        float a = findanySlotBoardIteminboard.transform.rotation.eulerAngles.z + Vector2.Angle(dir3, dirboard);
        Debug.Log("hahahschienthang"+ this.transform.eulerAngles.z);
        if (this.transform.eulerAngles.z < 180)
        {
            if (this.transform.eulerAngles.z < 60f)
            {
                findanySlotBoardIteminboard.transform.rotation = Quaternion.Euler(0, 0, findanySlotBoardIteminboard.transform.rotation.eulerAngles.z + Vector2.Angle(dir3, dirboard));
            }
            else
            {
                findanySlotBoardIteminboard.transform.rotation = Quaternion.Euler(0, 0, findanySlotBoardIteminboard.transform.rotation.eulerAngles.z - Vector2.Angle(dir3, dirboard));
            }
        }
        else
        {
            if (this.transform.eulerAngles.z < 360f)
            {
                Debug.Log("ok hehehe");
                findanySlotBoardIteminboard.transform.rotation = Quaternion.Euler(0, 0, findanySlotBoardIteminboard.transform.rotation.eulerAngles.z + Vector2.Angle(dir3, dirboard));
            }
            else
            {
                findanySlotBoardIteminboard.transform.rotation = Quaternion.Euler(0, 0, findanySlotBoardIteminboard.transform.rotation.eulerAngles.z - Vector2.Angle(dir3, dirboard));
            }
        }
        this.transform.SetParent(findanySlotBoardIteminboard.transform.parent);
        findanySlotBoardIteminboard.transform.SetParent(transform);
    }
}
