using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NailLayerController : MonoBehaviour
{
    public Dictionary<string, int> keyValuePairs = new Dictionary<string, int>();
    private void Start() {
       Debug.Log(InputNumber(new int[] { 1, 2, 3 }));
    }
    public int InputNumber(int[] inputs){
        char[] numberArray = "xxxxxxxx".ToCharArray();
        foreach(int input in inputs){
            numberArray[input] = 'y';
        }     
        string numberString = string.Join("", numberArray);
        if(keyValuePairs.ContainsKey(numberString)){
            return keyValuePairs[numberString];
        }else{
            int m = keyValuePairs.Count +1;
            keyValuePairs[numberString] = m;
            ChangeLayer(inputs,m);
            return m;
        }
    }

    public void ChangeLayer(int[] inputs,int layer){
        foreach(int input in inputs){
            Debug.Log("co chay");
            Physics2D.IgnoreLayerCollision(16+layer, 6 + input, true);   
        }   
    }
}
