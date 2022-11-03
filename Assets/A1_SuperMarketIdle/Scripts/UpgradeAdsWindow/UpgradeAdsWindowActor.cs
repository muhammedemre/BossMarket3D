using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeAdsWindowActor : MonoBehaviour
{
    public UpgradeAdsWindowState upgradeAdsWindowState { get; private set; } = UpgradeAdsWindowState.Nan;
    public GameObject SpeedUpgradeAds, SpeedTimerUpgradeAds, Revenue2XUpgradeAds, Revenue2XTimerUpgradeAds, CashierUpgradeAds;
    public UpgradeAdsWindowTimerOfficer speed2XTimerOfficer;
    public UpgradeAdsWindowTimerOfficer revenue2XTimerOfficer;

    public void RandomOpenUpgradeAdsWindow()
    {
        int index = UnityEngine.Random.Range(0, Enum.GetValues(typeof(UpgradeAdsWindowType)).Length);

        if (index == ((int)UpgradeAdsWindowType.Speed2X))
        {
            ChangeUpgradeAdsWindowState(UpgradeAdsWindowState.Speed2X);
        }
        else if (index == ((int)UpgradeAdsWindowType.Revenue2X))
        {
            ChangeUpgradeAdsWindowState(UpgradeAdsWindowState.Revenue2X);
        }
        else if (index == ((int)UpgradeAdsWindowType.Cashier))
        {
            ChangeUpgradeAdsWindowState(UpgradeAdsWindowState.Cashier);
        }
    }

    public void ChangeUpgradeAdsWindowState(UpgradeAdsWindowState state)
    {
        upgradeAdsWindowState = state;

        DeactiveAllStates();

        if (state == UpgradeAdsWindowState.Speed2X)
        {
            SpeedUpgradeAds.SetActive(true);
        }
        else if (state == UpgradeAdsWindowState.Revenue2X)
        {
            Revenue2XUpgradeAds.SetActive(true);
        }
        else if (state == UpgradeAdsWindowState.Cashier)
        {
            CashierUpgradeAds.SetActive(true);
        }
        else if (state == UpgradeAdsWindowState.Nan)
        {
            LevelManager.instance.levelPowerUpOfficer.RandomlyUpgradeAdsWindow();
        }
    }

    private void DeactiveAllStates()
    {
        SpeedUpgradeAds.SetActive(false);
        SpeedTimerUpgradeAds.SetActive(false);
        Revenue2XUpgradeAds.SetActive(false);
        Revenue2XTimerUpgradeAds.SetActive(false);
        CashierUpgradeAds.SetActive(false);
    }
}

public enum UpgradeAdsWindowType { Speed2X, Revenue2X, Cashier }
public enum UpgradeAdsWindowState { Nan, Speed2X, Revenue2X, Cashier }