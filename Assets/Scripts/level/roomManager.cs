using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomManager : MonoBehaviour
{
    #region 
    public static roomManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion
    [System.Serializable]
    public class allRooms
    {
        public string roomName;
        public string roomLocation;
        public GameObject roomToLoad;
        
    }

    public allRooms[] rooms;
    public int currentRoom = 0;
    public int doorToEnter = 0;
    public Transform levelPoint;
    public GameObject player;
    GameObject roomObject;
    public levelController lc;

    
    void Start()
    {
        //player = lc.thePlayer;
    }    

    /*public void loadNextRoom(int _roomNumber, int _doorToEnter)
    {
        Destroy(roomObject);
        currentRoom = _roomNumber;
        roomObject = Instantiate(rooms[currentRoom].roomToLoad, levelPoint.position, levelPoint.rotation);
        roomObject.transform.SetParent(levelPoint);
        Transform _doorPoint = roomObject.GetComponent<roomInfo>().doors[_doorToEnter];
        //move player to door position
        player.transform.position = _doorPoint.position;
        //Rotate player to match rotation of door
        player.localRotation = Quaternion.Euler(_doorPoint.rotation.x, _doorPoint.rotation.y, _doorPoint.rotation.z);

    } */

    public void loadCurrentRoom(int _roomNumber, int _doorToEnter, bool saveSpot)
    {
        Destroy(roomObject);
        currentRoom = _roomNumber;
        roomObject = Instantiate(rooms[currentRoom].roomToLoad, levelPoint.position, levelPoint.rotation);
        roomObject.transform.SetParent(levelPoint);
        roomInfo ri = roomObject.GetComponent<roomInfo>();
        Transform _doorPoint = ri.doors[_doorToEnter];
        if (saveSpot && ri.saveSpot != null)
        {
            _doorPoint = null;
            _doorPoint = ri.saveSpot;
            //Debug.Log("assigned from sace spot");
        }


        //move n rotate player to door position
        player.GetComponent<playerMovement>().warpPlayer(_doorPoint.position, _doorPoint.rotation.x, _doorPoint.rotation.y, _doorPoint.rotation.z);
       
        ///Debug.Log("room manager: Im in room " + currentRoom.ToString());
    }
}
