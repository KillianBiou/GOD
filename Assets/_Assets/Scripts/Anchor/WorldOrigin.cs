using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldOrigin : MonoBehaviour
{
    // Start is called before the first frame update
    public static Transform anchorTracked;
    public static Transform LeftAnchor { get; set; }
    public static Transform RightAnchor { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        anchorTracked = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (LeftAnchor != null && RightAnchor != null)
        {
            transform.position = new Vector3(LeftAnchor.position.x, 0, LeftAnchor.position.z);
            transform.rotation = Quaternion.LookRotation(new(RightAnchor.position.x - LeftAnchor.position.x, 0.0f, RightAnchor.position.z - LeftAnchor.position.z), Vector3.up);
        }
    }
}
