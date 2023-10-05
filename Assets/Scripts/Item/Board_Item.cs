using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Board_Item : MonoBehaviour, TInterface<Board_Item>
{
    //pool
    private ObjectPool<Board_Item> _pool;

    public List<Slot_board_Item> listslot;
    public SpriteRenderer spritemain;
    public Rigidbody2D rb;
    // Start is called before the first frame update

    private void Awake()
    {
        spritemain = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    //void Start()
    //{

    //    //if(mask != null)
    //    //{
    //    //    mask.frontSortingLayerID = spritemain.sortingLayerID + 1;
    //    //}      
    //}

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetLayer()
    {
        return spritemain.sortingLayerID;
    }

    public void AddSlotforBoard(Slot_board_Item slot_board)
    {   
        if(slot_board != null)
        {
            slot_board.mask.frontSortingOrder = spritemain.sortingLayerID + 11;
        }
        listslot.Add(slot_board);
    }


    public void SetupRb()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    public void SetPool(ObjectPool<Board_Item> pool)
    {
        _pool = pool;
    }

    public void ResetPool()
    {
        if(_pool == null)
        {
            Debug.Log("Faaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            return;
        }
        _pool.Release(this);
    }

    public Board_Item IGetComponentHieu()
    {
        return this;
    }
    public void ResetAfterRelease()
    {   
        rb.bodyType = RigidbodyType2D.Kinematic;   
        HingeJoint2D[] hingeJoints = gameObject.GetComponents<HingeJoint2D>();

        foreach (HingeJoint2D hingeJoint in hingeJoints)
        {
            Destroy(hingeJoint);
        }

        foreach (Slot_board_Item slot_board_item in listslot)
        {
            slot_board_item.ResetPool();
        }
    }

    public void StartCreate()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
    }
}
