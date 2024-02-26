using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class PatrolManager : MonoBehaviour
{
    [SerializeField]
    private GameObject character;
    [SerializeField]
    private GameObject characterHolder;

    [SerializeField]
    private Vector2 speedRange;

    [SerializeField]
    private Vector2 respawnRange;

    private void Start()
    {
        StartCoroutine(SpawnPatrol());
    }

    private IEnumerator SpawnPatrol()
    {
        yield return new WaitForSeconds(Random.Range(respawnRange.x, respawnRange.y));

        (Vector3, Vector3) patrolPos = BuildingManager.Instance.GetPatrolPos();
        Vector3 startingPos = patrolPos.Item1;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(patrolPos.Item1, out hit, 1.0f, NavMesh.AllAreas))
            startingPos = hit.position;
        GameObject a = Instantiate(character, startingPos, Quaternion.identity, characterHolder.transform);
        a.GetComponent<CharacterPatrol>().Setup(Random.Range(speedRange[0], speedRange[1]), patrolPos.Item2);
    }
}
