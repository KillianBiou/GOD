using UnityEngine;

public class MoveTowardsPosition : MonoBehaviour
{
    [SerializeField] Vector3 targetPosition = Vector3.zero;
    [SerializeField] float speed = 0.5f;

    private Vector3 initialPosition = Vector3.zero;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (!rb.isKinematic)
        {
            return;
        }

        rb.MovePosition(transform.position - speed * (transform.position - targetPosition).normalized);

        if ((transform.position - targetPosition).sqrMagnitude < 0.1f)
        {
            transform.position = initialPosition;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, targetPosition);
        Gizmos.DrawSphere(targetPosition, 0.2f);
    }
}
