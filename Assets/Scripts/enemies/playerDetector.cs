using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDetector : MonoBehaviour
{
    [HideInInspector] public Transform thePlayer;
    public float detectRadius;
    public bool playerDetected;
    public LayerMask whatIsPlayer;

    public float maxDistance = 50f;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = levelController.instance.thePlayer.transform;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //anim.SetBool("detected", playerDetected);
        if (!playerDetected)
        {
            //if the player is in range
            Collider[] _player = Physics.OverlapSphere(transform.position, detectRadius, whatIsPlayer);
            //playerInRange = _player.Length > 0;
            bool _playerInRange = _player.Length > 0;

            if (_playerInRange)
            {
                playerDetected = true;
                Vector3 _theTarget = new Vector3(thePlayer.position.x, transform.position.y, thePlayer.position.z);
                //turn the enemy body
                transform.LookAt(_theTarget);
            }
        }

        //if player is far from the enemy the enemy atops attacking
        float dist = Vector3.Distance(thePlayer.position, transform.position);

        if (dist > maxDistance)
        {
            playerDetected = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        // Display the detection radius when selected
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}
