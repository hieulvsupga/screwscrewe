using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bg_Item : MonoBehaviour, TInterface<Bg_Item>
{
    //pool
    private ObjectPool<Bg_Item> _pool;
    public void SetPool(ObjectPool<Bg_Item> pool)
    {
        _pool = pool;
    }
    public void ResetPool()
    {
        _pool.Release(this);
    }
    public Bg_Item IGetComponentHieu()
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
