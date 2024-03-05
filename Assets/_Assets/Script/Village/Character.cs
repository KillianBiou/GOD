using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    private Renderer characterRenderer;

    protected Transform root;

    protected void Awake()
    {
        root = transform.FindChildRecursive("Forward");
    }

    protected void Start()
    {
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
}
