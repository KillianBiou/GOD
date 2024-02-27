using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class HandManager : MonoBehaviour
{
    [SerializeField] private float slapSpeedThreshold = 1.0f;

    private Rigidbody rb;
    public enum HandState
    {
        Normal,
        Slap,
        Grab
    }
    private HandState state;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        IInteractable interactable;
        if (!collision.collider.TryGetComponent<IInteractable>(out interactable))
        {
            return;
        }

        switch (state)
        {
            case HandState.Normal:
                break;
            case HandState.Slap:
                interactable.Slap();
                break;
            case HandState.Grab:
                interactable.Grab();
                break;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckHandState();
    }

    void CheckHandState()
    {
        Debug.Log(rb.velocity.sqrMagnitude);
        if (rb.velocity.sqrMagnitude > slapSpeedThreshold) // && (handPose == openHand || handPose == closedHand || handPose == pointing)
        {
            state = HandState.Slap;
        }
        else if (true) // handPose == grab
        {
            state = HandState.Slap;
        }
        else
        {
            state = HandState.Normal;
        }
    }

    public HandState GetCurrentHandState()
    {
        return state;
    }
}
