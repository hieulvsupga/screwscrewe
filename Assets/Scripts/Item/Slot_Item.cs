using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Pool;

public class Slot_Item : MonoBehaviour, TInterface<Slot_Item>
{
    //pool
    private ObjectPool<Slot_Item> _pool;

    public bool hasNail;
    public bool hasLock;
    public Nail_Item nail_item;

    public Collider2D mainCheckCollider;
    // Start is called before the first frame update
    //private void OnMouseDown()
    //{
    //    ActiveWhenDown();
    //}


    public void ActiveWhenDown()
    {
        if (ControllPlayGame.Instance.targetNail == nail_item)
        {
            if(ControllPlayGame.Instance.targetNail != null)
            {
                ControllPlayGame.Instance.targetNail.ResetImageNail();
                ControllPlayGame.Instance.targetNail = null;
            }
            return;
        }

        if (ControllPlayGame.Instance.targetNail != null && hasNail == false)
        {
            //ControllPlayGame.Instance.targetNail.ColiderNail.isTrigger = true;
            //ControllPlayGame.Instance.targetNail.transform.position = transform.position;
            //ControllPlayGame.Instance.targetNail.ResetDisactiveListHingeJoint();
            //ControllPlayGame.Instance.targetNail.slot_item.ResetNail();
            //SetUpNail(ControllPlayGame.Instance.targetNail);
            //ControllPlayGame.Instance.targetNail.CheckOverlapBoxBoard();
            //ControllPlayGame.Instance.targetNail = null;
            StartCoroutine(Checkboardinslot());
        }
        else
        {
            
            if (nail_item != null && hasNail == true)
            {
                if (ControllPlayGame.Instance.targetNail != null)
                {
                    ControllPlayGame.Instance.targetNail.ResetImageNail();
                }
                ControllPlayGame.Instance.targetNail = nail_item;
                nail_item.ActiveImageNail();
            }
        }
    }

    IEnumerator Checkboardinslot()
    {
        yield return new WaitForSeconds(0);
        Vector2 size = mainCheckCollider.bounds.size;
        bool check = false;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(mainCheckCollider.transform.position, size, 0);
        foreach (Collider2D collider in colliders)
        {         
            if (collider.CompareTag("Board"))
            {                
                check = true;
                break;
            }
        }
        if (check == true)
        {
            Debug.Log("dA CHAM VAO ROI HEHEHEHE");
        }
        else
        {
            ControllPlayGame.Instance.targetNail.ResetImageNail();
            ControllPlayGame.Instance.targetNail.ColiderNail.isTrigger = true;
            ControllPlayGame.Instance.targetNail.transform.position = transform.position;
            ControllPlayGame.Instance.targetNail.ResetDisactiveListHingeJoint();
            ControllPlayGame.Instance.targetNail.slot_item.ResetNail();
            SetUpNail(ControllPlayGame.Instance.targetNail);
            ControllPlayGame.Instance.targetNail.CheckOverlapBoxBoard();
            ControllPlayGame.Instance.targetNail = null;
        }
    }
    float CalculateOverlapPercentage(Collider2D colliderA, Collider2D colliderB)
    {
        Bounds boundsA = colliderA.bounds;
        Bounds boundsB = colliderB.bounds;

        float intersectionWidth = Mathf.Min(boundsA.max.x, boundsB.max.x) - Mathf.Max(boundsA.min.x, boundsB.min.x);
        float intersectionHeight = Mathf.Min(boundsA.max.y, boundsB.max.y) - Mathf.Max(boundsA.min.y, boundsB.min.y);

        float intersectionArea = Mathf.Max(0, intersectionWidth) * Mathf.Max(0, intersectionHeight);
        float totalArea = boundsA.size.x * boundsA.size.y + boundsB.size.x * boundsB.size.y;

        float overlapPercentage = intersectionArea / totalArea * 100f;
        Debug.Log("hehehe" + overlapPercentage);
        return overlapPercentage;
    }

    public void ResetNail()
    {
        hasNail = false;
        nail_item = null;
    }

    public void SetUpNail(Nail_Item nail_Item2)
    {
        hasNail = true;
        nail_item = nail_Item2;
        nail_Item2.transform.parent = transform;
        nail_Item2.slot_item = this;
    }


    public void SetPool(ObjectPool<Slot_Item> pool)
    {
        _pool = pool;
    }

    public void ResetPool()
    {
        _pool.Release(this);
    }

    public Slot_Item IGetComponentHieu()
    {
        return this;
    }

    public void ResetAfterRelease()
    {
        nail_item = null;
    }

    public void StartCreate()
    {
    }

}
