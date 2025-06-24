using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private List<string> randomizableScenarios = new List<string> {
        "Scenario2", "Scenario3", "Scenario4", "Scenario5",
        "Scenario6", "Scenario7", "Scenario8", "Scenario9",
        "Scenario10", "Scenario11"
    };

    private Queue<string> scenarioQueue = new Queue<string>();
    private int totalScore = 0;
    private int dayCounter = 0; // dimulai dari 0 agar Scenario1 valid saat dimulai
    private int totalDays = 12;

    public string currentScenario = "";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            PrepareScenarioOrder();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void PrepareScenarioOrder()
    {
        scenarioQueue.Clear();

        scenarioQueue.Enqueue("Scenario1"); // selalu di awal
        ShuffleList(randomizableScenarios);
        foreach (var scenario in randomizableScenarios)
        {
            scenarioQueue.Enqueue(scenario);
        }
        scenarioQueue.Enqueue("Scenario12"); // selalu terakhir
    }

    void ShuffleList(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rand = Random.Range(i, list.Count);
            (list[i], list[rand]) = (list[rand], list[i]);
        }
    }

    public void LoadNextStep()
    {
        Debug.Log($"[GameManager] LoadNextStep called. Day = {dayCounter}");

        if (dayCounter >= totalDays)
        {
            SceneManager.LoadScene("EvaluationReport");
            return;
        }

        if (dayCounter > 0 && dayCounter % 4 == 0 && dayCounter < totalDays)
        {
            SceneManager.LoadScene("InterimEvaluation");
            return;
        }

        if (scenarioQueue.Count > 0)
        {
            currentScenario = scenarioQueue.Dequeue();
            SceneManager.LoadScene("LoadingScreen");
        }
        else
        {
            Debug.LogWarning("Scenario queue kosong padahal day belum mencapai totalDays.");
            SceneManager.LoadScene("EvaluationReport"); // fallback
        }
    }
    public void LoadCurrentScenario()
    {
        Debug.Log("[GameManager] Loading currentScenario: " + currentScenario);
        SceneManager.LoadScene(currentScenario);
    }

    public void AdvanceDay()
    {
        dayCounter++;
        Debug.Log("[GameManager] Day advanced to: " + dayCounter);
    }

    public int GetCurrentDay() => dayCounter;

    public void AddScore(int score) => totalScore += score;

    public int GetTotalScore() => totalScore;

    public void ResetGame()
    {
        totalScore = 0;
        dayCounter = 0;
        PrepareScenarioOrder();
    }
}