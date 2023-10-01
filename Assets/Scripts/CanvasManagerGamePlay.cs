using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CanvasManagerGamePlay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Test(){
        Debug.Log("Da an");
        for (int i=0; i < LevelController.Instance.rootlevel.litsnail.Count; i++)
        {           
            LevelController.Instance.rootlevel.litsnail[i].ResetPool();
            
        }
        LevelController.Instance.rootlevel.litsnail.Clear();
        // Nail_Item nail_Item = Controller.Instance.nailSpawner._pool_nail_item.Get();
        // Nail_Item nail_Item3 = Controller.Instance.nailSpawner._pool_nail_item.Get();
        // Nail_Item nail_Item4 = Controller.Instance.nailSpawner._pool_nail_item.Get();
        SceneManager.LoadScene("Level");
    }

    public void Test2(){
          Nail_Item nail_Item = Controller.Instance.nailSpawner._pool.Get();
        Nail_Item nail_Item3 = Controller.Instance.nailSpawner._pool.Get();
        Nail_Item nail_Item4 = Controller.Instance.nailSpawner._pool.Get();
    }
}
