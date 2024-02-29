using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int attackRating = 0;
    [SerializeField] private LayerMask targetLayerMask;
    
    Rigidbody rb;
    [SerializeField] private float speedThresholdToExplode = 3f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!Physics.CheckSphere(transform.position, 0.5f, targetLayerMask))
        {
            return;
        }

        Village.Instance.SubstractGodPower(attackRating);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (rb.velocity.sqrMagnitude > speedThresholdToExplode)
        {
            //Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.5f);
    }
}
