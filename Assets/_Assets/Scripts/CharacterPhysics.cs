using Oculus.Interaction;
using UnityEngine;

public class CharacterPhysics : MonoBehaviour
{
    private Collider characterCollider;
    private Rigidbody rb;
    private bool thrown = false;

    private float throwSpeedThreshold = 20f;

    Vector3 previousPosition = Vector3.zero;

    private PhysicsGrabbable grabbable;
    // Start is called before the first frame update
    void Start()
    {
        grabbable = transform.GetChild(0).GetComponent<PhysicsGrabbable>();
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

        if (speed < throwSpeedThreshold || !rb.isKinematic)
        {
            return;
        }
        Debug.Log("THROW");
        grabbable.RemoveSelection();
        grabbable.gameObject.SetActive(false);
        thrown = true;
        rb.constraints = RigidbodyConstraints.None;
        characterCollider.enabled = false;
        rb.isKinematic = false;
        Invoke("ResetCollision", 0.2f);
        rb.velocity = 100f * movementDirection;
        Invoke("Death", 3f);
    }

    void ResetCollision()
    {
        grabbable.gameObject.SetActive(true);
        characterCollider.enabled = true;
        thrown = false;
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
