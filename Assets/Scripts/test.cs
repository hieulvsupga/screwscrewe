using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class test : MonoBehaviour
{
    public Sprite a;
    // Start is called before the first frame update
    void Start()
    {
        AsyncOperationHandle<Sprite> asyncOperationHandle =  Addressables.LoadAssetAsync<Sprite>("Assets/Sprites/SpriteAsset/dinh_1.asset");
        asyncOperationHandle.Completed += AsyncOperationHandle_Completed;
        //Instantiate(hoard, new Vector2(other.transform.position.x, other.transform.position.y), Quaternion.identity);
    }


    private void AsyncOperationHandle_Completed(AsyncOperationHandle<Sprite> asyncOperationHandle)
    {
        if(asyncOperationHandle.Status == AsyncOperationStatus.Succeeded) {

            a = asyncOperationHandle.Result;
            Debug.Log("hfasflkwaehfklawejfalkwejfklawjeflkawjeflawjfelawjeflaw");
        }
        else
        {

        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
