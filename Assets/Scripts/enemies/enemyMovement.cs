using UnityEngine;
using UnityEngine.AI;

public class enemyMovement : MonoBehaviour
{
    NavMeshAgent agent;
    [HideInInspector] public Vector3 theTarget;
    public bool isStopped;
    [HideInInspector] public bool patrolMode;
    public float patrolRadius;
    public float moveTimer;
    float moveTimerReset;
    public float checkTimer = 0.2f;
    float checkTimerReset;
    Vector3 maxX;
    Vector3 minX;
    Vector3 maxZ;
    Vector3 minZ;
    Vector3 pointToGo;
    int pointCount = 0;
    bool nextDestination;
    bool hasStopped;
    Vector3 currentPosition;
    Vector3 pastPosition;
    [HideInInspector] public float stopDistance = 0;




    void Start() 
    {
        moveTimerReset = moveTimer;
        checkTimerReset = checkTimer;
        agent = GetComponent<NavMeshAgent>();
        
        maxX = new Vector3(transform.position.x + patrolRadius, transform.position.y, transform.position.z);
        minX = new Vector3(transform.position.x - patrolRadius, transform.position.y, transform.position.z);
        maxZ = new Vector3(transform.position.x, transform.position.y, transform.position.z + patrolRadius);
        minZ = new Vector3(transform.position.x, transform.position.y, transform.position.z - patrolRadius);
        
        theTarget = maxZ;
        //Debug.Log(theTarget);
        //Debug.Log(transform.position);
        nextDestination = true;
        pastPosition = transform.position;
        patrolMode = true;
        

    }

    // Update is called once per frame
    void Update()
    {
       

        if (!isStopped)
        {
            agent.stoppingDistance = stopDistance;
            
            if (patrolMode)
            {
                stopDistance = 0;
                
                if (pastPosition != currentPosition)
                {
                    checkTimer -= Time.deltaTime;

                    if(checkTimer <= 0)
                    {
                        pastPosition = currentPosition;
                        checkTimer = checkTimerReset;
                        

                        
                    }
                    
                }
                if (checkTimer > checkTimerReset * 0.9f)
                {
                    currentPosition = transform.position;
                }

                if (!hasStopped)
                {
                    checkTimer -= Time.deltaTime;
                }
                

                
                if (checkTimer <= 0)
                {
                    checkTimer = checkTimerReset;
                    if (pastPosition == currentPosition)
                    {
                        hasStopped = true;
                    }
                }
                
                
                
                if (hasStopped && nextDestination)
                {
                    moveTimer -= Time.deltaTime;
                    
                    if(moveTimer <= 0)
                    {
                        pointCount++;
                        nextDestination = false;
                        hasStopped = false;
                        checkTimer = checkTimerReset;

                        if (pointCount > 3)
                        {
                            pointCount = 0;
                        }
                    }                  
                    

                }
                if (pointCount == 0 && !nextDestination)
                {
                    theTarget = maxZ;
                    nextDestination = true;
                }
                else if (pointCount == 1 && !nextDestination)
                {
                    theTarget = maxX;
                    nextDestination = true;
                }
                else if (pointCount == 2 && !nextDestination)
                {
                    theTarget = minZ;
                    nextDestination = true;
                }
                else if (pointCount == 3 && !nextDestination)
                {
                    theTarget = minX;
                    nextDestination = true;
                                       
                }

                if (!hasStopped)
                {
                    agent.SetDestination(theTarget);
                    moveTimer = moveTimerReset;

                }

            }
            else
            {
                agent.SetDestination(theTarget);
            }



        }
        
        agent.isStopped = isStopped;
        
        
    }

    public void resetMovement()
    {
        maxX = new Vector3(transform.position.x + patrolRadius, transform.position.y, transform.position.z);
        minX = new Vector3(transform.position.x - patrolRadius, transform.position.y, transform.position.z);
        maxZ = new Vector3(transform.position.x, transform.position.y, transform.position.z + patrolRadius);
        minZ = new Vector3(transform.position.x, transform.position.y, transform.position.z - patrolRadius);

        checkTimer = checkTimerReset;
        moveTimer = moveTimerReset;
        hasStopped = false;
        theTarget = maxZ;
    }
    
}
