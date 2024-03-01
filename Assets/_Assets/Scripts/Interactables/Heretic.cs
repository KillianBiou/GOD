using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heretic : Enemy, IInteractable
{
    public void Grab(Transform target)
    {
        Debug.LogWarning("Heretic grab");
    }

    public void Slap()
    {
        Debug.LogWarning("Heretic slap");
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        //Debug.Log("HERESY ENDS");
    }
}
