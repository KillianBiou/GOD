using Oculus.Interaction;
using UnityEngine;

public class CharacterPhysics : MonoBehaviour
{
    private Collider characterCollider;
    private Rigidbody rb;
    private bool thrown = false;

    private float throwSpeedThreshold = 0.001f;

    Vector3 previousPosition = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        characterCollider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        previousPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (thrown)
        {
            return;
        }

        Vector3 movementDirection = transform.position - previousPosition;
        float speed = movementDirection.magnitude;

        previousPosition = transform.position;

        if (speed < throwSpeedThreshold)
        {
            return;
        }
        Debug.Log("THROW");
        thrown = true;
        rb.constraints = RigidbodyConstraints.None;
        characterCollider.enabled = false;
        rb.isKinematic = false;
        Invoke("ResetCollision", 0.2f);
        rb.velocity = 10 * movementDirection;
    }

    void ResetCollision()
    {
        characterCollider.enabled = true;
        thrown = false;
    }
}
