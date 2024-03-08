
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
[System.Serializable]
public enum ShopItemType
{
    Gold,
}

[System.Serializable]
public enum CurrencyType
{
    Gold
}

[Serializable]
public struct MagnetData
{
    public ShopItemType ItemType;
    public ObjectPoolBehaviour FXBool;
    public ParticleSystemForceField ForceField;
}

public class ShopItemSO : ScriptableObject
{
    public string itemName;
    public Sprite sprite;
    public float cost;
    public uint discount;
    public string promoBannerText;
    public uint contentValue;
    public ShopItemType contentType;
    public CurrencyType CostInCurrencyType
    {
        get
        {
            switch (contentType)
            {
                case (ShopItemType.Gold):
                    return CurrencyType.Gold;
                default:
                    return CurrencyType.Gold;
            }
        }
    }
}

public class CoinMagnet : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform testtestest;
    public UnityEngine.UI.Button m_ClaimButton;


    [SerializeField] List<MagnetData> m_MagnetData;

    [Header("Camera")]
    [Tooltip("Use Camera and Depth to calculate world space positions.")]
    [SerializeField] Camera m_Camera;
    [SerializeField] float m_ZDepth = 10f;

    [SerializeField] Vector3 m_SourceOffset = new Vector3(0f, 0.1f, 0f);



    private void Awake()
    {
        RegisterButtonCallbacks();
    }



    protected void RegisterButtonCallbacks()
    {
       // m_ClaimButton?.RegisterCallback<ClickEvent>(ClaimReward);
    }


    void ClaimReward(ClickEvent evt)
    {
        Debug.Log("hhhhhhhhhhhhhkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkfasdf");
    }



    ObjectPoolBehaviour GetFXPool(ShopItemType itemType)
    {
        MagnetData magnetData = m_MagnetData.Find(x => x.ItemType == itemType);
        return magnetData.FXBool;
    }

    ParticleSystemForceField GetForcefield(ShopItemType itemType)
    {
        MagnetData magnetData = m_MagnetData.Find(x => x.ItemType == itemType);
        return magnetData.ForceField;
    }

   
    public void TestSinhCoin()
    {
        Vector2 aa= testtestest.transform.position;
        PlayePooledFX(aa, ShopItemType.Gold);
    }

    void PlayePooledFX(Vector2 screenPos, ShopItemType contentType)
    {
        //Debug.Log(screenPos.ToString() + "fasdddddddddddddd");
        Vector3 worldPos; 
        //= screenPos.ScreenPosToWorldPos(m_Camera, m_ZDepth); 
        worldPos = new Vector3(screenPos.x, screenPos.y,1);
        //Debug.Log(worldPos + "ffffffffffffffffffff");
        //+ m_SourceOffset;
        ObjectPoolBehaviour fxPool = GetFXPool(contentType);


        ParticleSystem ps = fxPool.GetPooledObject().GetComponent<ParticleSystem>();

        if (ps == null) return;
        ps.gameObject.SetActive(true);
        ps.gameObject.transform.position = worldPos;
        ParticleSystem.ExternalForcesModule externalForces = ps.externalForces;
        externalForces.enabled = true;


        // add the Forcefield for destination
        ParticleSystemForceField forceField = GetForcefield(contentType);
        forceField.gameObject.SetActive(true);
        externalForces.AddInfluence(forceField);
        ps.Play();

    }
}
