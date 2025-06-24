using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScenarioBase : MonoBehaviour
{
    [Header("General Timing")]
    public float[] introDurations; // support multiple intro panels
    public GameObject[] introPanels;

    public float gameplayDuration = 10f;
    public GameObject gameplayPanel;

    public GameObject[] endingPanels;
    public int[] endingScores;

    protected bool scenarioEnded = false;
    protected float timer;

    protected virtual void Start()
    {
        StartCoroutine(PlayIntroSequence());
    }

    IEnumerator PlayIntroSequence()
    {
        if (introPanels != null && introPanels.Length > 0)
        {
            for (int i = 0; i < introPanels.Length; i++)
            {
                introPanels[i].SetActive(true);
                yield return new WaitForSeconds(introDurations[i]);
                introPanels[i].SetActive(false);
            }
        }
        StartGameplay();
    }

    protected virtual void StartGameplay()
    {
        if (gameplayPanel != null)
            gameplayPanel.SetActive(true);

        timer = gameplayDuration;
        scenarioEnded = false;
    }

    protected virtual void Update()
    {
        if (scenarioEnded) return;

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            TriggerEnding(0); // Default to first ending if not specified
        }
    }

    protected void TriggerEnding(int index)
    {
        if (scenarioEnded || index < 0 || index >= endingPanels.Length) return;

        scenarioEnded = true;

        if (gameplayPanel != null)
            gameplayPanel.SetActive(false);

        endingPanels[index]?.SetActive(true);

        if (endingScores != null && index < endingScores.Length)
            GameManager.Instance?.AddScore(endingScores[index]);

        GameManager.Instance?.AdvanceDay();
        Invoke(nameof(GoToNext), 2.5f);
    }

    protected virtual void GoToNext()
    {
        GameManager.Instance?.LoadNextStep();
    }
}
