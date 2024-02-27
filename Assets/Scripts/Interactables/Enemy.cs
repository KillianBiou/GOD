using UnityEngine;

public class Enemy : MonoBehaviour, IInteractable
{
    [SerializeField] private int attackRating = 0;
    [SerializeField] private LayerMask targetLayerMask;
    public void Grab()
    {

    }

    public void Slap()
    {
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (!Physics.CheckSphere(transform.position, 0.5f, targetLayerMask))
        {
            return;
        }

        Village.Instance.SubstractGodPower(attackRating);
    }
}
