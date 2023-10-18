using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;



public class AudioController : MonoBehaviour
{
    private static AudioController instance;
    public static AudioController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioController>();
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }

    Dictionary<string, AudioClip> myDictionary = new Dictionary<string, AudioClip>();

    public AudioSource audioSource;

    public void Test() { }
    public void PlayClip(string clipstring)
    {     
            if (myDictionary.ContainsKey(clipstring))
            {
                if (myDictionary[clipstring] == null)
                {
                    return;
                }
                audioSource.PlayOneShot(myDictionary[clipstring]);
            }
            else
            {
                string str = AddresAudioString(clipstring);
                if (str == "") { return; }
                AsyncOperationHandle<AudioClip> asyncOperationHandle11 = Addressables.LoadAssetAsync<AudioClip>(str);

                DateTime datecheck = DateTime.Now;
                myDictionary[clipstring] = null;
                asyncOperationHandle11.Completed += (handle) =>
                {
                    if (handle.Status == AsyncOperationStatus.Succeeded)
                    {
                        myDictionary[clipstring] = handle.Result;
                        if ((DateTime.Now - datecheck).TotalSeconds <= 1)
                        {
                            audioSource.PlayOneShot(handle.Result);
                        }
                    }
                };
            }
    }
    private string AddresAudioString(string str)
    {
        string h = "";

        switch (str)
        {
            case "naildam":
                h = "Assets/Audio/Thud.ogg";
                break;
        }


        return h;
    }
}
