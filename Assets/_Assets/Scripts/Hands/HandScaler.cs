using Oculus.Interaction;
using Oculus.Interaction.Input;
using UnityEngine;

public class HandScaler : MonoBehaviour
{
    private HandVisual handVisual;
    private HandPhysicsCapsules handPhysics;

    private Transform wrist = null;

    [SerializeField] private Vector3 staticOffset = Vector3.zero;
    [SerializeField] private Transform bodyTransform;

    [SerializeField] private float minDistance = 1;
    [SerializeField] private float maxDistance = 50f;

    [SerializeField] private float scaleRatio = 1f;

    private Vector3 offset = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        handVisual = GetComponent<HandVisual>();
        handPhysics = GetComponent<HandPhysicsCapsules>();

        wrist = transform.GetChild(0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!handVisual.Hand.IsTrackedDataValid)
        {
            offset = Vector3.zero;
            return;
        }
        /*float distance = wrist.localPosition.magnitude;

        if (distance < minDistance)
        {
            distance = minDistance;
        }
        else if (distance > maxDistance)
        {
            distance = maxDistance;
        }
        scaleFactor = distance;

        offset = ((1 - scaleFactor) * minDistance + scaleFactor * maxDistance) * (wrist.position - offset - transform.position).normalized;*/

        offset = scaleRatio * (wrist.position - bodyTransform.position - offset - staticOffset);

        handVisual.Offset = offset;
        handPhysics.Offset = offset;

        //previousPosition = wrist.position;
    }
}
