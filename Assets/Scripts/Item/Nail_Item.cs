using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SocialPlatforms.Impl;

public class Nail_Item : MonoBehaviour, TInterface<Nail_Item>
{
    //pool
    private ObjectPool<Nail_Item> _pool;
    public SpriteRenderer spriteRenderer;
    public Sprite[] spriteRange;




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
        for (int i = 0; i < listHingeJoin.Count; i++)
        {
            if (listHingeJoin[i]!=null)
            {
                listHingeJoin[i].enabled = false;
            }         
        }
        listHingeJoin.Clear();
    }



    public void CheckOverlapBoxBoard(Slot_Item slotItem)
    {
        StartCoroutine(checkover(slotItem));
    }
    IEnumerator checkover(Slot_Item slotItem)
    {
        yield return new WaitForSeconds(0f);
        Bounds boundnail = ColiderNail.bounds;
        Vector2 size = boundnail.size;
       // Debug.Log(size + "=VLOVK");
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, size, 0);
        List<int> layerboard = new List<int>();
        foreach (Collider2D collider in colliders)
        {
            // Bounds bounds1 = collider.bounds;
            // if (bounds1.Intersects(boundnail) && collider.CompareTag("Board"))
            // {
            //     //CalculateOverlapPercentage(ColiderNail, collider);
            //     boundnail.Encapsulate(bounds1.min);
            //     boundnail.Encapsulate(bounds1.max);
            //     float overlapArea = boundnail.size.x * boundnail.size.y;
            //     float overlapPercentage = (overlapArea / (bounds1.size.x * bounds1.size.y)) * 100f;
            //     if (overlapPercentage >= 90 && overlapPercentage <= 100.5f)
            //     {
            //         layerboard.Add(collider.gameObject.layer - 6);

            //     }
            //    // Debug.Log(overlapPercentage + "OKKAHE");

            // }
            if(collider.gameObject.layer == 29){

                Board_Item boarditem = collider.transform.parent.GetComponent<Board_Item>();
                Slot_board_Item slotboardItem = collider.GetComponent<Slot_board_Item>();

                Slot_board_Item findanySlotBoardIteminboard = CheckHingleInBoard(boarditem);
                if (findanySlotBoardIteminboard==null)
                {                                   
                    Vector3 positionchange = slotItem.transform.position - slotboardItem.transform.position;
                    boarditem.transform.position += positionchange;

                    listHingeJoin.Add(slotboardItem.hingeJointInSlot);
                    slotboardItem.hingeJointInSlot.enabled = true;
                    layerboard.Add(collider.transform.parent.gameObject.layer - 6);
                    //dich chuyen doi tuong
                    Board_Item parenttBoard = slotboardItem.transform.parent.GetComponent<Board_Item>();
                    Slot_board_Item parentt = parenttBoard.FindOtherSlotBoard(slotboardItem);
                    Vector3 dir = parentt.transform.position - this.transform.position;
                    Vector3 dir2 = parentt.transform.position - slotboardItem.transform.position;
                    // Đọc các giá trị rotation x, y, z của đối tượng A
                    float rotationX = parenttBoard.transform.eulerAngles.x;
                    float rotationY = parenttBoard.transform.eulerAngles.y;
                    float rotationZ = parenttBoard.transform.eulerAngles.z;
                    parenttBoard.DetermineCenterPoint(parentt);
                }
                else
                {

                    listHingeJoin.Add(slotboardItem.hingeJointInSlot);
                    slotboardItem.hingeJointInSlot.enabled = true;
                    layerboard.Add(collider.transform.parent.gameObject.layer - 6);
                    //dich chuyen doi tuong
                    Board_Item parenttBoard = slotboardItem.transform.parent.GetComponent<Board_Item>();
                    Slot_board_Item parentt = parenttBoard.FindOtherSlotBoard(slotboardItem);

                    //chuyen dinh thong minh



                    //Vector3 dir = parentt.transform.position - this.transform.position;
                    //Vector3 dir2 = parentt.transform.position - slotboardItem.transform.position;
                    //// Đọc các giá trị rotation x, y, z của đối tượng A
                    //float rotationX = parenttBoard.transform.eulerAngles.x;
                    //float rotationY = parenttBoard.transform.eulerAngles.y;
                    //float rotationZ = parenttBoard.transform.eulerAngles.z;
                    //parenttBoard.DetermineCenterPoint(parentt);
                    //chinh goc quay ne
                    Vector2 dirboard = findanySlotBoardIteminboard.transform.position - slotItem.transform.position;
                    //Vector2 dirboard = boarditem.transform.up;
                    Vector2 dir3 =  findanySlotBoardIteminboard.transform.position - slotboardItem.transform.position;
                   // Debug.Log(slotItem.transform.position.x + "eeeee" + slotboardItem.transform.position.x);
                    //Debug.Log("hehfahwklejfalwejflakwejfalkwe" + Vector2.Angle(dir3, dirboard));
                    findanySlotBoardIteminboard.transform.SetParent(boarditem.transform.parent);
                    boarditem.transform.SetParent(findanySlotBoardIteminboard.transform);
                    //Debug.Log(findanySlotBoardIteminboard.transform.rotation.eulerAngles.z + "hfnajknejfkawehfjakwehfjakwehfajwkehf");
                    //findanySlotBoardIteminboard.transform.rotation.eulerAngles.z + Vector2.Angle(dir3, dirboard)
                    //if(slotItem.transform.position.x >= slotboardItem.transform.position.x)
                    //{
                    //    findanySlotBoardIteminboard.transform.rotation = Quaternion.Euler(0, 0, findanySlotBoardIteminboard.transform.rotation.eulerAngles.z - Vector2.Angle(dir3, dirboard));
                    //}
                    //else
                    //{
                    //    findanySlotBoardIteminboard.transform.rotation = Quaternion.Euler(0, 0, findanySlotBoardIteminboard.transform.rotation.eulerAngles.z + Vector2.Angle(dir3, dirboard));
                    //}


                    float a = findanySlotBoardIteminboard.transform.rotation.eulerAngles.z + Vector2.Angle(dir3, dirboard);
                   // Debug.Log(boarditem.transform.localEulerAngles.z);
                    if(boarditem.transform.localEulerAngles.z >= 0 && boarditem.transform.localEulerAngles.z<90)
                    {
                        findanySlotBoardIteminboard.transform.rotation = Quaternion.Euler(0, 0, findanySlotBoardIteminboard.transform.rotation.eulerAngles.z + Vector2.Angle(dir3, dirboard));
                    }
                    else
                    {
                        findanySlotBoardIteminboard.transform.rotation = Quaternion.Euler(0, 0, findanySlotBoardIteminboard.transform.rotation.eulerAngles.z - Vector2.Angle(dir3, dirboard));
                    }
                    boarditem.transform.SetParent(findanySlotBoardIteminboard.transform.parent);
                    findanySlotBoardIteminboard.transform.SetParent(boarditem.transform);
                    //Debug.Log(Vector2.Angle(dir3, dirboard)+"Ffff");
                    //Debug.Log(dir3+"Ffe3f");
                    //Debug.Log(dirboard + "Ffe3f");
                }



            }

           


                // Sử dụng các giá trị rotation
                //Debug.Log("Rotation X: " + rotationX);
                //Debug.Log("Rotation Y: " + rotationY);
                //Debug.Log("Rotation Z: " + rotationZ);
                //Quaternion targetRotation = Quaternion.FromToRotation(dir2, dir);

                // Gán targetRotation vào rotation của đối tượng
                // parenttBoard.transform.rotation = targetRotation;
           // }
        }

        //Debug.Log("CHUYEN DOI" + string.Join(", ", layerboard));
        gameObject.layer = Controller.Instance.nailLayerController.InputNumber(layerboard);
        ColiderNail.isTrigger = false;
    }

    //kiem tra co higlejoint nao trong board con hoat dong khong
    public Slot_board_Item CheckHingleInBoard(Board_Item board_item)
    {
        for(int i=0; i< board_item.listslot.Count; i++)
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
}
