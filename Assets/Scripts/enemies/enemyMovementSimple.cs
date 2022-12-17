using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovementSimple : MonoBehaviour
{
    public float speed;
    public bool moveZ;
    public bool moveY;
    public bool moveX;
    float minPos;
    float maxPos;
    public float stepsTo;
    public bool moveNegative;
    playerDetector pd;
    shockwaveEnemy se;
    bool changeDir;
    public bool isShockwave;
    [HideInInspector]
    public bool canMove;
    Animator anim;
    public bool hasWalkingAnim;
    // Start is called before the first frame update
    void Start()
    {
        pd = GetComponent<playerDetector>();
        se = GetComponent<shockwaveEnemy>();
        anim = GetComponent<Animator>();
        canMove = true;
        if (!moveNegative)
        {
            if (moveZ)
            {
                minPos = transform.position.z;
                maxPos = minPos + stepsTo;

            } 
            else if (moveY)
            {
                minPos = transform.position.y;
                maxPos = minPos + stepsTo;
            }
            else if (moveX)
            {
                minPos = transform.position.x;
                maxPos = minPos + stepsTo;
            }
        } else
        {
            if (moveZ)
            {
                maxPos = transform.position.z;
                minPos = maxPos - stepsTo;
                

            }
            else if (moveY)
            {
                maxPos = transform.position.y;
                minPos = maxPos - stepsTo;
            }
            else if (moveX)
            {
                maxPos = transform.position.x;
                minPos = maxPos - stepsTo; 
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool _playerDetected = false;
        

        if(isShockwave)
        {
            _playerDetected = se.canShock;
        } else
        {
            _playerDetected = pd.playerDetected;
        }

        if (!_playerDetected)
        {
            
            if (moveZ)
            {
                if(transform.position.z >= maxPos)
                {
                    changeDir = false;
                } 
                else if (transform.position.z <= minPos)
                {
                    changeDir = true;
                }

            }
            else if (moveY)
            {
                if (transform.position.y >= maxPos)
                {
                    changeDir = false;
                }
                else if (transform.position.y <= minPos)
                {
                    changeDir = true;
                }
            }
            else if (moveX)
            {
                if (transform.position.x >= maxPos)
                {
                    changeDir = false;
                }
                else if (transform.position.x <= minPos)
                {
                    changeDir = true;
                }
            }
        } 
        
    }

    void FixedUpdate()
    {
        bool _playerDetected = false;

        if (isShockwave)
        {
            _playerDetected = se.canShock;
        }
        else
        {
            _playerDetected = pd.playerDetected;
        }

        if (!_playerDetected && canMove)
        {
            if (hasWalkingAnim)
            {
                anim.SetBool("iswalking", true);
            }
            if (moveZ)
            {
                if (changeDir)
                {
                    transform.position += (new Vector3(0, 0, speed) * Time.fixedDeltaTime);
                    //limit rotation to y axis
                    Vector3 _theTarget = new Vector3(transform.position.x, transform.position.y, maxPos);
                    //turn the enemy body
                    transform.LookAt(_theTarget);
                }
                else
                {
                    transform.position -= (new Vector3(0, 0, speed) * Time.fixedDeltaTime);
                    //limit rotation to y axis
                    Vector3 _theTarget = new Vector3(transform.position.x, transform.position.y, minPos);
                    //turn the enemy body
                    transform.LookAt(_theTarget);
                }


            }
            else if (moveY)
            {
                if (changeDir)
                {
                    transform.position += (new Vector3(0, speed, 0) * Time.fixedDeltaTime);
                    //limit rotation to y axis
                    Vector3 _theTarget = new Vector3(transform.position.x, maxPos, transform.position.z);
                    //turn the enemy body
                    transform.LookAt(_theTarget);
                }
                else
                {
                    transform.position -= (new Vector3(0, speed, 0) * Time.fixedDeltaTime);
                    //limit rotation to y axis
                    Vector3 _theTarget = new Vector3(transform.position.x, minPos, transform.position.z);
                    //turn the enemy body
                    transform.LookAt(_theTarget);
                }
            }
            else if (moveX)
            {
                if (changeDir)
                {
                    transform.position += (new Vector3(speed, 0, 0) * Time.fixedDeltaTime);
                    //limit rotation to y axis
                    Vector3 _theTarget = new Vector3(maxPos, transform.position.y, transform.position.z);
                    //turn the enemy body
                    transform.LookAt(_theTarget);
                }
                else
                {
                    transform.position -= (new Vector3(speed, 0, 0) * Time.fixedDeltaTime);
                    //limit rotation to y axis
                    Vector3 _theTarget = new Vector3(minPos, transform.position.y, transform.position.z);
                    //turn the enemy body
                    transform.LookAt(_theTarget);
                }
            }
        }
        else
        {

            if (hasWalkingAnim)
            {
                anim.SetBool("iswalking", false);
            }
        }

    }
}
