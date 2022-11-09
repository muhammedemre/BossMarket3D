using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class BoostAdsWindowActor : MonoBehaviour
{
    public BoostAdsWindowState boostAdsWindowState { get; private set; } = BoostAdsWindowState.Nan;
    public GameObject SpeedBoostAds, SpeedTimerBoostAds, Revenue2XBoostAds, Revenue2XTimerBoostAds, CashierBoostAds, CashierTimerBoostAds;
    public BoostAdsWindowTimerOfficer speed2XTimerOfficer;
    public BoostAdsWindowTimerOfficer revenue2XTimerOfficer;
    public BoostAdsWindowTimerOfficer cashierTimerOfficer;

    public RectTransform leftRect, rightRect;
    public float openDuration;

    private void Start()
    {
        LevelManager.instance.levelPowerUpOfficer.NewAdsBoostCheck();
    }

    public void RandomOpenBoostAdsWindow()
    {
        StopCoroutine(LevelManager.instance.levelPowerUpOfficer.lastNewAdsBoostCoroutine);
        int index = UnityEngine.Random.Range(0, Enum.GetValues(typeof(BoostAdsWindowType)).Length);

        if (index == ((int)BoostAdsWindowType.Speed2X))
        {
            ChangeBoostAdsWindowState(BoostAdsWindowState.Speed2X);
        }
        else if (index == ((int)BoostAdsWindowType.Revenue2X))
        {
            ChangeBoostAdsWindowState(BoostAdsWindowState.Revenue2X);
        }
        else if (index == ((int)BoostAdsWindowType.Cashier))
        {
            ChangeBoostAdsWindowState(BoostAdsWindowState.Cashier);
        }
    }

    public void ChangeBoostAdsWindowState(BoostAdsWindowState state)
    {
        DeactiveState(false, () =>
        {
            boostAdsWindowState = state;

            if (state == BoostAdsWindowState.Speed2X)
            {
                OpenState(SpeedBoostAds);
            }
            else if (state == BoostAdsWindowState.Revenue2X)
            {
                OpenState(Revenue2XBoostAds);
            }
            else if (state == BoostAdsWindowState.Cashier)
            {
                OpenState(CashierBoostAds);
            }
            else if (state == BoostAdsWindowState.Nan)
            {
                LevelManager.instance.levelPowerUpOfficer.NewAdsBoostCheck();
            }
        });
    }

    void OpenState(GameObject adsObject)
    {
        adsObject.transform.position = rightRect.transform.position;
        adsObject.transform.DOMove(leftRect.transform.position, openDuration);
        adsObject.SetActive(true);
    }

    void CloseState(GameObject adsObject, UnityAction onComplete)
    {
        adsObject.transform.DOMove(rightRect.transform.position, openDuration).OnComplete(() =>
        {
            adsObject.SetActive(false);
            onComplete?.Invoke();
        });
    }

    public void OpenTimer(BoostAdsWindowTimerOfficer timer)
    {
        if (boostAdsWindowState == BoostAdsWindowState.Speed2X)
        {
            CloseState(SpeedBoostAds, () =>
            {
                OpenState(SpeedTimerBoostAds);
                timer.StartTimer();
            });
        }
        else if (boostAdsWindowState == BoostAdsWindowState.Revenue2X)
        {
            CloseState(Revenue2XBoostAds, () =>
            {
                OpenState(Revenue2XTimerBoostAds);
                timer.StartTimer();
            });
        }
        else if (boostAdsWindowState == BoostAdsWindowState.Cashier)
        {
            CloseState(CashierBoostAds, () =>
            {
                OpenState(CashierTimerBoostAds);
                timer.StartTimer();
            });
        }
    }

    public void DeactiveState(bool isTimer, UnityAction onComplete)
    {
        Debug.Log(boostAdsWindowState);
        if (boostAdsWindowState == BoostAdsWindowState.Speed2X)
        {
            CloseState(isTimer ? SpeedTimerBoostAds : SpeedBoostAds, onComplete);
        }
        else if (boostAdsWindowState == BoostAdsWindowState.Revenue2X)
        {
            CloseState(isTimer ? Revenue2XTimerBoostAds : Revenue2XBoostAds, onComplete);
        }
        else if (boostAdsWindowState == BoostAdsWindowState.Cashier)
        {
            CloseState(isTimer ? CashierTimerBoostAds : CashierBoostAds, onComplete);
        }
        else
        {
            onComplete?.Invoke();
        }

        if (isTimer) LevelManager.instance.levelPowerUpOfficer.NewAdsBoostCheck();
    }

    [Title("ButtonRandomlyBoostAdsWindow")]
    [Button("ButtonRandomlyBoostAdsWindow", ButtonSizes.Gigantic)]
    void ButtonRandomlyBoostAdsWindow()
    {
        if (!Application.isPlaying)
        {
            Debug.Log("Only playing!");
            return;
        }
        LevelManager.instance.levelPowerUpOfficer.RandomlyBoostAdsWindow();
    }
}

public enum BoostAdsWindowType { Speed2X, Revenue2X, Cashier }
public enum BoostAdsWindowState { Nan, Speed2X, Revenue2X, Cashier }