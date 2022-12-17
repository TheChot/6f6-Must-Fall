using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class mapIcon : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector] public bool important;
    [HideInInspector] public TextMeshPro iconText;
    [HideInInspector] public Transform toFollow;
    [HideInInspector] public Color spriteColor;
    public SpriteRenderer sr;
    [HideInInspector] public bool isTimed;
    [HideInInspector] public float timer;
    [HideInInspector] public float maxDistanceFromMap;
    [HideInInspector] public float mapInterval;
    float mapHeight;
    float distanceFromMap;
    [HideInInspector] public bool isPlayer;
    [HideInInspector] public bool isMcguffin;

    void Start()
    {
        iconText.text = "";         
        sr.color = spriteColor;
        //mapController.instance.addToMap(this);
        mapHeight = mapController.instance.mapHeight;
        transform.SetParent(mapController.instance.mapParent);
        mapInterval = mapController.instance.interval;
    }

    void LateUpdate()
    {
        float _distanceFromMap = mapHeight;
        if (isTimed)
        {
            _distanceFromMap -= (timer / 5) * mapInterval;//eg 4 = .8
            //mapheight + d
        }

        if (important && !isTimed)
        {
            _distanceFromMap -= (maxDistanceFromMap - mapInterval);
            //184 = 10 - 0.2 * 2 || 9.6
        }

        if (important && !isTimed && isMcguffin)
        {
            _distanceFromMap -= (maxDistanceFromMap - (mapInterval * 2));

        }

        if (!important)
        {
            _distanceFromMap -= maxDistanceFromMap;
        }
        if (isPlayer)
        {
            transform.rotation = Quaternion.Euler(0f, toFollow.eulerAngles.y, 0f);
        }
        
        if (important)
        {
            if(toFollow.position.z > mapController.instance.top.position.z)
            {
                transform.position = new Vector3(transform.position.x, _distanceFromMap, mapController.instance.top.position.z);
            }
            else if (toFollow.position.z < mapController.instance.bottom.position.z)
            {
                transform.position = new Vector3(transform.position.x, _distanceFromMap, mapController.instance.bottom.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, _distanceFromMap, toFollow.position.z);
            }
            

            if (toFollow.position.x > mapController.instance.right.position.x)
            {
                transform.position = new Vector3(mapController.instance.right.position.x, _distanceFromMap, transform.position.z);
            }
            else if (toFollow.position.x < mapController.instance.left.position.x)
            {
                transform.position = new Vector3(mapController.instance.left.position.x, _distanceFromMap, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(toFollow.position.x, _distanceFromMap, transform.position.z);
            }
        } else
        {
            transform.position = new Vector3(toFollow.position.x, _distanceFromMap, toFollow.position.z);
        }

        
    }
}
