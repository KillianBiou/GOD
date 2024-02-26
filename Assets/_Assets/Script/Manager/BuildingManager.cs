using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance;

    [SerializeField]
    private List<Building> buildings = new List<Building>();

    private void Awake()
    {
        Instance = this;
    }

    public void RegisterBuilding(Building buidling)
    {
        buildings.Add(buidling);
    }

    public (Vector3, Vector3) GetPatrolPos()
    {
        int index0 = Random.Range(0, buildings.Count);
        int index1;

        do
        {
            index1 = Random.Range(0, buildings.Count);
        }while(index1 == index0);


        return (buildings[index0].exit.position, buildings[index1].exit.position);
    }
}
