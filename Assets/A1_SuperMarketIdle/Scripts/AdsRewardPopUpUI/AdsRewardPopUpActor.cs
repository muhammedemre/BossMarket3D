using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsRewardPopUpActor : MonoBehaviour
{
    [SerializeField] private GameObject fillRoomPopUp, freeCoinsPopUp;

    public void ActivePopUpState(AdsRewardPopUpState state)
    {
        if (state == AdsRewardPopUpState.FillRoom)
        {
            freeCoinsPopUp.SetActive(false);
            fillRoomPopUp.SetActive(true);
        }
        else if (state == AdsRewardPopUpState.FreeCoins)
        {
            fillRoomPopUp.SetActive(false);
            freeCoinsPopUp.SetActive(true);
        }
    }
}

public enum AdsRewardPopUpState { FillRoom, FreeCoins }
