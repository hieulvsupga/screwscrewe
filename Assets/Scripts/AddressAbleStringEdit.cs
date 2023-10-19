using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class AddressAbleStringEdit
{
   public static string URLAddress(string str)
   {
        string url = null;
        switch (str)
        {
            case "locked":
                url = "Assets/Prefabs/Locked.prefab";
                break;
            case "nail_particle":
                url = "Assets/Particle/Flash_magic_ellow_blue.prefab";
                break;
            case "board_tam_giac":
                url = "Assets/Prefabs/ItemInMap/board_tam_giac.prefab";
                break;
            case "board_U":
                url = "Assets/Prefabs/ItemInMap/board_U.prefab";
                break;
            case "slot_board":
                url = "Assets/Prefabs/ItemInMap/slotinboard.prefab";
                break;
            case "board_400x175":
                url = "Assets/Prefabs/ItemInMap/board_400x175.prefab";
                break;
            case "board_700x100":
                url = "Assets/Prefabs/ItemInMap/board_700x100.prefab";
                break;
            case "board_W":
                url = "Assets/Prefabs/ItemInMap/board_W.prefab";
                break;
            case "board_L":
                url = "Assets/Prefabs/ItemInMap/board_L.prefab";
                break;
            case "board_400x400":
                url = "Assets/Prefabs/ItemInMap/400x400.prefab";
                break;
            case "ad":
                url = "Assets/Prefabs/ItemInMap/ad.prefab";
                break;
            case "board_400x100":
                url = "Assets/Prefabs/ItemInMap/400x100.prefab";
                break;
            case "board_150x150":
                url = "Assets/Prefabs/ItemInMap/150x150.prefab";
                break;
            case "board_550x100":
                url = "Assets/Prefabs/ItemInMap/550x100.prefab";
                break;
            case "lock":
                url = "Assets/Prefabs/ItemInMap/lock.prefab";
                break;
            case "key":
                url = "Assets/Prefabs/ItemInMap/key.prefab";
                break;
            case "board_275x100":
                url = "Assets/Prefabs/ItemInMap/275x100.prefab";
                break;
            case "bomb":
                url = "Assets/Prefabs/ItemInMap/bomb.prefab";
                break;
            case "slot":
                url = "Assets/Prefabs/ItemInMap/slot.prefab";
                break;
            case "nail":
                url = "nail";
                break;
            case "bg":
                url = "Assets/Prefabs/ItemInMap/bg.prefab";
                //url = "Assets/Prefabs/Sphere.prefab";
                break;
            case "txt":
                url = "Assets/Prefabs/ItemInMap/text.prefab";
                break;
            case "hint":
                url = "Assets/Prefabs/ItemInMap/hint.prefab";
                break;
        }
        return url;
   }
}
