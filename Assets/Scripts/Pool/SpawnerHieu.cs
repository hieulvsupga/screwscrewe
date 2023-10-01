using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;

public class SpawnerHieu<T,U,X> : MonoBehaviour where T : MonoBehaviour where U : MonoBehaviour,TInterface<X> where X : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }
    public ObjectPool<X> _pool;
    public X _poolItemPrefab;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this.GetComponent<T>();
            DontDestroyOnLoad(this);
            AsyncOperationHandle<GameObject> asyncOperationHandle = Addressables.LoadAssetAsync<GameObject>(AddressAbleStringEdit.URLAddress("nail"));
            asyncOperationHandle.Completed += (handle) =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    Debug.Log("?????????????????1");
                    Controller.Instance.LoadDataIndex++;
                    _poolItemPrefab = handle.Result.GetComponent<X>();
                }
            };
            Debug.Log("?????????????????2");
            _pool = new ObjectPool<X>(CreatePoolItem, OnTakePoolItemFromPool, OnReturnPoolItemToPool, OnDestroyPoolItem, true, 1000, 2000);
        }
        else
        {
            Destroy(this);
        }
    }


    private X CreatePoolItem()
    {
        X pool_Item = null;
        if (_poolItemPrefab != null)
        {
            Debug.Log("tao moi pool");
            U u = Instantiate(_poolItemPrefab).GetComponent<U>();
            pool_Item = u.IGetComponentHieu();
            u.SetPool(_pool);
        }
        return pool_Item;
    }

    private void OnTakePoolItemFromPool(X pool_Item)
    {
        if (pool_Item == null)
        {
           return;
        }
        pool_Item.gameObject.SetActive(true);
    }

    private void OnReturnPoolItemToPool(X pool_Item)
    {
        if (pool_Item == null) return;
        pool_Item.transform.parent = Controller.Instance.transform;
        pool_Item.gameObject.SetActive(false);
        //LevelController.Instance.rootlevel.litsnail.Remove(nail_Item);
    }

    private void OnDestroyPoolItem(X pool_Item)
    {
        if (pool_Item == null) return;    
        Destroy(pool_Item.gameObject);
    }
}
