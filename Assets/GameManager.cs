using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private List<string> randomizableScenarios = new List<string> {
        "Scenario2", "Scenario3", "Scenario4", "Scenario5",
        "Scenario6", "Scenario7", "Scenario8", "Scenario9"
    };

    private Queue<string> scenarioQueue = new Queue<string>();
    private int totalScore = 0;
    private int dayCounter = 1;
    private int totalDays = 10;

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

        // Urutan fix
        scenarioQueue.Enqueue("Scenario1");

        // Acak sisa scenario
        ShuffleList(randomizableScenarios);
        foreach (var scenario in randomizableScenarios)
        {
            scenarioQueue.Enqueue(scenario);
        }

        scenarioQueue.Enqueue("Scenario10");
    }

    void ShuffleList(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            (list[i], list[randomIndex]) = (list[randomIndex], list[i]);
        }
    }

    public void LoadNextStep()
    {
        Debug.Log($"[GameManager] LoadNextStep called. dayCounter = {dayCounter}");

        if ((dayCounter == 6) && dayCounter <= totalDays)
        {
            SceneManager.LoadScene("InterimEvaluation");
        }
        else if (dayCounter > totalDays)
        {
            SceneManager.LoadScene("EvaluationReport");
        }
        else
        {
            if (scenarioQueue.Count > 0)
            {
                currentScenario = scenarioQueue.Dequeue();
                Debug.Log($"[GameManager] Loading next scenario: {currentScenario}");
                SceneManager.LoadScene("LoadingScreen");
            }
            else
            {
                Debug.LogWarning("[GameManager] Scenario queue is empty!");
            }
        }
    }

    // 💡 Tambahan agar kompatibel dengan ScenarioController.cs
    public void LoadNextScenario()
    {
        LoadNextStep();
    }

    public void LoadCurrentScenario()
    {
        Debug.Log("[GameManager] Loading scenario: " + currentScenario);
        SceneManager.LoadScene(currentScenario);
    }

    public int GetCurrentDay()
    {
        return dayCounter;
    }

    public void AdvanceDay()
    {
        dayCounter++;
    }

    public void AddScore(int score)
    {
        totalScore += score;
    }

    public int GetTotalScore()
    {
        return totalScore;
    }

    public void ResetGame()
    {
        totalScore = 0;
        dayCounter = 1;
        PrepareScenarioOrder();
    }
}
