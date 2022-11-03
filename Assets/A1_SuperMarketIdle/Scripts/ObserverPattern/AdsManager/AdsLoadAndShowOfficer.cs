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
    public Dictionary<AdPlacement, InterstitialAd> InterstitialAds { get; private set; } = new Dictionary<AdPlacement, InterstitialAd>();

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
            else if (ad.Value.adFormat == AdFormat.Interstitial)
            {
                LoadInterstitialAd(ad.Key, ad.Value);
            }
        }
    }

    private void LoadRewardedAd(AdPlacement placement, GoogleAdMobType type)
    {
        string unitId = "unexpected_platform";
        if (Application.platform == RuntimePlatform.Android)
        {
            if (GameManager.instance.isTest) unitId = googleAdMobAdsDataOfficer.Ads[AdPlacement.TestRewarded].androidUnitID;
            else unitId = type.androidUnitID;
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (GameManager.instance.isTest) unitId = googleAdMobAdsDataOfficer.Ads[AdPlacement.TestRewarded].iosUnitID;
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

    private void LoadInterstitialAd(AdPlacement placement, GoogleAdMobType type)
    {
        string unitId = "unexpected_platform";
        if (Application.platform == RuntimePlatform.Android)
        {
            if (GameManager.instance.isTest) unitId = googleAdMobAdsDataOfficer.Ads[AdPlacement.TestInterstitial].androidUnitID;
            else unitId = type.androidUnitID;
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (GameManager.instance.isTest) unitId = googleAdMobAdsDataOfficer.Ads[AdPlacement.TestInterstitial].iosUnitID;
            else unitId = type.iosUnitID;
        }

        if (!InterstitialAds.ContainsKey(placement))
        {
            InterstitialAds.Add(placement, new InterstitialAd(unitId));
        }
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

    public void ShowInterstitialAd(AdPlacement placement, UnityAction onClosed = null)
    {
        if (!InterstitialAds.ContainsKey(placement))
        {
            Debug.Log($"Not found '{placement.ToString()}' ad");
            return;
        }
        if (InterstitialAds[placement].IsLoaded())
        {
            InterstitialAds[placement].Show();
            LoadInterstitialAd(placement, googleAdMobAdsDataOfficer.Ads[placement]);
            InterstitialAds[placement].OnAdClosed += (sender, args) => onClosed?.Invoke();
        }
    }
}
