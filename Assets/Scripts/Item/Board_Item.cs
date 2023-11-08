using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.ResourceManagement.ResourceProviders.Simulation;

public class Board_Item : MonoBehaviour, TInterface<Board_Item>
{
    //pool
    private ObjectPool<Board_Item> _pool;

    public List<Slot_board_Item> listslot;
    public SpriteRenderer spritemain;
    public Rigidbody2D rb;
    public Board boardinfomation;
    public Vector3 rotationPre;

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

    public void NotRb()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.velocity = Vector3.zero; 
        rb.angularVelocity = 0; 
    }

    public void SetPool(ObjectPool<Board_Item> pool)
    {
        _pool = pool;
    }

    public void ResetPool()
    {
        //if(_pool == null)
        //{       
        //    return;
        //}
        _pool.Release(this);
    }

    public Board_Item IGetComponentHieu()
    {
        return this;
    }
    public void ResetAfterRelease()
    {
        NotRb();
    }

    public void StartCreate()
    {
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
        NotRb();
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

        for(int i = 0; i < listslot.Count; i++)
        {
            if(listslot[Index] == a)
            {
                break;
            }
        }

        //while (listslot[Index] == a && Index < listslot.Count)
        //{           
        //    Index++;
        //}       
        return listslot[Index];
    }

    public virtual void DetermineCenterPoint(Slot_board_Item a){

    }
    public bool CompareVectors()
    {
        if (Mathf.Abs(transform.rotation.eulerAngles.x - boardinfomation.rot.x) <=0.1f && Mathf.Abs(transform.rotation.eulerAngles.y - boardinfomation.rot.y) <= 0.1f && Mathf.Abs(transform.rotation.eulerAngles.z - boardinfomation.rot.z) <= 0.1f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

     public virtual void AutoRotate(Slot_board_Item slotboardItem, Slot_board_Item findanySlotBoardIteminboard, Slot_Item slotItem)
     {
        Slot_board_Item parentt = FindOtherSlotBoard(slotboardItem);
        Vector2 dirboard = findanySlotBoardIteminboard.transform.position - slotItem.transform.position;
        Vector2 dir3 = findanySlotBoardIteminboard.transform.position - slotboardItem.transform.position;
        findanySlotBoardIteminboard.transform.SetParent(this.transform.parent);
        this.transform.SetParent(findanySlotBoardIteminboard.transform);
        float a = findanySlotBoardIteminboard.transform.rotation.eulerAngles.z + Vector2.Angle(dir3, dirboard);     
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
            if (this.transform.eulerAngles.z < 300f)
            {
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
