using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollEntity : MonoBehaviour, IInteractable
{
    [SerializeField]
    private Character parent;

    public void Setup(Character parent)
    {
        this.parent = parent;
    }

    public void Grab(Transform target)
    {
        parent.Grab(target);
    }

    public void Slap()
    {
        parent.Slap();
    }
}
