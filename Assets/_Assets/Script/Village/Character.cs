using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(PeonRagdoll))]
public class Character : MonoBehaviour, IInteractable
{
    [SerializeField]
    private Renderer characterRenderer;

    protected Transform root;

    protected bool scheduleKO = false;

    protected void Awake()
    {
        root = transform.FindChildRecursive("Forward");
    }

    protected void Start()
    {
        foreach (var b in GetComponent<PeonRagdoll>().bonesMapping)
        {
            if (b.type == BoneType.Head)
                b.bone.AddComponent<RagdollEntity>().Setup(this);
        }

        if (characterRenderer)
        {
            (Color, Color, Color, Color, Color) randomColors = CharacterRandomizer.instance.FetchRandomSetup();

            characterRenderer.material.SetColor("_ColorSkin", randomColors.Item1);
            characterRenderer.material.SetColor("_ColorCloth", randomColors.Item2);
            characterRenderer.material.SetColor("_ColorPant", randomColors.Item3);
            characterRenderer.material.SetColor("_ColorShose", randomColors.Item4);
            characterRenderer.material.SetColor("_ColorHair", randomColors.Item5);
        }
    }



    public void Grab(Transform target)
    {
        Debug.Log("");
        throw new System.NotImplementedException();
    }

    public void Slap()
    {
        scheduleKO = true;
        Debug.Log("I got slapped");
    }
}
