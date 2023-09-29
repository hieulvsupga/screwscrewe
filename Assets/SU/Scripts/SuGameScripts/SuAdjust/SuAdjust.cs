using com.adjust.sdk;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SuAdjust : BaseSUUnit
{
    public AdjustEventTokenDB eventTokenDB;
    string GetTransactionID()
    {
        return System.DateTime.Now.Ticks.ToString();
    }
    public void LogRevenue(string network, double revenue, string currencyCode, int impressionCount, string placement, string adUnit, Dictionary<string, string> addData)
    {
        AdjustAdRevenue adRevenue = new AdjustAdRevenue(AdjustConfig.AdjustAdRevenueSourceAdMob);
        adRevenue.setRevenue(revenue, currencyCode);
        adRevenue.setAdRevenueNetwork(network);
        adRevenue.setAdImpressionsCount(impressionCount);
        adRevenue.setAdRevenuePlacement(placement);
        adRevenue.setAdRevenueUnit(adUnit);

        foreach (KeyValuePair<string, string> pr in addData)
        {
            adRevenue.addPartnerParameter(pr.Key, pr.Value);
        }
        Adjust.trackAdRevenue(adRevenue);
    }

    public void LogEvent(EventName eventName, float revenue = 0, params Param[] _param)
    {
        string token = eventTokenDB.GetToken(eventName);
        if (token == "")
        {
            Utiliti.Log(this, "Chưa set event token cho event name " + eventName + " này");
            return;
        }
        AdjustEvent ev = new AdjustEvent(token.ToString());
        foreach (Param _pr in _param)
        {
            ev.addPartnerParameter(_pr.paramName.ToString(), _pr.value.ToString());
            ev.addCallbackParameter(_pr.paramName.ToString(), _pr.value.ToString());
        }
        Utiliti.Log(this, "Log Adjust event " + eventName.ToString() + "token : " + ev.eventToken);
        ev.setRevenue(revenue, "USD");
        ev.setTransactionId(GetTransactionID());
        Adjust.trackEvent(ev);

    }

    public void LogEvent(string eventName, float revenue = 0, params Param[] _param)
    {
        string token = eventTokenDB.GetToken(eventName);
        if (token == "")
        {
            Debug.Log("Chưa set event token cho event name " + eventName + " này");
            return;
        }
        AdjustEvent ev = new AdjustEvent(token.ToString());
        foreach (Param _pr in _param)
        {
            ev.addPartnerParameter(_pr.paramName.ToString(), _pr.value.ToString());
            ev.addCallbackParameter(_pr.paramName.ToString(), _pr.value.ToString());
        }
        Debug.Log("Log Adjust event " + eventName.ToString() + "token : " + ev.eventToken);
        ev.setRevenue(revenue, "USD");
        ev.setTransactionId(GetTransactionID());
        Adjust.trackEvent(ev);

    }

    public void LogIAPEvent(string currency, string revenue)
    {

    }

    public override void Init()
    {
        // enable thirdparty sharing 
        AdjustThirdPartySharing thirdPartySharing = new AdjustThirdPartySharing(true);
        Adjust.trackThirdPartySharing(thirdPartySharing);
    }
}
