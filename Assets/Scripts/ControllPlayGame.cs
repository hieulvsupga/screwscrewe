using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class ControllPlayGame : MonoBehaviour
{
    private static ControllPlayGame instance;
    public static ControllPlayGame Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ControllPlayGame>();
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }

    public Nail_Item targetNail;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("co an");
            Vector3 mousePositionBD = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 13);
            Vector2 mousePosition = Controller.Instance.cameraMain.ScreenToWorldPoint(mousePositionBD);       
            RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, Vector2.zero);
            foreach (RaycastHit2D hit in hits)
            {
                Debug.Log(hit.collider.name);
                if (hit.collider.CompareTag("Slot")){
                    Debug.Log("???????????????????????????????????????????");
                    hit.transform.GetComponent<Slot_Item>().ActiveWhenDown();
                }
            }
        }
    }
}
