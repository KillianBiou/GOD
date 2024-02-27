using UnityEngine;

public class MoveTowardsPosition : MonoBehaviour
{
    [SerializeField] Vector3 targetPosition = Vector3.zero;
    private void Update()
    {
        transform.position -= 2 * Time.deltaTime * (transform.position - targetPosition).normalized;

        if (transform.position.sqrMagnitude < 0.1f)
        {
            transform.position = new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f));
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, targetPosition);
        Gizmos.DrawSphere(targetPosition, 0.2f);
    }
}
