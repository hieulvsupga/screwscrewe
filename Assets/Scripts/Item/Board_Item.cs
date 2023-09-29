using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board_Item : MonoBehaviour
{
    public List<SpriteMask> listslot;
    public SpriteRenderer spritemain;
    private Rigidbody2D rb;
    // Start is called before the first frame update

    private void Awake()
    {
        spritemain = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {

        //if(mask != null)
        //{
        //    mask.frontSortingLayerID = spritemain.sortingLayerID + 1;
        //}      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetLayer()
    {
        return spritemain.sortingLayerID;
    }

    public void AddSlotforBoard(GameObject a)
    {
        SpriteMask mask = a.GetComponent<SpriteMask>();    
        if(mask != null)
        {
            mask.frontSortingOrder = spritemain.sortingLayerID + 11;
        }
        listslot.Add(mask);
    }


    public void SetupRb()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
