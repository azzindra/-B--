using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario9Controller : MonoBehaviour
{
    public static Scenario9Controller Instance;

    [Header("UI Panels")]
    public GameObject introPanel;
    public GameObject gameplayArea;
    public GameObject goodEndingPanel;
    public GameObject crashEndingPanel;
    public GameObject disabilityEndingPanel;
    public GameObject stupidEndingPanel;

    [Header("Timing")]
    public float introDuration = 2f;
    public float gameplayDuration = 20f;

    private bool scenarioEnded = false;
    private float gameplayTimer = 0f;
    private bool isGameplayStarted = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        // Atur tampilan awal
        introPanel.SetActive(true);
        gameplayArea.SetActive(false);
        goodEndingPanel.SetActive(false);
        crashEndingPanel.SetActive(false);
        disabilityEndingPanel.SetActive(false);
        stupidEndingPanel.SetActive(false);

        // Mulai intro
        Invoke(nameof(StartGameplay), introDuration);
    }

    private void StartGameplay()
    {
        introPanel.SetActive(false);
        gameplayArea.SetActive(true);
        isGameplayStarted = true;
        gameplayTimer = gameplayDuration;
    }

    private void Update()
    {
        if (!isGameplayStarted || scenarioEnded) return;

        gameplayTimer -= Time.deltaTime;
        if (gameplayTimer <= 0f)
        {
            TriggerStupidEnding(); // default ending kalau waktu habis
        }
    }

    public bool IsScenarioEnded()
    {
        return scenarioEnded;
    }


    public void TriggerGoodEnding()
    {
        if (scenarioEnded) return;
        scenarioEnded = true;
        Debug.Log("Good Ending");
        ShowEndingPanel(goodEndingPanel);
    }

    public void TriggerCrashEnding()
    {
        if (scenarioEnded) return;
        scenarioEnded = true;
        Debug.Log("Crash Ending");
        ShowEndingPanel(crashEndingPanel);
    }

    public void TriggerDisabilityEnding()
    {
        if (scenarioEnded) return;
        scenarioEnded = true;
        Debug.Log("Disability Ending");
        ShowEndingPanel(disabilityEndingPanel);
    }

    public void TriggerStupidEnding()
    {
        if (scenarioEnded) return;
        scenarioEnded = true;
        Debug.Log("Stupid Ending (Waktu habis)");
        ShowEndingPanel(stupidEndingPanel);
    }

    private void ShowEndingPanel(GameObject panel)
    {
        gameplayArea.SetActive(false);
        panel.SetActive(true);
        isGameplayStarted = false;
    }
}
