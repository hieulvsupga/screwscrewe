using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Key_Item : MonoBehaviour, TInterface<Key_Item>
{
    //pool
    private ObjectPool<Key_Item> _pool;
    public void SetPool(ObjectPool<Key_Item> pool)
    {
        _pool = pool;
    }

    public void ResetPool()
    {
        _pool.Release(this);
    }

    public Key_Item IGetComponentHieu()
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
