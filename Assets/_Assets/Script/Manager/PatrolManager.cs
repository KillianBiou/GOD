using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;


public class PatrolManager : MonoBehaviour
{
    public List<GameObject> possibleCharacters;
    public GameObject characterHolder;
    public Vector2 speedRange;

    [SerializeField]
    private Vector2 respawnRange;

    [SerializeField]
    private int maxPeon;
    private int currentPeon;

    public static PatrolManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(SpawnPatrol());
    }

    private IEnumerator SpawnPatrol()
    {
        yield return new WaitForSeconds(Random.Range(respawnRange.x, respawnRange.y));
        if(currentPeon < maxPeon)
        {
            (Vector3, Vector3) patrolPos = BuildingManager.Instance.GetPatrolPos();
            Vector3 startingPos = patrolPos.Item1;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(patrolPos.Item1, out hit, Mathf.Infinity, NavMesh.AllAreas))
            {
                startingPos = hit.position;
            }
            GameObject a = Instantiate(possibleCharacters[Random.Range(0, possibleCharacters.Count)], startingPos, Quaternion.identity, characterHolder.transform);
            a.GetComponent<CharacterPatrol>().Setup(Random.Range(speedRange[0], speedRange[1]), patrolPos.Item2);
            currentPeon++;
        }

        StartCoroutine(SpawnPatrol());
    }

    public void KillPeon()
    {
        currentPeon--;
    }
}
