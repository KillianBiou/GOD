using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRandomizer : MonoBehaviour
{
    [SerializeField]
    private List<Color> skinPossibleColors;
    [SerializeField]
    private List<Color> textilePossibleColor;
    [SerializeField]
    private List<Color> hairPossibleColor;

    public static CharacterRandomizer instance;

    private void Awake()
    {
        instance = this;
    }

    public (Color, Color, Color, Color, Color) FetchRandomSetup()
    {
        return (skinPossibleColors[Random.Range(0, skinPossibleColors.Count)],
                textilePossibleColor[Random.Range(0, textilePossibleColor.Count)],
                textilePossibleColor[Random.Range(0, textilePossibleColor.Count)],
                textilePossibleColor[Random.Range(0, textilePossibleColor.Count)],
                hairPossibleColor[Random.Range(0, hairPossibleColor.Count)]);
    }
}
