using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EndingType
{
    Good,
    Bad,
    Stupid,
    Neutral,
    Other
}

[System.Serializable]
public struct EndingScore
{
    public EndingType type;
    public int score;
}


public class Scenario7Controller : MonoBehaviour
{
    [Header("Scene Sections")]
    public GameObject introCutscene;
    public GameObject gameplay;
    public GameObject endingCutscene;

    [Header("Ending Panels")]
    public GameObject goodEndingPanel;
    public GameObject badEndingPanel;
    public GameObject stupidEndingPanel; // Optional tambahan

    [Header("Reference")]
    public FireController fireController;

    [Header("Timing")]
    public float timeLimit = 12f;
    private float timer;

    private bool scenarioEnded = false;

    [Header("Score Settings")]
    public List<EndingScore> endingScores = new List<EndingScore>();

    void Start()
    {
        timer = timeLimit;
        introCutscene.SetActive(true);
        gameplay.SetActive(false);
        endingCutscene.SetActive(false);

        Invoke(nameof(StartGameplay), 2f); // Durasi intro 2 detik
    }

    void StartGameplay()
    {
        introCutscene.SetActive(false);
        gameplay.SetActive(true);
    }

    void Update()
    {
        if (!gameplay.activeSelf || scenarioEnded) return;

        timer -= Time.deltaTime;

        if (fireController != null && fireController.HasReachedEnd)
        {
            TriggerEnding(EndingType.Good);
        }
        else if (timer <= 0f)
        {
            TriggerEnding(EndingType.Bad);
        }
    }

    void TriggerEnding(EndingType type)
    {
        if (scenarioEnded) return;

        scenarioEnded = true;
        gameplay.SetActive(false);
        endingCutscene.SetActive(true);

        // Reset all panels first
        goodEndingPanel?.SetActive(false);
        badEndingPanel?.SetActive(false);
        stupidEndingPanel?.SetActive(false);

        // Show panel based on type
        switch (type)
        {
            case EndingType.Good:
                goodEndingPanel?.SetActive(true);
                break;
            case EndingType.Bad:
                badEndingPanel?.SetActive(true);
                break;
            case EndingType.Stupid:
                stupidEndingPanel?.SetActive(true);
                break;
            // Tambah panel lainnya sesuai kebutuhan
        }

        // Tambah skor berdasarkan ending type
        int scoreToAdd = GetScoreByEnding(type);
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(scoreToAdd);
        }

        Debug.Log($"🔔 {type} Ending Triggered! Score: {scoreToAdd}");
    }

    int GetScoreByEnding(EndingType type)
    {
        foreach (var entry in endingScores)
        {
            if (entry.type == type)
                return entry.score;
        }
        return 0; // Default kalau gak ketemu
    }
}