using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootingEnemy : MonoBehaviour
{
    //enemyMovement em;
    
    playerDetector pd;
    public float detectRadius;
    bool playerDetected;

    public GameObject bullet;
    public Transform shootPoint;
    bool isShooting = false;
    bool canFire;// for the animation
    
    public float damping = 1;

    public float shootDelayTime = 3;
    float shootDelayTimeReset;
    public float relaxTime = 2;
    float relaxTimeReset;

    public bool isRapidFire;
    public int numberOfBullets = 5;
    public float bulletGap = 0.1f;
    

    

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

        playerDetected = pd.playerDetected;

        if (playerDetected && !isShooting)
        {
            shootDelayTime -= Time.deltaTime;

            if(shootDelayTime <= 0)
            {
                shootThePlayer();
            }
            anim.SetBool("detected", true);

        }

        if (isShooting && canFire)
        {
            relaxTime -= Time.deltaTime;
            if(relaxTime <= 0)
            {
                relaxTime = relaxTimeReset;
                isShooting = false;
                canFire = false;
                shootDelayTime = shootDelayTimeReset;
                anim.SetBool("detected", false);
            }
        }

        //anim.SetBool("detected", canFire);

    }

    void shootThePlayer()
    {
        //Turn slowly
        /*Vector3 lookPos = theTarget.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);*/

        //parent look at to pelvis or spine armature

        isShooting = true;
        //canFire = false; // for animation
        //limit rotation to y axis
        Vector3 _theTarget = new Vector3(pd.thePlayer.position.x, transform.position.y, pd.thePlayer.position.z);
        //turn the enemy body
        transform.LookAt(_theTarget);
        //turn the enemy weapon
        shootPoint.LookAt(pd.thePlayer);


        //GameObject _bullet = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
        anim.SetTrigger("shoot");

        /*if (!targetInRange)
        {
            isShooting = false;
            
        }*/
    }

    public void shootBullet()
    {
        if (!isRapidFire)
        {
            GameObject _bullet = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
        }
        else
        {
            StartCoroutine(rapidBullets());
        }
        
    }

    IEnumerator rapidBullets()
    {
        for (int i = 0; i < numberOfBullets; i++)
        {
            yield return new WaitForSeconds(bulletGap);
            //limit rotation to y axis
            Vector3 _theTarget = new Vector3(pd.thePlayer.position.x, transform.position.y, pd.thePlayer.position.z);
            //turn the enemy body
            transform.LookAt(_theTarget);
            //turn the enemy weapon
            shootPoint.LookAt(pd.thePlayer);
            GameObject _bullet = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
        }
    }

    public void stopAnim() 
    {
        canFire = true;
    }

    
}
