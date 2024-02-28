using Oculus.Interaction.Input;
using UnityEngine;

[RequireComponent(typeof(HandPhysicsCapsules))]
public class HandManager : MonoBehaviour
{
    [SerializeField] private float slapSpeedThreshold = 2.0f;
    public enum HandState
    {
        Normal,
        Slap,
        Grab
    }
    private HandState state;
    // Start is called before the first frame update

    private HandPhysicsCapsules physics;

    private bool isPreparingAttack = false;

    void Start()
    {
        physics = GetComponent<HandPhysicsCapsules>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckHandState();
    }

    void CheckHandState()
    {
        if (state == HandState.Grab)
        {
            return;
        }
        Vector3 velocity = physics.GetHandVelocity();
        if (isPreparingAttack && velocity.sqrMagnitude > slapSpeedThreshold) // && (handPose == openHand || handPose == closedHand || handPose == pointing)
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
    public void SetNormalState()
    {
        SetHandState(HandState.Normal);
    }
    public void SetGrabState()
    {
        SetHandState(HandState.Grab);
    }
    public void SetSlapState()
    {
        SetHandState(HandState.Slap);
    }

    public void SetHandState(HandState state)
    {
        this.state = state;
    }

    public void PrepareAttack(bool value)
    {
        isPreparingAttack = value;
    }
}
