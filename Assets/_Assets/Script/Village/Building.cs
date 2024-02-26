using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public Transform exit;

    private void Awake()
    {
        exit = transform.Find("Exit");
    }

    private void Start()
    {
        BuildingManager.Instance.RegisterBuilding(this);
    }
}
