using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScreenShotCamera : MonoBehaviour
{
    public bool captureScreenshot = false;
    public Camera cameraScreenshot;
    public Sprite spritescreenshot;
    // public void TakeScreenshot(){
    // //    captureScreenshot = true;
    // //    OnPostRender();
    // }
    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.S))
    //     {
    //         captureScreenshot = true;
    //         Debug.Log("dang an");
    //     }
    // }
    private void OnPostRender()
    { 
        if (captureScreenshot)
        {        
            cameraScreenshot.targetTexture = RenderTexture.GetTemporary(270,479,16);
            RenderTexture rendertexture = cameraScreenshot.targetTexture;
            Texture2D renderResult = new Texture2D(rendertexture.width, rendertexture.height, TextureFormat.ARGB32, false);
            // spritescreenshot = Utiliti.ConvertToSprite(renderResult);
            Rect rect = new Rect(0,0, rendertexture.width, rendertexture.height);
            renderResult.ReadPixels(rect,0,0);
            renderResult.Apply();
            spritescreenshot = Sprite.Create(renderResult, new Rect(0, 0, renderResult.width, renderResult.height), Vector2.one * 0.5f);
            // byte[] byteArray = renderResult.EncodeToPNG();
            // string h = Application.dataPath + "/came.png";
            // Debug.Log(h);
            // System.IO.File.WriteAllBytes(h,byteArray);
            RenderTexture.ReleaseTemporary(rendertexture);
            cameraScreenshot.targetTexture = null;

            for (int i = 0; i < Controller.Instance.rootlevel.listHint.Count; i++)
            {
                Controller.Instance.rootlevel.listHint[i].gameObject.SetActive(false);
            }   
            captureScreenshot = false;
            // Destroy(rendertexture);
        }
    }
}
