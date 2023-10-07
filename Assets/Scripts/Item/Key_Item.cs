using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using DG.Tweening;
public class Key_Item : MonoBehaviour, TInterface<Key_Item>
{
    //pool
    private ObjectPool<Key_Item> _pool;
    private bool checkRunning = false;
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
        checkRunning = false;
    }

    public void StartCreate()
    {

    }

    public void FindLock(){
        if(checkRunning == true || Controller.Instance.rootlevel.litslock.Count == 0) return;
        Lock_Item lock_Item = Controller.Instance.rootlevel.litslock[0];
        if(lock_Item == null){
            return;
        }
        checkRunning = true;
        Controller.Instance.rootlevel.listkey.Remove(this);
        Controller.Instance.rootlevel.litslock.Remove(lock_Item);
        transform.DOMove(lock_Item.transform.position,2.0f).OnComplete(()=>{
            lock_Item.ResetPool();
            this.ResetPool();
        });
    }
}
