//using Firebase.Crashlytics;
using System;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using UnityEngine.Purchasing.Security;

using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Linq.Expressions;
using UnityEngine.UIElements;
using static UnityEngine.UIElements.UxmlAttributeDescription;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.PackageManager.Requests;
using UnityEditor.PackageManager;
#endif


public class SuInAppPurchase : BaseSUUnit, IStoreListener
{
    public static Action<IAPProductIDName> OnPurchaseSuccess, OnInitializeSuccess;
    public static Dictionary<IAPProductIDName, IAPProductModule> ProductsDict;

    IStoreController controller;
    IExtensionProvider extensions;

    public static bool isInitialized = false;
    public static SuInAppPurchase instance;
    public List<IAPProductModule> productsList;
    ConfigurationBuilder builder;
    public override void Init()
    {
        // 
    }
    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        Debug.Log(error);   
    }    
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
    {
#if SUGAME_VALIDATED
        foreach (IAPProductModule _product in productsList)
        {
            if (e.purchasedProduct.definition.id == _product.productId)
            {
                bool validPurchase = true;
                try
                {
                    try
                    {
                        var validator = new CrossPlatformValidator(GooglePlayTangle.Data(), AppleTangle.Data(), Application.identifier);
                        // On Google Play, result has a single product ID.
                        // On Apple stores, receipts contain multiple products.
                        var result = validator.Validate(e.purchasedProduct.receipt);
                        // For informational purposes, we list the receipt(s)
                        //Debug.Log("Receipt is valid. Contents:");
                        foreach (IPurchaseReceipt productReceipt in result)
                        {
                            Debug.Log(productReceipt.productID);
                            Debug.Log(productReceipt.purchaseDate);
                            Debug.Log(productReceipt.transactionID);
                        }
                    }
                    catch
                    {

                    }

                }
                catch (IAPSecurityException eee)
                {
                    Debug.Log("Invalid receipt, not unlocking content " + eee);
                    validPurchase = false;
                }
#if UNITY_EDITOR
                //editor thì luôn set = true để test
                validPurchase = true;
#endif

                if (validPurchase)
                {
                    OnPurchaseSuccessed(_product);
                    _product.OnPurchaseSuccess?.Invoke();

                    //log event
                    SuGame.Get<SuAnalytics>().LogEvent(EventName.Purchase_Success, new Param(ParaName.ID, _product.productId));
                    
                    // SuGame.Get<SuAnalytics>().LogEvent(EventName.iap_sdk, new Param(ParaName.level, LevelManager.Instance.LevelIDInt),
                    // new Param(ParaName.value, _product.price), new Param(ParaName.currency, _product.product.metadata.isoCurrencyCode)
                    // );


                    SuGame.Get<SuAnalytics>().LogEvent(EventName.iap_sdk
                        //,
                      //new Param(ParaName.level, AnalyticsHieu.level()),
                      //new Param(ParaName.value, _product.price),
                      //new Param(ParaName.currency, _product.product.metadata.isoCurrencyCode),
                      //new Param(ParaName.highest_level, AnalyticsHieu.highest_level(LevelManager.Instance.LevelIDInt)),
                      //new Param(ParaName.move_left, AnalyticsHieu.move_left()),
                      //new Param(ParaName.coins, AnalyticsHieu.coins()),
                      //new Param(ParaName.current_skin, AnalyticsHieu.current_skin()),
                      //new Param(ParaName.current_trail, AnalyticsHieu.current_trail()),
                      //new Param(ParaName.current_touch, AnalyticsHieu.current_touch())
                    );
                    //*level : Level hiện tại đang chơi
                    //* value: revenue thu được từ lần mua này
                    //* currency : đơn vị tiền tệ
                    //*highest_level: level cao nhất mà user từng chơi
                    //* move_left: số lượt đi còn lại
                    //* bomb_left: số bom còn lại
                    //*coins: số coin còn lại
                    //*current_skin: 
                    //*current_trail: 
                    //*current_touch:




                    //SuGame.Get<SuAnalytics>().LogEvent(EventName.RemoveAds_Pay);

                    Debug.Log("Buy success product " + _product.ProductName);
                }
                else
                {
                    SuGame.Get<SuAnalytics>().LogEvent(EventName.Purchase_Fail, new Param(ParaName.ID, _product.productId), new Param(ParaName.Reason, "invalid_receipt"));
                    //Debug.Log("khong mua thanh cong");
                }
                break;

            }
        }

        return PurchaseProcessingResult.Complete;
#else
        return PurchaseProcessingResult.Complete;
#endif

    }
    public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
    {
#if SUGAME_VALIDATED
        SuGame.Get<SuAnalytics>().LogEvent(EventName.Purchase_Fail, new Param(ParaName.ID, i.definition.id), new Param(ParaName.Reason, p.ToString()));
#endif
    }
    // OnInitFailed
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        isInitialized = false;
    }
    // Init
    public void OnInitialized(IStoreController _controller, IExtensionProvider _extensions)
    {
#if SUGAME_VALIDATED
        controller = _controller;
        extensions = _extensions;
        isInitialized = true;
        foreach (var product in controller.products.all)
        {
            for (int i = 0; i < productsList.Count; i++)
            {
                IAPProductModule _product = productsList[i];
                if (_product.productId == product.definition.id)
                {
                    // set real price and product for IAPProduct 
                    _product.price = product.metadata.localizedPriceString;
                    _product.priceNumber = product.metadata.localizedPrice;
                    _product.product = product;
                    OnInitializeSuccess?.Invoke(_product.ProductName);
                }
            }
        }
#endif

    }
#if SUGAME_VALIDATED
    void Awake()
    {
        //StandardPurchasingModule.Instance().useFakeStoreAlways = true;
        ProductsDict = new Dictionary<IAPProductIDName, IAPProductModule>();
        for (int i = 0; i < productsList.Count; i++)
        {
            IAPProductModule md = productsList[i];
            if (!ProductsDict.ContainsKey(md.ProductName))
            {
                ProductsDict.Add(md.ProductName, md);
            }
        }
        instance = this;
        builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        ///----------------------- ADD ITEM TO PURCHASE MANAGER----------------------//

        foreach (IAPProductModule _product in productsList)
        {
            builder.AddProduct(_product.productId, _product.productType);
        }

        UnityPurchasing.Initialize(this, builder);
        // Hide banner when purchase success ( for all game)
        OnPurchaseSuccess += (productIDName) =>
        {
            Debug.Log("User đã mua product " + productIDName);
            if (productIDName == IAPProductIDName.removeads)
            {
#if SUGAME_VALIDATED
                //SuGame.Get<SuAdmob>().HideBanner();
#endif
            }
        };

        //SuGame.Get<SuInAppPurchase>().BuyProduct(IAPProductIDName.noads);
    }

    public static IAPProductModule GetProduct(IAPProductIDName name)
    {
        if (ProductsDict.ContainsKey(name))
        {
            return ProductsDict[name];
        }
        return null;
    }


    public static void AddProduct(IAPProductModule _product)
    {

        if (instance.productsList.Find(x => x.productId == _product.productId) == null)
        {
            instance.productsList.Add(_product);
            instance.builder.AddProduct(_product.productId, _product.productType);
            UnityPurchasing.Initialize(instance, instance.builder);
        }
    }









    public void BuyProductId(string productId)
    {

        if (isInitialized == true)
        {
            Product _product = controller.products.WithID(productId);
            if (_product != null && _product.availableToPurchase)
            {
                // Dont show forcus ads if focus by buy iap
                //SuGame.Get<SuAdmob>().LockFocusAds = true;
                //
                controller.InitiatePurchase(_product);
            }

        }
        else
        {
            //GameManager.ShowMessenger(string.Format(Localization.Get("lb_check_internet_error"), "Google"), Localization.Get("tit_warning"));
        }
    }

    public void BuyProduct(IAPProductIDName name)
    {
        
        if (isInitialized == true)
        {
            
            IAPProductModule prd = productsList.Find(x => x.ProductName == name);
            if (prd != null)
            {
               
                Product _product = controller.products.WithID(prd.productId);
                if (_product != null && _product.availableToPurchase)
                {
                    
                    // Dont show forcus ads if focus by buy iap
                    //SuGame.Get<SuAdmob>().LockFocusAds = true;
                    controller.InitiatePurchase(_product);
                }
            }
            //else
            //{
            //    gfsdd
            //}
        }
        else
        {
            //GameManager.ShowMessenger(string.Format(Localization.Get("lb_check_internet_error"), "Google"), Localization.Get("tit_warning"));
        }
    }





    public void OnPurchaseSuccessed(IAPProductModule _productModule)
    {
        switch (_productModule.ProductName)
        {
            case IAPProductIDName.removeads:

                break;
        }
        OnPurchaseSuccess?.Invoke(_productModule.ProductName);
    }




#if UNITY_EDITOR
    // khi nào sếp hỏi "em ơi cho anh danh sách các productID với giá mặc định" thì chạy cái này để lấy cho nhanh 
    [MenuItem("IAP/GetAllIAPProduct")]
    public static void GetAllIAPProduct()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<SuInAppPurchase>();
            if (instance == null)
            {
                return;
            }
        }
        string text = "";
        for (int i = 0; i < instance.productsList.Count; i++)
        {
            IAPProductModule md = instance.productsList[i];
            text += "\n" + md.productId + "_______________" + md.price;
        }
        Debug.Log("IAP product : " + text);
    }




#endif
#endif
}

[Serializable]
public class IAPProductModule
{
#if SUGAME_VALIDATED
    public IAPProductIDName ProductName;
    public string productId
    {
        get
        {
            // quy tắc đặt tên product là packageName.productName
            // ví dụ com.sg.blockpuzzle.noads
            return "com.sg.tapaway."  + ProductName;
        }
    }
    public ProductType productType;
    [HideInInspector]
    public decimal priceNumber;
    [LabelText("Default Price")]
    public string price;
    public int vipPoint;
    public UnityEvent OnPurchaseSuccess;
    [HideInInspector]
    public Product product;
    [HideInInspector]
    public SubscriptionInfo subsInfo;
#endif
}

[System.Serializable]
public enum IAPProductIDName
{
    removeads,
}


