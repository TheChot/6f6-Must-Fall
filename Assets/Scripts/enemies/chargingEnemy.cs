using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chargingEnemy : MonoBehaviour
{

    playerDetector pd;
    public float chargeRadius = 10;
    bool playerInRange;
    public LayerMask whatIsPlayer;
    public int damageToDo = 2;

    public Transform chargePoint;
    bool isShooting = false;
    bool canFire;// for the animation

    public float damping = 1;

    public float shootDelayTime = 3;
    float shootDelayTimeReset;
    public float chargeTime = 2;
    float chargeTimeReset;

    
    bool canCharge;

    public float chargeSpeed = 25;
    bool isCharging;

    Animator anim;
    enemyMovementSimple ems;
    // Start is called before the first frame update
    void Start()
    {
        shootDelayTimeReset = shootDelayTime;
        chargeTimeReset = chargeTime;
        ems = GetComponent<enemyMovementSimple>();

        anim = GetComponent<Animator>();
        pd = GetComponent<playerDetector>();
    }

    // Update is called once per frame
    void Update()
    {

        playerInRange = pd.playerDetected;

        //playerInRange = _player.Length > 0;
        //turn to face the player if not shooting

        if (playerInRange && !isShooting)
        {
            canCharge = true;
            ems.canMove = false;
        }
        if (canCharge && !isShooting)
        {
            // limit rot to y axis
            Vector3 _theTarget = new Vector3(pd.thePlayer.position.x, transform.position.y, pd.thePlayer.position.z);
            //turn the enemy body
            transform.LookAt(_theTarget);

            shootDelayTime -= Time.deltaTime;

            if (shootDelayTime <= 0)
            {
                chargeThePlayer();
            }

        }

        
        //charge the player
        if (isCharging && isShooting)
        {
            transform.position += transform.forward * chargeSpeed * Time.deltaTime;
            
            Collider[] _player = Physics.OverlapSphere(chargePoint.position, chargeRadius, whatIsPlayer);

            if (_player.Length > 0)
            {
                for (int i = 0; i < _player.Length; i++)
                {
                    _player[i].gameObject.GetComponent<playerController>().damageThePlayer(damageToDo);

                }

                //isCharging = false;
                chargeTime = -1;
            }
            
        }        

        if (isShooting && isCharging)
        {
            chargeTime -= Time.deltaTime;
            if (chargeTime <= 0)
            {
                chargeTime = chargeTimeReset;
                isShooting = false;
                canFire = false;
                isCharging = false;
                shootDelayTime = shootDelayTimeReset;
                canCharge = false;
                ems.canMove = true;
                
            }
        }
        anim.SetBool("detected", canCharge);
        anim.SetBool("charging", isCharging);
    }

    void chargeThePlayer()
    {


        isShooting = true;

        //limit rotation to y axis
        Vector3 _theTarget = new Vector3(pd.thePlayer.position.x, transform.position.y, pd.thePlayer.position.z);
        //turn the enemy body
        transform.LookAt(_theTarget);

        //GameObject _bullet = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
        //anim.SetBool("charging", true);
        isCharging = true;

    }

    public void charge()
    {
        isCharging = true;
    }

    void OnDrawGizmosSelected()
    {
        // Display the detection radius when selected
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(chargePoint.position, chargeRadius);
    }

    public void stopAnim()
    {
        canFire = true;
    }
}
