using UnityEngine;
using Unity.Cinemachine;

public class NavigationManager : MonoBehaviour
{
    [SerializeField] private Transform[] rooms;
    [SerializeField] private CinemachineCamera camera;
    [SerializeField] private Transform player;

    [SerializeField]
    [Range(0,2)]
    private int roomNumber = 1;

    public void GoNextRoom()
    {
        int nextRoomNumber = roomNumber + 1;
        Debug.Log("IR AL ROOM " + nextRoomNumber);
        if(nextRoomNumber == rooms.Length) return;

        roomNumber = nextRoomNumber;
        camera.Target.TrackingTarget = rooms[roomNumber];
    }
    
    public void GoPrevRoom()
    {
        int prevRoomNumber = roomNumber - 1;
        Debug.Log("IR AL ROOM " + prevRoomNumber);
        
        if(prevRoomNumber < 0) return;

        roomNumber = prevRoomNumber;
        camera.Target.TrackingTarget = rooms[roomNumber];
    }
}
