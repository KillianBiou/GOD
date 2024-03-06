using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(NavMeshAgent))]
public class KnightBehaviour : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GameObject knight;
    [SerializeField]
    private GameObject horse;

    private NavMeshAgent agent;
    private Animator animK;
    private Animator animH;

    private CharacterState characterState;
    private Building target;

    private void Awake()
    {

        agent = GetComponent<NavMeshAgent>();

        animK = knight.GetComponent<Animator>();
        animH = horse.GetComponent<Animator>();

        animK.SetTrigger("KnightRun");
        animH.SetTrigger("HorseRun");

        horse.GetComponent<PeonRagdoll>().ChangeState(RagdollPreset.LOCKED_ALL);
        knight.GetComponent<PeonRagdoll>().ChangeState(RagdollPreset.LOCKED_ALL);
    }
    

    private void SetupColor()
    {
        (Color, Color, Color, Color, Color) randomColors1 = CharacterRandomizer.instance.FetchRandomSetup();
        (Color, Color, Color, Color, Color) randomColors2 = CharacterRandomizer.instance.FetchRandomSetup();

        knight.transform.Find("Peon").GetComponent<Renderer>().material.SetColor("_ColorSkin", randomColors1.Item1);
        knight.transform.Find("Peon").GetComponent<Renderer>().material.SetColor("_ColorCloth", randomColors1.Item2);
        knight.transform.Find("Peon").GetComponent<Renderer>().material.SetColor("_ColorPant", randomColors1.Item3);
        knight.transform.Find("Peon").GetComponent<Renderer>().material.SetColor("_ColorShose", randomColors1.Item4);
        knight.transform.Find("Peon").GetComponent<Renderer>().material.SetColor("_ColorHair", randomColors1.Item5);

        horse.transform.Find("Peon").GetComponent<Renderer>().material.SetColor("_ColorSkin", randomColors2.Item1);
        horse.transform.Find("Peon").GetComponent<Renderer>().material.SetColor("_ColorCloth", randomColors2.Item2);
        horse.transform.Find("Peon").GetComponent<Renderer>().material.SetColor("_ColorPant", randomColors2.Item3);
        horse.transform.Find("Peon").GetComponent<Renderer>().material.SetColor("_ColorShose", randomColors2.Item4);
        horse.transform.Find("Peon").GetComponent<Renderer>().material.SetColor("_ColorHair", randomColors2.Item5);
    }

    private void Start()
    {
        target = BuildingManager.Instance.GetPotentialTarget();
        SetupColor();

        agent.speed = 10f;
        agent.SetDestination(target.exit.transform.position);
        characterState = CharacterState.RUNATTACK;
    }
    
    private void Update()
    {
        if (DistanceToTarget() <= 3f && characterState != CharacterState.ATTACK)
        {
            agent.isStopped = true;
            animK.SetTrigger("KnightAttack");
            animH.SetTrigger("HorseAttackWait");
            characterState = CharacterState.ATTACK;
        }
        if (DistanceToTarget() > 3f && characterState != CharacterState.RUNATTACK)
        {
            agent.SetDestination(target.exit.transform.position);
            animK.SetTrigger("KnightRun");
            animH.SetTrigger("HorseRun");
            agent.isStopped = false;
            characterState = CharacterState.RUNATTACK;
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Separate();
        }
    }

    private float DistanceToTarget()
    {
        (Vector2, Vector2) tPose = (new Vector2(transform.position.x, transform.position.z), new Vector2(target.exit.transform.position.x, target.exit.transform.position.z));
        return Vector2.Distance(tPose.Item1, tPose.Item2);
    }

    public void Separate()
    {
        VikingBehaviour hBehaviour = horse.AddComponent<VikingBehaviour>();
        VikingBehaviour kBehaviour = knight.AddComponent<VikingBehaviour>();

        hBehaviour.transform.SetParent(transform.parent);
        kBehaviour.transform.SetParent(transform.parent);

        hBehaviour.skipStart = true;
        kBehaviour.skipStart = true;

        kBehaviour.GetComponent<NavMeshAgent>().enabled = true;
        hBehaviour.GetComponent<NavMeshAgent>().enabled = true;

        ResetAllTriggers(animH);
        ResetAllTriggers(animK);

        animH.Play("Idle");
        animK.Play("Idle");

        hBehaviour.Setup(target);
        kBehaviour.Setup(target);
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

    public void Grab(Transform target)
    {
        throw new System.NotImplementedException();
    }

    public void Slap()
    {
        Destroy(gameObject);
        throw new System.NotImplementedException();
    }
}
