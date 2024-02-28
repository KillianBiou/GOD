using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCollisionBehaviour : MonoBehaviour
{
    private HandManager handManager;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hand collision");
        IInteractable interactable;
        if (!collision.collider.TryGetComponent<IInteractable>(out interactable))
        {
            return;
        }

        if (!handManager)
        {
            Debug.Log("Pas de hand manager");
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

    public void SetHandManager(HandManager handManager)
    {
        this.handManager = handManager;
    }
}
