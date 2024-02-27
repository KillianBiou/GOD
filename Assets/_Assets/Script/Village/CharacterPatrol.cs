using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterPatrol : Character
{
    private NavMeshAgent agent;
    private Vector3 destination;

    public void Setup(float speed, Vector3 destination)
    {
        agent = GetComponent<NavMeshAgent>();
        this.destination = destination;
        agent.speed = speed;
        agent.SetDestination(destination);
    }

    private void Update()
    {
        Debug.Log(DistanceToTarget());
        if(DistanceToTarget() <= .5f)
        {
            Destroy(gameObject);
        }
    }

    private float DistanceToTarget()
    {
        (Vector2, Vector2) tPose=  (new Vector2(transform.position.x, transform.position.z), new Vector2(destination.x, destination.z));
        return Vector2.Distance(tPose.Item1, tPose.Item2);
    }
}
