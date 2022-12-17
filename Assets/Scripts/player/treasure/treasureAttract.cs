using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treasureAttract : MonoBehaviour
{
    public float playerRadius;
    Transform thePlayer;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = levelController.instance.thePlayer.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float _distance = Vector3.Distance(transform.position, thePlayer.position);

        if(_distance < playerRadius)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, thePlayer.position, step);
        }
    }
}
