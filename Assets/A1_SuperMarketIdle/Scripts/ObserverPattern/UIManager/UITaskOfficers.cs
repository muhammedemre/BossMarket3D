using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITaskOfficers : MonoBehaviour
{
    [SerializeField] AudioSource buttonAudio;

    public void SettingsButton(bool state)
    {
        UIManager.instance.settingsWindow.gameObject.SetActive(state);
        buttonAudio.Play();
    }

    public void UpgradeWindowClose()
    {
        UpgradeWindowSetState(false, null);
    }

    public void UpgradeWindowSetState(bool state, RoomActor relatedRoomActor)
    {
        
        UIManager.instance.upgradeWindow.transform.GetChild(0).gameObject.SetActive(state);       
        if (state)
        {
            UIManager.instance.upgradeWindow.GetComponent<UpgradeWindowUpgradeOfficer>().relatedRoomActor = relatedRoomActor;
            UIManager.instance.upgradeWindow.GetComponent<UpgradeWindowActor>().GetPrepared();
        }
    }
    
    public void TruckUpgradeWindowClose()
    {
        OpenAndCloseTruckUpgradeWindow(false, null);
    }

    public void OpenAndCloseTruckUpgradeWindow(bool state, RoomActor relatedRoomActor)
    {
        UIManager.instance.truckUpgradeWindow.SetActive(state);
        if (state)
        {
            UIManager.instance.upgradeWindow.GetComponent<UpgradeWindowUpgradeOfficer>().relatedRoomActor = relatedRoomActor;
            UIManager.instance.upgradeWindow.GetComponent<UpgradeWindowActor>().GetPrepared();
        }
    }

    public void MoneyCheat()
    {
        PlayerManager.instance.playerCurrencyOfficer.Money += 9999;
    }

}
