using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Lock_Item : MonoBehaviour, TInterface<Lock_Item>
{
    public Animator animator;
    public SpriteRenderer spriteRenderer;
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
        spriteRenderer.transform.localPosition = Vector3.zero;
        Color currentColor = spriteRenderer.color;
        currentColor.a = 1f;
        spriteRenderer.color = currentColor;
    }

    public void StartCreate()
    {

    }

    public void ResetPool2()
    {
        ResetPool();
        Controller.Instance.rootlevel.litslock.Remove(this);
    }
}
