using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapController : MonoBehaviour
{
    #region 
    public static mapController instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public Transform thePlayer;
    public Transform top;
    public Transform bottom;
    public Transform right;
    public Transform left;
    public float mapHeight;
    public Transform mapParent;
    public GameObject theMapIcon;
    public GameObject playerMapIcon;

    //List<mapIcon> mapIcons;
    //public int levels;
    public float interval;
    // Start is called before the first frame update
    void Start()
    {
        //mapIcons = new List<mapIcon>;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 _playerPos = new Vector3(thePlayer.position.x, transform.position.y, thePlayer.position.z);
        Quaternion _playerRot = Quaternion.Euler(90f, thePlayer.eulerAngles.y, 0f);
        transform.position = _playerPos;
        //transform.rotation = _playerRot;
        //Debug.Log("the players rot " + thePlayer.rotation.y);
        //Debug.Log("the cameras rot " + transform.rotation.y);

    }

    /*public void addToMap(mapIcon _mi)
    {
        mapIcons.Add(_mi);
    }*/
}
