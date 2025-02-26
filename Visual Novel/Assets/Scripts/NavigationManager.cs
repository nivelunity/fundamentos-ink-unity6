using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.Serialization;

public class NavigationManager : MonoBehaviour
{
    [SerializeField] private Transform[] rooms;
    [SerializeField] private CinemachineCamera cinemachineCamera;
    [SerializeField] private Transform player;

    [SerializeField]
    [Range(0,2)]
    private int roomNumber = 1;
    
    
    [SerializeField]
    [Range(10,50)]
    private float positionOffset = 30f;
    
    public void GoNextRoom()
    {
        int nextRoomNumber = roomNumber + 1;
        Debug.Log("IR AL ROOM " + nextRoomNumber);
        if(nextRoomNumber == rooms.Length) return;

        roomNumber = nextRoomNumber;
        cinemachineCamera.Target.TrackingTarget = rooms[roomNumber];
        
        RepositionPlayer(positionOffset);
    }
    
    public void GoPrevRoom()
    {
        int prevRoomNumber = roomNumber - 1;
        Debug.Log("IR AL ROOM " + prevRoomNumber);
        
        if(prevRoomNumber < 0) return;

        roomNumber = prevRoomNumber;
        cinemachineCamera.Target.TrackingTarget = rooms[roomNumber];
        
        RepositionPlayer(-positionOffset);
    }

    private void RepositionPlayer(float offset)
    {
        float newX = player.position.x + offset;
        player.position = new Vector3(newX, player.position.y, player.position.z);
    }
}
