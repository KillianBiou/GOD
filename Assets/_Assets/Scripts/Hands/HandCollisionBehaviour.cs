using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCollisionBehaviour : MonoBehaviour
{
    private HandManager handManager;

    private Transform handTransform;

    private Rigidbody rb;

    private float pushSpeedThreshold = 3.0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        handTransform = transform.GetChild(0);
    }
    private void OnCollisionEnter(Collision collision)
    {
        IInteractable interactable = null;
        /*if (!collision.collider.TryGetComponent<IInteractable>(out interactable))
        {
            return;
        }

        if (!handManager)
        {
            return;
        }*/

        Debug.Log(rb.velocity.magnitude);

        Vector3 collisionDirection = (collision.rigidbody.worldCenterOfMass - collision.contacts[0].point).normalized;
        if (rb.velocity.magnitude > pushSpeedThreshold && Vector3.Dot(collisionDirection, Vector3.up) > - 0.3f)
        {
            collision.rigidbody.velocity += 5f * collisionDirection;
            return;
        }

        HandManager.HandState state = handManager.GetCurrentHandState();

        switch (state)
        {
            case HandManager.HandState.Normal:
                break;
            case HandManager.HandState.Slap:
                interactable.Slap();
                break;
            case HandManager.HandState.Grab:
                interactable.Grab(transform);
                break;
        }
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
    public void SetHandManager(HandManager handManager)
    {
        this.handManager = handManager;
    }
}
