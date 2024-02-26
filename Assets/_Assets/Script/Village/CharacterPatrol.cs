using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterPatrol : Character
{
    private NavMeshAgent agent;

    public void Setup(float speed, Vector3 destination)
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.SetDestination(destination);
    }
}
