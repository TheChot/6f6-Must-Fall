using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class renderMapIcon : MonoBehaviour
{
    GameObject theMapIcon;
    [HideInInspector] public TextMeshPro iconText;
    public Color iconColor;
    public bool isImportant;
    [HideInInspector] public GameObject theIcon;
    public bool isTimed;
    public bool isPlayer = false;
    [HideInInspector] public mapIcon mi;
    public bool isMcguffin;


    // Start is called before the first frame update
    void Start()
    {
        Vector3 _mapLoc = new Vector3(transform.position.x, mapController.instance.mapHeight, transform.position.z);
        Quaternion _mapRot = Quaternion.Euler(0,0,0);
        theMapIcon = mapController.instance.theMapIcon;
        GameObject _mapIcon;
        if (isPlayer)
        {
            theMapIcon = mapController.instance.playerMapIcon;
            _mapIcon = Instantiate(theMapIcon, _mapLoc, _mapRot);
        }
        else
        {
            _mapIcon = Instantiate(theMapIcon, _mapLoc, _mapRot);
        }
        
        theIcon = _mapIcon;
        mi = _mapIcon.GetComponent<mapIcon>();
        iconText = mi.iconText;
        mi.toFollow = this.transform;
        mi.spriteColor = iconColor;
        mi.important = isImportant;
        mi.isTimed = isTimed;
        mi.isPlayer = isPlayer;
        mi.isMcguffin = isMcguffin;

    }    

    void OnDestroy()
    {
        Destroy(theIcon);
    }
}
