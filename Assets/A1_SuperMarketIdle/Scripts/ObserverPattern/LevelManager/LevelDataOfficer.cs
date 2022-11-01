using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class LevelDataOfficer : SerializedMonoBehaviour
{
    [SerializeField] LevelActor levelActor;
    public Dictionary<int, GameObject> activeRooms = new Dictionary<int, GameObject>();

    public void AssignLevelDatas()
    {
        foreach (int roomIndex in activeRooms.Keys)
        {
            if (!levelActor.levelRoomOfficer.levelRooms[roomIndex].activeSelf)
            {
                levelActor.levelRoomOfficer.ActivateARooom(roomIndex);
                //levelActor.levelRoomOfficer.levelRooms[roomIndex].SetActive(true);
            }
            activeRooms[roomIndex].GetComponent<RoomActor>().roomDataOfficer.AssignTheVariables();
        }
        SoundManager.instance.MusicOnOff(UIManager.instance.settingsMenuActor.musicState);
    }

    public void LetMeKnowRoomIsActivated(int roomIndex, GameObject roomGameObject)
    {
        activeRooms.Add(roomIndex, roomGameObject);
    }
}
