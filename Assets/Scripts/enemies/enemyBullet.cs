using UnityEngine;

public class enemyBullet : MonoBehaviour
{
    public float speed = 10f;    
    
    void FixedUpdate()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

    }    
}
