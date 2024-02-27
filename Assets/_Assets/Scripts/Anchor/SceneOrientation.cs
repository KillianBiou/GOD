using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneOrientation : MonoBehaviour
{
    void Start()
    {
        transform.SetParent(CreateWorldOrigin.worldOrigin.transform, false);
    }
}
