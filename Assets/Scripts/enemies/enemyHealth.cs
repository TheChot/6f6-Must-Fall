using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{

    public int health;
    public Transform infoPoint;
    public GameObject healthInfo;
    // Start is called before the first frame update
    public void deductHealth(int _health)
    {
        health -= _health;
        GameObject _healthInfo = Instantiate(healthInfo, infoPoint.position, infoPoint.rotation);
        _healthInfo.gameObject.GetComponent<healthText>().messageText.text = _health.ToString();
    }

}
