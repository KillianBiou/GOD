using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(PeonRagdoll))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class StaticCharacter : Character
{
    private Vector3 startPos;
    private Quaternion baseRotation;
    private CharacterState characterState;

    [SerializeField]
    private CharacterState  baseState;
    [SerializeField]
    private GameObject toolToInstantiate;

    private GameObject toolRef;

    private PeonRagdoll peonRagdoll;
    private NavMeshAgent agent;
    private Animator animator;
    private bool animationLock = false;

    private void Awake()
    {
        base.Awake();
        peonRagdoll = GetComponent<PeonRagdoll>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        startPos = transform.position;
        baseRotation = transform.rotation;

        if (toolToInstantiate != null)
            toolRef = Instantiate(toolToInstantiate, transform.FindChildRecursive("R_LowerArm"));

        ChangeState(baseState);
    }


    private void Update()
    {
        if (scheduleKO)
        {
            ChangeState(CharacterState.KO);
        }

        if(animationLock)
        {
            return;
        }

        if (DistanceToTarget() <= 1f && characterState != baseState)
        {
            ResetAllTriggers(animator);
            if (toolToInstantiate != null)
                toolRef = Instantiate(toolToInstantiate, transform.FindChildRecursive("R_LowerArm"));
            agent.isStopped = true;
            transform.position = startPos;
            transform.rotation = baseRotation;
            ChangeState(baseState);
        }
        if (DistanceToTarget() > 1f && characterState != CharacterState.WALKING)
        {
            ResetAllTriggers(animator);
            if (toolRef)
                Destroy(toolRef);
            agent.SetDestination(startPos);
            agent.isStopped = false;
            ChangeState(CharacterState.WALKING);
            animator.Play("PeonWalking");
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

                NavMeshHit hit;
                if (!NavMesh.SamplePosition(transform.position, out hit, Mathf.Infinity, 0))
                {
                    Destroy(gameObject);
                }
                transform.position = hit.position;

                agent.isStopped = false;
                agent.enabled = true;
                break;
            case CharacterState.GRABBED:
                peonRagdoll.ChangeState(RagdollPreset.FREE_ARM_HEAD);
                animator.ResetTrigger("Walk");
                animator.SetTrigger("Grab");
                agent.isStopped = true;
                agent.enabled = false;
                break;
            case CharacterState.KO:
                scheduleKO = false;
                peonRagdoll.ChangeState(RagdollPreset.FREE_ALL);
                animator.enabled = false;
                agent.isStopped = true;
                StartCoroutine(ScheduleGetUp());
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
            case CharacterState.DISCUSSION:
                peonRagdoll.ChangeState(RagdollPreset.LOCKED_ALL);
                float totalFrames = animator.GetCurrentAnimatorClipInfo(0)[0].clip.frameRate;
                float randomFrame = Random.Range(0, totalFrames);
                float normalizedTime = (float)randomFrame / totalFrames;
                animator.Play("PeonDiscussion", 0, normalizedTime);
                agent.isStopped = true;
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
        animationLock = true;
        yield return new WaitForSeconds(3f);
        Debug.Log(Vector3.Dot(root.forward, Vector3.down));
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

        animationLock = false;
    }

    private void OnDestroy()
    {
        PatrolManager.instance.KillPeon();
    }

    private void ResetAllTriggers(Animator animator)
    {
        foreach (var param in animator.parameters)
        {
            if (param.type == AnimatorControllerParameterType.Trigger)
            {
                animator.ResetTrigger(param.name);
            }
        }
    }
}
