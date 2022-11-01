using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ItemStandModelOfficer : SerializedMonoBehaviour
{
    [SerializeField] ItemStandActor itemStandActor;
    [SerializeField] Dictionary<ItemType, Transform> itemTypeItemPositionsContainers = new Dictionary<ItemType, Transform>();
    [SerializeField] Dictionary<ItemType, MeshRenderer> itemTypeStandModels = new Dictionary<ItemType, MeshRenderer>();
    [SerializeField] Dictionary<ItemType, Transform> itemTypeBoards = new Dictionary<ItemType, Transform>();
    [SerializeField] Dictionary<ItemType, Transform> itemTypeBoardIcons = new Dictionary<ItemType, Transform>();

    public void SelectTheModel(ItemType selectedModel)
    {
        CloseAll();
        if (itemTypeStandModels.ContainsKey(selectedModel))
        {
            //modelList[(int)selectedModel].SetActive(true);
            itemTypeStandModels[selectedModel].GetComponent<MeshRenderer>().enabled = true;
            itemTypeBoards[selectedModel].gameObject.SetActive(true);
            itemTypeBoardIcons[selectedModel].gameObject.SetActive(true);
        }
        itemStandActor.itemStandItemHandleOfficer.standsItemType = selectedModel;
        Transform selectedItemPositions = itemTypeItemPositionsContainers[selectedModel];
        itemStandActor.itemStandItemHandleOfficer.AssignItemPositions(selectedItemPositions);

    }

    void CloseAll()
    {
        foreach (ItemType model in itemTypeStandModels.Keys)
        {
            //model.SetActive(false);
            itemTypeStandModels[model].GetComponent<MeshRenderer>().enabled = false;
            itemTypeBoards[model].gameObject.SetActive(false);
            itemTypeBoardIcons[model].gameObject.SetActive(false);
        }
    }

    #region Button

    [Title("Select Model Button")]
    [Button("Select Model", ButtonSizes.Large)]
    void ButtonSelectTheModel(ItemType selectedModel)
    {
        SelectTheModel(selectedModel);
    }
    #endregion
}
