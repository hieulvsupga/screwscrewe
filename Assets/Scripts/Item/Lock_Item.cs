using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Lock_Item : MonoBehaviour, TInterface<Lock_Item>
{
    //pool
    private ObjectPool<Lock_Item> _pool;
    public void SetPool(ObjectPool<Lock_Item> pool)
    {
        _pool = pool;
    }
    public void ResetPool()
    {
        _pool.Release(this);
    }

    public Lock_Item IGetComponentHieu()
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
