using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPowerUpOfficer : MonoBehaviour
{
    public bool speedUpActive = false, coinBoostActive = false;
    public float coinBoostCoefficient = 1, speedUpCoefficient = 1;


    public void FillTheRoomsItemStands() 
    {
    
    }
    public void SpeedUpTheCustomersForSomeTime(float speedBoostCoefficient, float duration) 
    {
        StartCoroutine(DeactivateSpeedUpTheCustomers(duration));
    }

    public void GetCoinReward(int coinAmount)
    {
        PlayerManager.instance.playerCurrencyOfficer.MoneyDepositToTheWallet(coinAmount);
    }

    public void CoinEarnBoostForSomeTime(float _coinBoostCoefficient, float duration) 
    {
        coinBoostActive = true;
        coinBoostCoefficient = _coinBoostCoefficient;
        StartCoroutine(DeactivateCoinBoost(duration));
    }

    IEnumerator DeactivateCoinBoost(float duration) 
    {
        yield return new WaitForSeconds(duration);
        coinBoostActive = false;
        coinBoostCoefficient = 1;
    }

    IEnumerator DeactivateSpeedUpTheCustomers(float duration)
    {
        yield return new WaitForSeconds(duration);
        speedUpActive = false;
        speedUpCoefficient = 1;
    }
}
