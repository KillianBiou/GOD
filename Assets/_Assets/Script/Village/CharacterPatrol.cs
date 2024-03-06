using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum CharacterState
{
    WALKING,
    GRABBED,
    KO,
    RUNATTACK,
    ATTACK,
    TALK,
    FARM,
    CHOP,
    REST,
    DISCUSSION,
    GETUP_FACE,
    GETUP_BACK,
    UNKNOWN
}

[RequireComponent(typeof(PeonRagdoll))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class CharacterPatrol : Character
{
    private NavMeshAgent agent;
    private Vector3 destination;
    private CharacterState characterState;
    private PeonRagdoll peonRagdoll;

    private Animator animator;
    private bool animationLock = false;

    private void Awake()
    {
        base.Awake();

        peonRagdoll = GetComponent<PeonRagdoll>();
        animator = GetComponent<Animator>();

        peonRagdoll.ChangeState(RagdollPreset.LOCKED_ALL);
    }

    public void Setup(float speed, Vector3 destination)
    {
        agent = GetComponent<NavMeshAgent>();
        this.destination = destination;
        agent.speed = speed;
        agent.SetDestination(destination);
        ChangeState(CharacterState.WALKING);
    }

    private void Update()
    {
        if(DistanceToTarget() <= 1.5f)
        {
            Destroy(gameObject);
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            ChangeState(CharacterState.WALKING);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            ChangeState(CharacterState.GRABBED);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            ChangeState(CharacterState.KO);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeState(CharacterState.UNKNOWN);
        }
    }

    private float DistanceToTarget()
    {
        (Vector2, Vector2) tPose=  (new Vector2(transform.position.x, transform.position.z), new Vector2(destination.x, destination.z));
        return Vector2.Distance(tPose.Item1, tPose.Item2);
    }

    public void ChangeState(CharacterState state)
    {
        switch (state)
        {
            case CharacterState.WALKING:
                peonRagdoll.ChangeState(RagdollPreset.FREE_ARM);
                animator.ResetTrigger("Grab");
                animator.SetTrigger("Walk");
                agent.isStopped = false;
                agent.updatePosition = true;
                break;
            case CharacterState.GRABBED:
                peonRagdoll.ChangeState(RagdollPreset.FREE_ARM_HEAD);
                animator.ResetTrigger("Walk");
                animator.SetTrigger("Grab");
                agent.isStopped = true;
                agent.updatePosition = false;
                break;
            case CharacterState.KO:
                peonRagdoll.ChangeState(RagdollPreset.FREE_ALL);
                animator.enabled = false;
                agent.isStopped = true;
                StartCoroutine(ScheduleGetUp());
                break;
            case CharacterState.GETUP_FACE:
                animator.enabled = true;
                peonRagdoll.ChangeState(RagdollPreset.LOCKED_ALL);
                animator.Play("StandUpFace");
                agent.isStopped = true;
                break;
            case CharacterState.GETUP_BACK:
                animator.enabled = true;
                peonRagdoll.ChangeState(RagdollPreset.LOCKED_ALL);
                animator.Play("StandUpBack");
                agent.isStopped = true;
                break;
            default:
                peonRagdoll.ChangeState(RagdollPreset.LOCKED_ALL);
                agent.isStopped = false;
                break;
        }
        characterState = state;
    }

    private IEnumerator ScheduleGetUp()
    {
        NavMeshHit hit;
        if (!NavMesh.SamplePosition(root.position, out hit, Mathf.Infinity, NavMesh.AllAreas))
        {
            Destroy(gameObject);
        }

        yield return new WaitForSeconds(3f);

        agent.updatePosition = true;

        if (Vector3.Dot(root.forward, Vector3.down) < 0f)
        {
            ChangeState(CharacterState.GETUP_BACK);
            yield return new WaitForSeconds(3f);
        }
        else
        {
            ChangeState(CharacterState.GETUP_FACE);
            yield return new WaitForSeconds(2f);
        }

        agent.isStopped = false;

        ChangeState(CharacterState.WALKING);
    }

    private void OnDestroy()
    {
        PatrolManager.instance.KillPeon();
    }
}
