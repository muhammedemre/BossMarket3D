using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionOfficer : MonoBehaviour
{
    [SerializeField] PlayerActor playerActor;
    [SerializeField] bool busyWCashier = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CashierDesk")
        {
            if (UIManager.instance.settingsMenuActor.soundState)
            {
                other.GetComponent<RootFinderOfficer>().root.GetComponent<CashierActor>().moneyHandleOfficer.audioSource.Play();
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "CashierDesk")
        {
            busyWCashier = false;
        }
        else if (other.tag == "ItemTakePlace")
        {
            playerActor.itemCarryStackOfficer.UnsubscribeToTheItemTakeLine(other.GetComponent<ItemTakePlaceStackOfficer>());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "ItemTakePlace")
        {
            playerActor.itemCarryStackOfficer.RegisterToTheItemTakeLine(other.GetComponent<ItemTakePlaceStackOfficer>());
        }
        else if (other.tag == "ItemStand")
        {
            playerActor.itemCarryStackOfficer.CheckToGiveItemToTheStand(other.GetComponent<ItemStandActor>(), false);
        }
        else if (other.tag == "CashierDesk")
        {
            //busyWCashier = true;
            if (!busyWCashier)
            {
                busyWCashier = true;
            }
            other.GetComponent<RootFinderOfficer>().root.GetComponent<CashierActor>().moneyHandleOfficer.GetTheMoneyOnTheDesk();
        }
    }
}
