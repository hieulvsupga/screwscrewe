using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Slot_board_Item : MonoBehaviour, TInterface<Slot_board_Item>
{
    private ObjectPool<Slot_board_Item> _pool;
    public SpriteMask mask;
    public HingeJoint2D hingeJointInSlot;
    public SpriteRenderer spriteBorder;
    public void SetPool(ObjectPool<Slot_board_Item> pool)
    {
        _pool = pool;
    }

    public void ResetPool()
    {
        _pool.Release(this);
    }

    public Slot_board_Item IGetComponentHieu()
    {
        return this;
    }

    public void ResetAfterRelease()
    {       
        hingeJointInSlot = null;
    }

    public void StartCreate()
    {
    }
}
