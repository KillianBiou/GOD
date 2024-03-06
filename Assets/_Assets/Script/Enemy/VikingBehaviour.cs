using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(PeonRagdoll))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class VikingBehaviour : Character
{
    private NavMeshAgent agent;
    private CharacterState characterState;
    private PeonRagdoll peonRagdoll;
    private Building target;

    private Animator animator;

    public bool skipStart = false;

    private void Awake()
    {
        peonRagdoll = GetComponent<PeonRagdoll>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        peonRagdoll.ChangeState(RagdollPreset.LOCKED_ALL);
    
    }

    private void Start()
    {
        if (!skipStart)
        {
            base.Start();

            target = BuildingManager.Instance.GetPotentialTarget();

            agent.speed = 10f;
            agent.SetDestination(target.exit.transform.position);
            ChangeState(CharacterState.RUNATTACK);
        }
    }

    private void Update()
    {
        if (scheduleKO)
        {
            ChangeState(CharacterState.KO);
        }
        if(DistanceToTarget() <= 3f && characterState != CharacterState.ATTACK)
        if (target.GetBuildingState() == BuildingState.DESTROYED)
            target = BuildingManager.Instance.GetPotentialTarget();

        if (DistanceToTarget() <= 3f && characterState != CharacterState.ATTACK)
        {
            agent.isStopped = true;
            ChangeState(CharacterState.ATTACK);
        }
        if (DistanceToTarget() > 3f && characterState != CharacterState.RUNATTACK)
        {
            agent.SetDestination(target.exit.transform.position);
            agent.isStopped = false;
            ChangeState(CharacterState.RUNATTACK);
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
        (Vector2, Vector2) tPose=  (new Vector2(transform.position.x, transform.position.z), new Vector2(target.exit.transform.position.x, target.exit.transform.position.z));
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
                scheduleKO = false;
                Destroy(gameObject, 5f);
                break;
            case CharacterState.RUNATTACK:
                animator.SetTrigger("AttackRun");
                peonRagdoll.ChangeState(RagdollPreset.LOCKED_ALL);
                break;
            case CharacterState.ATTACK:
                animator.SetTrigger("Attack");
                peonRagdoll.ChangeState(RagdollPreset.LOCKED_ALL);
                break;
            default:
                peonRagdoll.ChangeState(RagdollPreset.LOCKED_ALL);
                agent.isStopped = false;
                break;
        }
        characterState = state;
    }

    public void Setup(Building target)
    {
        this.target = target;
        agent.speed = 10f;
        agent.SetDestination(target.exit.transform.position);
        ChangeState(CharacterState.RUNATTACK);
    }

    public void TriggerAttack()
    {
        Debug.Log("ATTACKED BUILDING");
        if (target.GetBuildingState() != BuildingState.DESTROYED)
            target.TakeDamage(1);
        else
            target = BuildingManager.Instance.GetPotentialTarget();
    }

    private void OnDestroy()
    {
        PatrolManager.instance.KillPeon();
    }
}
