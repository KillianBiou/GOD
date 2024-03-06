using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public enum BuildingState
{
    FULL_LIFE,
    DAMAGED1,
    DAMAGED2,
    DAMAGED3,
    DESTROYED
}

public class Building : MonoBehaviour
{
    public Transform exit;
    private BuildingState buildingState;
    private int maxHp;
    private int hp;

    private void Awake()
    {
        exit = transform.Find("Exit"); BuildingManager.Instance.RegisterBuilding(this);
    }

    public void Setup(int maxHp)
    {
        this.maxHp = maxHp;
        this.hp = maxHp;
        ChangeBuildingState(BuildingState.FULL_LIFE);
    }

    public BuildingState GetBuildingState()
    {
        return buildingState;
    }

    /* Add trigger in dedicated section */
    private void ChangeBuildingState(BuildingState state)
    {
        if(state == buildingState) 
            return;

        switch (state)
        {
            case BuildingState.FULL_LIFE:
                BuildingManager.Instance.BecameValid(this);
                break;
            case BuildingState.DAMAGED1:
                BuildingManager.Instance.BecameValid(this);
                break;
            case BuildingState.DAMAGED2:
                BuildingManager.Instance.BecameValid(this);
                break;
            case BuildingState.DAMAGED3:
                BuildingManager.Instance.BecameValid(this);
                break;
            case BuildingState.DESTROYED:
                BuildingManager.Instance.BecameInvalid(this);
                break;
        }

        this.buildingState = state;
    }

    public void TakeDamage(int damage)
    {
        this.hp -= damage;

        if (hp < 0)
        {
            ChangeBuildingState(BuildingState.DESTROYED);
        }
        else if (hp < Mathf.Floor(maxHp / 4) * 1)
        {
            ChangeBuildingState(BuildingState.DAMAGED3);
        }
        else if (hp < Mathf.Floor(maxHp / 4) * 2)
        {
            ChangeBuildingState(BuildingState.DAMAGED2);
        }
        else if (hp < Mathf.Floor(maxHp / 4) * 3)
        {
            ChangeBuildingState(BuildingState.DAMAGED1);
        }
        else
        {
            ChangeBuildingState(BuildingState.FULL_LIFE);
        }
    }
}
