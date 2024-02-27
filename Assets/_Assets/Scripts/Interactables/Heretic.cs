using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heretic : Enemy, IInteractable
{
    public void Grab()
    {

    }

    public void Slap()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Debug.Log("HERESY ENDS");
    }
}
