using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Way : MonoBehaviour
{
    public Transform pointA; 
    public Transform pointB;  
    public Transform pointC;  
    private UnityEngine.AI.NavMeshAgent agent; 
    private Transform currentTarget;  

    void Start()
    {
        pointA.position = new Vector3(40, 2 , 55);
        pointB.position = new Vector3(60, 2, 60 );
        pointC.position = new Vector3(50, 2, 63);
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();  
        currentTarget = pointA;  
        agent.SetDestination(currentTarget.position);  
    }

    void Update()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            if (currentTarget == pointA)
            {
                currentTarget = pointB; 
            }
            else if (currentTarget == pointB) 
            {
                    currentTarget = pointC;  
             }
            else
            {
                    currentTarget = pointA;
             }

                agent.SetDestination(currentTarget.position);  
        } 
    }
}
