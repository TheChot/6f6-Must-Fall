using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombMain : MonoBehaviour
{
    [HideInInspector]public int weakPointCount;
    [HideInInspector] public int weakPointsHit;
    [HideInInspector] public float timeToDestroy;
    [HideInInspector] public renderMapIcon rm;
    //[HideInInspector] public mapIcon mi;
    public GameObject weakPoint;
    [HideInInspector] public bool itemDestroyed;
    public bool isHidden;
    bool isDestroyed;


    // Start is called before the first frame update
    void Start()
    {
        rm = GetComponent<renderMapIcon>();
        //mi = rm.theIcon.GetCo`mponent<mapIcon>();
        timeToDestroy = Random.Range(timedManager.instance.minTime, timedManager.instance.maxTime);

        if (!isHidden)
        {
            missionManager.instance.numberOfPublicRescues++;
        }
        else
        {
            missionManager.instance.numberOfPrivateRescues++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeToDestroy -= Time.deltaTime;
        rm.iconText.text = Mathf.Round(timeToDestroy).ToString();
        rm.mi.timer = timeToDestroy;
        if (timeToDestroy <= 0 && !itemDestroyed && !isDestroyed)
        {
            missionManager.instance.numberOfExplodes++;
            Destroy(rm.theIcon);
            weakPoint.SetActive(false);
            scoreManager.instance.strikes++;
            uicontroller.instance.noti = "BOMB EXPLPODED";
            uicontroller.instance.isNoti = true;
            isDestroyed = true;
            //Destroy(gameObject);

        }

        if(weakPointsHit == weakPointCount && !itemDestroyed)
        {
            itemDestroyed = true;
            Destroy(rm.theIcon);
            weakPoint.SetActive(false);
            missionManager.instance.totalDefused++;
            uicontroller.instance.noti = "BOMB DEFUSED";
            uicontroller.instance.isNoti = true;
            scoreManager.instance.score += scoreManager.instance.scoreToAdd;
        }
    }
}
