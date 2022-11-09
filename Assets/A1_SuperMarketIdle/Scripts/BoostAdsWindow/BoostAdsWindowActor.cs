using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class BoostAdsWindowActor : MonoBehaviour
{
    public BoostAdsWindowState boostAdsWindowState { get; private set; } = BoostAdsWindowState.Nan;
    public GameObject SpeedBoostAds, SpeedTimerBoostAds, Revenue2XBoostAds, Revenue2XTimerBoostAds, CashierBoostAds, CashierTimerBoostAds;
    public BoostAdsWindowTimerOfficer speed2XTimerOfficer;
    public BoostAdsWindowTimerOfficer revenue2XTimerOfficer;
    public BoostAdsWindowTimerOfficer cashierTimerOfficer;

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
        boostAdsWindowState = state;

        DeactiveAllStates();

        if (state == BoostAdsWindowState.Speed2X)
        {
            SpeedBoostAds.SetActive(true);
        }
        else if (state == BoostAdsWindowState.Revenue2X)
        {
            Revenue2XBoostAds.SetActive(true);
        }
        else if (state == BoostAdsWindowState.Cashier)
        {
            CashierBoostAds.SetActive(true);
        }
        else if (state == BoostAdsWindowState.Nan)
        {
            LevelManager.instance.levelPowerUpOfficer.NewAdsBoostCheck();
        }
    }

    public void DeactiveAllStates()
    {
        SpeedBoostAds.SetActive(false);
        SpeedTimerBoostAds.SetActive(false);
        Revenue2XBoostAds.SetActive(false);
        Revenue2XTimerBoostAds.SetActive(false);
        CashierBoostAds.SetActive(false);
        CashierTimerBoostAds.SetActive(false);
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