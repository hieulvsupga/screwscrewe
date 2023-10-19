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
            slot_board.mask.frontSortingOrder = spritemain.sortingOrder;
            slot_board.mask.backSortingOrder = spritemain.sortingOrder-1;
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
        listslot.Clear();
    }

    public void StartCreate()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    public void WhenTriggerandBoardCheck()
    {
        Controller.Instance.rootlevel.listboard.Remove(this);
        ResetPool();
        if(Controller.Instance.rootlevel.listboard.Count == 0)
        {
            AudioController.Instance.PlayClip("win");
            UIEvents.Instance.ShowWinUI();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == 31){
            Key_Item key_Item = other.transform.GetComponent<Key_Item>();
            key_Item.FindLock();
        }
    }

    public Slot_board_Item FindOtherSlotBoard(Slot_board_Item a){
    
        int Index = 0;
        while (listslot[Index] == a && Index < listslot.Count)
        {           
            Index++;
        }       
        return listslot[Index];
    }

    public virtual void DetermineCenterPoint(Slot_board_Item a){

    }
}
