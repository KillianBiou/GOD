using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance;

    [SerializeField]
    private int buildingMaxHP;

    [SerializeField]
    private List<Building> buildings = new List<Building>();

    private List<Building> validBuildings = new List<Building>();

    private void Awake()
    {
        Instance = this;
    }

    public void RegisterBuilding(Building buidling)
    {
        if (buidling.transform.Find("Exit"))
        {
            buildings.Add(buidling);
            BecameValid(buidling);
        }
        buidling.Setup(buildingMaxHP);
    }

    public (Vector3, Vector3) GetPatrolPos()
    {
        int index0 = Random.Range(0, validBuildings.Count);
        int index1;

        do
        {
            index1 = Random.Range(0, validBuildings.Count);
        } while (index1 == index0);


        return (validBuildings[index0].exit.position, validBuildings[index1].exit.position);
    }

    public void BecameValid(Building building)
    {
        if(!validBuildings.Contains(building))
            validBuildings.Add(building);
    }

    public void BecameInvalid(Building building)
    {
        validBuildings.Remove(building);
    }

    public Building GetPotentialTarget()
    {
        return validBuildings[Random.Range(0, validBuildings.Count)];
    }
}
