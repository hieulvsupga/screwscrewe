using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SocialPlatforms.Impl;
using System;
public class Nail_Item : MonoBehaviour, TInterface<Nail_Item>
{
    //pool
    private ObjectPool<Nail_Item> _pool;
    public SpriteRenderer spriteRenderer;
    public Sprite[] spriteRange;
    public Nail nail;
    public Slot_Item slot_item;
    public Collider2D ColiderNail;
    public List<HingeJoint2D> listHingeJoin;
    private Board_Item boardItemBeforeandAfter;
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
        for (int i = 0; i < listHingeJoin.Count; i++)
        {
            if (listHingeJoin[i]!=null)
            {
                listHingeJoin[i].enabled = false;
            }         
        }
        listHingeJoin.Clear();
    }



    public void CheckOverlapBoxBoard(Slot_Item slotItem, Nail_Item nailtargetbefore)
    {
        StartCoroutine(checkover(slotItem, nailtargetbefore));
    }
    IEnumerator checkover(Slot_Item slotItem, Nail_Item nailtargetbefore)
    {
        yield return new WaitForSeconds(0f);
        try
        {
            Bounds boundnail = ColiderNail.bounds;
            Vector2 size = boundnail.size;
            Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, size, 0);
            List<int> layerboard = new List<int>();
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject.layer == 29)
                {

                    Board_Item boarditem = collider.transform.parent.GetComponent<Board_Item>();
                    Slot_board_Item slotboardItem = collider.GetComponent<Slot_board_Item>();
                    Slot_board_Item findanySlotBoardIteminboard = CheckHingleInBoard(boarditem);
                    if (findanySlotBoardIteminboard == null)
                    {
                        Vector3 positionchange = slotItem.transform.position - slotboardItem.transform.position;
                        boarditem.transform.position += positionchange;
                        listHingeJoin.Add(slotboardItem.hingeJointInSlot);
                        slotboardItem.hingeJointInSlot.enabled = true;
                        layerboard.Add(collider.transform.parent.gameObject.layer - 6);
                    }
                    else
                    {

                        listHingeJoin.Add(slotboardItem.hingeJointInSlot);
                        slotboardItem.hingeJointInSlot.enabled = true;
                        layerboard.Add(collider.transform.parent.gameObject.layer - 6);
                        //dich chuyen doi tuong
                        boarditem.AutoRotate(slotboardItem, findanySlotBoardIteminboard, slotItem);
                    }
                }
            }
            gameObject.layer = Controller.Instance.nailLayerController.InputNumber(layerboard);
            ColiderNail.isTrigger = false;
            nailtargetbefore.testKinematicBoardParent();
        }
        catch (Exception e)
        {      
            Debug.LogException(e);
            LevelController.Instance.ResetLevel();         
        }
    }

    //kiem tra co higlejoint nao trong board con hoat dong khong
    public Slot_board_Item CheckHingleInBoard(Board_Item board_item)
    {        
            for (int i = 0; i < board_item.listslot.Count; i++)
            {
                if (board_item.listslot[i].hingeJointInSlot.enabled)
                {
                    return board_item.listslot[i];
                }
            }
            return null;      
    }

    float CalculateOverlapPercentage(Collider2D colliderA, Collider2D colliderB)
    {
        Bounds boundsA = colliderA.bounds;
        Bounds boundsB = colliderB.bounds;

        float intersectionWidth = Mathf.Min(boundsA.max.x, boundsB.max.x) - Mathf.Max(boundsA.min.x, boundsB.min.x);
        float intersectionHeight = Mathf.Min(boundsA.max.y, boundsB.max.y) - Mathf.Max(boundsA.min.y, boundsB.min.y);

        float intersectionArea = Mathf.Max(0, intersectionWidth) * Mathf.Max(0, intersectionHeight);
        float areaA = boundsA.size.x * boundsA.size.y;

        float overlapPercentage = intersectionArea / areaA * 100f;
        return overlapPercentage;
    }
    public void SetPool(ObjectPool<Nail_Item> pool) {
        _pool = pool;
    }

    public void ResetPool() {
        _pool.Release(this);
    }

    public Nail_Item IGetComponentHieu() {
        return this;
    }

    public void ResetAfterRelease()
    {
        listHingeJoin.Clear();
        ColiderNail.isTrigger = false;
        spriteRenderer.sprite = spriteRange[0];
        spriteRenderer.transform.localPosition = new Vector3(0, 0, 0);
    }

    public void StartCreate()
    { 
    } 

    public void ActiveImageNail()
    {
        AudioController.Instance.PlayClip("naildam");
        spriteRenderer.sprite = spriteRange[1];
        spriteRenderer.transform.localPosition = new Vector3(0,0.2f,0);
    }
    public void ResetImageNailWithParticle()
    {
        AudioController.Instance.PlayClip("naildam");
        ParticleNailItem particleNailItem = ParticleNailSpawner.Instance._pool.Get();
        particleNailItem.transform.position = transform.position;
        ResetImageNail();
    }
    public void ResetImageNail()
    {
        AudioController.Instance.PlayClip("naildam");
        spriteRenderer.sprite = spriteRange[0];
        spriteRenderer.transform.localPosition = new Vector3(0,0,0);
    }

    // dùng khi bắt đầu lựa chọn đinh, đổi board cha sang kinematic để tránh tạo tương tác vật lý không mong muốn
    public void KinematicBoardParent()
    {
        if (boardItemBeforeandAfter != null)
        {
            boardItemBeforeandAfter.rb.isKinematic = true;
            return;
        }
        if (listHingeJoin.Count == 0) return;
        HingeJoint2D hingle = listHingeJoin[0];
        boardItemBeforeandAfter = hingle.transform.GetComponent<Board_Item>();
        boardItemBeforeandAfter.rb.isKinematic = true;
    }

    public void testKinematicBoardParent()
    {
        if (boardItemBeforeandAfter != null)
        {
            
            boardItemBeforeandAfter.rb.isKinematic = false;
            boardItemBeforeandAfter = null;
            return;
        }
    }
}
