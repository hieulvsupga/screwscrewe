using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NailLayerController : MonoBehaviour
{
    public Dictionary<string, int> keyValuePairs = new Dictionary<string, int>();
    //private void Start()
    //{
    //    //Debug.Log(InputNumber(new int[] { 1, 2, 3 }));
    //    ResetLayer(17);
    //}
    public int InputNumber(List<int> inputs){
        char[] numberArray = "xxxxxxxx".ToCharArray();
        foreach(int input in inputs){
            numberArray[input] = 'y';
        }     

        string numberString = string.Join("", numberArray);
        if(keyValuePairs.ContainsKey(numberString)){          
            return keyValuePairs[numberString];
        }else{
            int m = keyValuePairs.Count + 17;
            keyValuePairs[numberString] = m;
            ChangeLayer(inputs,m);            
            return m;
        }
    }

    private void ChangeLayer(List<int> inputs, int layer){
        ResetLayer(layer);
        foreach (int input in inputs){        
            Physics2D.IgnoreLayerCollision(layer, 6 + input, true);   
        }   
    }

    public void ResetLayer(int layer)
    {
        int m = 6;
        while(m <= 12)
        {
            Physics2D.IgnoreLayerCollision(layer, m, false);
            m++;
        }
    }
    public void ClearLayer()
    {
        keyValuePairs.Clear();
    }
}
