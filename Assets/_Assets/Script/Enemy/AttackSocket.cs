using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AttackSocket : MonoBehaviour
{
    private Building target;

    private void GetTarget()
    {
        target = GetComponentInParent<KnightBehaviour>().GetTarget();
    }

    public void TriggerAttack()
    {
        if (target == null || target.GetBuildingState() == BuildingState.DESTROYED)
            GetTarget();
        if (target.GetBuildingState() != BuildingState.DESTROYED)
            target.TakeDamage(1);
        else
            target = BuildingManager.Instance.GetPotentialTarget();
    }
}
