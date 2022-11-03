using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGoogleAdMob;
using GoogleMobileAds.Api;
using UnityEngine.Events;

public class AdsLoadAndShowOfficer : MonoBehaviour
{
    public GoogleAdMobInit googleAdMobInit;
    public GoogleAdMobAdsDataOfficer googleAdMobAdsDataOfficer;
    public Dictionary<AdPlacement, RewardedAd> RewardedAds { get; private set; } = new Dictionary<AdPlacement, RewardedAd>();

    private void OnEnable()
    {
        googleAdMobInit.OnInitialized.AddListener(LoadAllAds);
    }

    private void OnDisable()
    {
        googleAdMobInit.OnInitialized.AddListener(LoadAllAds);
    }

    public void LoadAllAds()
    {
        foreach (KeyValuePair<AdPlacement, GoogleAdMobType> ad in googleAdMobAdsDataOfficer.Ads)
        {
            if (ad.Value.adFormat == AdFormat.Rewarded)
            {
                LoadRewardedAd(ad.Key, ad.Value);
            }
        }
    }

    private void LoadRewardedAd(AdPlacement placement, GoogleAdMobType type)
    {
        string unitId = "unexpected_platform";
        if (Application.platform == RuntimePlatform.Android)
        {
            if (GameManager.instance.isTest) unitId = googleAdMobAdsDataOfficer.Ads[AdPlacement.Test].androidUnitID;
            else unitId = type.androidUnitID;
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (GameManager.instance.isTest) unitId = googleAdMobAdsDataOfficer.Ads[AdPlacement.Test].iosUnitID;
            else unitId = type.iosUnitID;
        }

        if (!RewardedAds.ContainsKey(placement))
        {
            RewardedAds.Add(placement, new RewardedAd(unitId));
        }

        AdRequest request = new AdRequest.Builder().Build();
        RewardedAds[placement].LoadAd(request);
        RewardedAds[placement].OnAdLoaded += (_, __) => googleAdMobAdsDataOfficer.Ads[placement].isLoaded = true;
        RewardedAds[placement].OnAdFailedToLoad += (_, error) => Debug.LogError(error.LoadAdError.GetMessage());
    }

    /// <param name="onRewarded">Returns the reward amount</param>
    public void ShowRewardedAd(AdPlacement placement, UnityAction<double> onRewarded)
    {
        if (!RewardedAds.ContainsKey(placement))
        {
            Debug.Log($"Not found '{placement.ToString()}' ad");
            return;
        }
        if (RewardedAds[placement].IsLoaded())
        {
            RewardedAds[placement].Show();
            LoadRewardedAd(placement, googleAdMobAdsDataOfficer.Ads[placement]);
            RewardedAds[placement].OnUserEarnedReward += (sender, args) => onRewarded?.Invoke(args.Amount);
        }
    }
}
