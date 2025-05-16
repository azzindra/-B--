using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    public static PointManager Instance;

    public int totalPoints = 0;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddPoints(int points)
    {
        totalPoints += points;
        Debug.Log("Points Added: " + points + " | Total: " + totalPoints);
    }

    public void ResetPoints()
    {
        totalPoints = 0;
    }

    public int GetTotalPoints()
    {
        return totalPoints;
    }
}
