using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyMoveTowardsPosition : MonoBehaviour
{
    [SerializeField] Vector3 targetPosition = Vector3.zero;
    [SerializeField] float speed = 1f;
    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }
    private void FixedUpdate()
    {
        Vector3 direction = transform.position - targetPosition;
        rb.velocity = - speed * direction.normalized;

        if (direction.sqrMagnitude < 0.1f)
        {
            transform.position = targetPosition + new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f));
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, targetPosition);
        Gizmos.DrawSphere(targetPosition, 0.2f);
    }
}
