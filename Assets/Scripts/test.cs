using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class test : MonoBehaviour
{
    //public Sprite a;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    AsyncOperationHandle<Sprite> asyncOperationHandle =  Addressables.LoadAssetAsync<Sprite>("Assets/Sprites/SpriteAsset/dinh_1.asset");
    //    asyncOperationHandle.Completed += AsyncOperationHandle_Completed;
    //    //Instantiate(hoard, new Vector2(other.transform.position.x, other.transform.position.y), Quaternion.identity);
    //}


    //private void AsyncOperationHandle_Completed(AsyncOperationHandle<Sprite> asyncOperationHandle)
    //{
    //    if(asyncOperationHandle.Status == AsyncOperationStatus.Succeeded) {

    //        a = asyncOperationHandle.Result;
    //        Debug.Log("hfasflkwaehfklawejfalkwejfklawjeflkawjeflawjfelawjeflaw");
    //    }
    //    else
    //    {

    //    }
    //}


    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.K)) // Kiểm tra nút chuột trái được nhấn
        //{       
        //    for (int i = 0; i < Controller.Instance.rootlevel.litsnail.Count; i++)
        //    {
               
        //        Bounds boundnail = Controller.Instance.rootlevel.litsnail[i].ColiderNail.bounds;
        //        Vector2 size = boundnail.size;
        //        List<int> layerboard = new List<int>();
        //        Collider2D[] colliders = Physics2D.OverlapBoxAll(Controller.Instance.rootlevel.litsnail[i].transform.position, size, 0);

        //        foreach (Collider2D collider in colliders)
        //        {
        //            Bounds bounds1 = collider.bounds;
        //            Debug.Log(bounds1.Intersects(boundnail).ToString() + "h00000" + (collider.name));
        //            if (bounds1.Intersects(boundnail) && collider.CompareTag("Board"))
        //            {
        //                Bounds overlapBounds = boundnail;
        //                overlapBounds.Encapsulate(bounds1.min);
        //                overlapBounds.Encapsulate(bounds1.max);
        //                float overlapArea = overlapBounds.size.x * overlapBounds.size.y;
        //                float overlapPercentage = (overlapArea / (bounds1.size.x * bounds1.size.y)) * 100f;
        //                if (overlapPercentage >= 90 && overlapPercentage <= 100.5f)
        //                {

                          
        //                }
        //            }
        //        }                
        //    }
        //}
        //if(Input.GetKeyDown(KeyCode.E)) {

        //   // ControllPlayGame.Instance.targetNail.CheckOverlapBoxBoard();
        //}
    }
}
