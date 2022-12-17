using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batteryHost : MonoBehaviour
{
    public batteryTerminal[] bts;
    public int btActiveCount;
    public GameObject lightBridge;
    public Material material;
    public float lightBridgeCount = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < bts.Length; i++)
        {
            bts[i].bh = this;
        }

        material = lightBridge.GetComponent<Renderer>().sharedMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        if (btActiveCount == bts.Length)
        {
            if (!lightBridge.activeInHierarchy)
            {
                lightBridge.SetActive(true);
            }            
        }

        if (lightBridge.activeInHierarchy && lightBridgeCount > 0)
        {
            lightBridgeCount -= Time.deltaTime;
            material.SetFloat("Vector1_1A69BC2F", lightBridgeCount);
            //material.SetVector("_timer", new Vector4(lightBridgeCount, 0));
        }
    }

}
