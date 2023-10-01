using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Nail_Item : MonoBehaviour, TInterface<Nail_Item>
{
    //pool
    private ObjectPool<Nail_Item> _pool;




    // public bool hasSlot = false;
    // Start is called before the first frame update
    public Nail nail;
    public Slot_Item slot_item;
    public Collider2D ColiderNail;
    public List<HingeJoint2D> listHingeJoin;
    private void Awake()
    {
        ColiderNail = GetComponent<Collider2D>();
    }
    //private void OnMouseDown()
    //{
    //    //Debug.Log("CHIEN than than than than than");
    //    if (transform.parent.CompareTag("Slot"))
    //    {
    //        transform.parent.GetComponent<Slot_Item>().ActiveWhenDown();
    //    }
    //}

    public void ResetDisactiveListHingeJoint()
    {
        for(int i=0; i<listHingeJoin.Count; i++)
        {
            listHingeJoin[i].enabled = false;
        }
    }



    public void CheckOverlapBoxBoard()
    {
        Collider2D[] results = new Collider2D[LevelController.Instance.rootlevel.listboard.Count];
        Bounds boundnail = ColiderNail.bounds;
        int flag = 0;
        int layercheck = 0;
        Vector2 size = boundnail.size;
        int numColliders = Physics2D.OverlapBoxNonAlloc(transform.position, size, 0, results, 1 << 6);
        for (int j = 0; j < numColliders; j++)
        {
            Bounds bounds1 = results[j].GetComponent<Collider2D>().bounds;

            if (bounds1.Intersects(boundnail))
            {
                Bounds overlapBounds = boundnail;
                overlapBounds.Encapsulate(bounds1.min);
                overlapBounds.Encapsulate(bounds1.max);
                float overlapArea = overlapBounds.size.x * overlapBounds.size.y;
                float overlapPercentage = (overlapArea / (bounds1.size.x * bounds1.size.y)) * 100f;
                if (overlapPercentage >= 90 && overlapPercentage <= 100)
                {
                    layercheck = results[j].gameObject.layer;
                    flag++;
                }
            }
        }
        if(flag == 0)
        {
            ColiderNail.isTrigger = false;
        }
        else if(flag >=2)
        {
            ColiderNail.isTrigger = true;
        }
        else
        {
            gameObject.layer = (17 + (layercheck - 6));
        }
    }

    public void SetPool(ObjectPool<Nail_Item> pool){
        _pool = pool;
    }

    public void ResetPool(){
        _pool.Release(this);
    }

    public Nail_Item IGetComponentHieu(){
        return this;
    }
}
