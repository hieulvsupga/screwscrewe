using Google.Play.Common.LoadingScreen;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadingBar : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(LoadAsyncOperation());
    }
    IEnumerator LoadAsyncOperation()
    {
        float elapsedTime = 0;
        while (elapsedTime < 3f)
        {
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        SceneManager.LoadSceneAsync("Level");
    }
}
