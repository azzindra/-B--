using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    public static PointManager Instance { get; private set; }

    private int totalPoints = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Hancurkan duplikat
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // BERTAHAN ANTAR SCENE
    }

    public void AddPoints(int amount)
    {
        totalPoints += amount;
        Debug.Log($"[PointManager] Add: {amount} → Total: {totalPoints}");
    }

    public int GetTotalPoints()
    {
        Debug.Log($"[PointManager] Get Total: {totalPoints}");
        return totalPoints;
    }

    public void ResetPoints()
    {
        totalPoints = 0;
        Debug.Log($"[PointManager] RESET. Total now: {totalPoints}");
    }
}
