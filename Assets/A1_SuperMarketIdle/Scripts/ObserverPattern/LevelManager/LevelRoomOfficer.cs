using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class LevelRoomOfficer : SerializedMonoBehaviour
{
    [SerializeField] LevelActor levelActor;
    //public List<Transform> levelRooms = new List<Transform>();
    public Dictionary<int, GameObject> levelRooms = new Dictionary<int, GameObject>();
    public Dictionary<int, GameObject> levelRoomActivators = new Dictionary<int, GameObject>();
    //int nextRoomIndex = 0;


    public void ActivateARooom(int roomIndex)
    {
        levelRoomActivators[roomIndex].GetComponent<ActivisionPointAnchor>().ActivisionCalculateOfficer.ActivateTheObject();
    }
}
