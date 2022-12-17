using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shockwaveEnemy : MonoBehaviour
{
    playerDetector pd;
    public float shockRadius = 10;
    bool playerInRange;
    public LayerMask whatIsPlayer;
    public int damageToDo = 2;

    public Transform shockPoint;
    bool isShooting = false;
    bool canFire;// for the animation

    public float damping = 1;

    public float shootDelayTime = 3;
    float shootDelayTimeReset;
    public float relaxTime = 2;
    float relaxTimeReset;

    bool stopShock;
    public bool canShock;

    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        shootDelayTimeReset = shootDelayTime;
        relaxTimeReset = relaxTime;


        anim = GetComponent<Animator>();
        pd = GetComponent<playerDetector>();
    }

    // Update is called once per frame
    void Update()
    {
        //playerDetected = pd.playerDetected;
        Collider[] _player = Physics.OverlapSphere(shockPoint.position, shockRadius, whatIsPlayer);
        playerInRange = _player.Length > 0;
        //turn to face the player if not shooting
        if (playerInRange && !isShooting)
        {
            // limit rot to y axis
            Vector3 _theTarget = new Vector3(pd.thePlayer.position.x, transform.position.y, pd.thePlayer.position.z);
            //turn the enemy body
            transform.LookAt(_theTarget);
        }
        
        

        if (playerInRange && !isShooting && !stopShock)
        {
            canShock = true;    

        }

        if (canShock && !isShooting)
        {
            shootDelayTime -= Time.deltaTime;

            if (shootDelayTime <= 0)
            {
                shockThePlayer();
            }
        }

        if (isShooting && canFire)
        {
            relaxTime -= Time.deltaTime;
            if (relaxTime <= 0)
            {
                relaxTime = relaxTimeReset;
                isShooting = false;
                canFire = false;
                canShock = false;
                shootDelayTime = shootDelayTimeReset;
            }
        }
    }

    void shockThePlayer()
    {
        

        isShooting = true;
        
        //limit rotation to y axis
        Vector3 _theTarget = new Vector3(pd.thePlayer.position.x, transform.position.y, pd.thePlayer.position.z);
        //turn the enemy body
        transform.LookAt(_theTarget);

        //GameObject _bullet = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
        anim.SetTrigger("shock");

    }

    public void shockwaveBlast()
    {
        Collider[] _player = Physics.OverlapSphere(shockPoint.position, shockRadius, whatIsPlayer);
        for (int i = 0; i < _player.Length; i++)
        {
            _player[i].gameObject.GetComponent<playerController>().damageThePlayer(damageToDo);
        }
    }
    public void stopAnim()
    {
        canFire = true;
    }

    void OnDrawGizmosSelected()
    {
        // Display the detection radius when selected
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shockRadius);
    }
}
