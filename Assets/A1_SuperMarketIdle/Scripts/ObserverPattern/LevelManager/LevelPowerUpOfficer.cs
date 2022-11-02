using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPowerUpOfficer : MonoBehaviour
{
    [SerializeField] bool speedUpActive = false, coinBoostActive = false;

    public void FillTheRoomsItemStands() 
    {
    
    }
    public void SpeedUpTheCustomersForSomeTime(float speedBoostCoefficient, float duration) 
    {
    
    }

    public void GetCoinReward(int coinAmount)
    {
        PlayerManager.instance.playerCurrencyOfficer.MoneyDepositToTheWallet(coinAmount);
    }

    public void CoinEarnBoostForSomeTime(float coinBoostCoefficient, float duration) 
    {

    }

    
}
