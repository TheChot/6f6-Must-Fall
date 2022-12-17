using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rescueCharacter : MonoBehaviour
{
    public float rescueTime;
    public bool isTimed;
    public bool isOnMap = true;
    public renderMapIcon rm;
    public mapIcon mi;
    bool isDestroyed;

    // Start is called before the first frame update
    void Start()
    {
        if (isOnMap)
        {
            missionManager.instance.numberOfPublicRescues++;
        } else
        {
            missionManager.instance.numberOfPrivateRescues++;
        }

        rm = GetComponent<renderMapIcon>();
        rescueTime = Random.Range(timedManager.instance.minTime, timedManager.instance.maxTime);


    }

    // Update is called once per frame
    void Update()
    {
        if(mi == null)
        {
            mi = rm.theIcon.GetComponent<mapIcon>();
        }
        if (isTimed)
        {
            rescueTime -= Time.deltaTime;
            rm.iconText.text = Mathf.Round(rescueTime).ToString();
            mi.timer = rescueTime;
            if(rescueTime <= 0 && !isDestroyed)
            {
                isDestroyed = true;
                scoreManager.instance.strikes++;
                missionManager.instance.numberOfDeaths++;
                uicontroller.instance.noti = "RESCUE FAILED";
                uicontroller.instance.isNoti = true;
                Destroy(rm.theIcon);
                Destroy(gameObject);
                

            }
        }
    }
}
