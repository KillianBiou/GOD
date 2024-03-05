using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(PeonRagdoll))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class StaticCharacter : Character
{
    private Vector3 startPos;
    private CharacterState characterState;

    [SerializeField]
    private CharacterState  baseState;
    [SerializeField]
    private GameObject toolToInstantiate;

    private PeonRagdoll peonRagdoll;
    private NavMeshAgent agent;
    private Animator animator;

    private void Awake()
    {
        peonRagdoll = GetComponent<PeonRagdoll>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        startPos = transform.position;

        if (toolToInstantiate != null)
            Instantiate(toolToInstantiate, transform.FindChildRecursive("R_LowerArm"));

        ChangeState(baseState);
    }


    private void Update()
    {
        if(DistanceToTarget() <= 1.5f)
        {
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
        (Vector2, Vector2) tPose=  (new Vector2(transform.position.x, transform.position.z), new Vector2(startPos.x, startPos.z));
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
                break;
            case CharacterState.GRABBED:
                peonRagdoll.ChangeState(RagdollPreset.FREE_ARM_HEAD);
                animator.ResetTrigger("Walk");
                animator.SetTrigger("Grab");
                agent.isStopped = true;
                break;
            case CharacterState.KO:
                peonRagdoll.ChangeState(RagdollPreset.FREE_ALL);
                animator.enabled = false;
                agent.isStopped = true;
                Destroy(gameObject, 5f);
                break;
            case CharacterState.REST:
                peonRagdoll.ChangeState(RagdollPreset.LOCKED_ALL);
                animator.SetTrigger("Rest");
                agent.isStopped = true;
                break;
            case CharacterState.FARM:
                peonRagdoll.ChangeState(RagdollPreset.LOCKED_ALL);
                animator.SetTrigger("Farm");
                agent.isStopped = true;
                break;
            default:
                peonRagdoll.ChangeState(RagdollPreset.LOCKED_ALL);
                agent.isStopped = false;
                break;
        }
        characterState = state;
    }

    private void OnDestroy()
    {
        PatrolManager.instance.KillPeon();
    }
}
