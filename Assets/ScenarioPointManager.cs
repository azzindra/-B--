using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioPointManager : MonoBehaviour
{
    public enum PointType { Good, Bad, Neutral }

    public static ScenarioPointManager Instance;

    private int goodPoints = 0;
    private int badPoints = 0;
    private int neutralPoints = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Supaya tetap hidup antar scene
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddPoint(PointType type)
    {
        switch (type)
        {
            case PointType.Good:
                goodPoints++;
                break;
            case PointType.Bad:
                badPoints++;
                break;
            case PointType.Neutral:
                neutralPoints++;
                break;
        }
    }

    public int GetGoodPoints() => goodPoints;
    public int GetBadPoints() => badPoints;
    public int GetNeutralPoints() => neutralPoints;

    public void ResetPoints()
    {
        goodPoints = 0;
        badPoints = 0;
        neutralPoints = 0;
    }
}
