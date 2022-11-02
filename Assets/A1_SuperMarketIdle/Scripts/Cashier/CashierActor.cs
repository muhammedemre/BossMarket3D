using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashierActor : MonoBehaviour
{
    public MoneyHandleOfficer moneyHandleOfficer;
    public MoneyStackOfficer moneyStackOfficer;
    public Transform interactionPoint;
    public RoomActor belongingRoom;

    [SerializeField] CashierWorkerActor cashierWorkerActor;

    public void CashierWorkerActivate()
    {
        cashierWorkerActor.gameObject.SetActive(true);
        cashierWorkerActor.StartProcess();
    }

    #region Button

    [Title("Activate The Cashier Worker")]
    [Button("Activate The Cashier Worker", ButtonSizes.Large)]
    void ButtonActivateTheCashierWorker()
    {
        CashierWorkerActivate();
    }
    #endregion
}
