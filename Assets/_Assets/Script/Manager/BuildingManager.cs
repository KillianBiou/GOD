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
}
