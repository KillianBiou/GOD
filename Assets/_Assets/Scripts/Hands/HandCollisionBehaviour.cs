using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCollisionBehaviour : MonoBehaviour
{
    private Rigidbody rb;

    private float pushSpeedThreshold = 3f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("HAND COLLISION");
        IInteractable interactable = null;
        if (!collision.collider.TryGetComponent<IInteractable>(out interactable))
        {
            return;
        }

        //PUSH

        Vector3 collisionDirection = (collision.rigidbody.worldCenterOfMass - collision.contacts[0].point).normalized;

        if (rb.velocity.magnitude < pushSpeedThreshold)
        {
            return;
        }

        if (Vector3.Dot(collisionDirection, Vector3.up) > - 0.3f)
        {
            collision.rigidbody.constraints = RigidbodyConstraints.None;
            collision.rigidbody.isKinematic = false;
            collision.rigidbody.velocity += 5f * collisionDirection;
            return;
        }

        //SLAP

        Debug.Log("SLAP");
        interactable.Slap();
        }
    

/*    private void OnCollisionExit(Collision collision)
    {
        //Vector3 expulsionDirection = (collision.transform.position - transform.position).normalized;
        //collision.rigidbody.AddForce(expulsionDirection, ForceMode.Impulse);
        if (collision.rigidbody.velocity.sqrMagnitude < 1.2f)
        {
            return;
        }
        collision.rigidbody.velocity += 0.5f * handTransform.up + 0.5f * handTransform.forward;
    }*/
}
