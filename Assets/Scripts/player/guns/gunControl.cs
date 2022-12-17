using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class gunControl : MonoBehaviour
{
    GameObject muzzleFlash;
    [HideInInspector] public int ammoCount;
    float delayTime;
    float delayTimeReset;
    Transform muzzlePoint;
    float rayLength;
    //bool barrelType;
    bool singleFire;
    int dmgPoints;
    string gunAnim;
    bool isAnim;
    bool isMelee;
    bool isShotgun;
    

    bool isFiring;
    bool startCountDown;
    public gunManager gm;
    public gunInventory gi;

    public LayerMask whatIsEnemies;
    public Transform camPos;
    Animator anim;

    public float shotgunRadius;
    public float boxXY = 2;
    public float meleeRadius;
    public Transform shotgunPoint;
    public Transform meleePoint;

    public Image reticule;
    public float rayLength1;
    playerController playerCon;
    public GameObject bloodSpatterEffect;
    public GameObject groundHitEffect;
    uicontroller uicon;

    playerMovement pm;
    // Start is called before the first frame update
    void Start()
    {
        gm = gunManager.instance;
        gm.gc = this;
        gi = gm.gi;
        anim = GetComponent<Animator>();
        playerCon = GetComponent<playerController>();
        uicon = uicontroller.instance;
        pm = GetComponent<playerMovement>();

    }

    // Update is called once per frame
    void Update()
    {
        uicon.amooCountText.gameObject.SetActive(!isMelee);
        uicon.clipSizeText.gameObject.SetActive(!isMelee);

        //the shotgun question? how do we approach shotguns?
        RaycastHit hit;
        //add code that detects an enemy and turns reticule red
        if (Physics.Raycast(camPos.position, camPos.TransformDirection(Vector3.forward), out hit, rayLength))
        {
            if (hit.collider.gameObject.CompareTag("enemy"))
            {
                reticule.color = Color.red;
            }
        }
        else
        {
            reticule.color = Color.white;
        }


        if (gi.weaponEquipped)
        {

            if (Input.GetMouseButton(0) && pm.canControl)
            {
                if (ammoCount > 0 || isMelee)
                {
                    if (!isFiring && !isAnim)
                    {
                        
                        if (!isMelee)
                        {
                            GameObject _muzzleFlash = Instantiate(muzzleFlash, muzzlePoint.position, muzzlePoint.rotation);
                            ammoCount -= 1;
                            gm.gi.deductAmmo(1);
                            _muzzleFlash.transform.SetParent(muzzlePoint);
                        }
                        
                        hitEnemy();
                        isFiring = true;
                        startCountDown = true;
                        anim.SetTrigger(gunAnim + "_fire");
                        anim.SetBool("fingeron", true);
                    }
                    if (startCountDown)
                    {
                        delayTime -= Time.deltaTime;

                    }
                    if (!singleFire && ammoCount > 0)
                    {
                        if (delayTime <= 0 && !isAnim)
                        {
                            delayTime = delayTimeReset;
                            GameObject _muzzleFlash = Instantiate(muzzleFlash, muzzlePoint.position, muzzlePoint.rotation);
                            _muzzleFlash.transform.SetParent(muzzlePoint);
                            hitEnemy();
                            ammoCount -= 1;
                            gm.gi.deductAmmo(1);
                            anim.SetTrigger(gunAnim + "_fire");

                        }
                    }

                    if (!singleFire && ammoCount <= 0)
                    {
                        anim.SetBool("fingeron", false);
                    }

                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            isFiring = false;
            startCountDown = false;
            delayTime = delayTimeReset;
            anim.SetBool("fingeron", false);
        }
    }

    //Update in gun inventory code
    public void assignVal(int _ammo, float _delayTime, GameObject _muzzleFlash, Transform _muzzlePoint, float _rayLength, bool _singleFire, int _dmgPoints, string _gunAnim, bool _isMelee, bool _isShotgun)
    {
        ammoCount = _ammo;
        delayTime = _delayTime;
        delayTimeReset = delayTime;
        muzzleFlash = _muzzleFlash;
        muzzlePoint = _muzzlePoint;
        rayLength = _rayLength;
        singleFire = _singleFire;
        dmgPoints = _dmgPoints;
        gunAnim = _gunAnim;
        isMelee = _isMelee;
        isShotgun = _isShotgun;
        gameObject.GetComponent<playerController>().currentGun = _gunAnim;
        //playerCon.currentGun = _gunAnim;
    }

    void hitEnemy()
    {
        RaycastHit hit;
        if (isShotgun)
        {
            //Collider[] hitColliders = Physics.OverlapSphere(shotgunPoint.position, shotgunRadius, whatIsEnemies);
            shotgunPoint.localPosition = new Vector3(shotgunPoint.localPosition.x, shotgunPoint.localPosition.y, ((rayLength / 2) + 1));
            Collider[] hitColliders = Physics.OverlapBox(shotgunPoint.position, new Vector3(boxXY/2 , boxXY/2, rayLength / 2), shotgunPoint.rotation, whatIsEnemies);

            if (hitColliders.Length > 0)
            {
                for (int i = 0; i < hitColliders.Length; i++)
                {
                    //Debug.Log("hit Enemy");
                    enemyHealth _eh = hitColliders[i].gameObject.GetComponent<enemyHealth>();

                    if (_eh != null)
                    {
                        _eh.deductHealth(dmgPoints);
                        Debug.Log("hit Enemy");
                        
                        if (Physics.Raycast(camPos.position, camPos.TransformDirection(Vector3.forward), out hit, rayLength))
                        {
                            Instantiate(bloodSpatterEffect, hit.point, Quaternion.LookRotation(hit.normal));
                        }
                    }
                    else
                    {
                        if (Physics.Raycast(camPos.position, camPos.TransformDirection(Vector3.forward), out hit, rayLength))
                        {
                            Instantiate(groundHitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                        }
                    }
                    //hit.collider
                    //Destroy(hit.collider.gameObject);
                }


            }
        } 
        else if (isMelee)
        {
            Collider[] hitColliders = Physics.OverlapSphere(meleePoint.position, meleeRadius, whatIsEnemies);

            if (hitColliders.Length > 0)
            {
                for (int i = 0; i < hitColliders.Length; i++)
                {
                    //Debug.Log("hit Enemy");
                    enemyHealth _eh = hitColliders[i].gameObject.GetComponent<enemyHealth>();

                    if (_eh != null)
                    {
                        _eh.deductHealth(dmgPoints);
                        Debug.Log("hit Enemy");
                        if (Physics.Raycast(camPos.position, camPos.TransformDirection(Vector3.forward), out hit, rayLength))
                        {
                            Instantiate(bloodSpatterEffect, hit.point, Quaternion.LookRotation(hit.normal));
                        }                            
                    }
                    else
                    {
                        if (Physics.Raycast(camPos.position, camPos.TransformDirection(Vector3.forward), out hit, rayLength))
                        {
                            Instantiate(groundHitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                        }
                        
                    }
                    //hit.collider
                    //Destroy(hit.collider.gameObject);
                }


            }
        } 
        else if (Physics.Raycast(camPos.position, camPos.TransformDirection(Vector3.forward), out hit, rayLength))
        {
            //Debug.Log("hit Enemy");
            enemyHealth _eh = hit.collider.gameObject.GetComponent<enemyHealth>();

            if(_eh != null)
            {
                _eh.deductHealth(dmgPoints);
                Debug.Log("hit Enemy");
                Instantiate(bloodSpatterEffect, hit.point, Quaternion.LookRotation(hit.normal));
            } else
            {
                Instantiate(groundHitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }

            
            //hit.collider
            //Destroy(hit.collider.gameObject);

        }
    }

    public void setAnimTrue()
    {
        isAnim = true;
    }
    public void setAnimFalse()
    {
        isAnim = false;
    }

    void OnDrawGizmosSelected()
    {
        // Display the detection radius when selected
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(shotgunPoint.position, shotgunRadius);
        Gizmos.DrawWireCube(shotgunPoint.position, new Vector3(boxXY, boxXY, shotgunRadius));
        Gizmos.DrawWireSphere(meleePoint.position, meleeRadius);
        Gizmos.DrawLine(camPos.position, new Vector3(camPos.position.x, camPos.position.y, camPos.position.z + rayLength1));
    }
}
