using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int attackRating = 0;
    [SerializeField] private LayerMask targetLayerMask;
    
    private void FixedUpdate()
    {
        if (!Physics.CheckSphere(transform.position, 0.5f, targetLayerMask))
        {
            return;
        }

        Village.Instance.SubstractGodPower(attackRating);
    }
}
