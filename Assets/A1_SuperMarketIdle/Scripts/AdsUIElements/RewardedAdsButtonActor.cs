using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RewardedAdsButtonActor : MonoBehaviour
{
    public Button button;
    public MyGoogleAdMob.AdPlacement placement;
    public UnityEvent OnReward = new UnityEvent();

    private bool isLoaded = false;

    private void OnEnable()
    {
        button.interactable = false;
        button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(OnClick);
    }

    private void FixedUpdate()
    {
        if (!isLoaded)
        {
            CheckInteractable();
        }
    }

    private void CheckInteractable()
    {
        isLoaded = AdsManager.instance.adsActor.adsShowOfficer.RewardedAds[placement].IsLoaded();
        if (isLoaded) button.interactable = true;
    }

    private void OnClick()
    {
        OnReward?.Invoke();
    }
}

