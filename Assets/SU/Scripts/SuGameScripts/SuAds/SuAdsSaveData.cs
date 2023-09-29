
[System.Serializable]
public class SuAdsSaveData
{
    public uint InterstitialCount, BannerCount, RewardedVideoCount, AppOpenCount;
    public double InterstitialRevenue, BannerRevenue, RewardedVideoRevenue, AppOpenRevenue;
    public double TotalRevenue
    {
        get
        {
            return InterstitialRevenue + BannerRevenue + RewardedVideoRevenue + AppOpenRevenue;
        }
    }
}
