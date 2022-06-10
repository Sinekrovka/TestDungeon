using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class StepController: MonoBehaviour
{
    private static bool step;

    public static void GenerateFirstStep()
    {
        step = Convert.ToBoolean(Random.Range(0, 2));
    }

    public static bool Step
    {
        get => step;
        set => step = value;
    }
}