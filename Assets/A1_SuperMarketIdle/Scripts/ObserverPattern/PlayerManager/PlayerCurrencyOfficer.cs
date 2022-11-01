using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCurrencyOfficer : MonoBehaviour
{
    [SerializeField] int money = 0;
    [SerializeField] int testMoneyStartData; // remove this when datamanager is ready.
    [SerializeField] bool testData = true;
    [SerializeField] AudioClip moneyAudioClip;
 
    public int Money
    {
        get
        {
            return money;
        }
        set
        {
            money = value;
            if (money < 0 )
            {
                money = 0;
            }
            UIManager.instance.UpdateMoneyText(money);
        }
    }

    public void MoneyDepositToTheWallet(int moneyToDeposit)
    {
        Money += moneyToDeposit;
    }

    public void GetMoneyDataFromDataManager()
    {
        if (testData)
        {
            Money = testMoneyStartData;
        }
        else
        {
            //Money = moneyFromData; // Get the data from datamanager
        }
        
    }
}
