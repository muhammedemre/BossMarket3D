using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PortionActor : MonoBehaviour
{
    [SerializeField] RoomActor relatedRoomActor;
    [SerializeField] NavMeshSurface portionNavmeshSurface;
    private void Start()
    {
        LetLevelDataIamActivated();
        TriggerTheRoomEvent();
        RegisterToLevelNavmesh();
    }

    void RegisterToLevelNavmesh()
    {
        relatedRoomActor.roomFixturesOfficer.AddNavmeshSurfaceToTheActiveNavmeshSurfaces(portionNavmeshSurface);
    }

    void LetLevelDataIamActivated()
    {
        relatedRoomActor.roomDataOfficer.isPortionOpen = true;
        //DataManager.instance.DataSaveAndLoadOfficer.SaveTheData();
    }

    void TriggerTheRoomEvent()
    {
        EventsManager.instance.RoomEventsTrigger(false, relatedRoomActor.roomIndex);
    }
}
