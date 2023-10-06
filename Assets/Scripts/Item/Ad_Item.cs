using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Ad_Item : MonoBehaviour, TInterface<Ad_Item>
{
    //pool
    private ObjectPool<Ad_Item> _pool;
    public void SetPool(ObjectPool<Ad_Item> pool)
    {
        _pool = pool;
    }
    public void ResetPool()
    {
        _pool.Release(this);
    }

    public Ad_Item IGetComponentHieu()
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
