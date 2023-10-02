using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Slot_Item : MonoBehaviour, TInterface<Slot_Item>
{
    //pool
    private ObjectPool<Slot_Item> _pool;

    public bool hasNail;
    public bool hasLock;
    public Nail_Item nail_item;
    // Start is called before the first frame update
    //private void OnMouseDown()
    //{
    //    ActiveWhenDown();
    //}
    public void ActiveWhenDown()
    {
        if (ControllPlayGame.Instance.targetNail == nail_item)
        {          
            return;
        }

        if (ControllPlayGame.Instance.targetNail != null && hasNail == false)
        {
            ControllPlayGame.Instance.targetNail.ColiderNail.isTrigger = true;
            ControllPlayGame.Instance.targetNail.transform.position = transform.position;
            ControllPlayGame.Instance.targetNail.ResetDisactiveListHingeJoint();
            ControllPlayGame.Instance.targetNail.slot_item.ResetNail();
            ControllPlayGame.Instance.targetNail.CheckOverlapBoxBoard();

            SetUpNail(ControllPlayGame.Instance.targetNail);
            ControllPlayGame.Instance.targetNail = null;
        }
        else
        {
            
            if (nail_item != null && hasNail == true)
            {
                ControllPlayGame.Instance.targetNail = nail_item;
            }
        }
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
    }

    public void StartCreate()
    {
    }

}
