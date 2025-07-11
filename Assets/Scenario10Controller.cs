using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scenario10Controller : MonoBehaviour
{
    [Header("Scenario References")]
    public GameObject introPanel;

    public float introDuration = 2f;
    public GameObject gameplayGroup;
    public GameObject goodEndingPanel;
    public GameObject badEndingPanel;
    public float scenarioDuration = 10f;

    [Header("Item Movement")]
    public ItemMover itemMover;

    private float timer;
    private bool scenarioStarted = false;
    private bool scenarioEnded = false;

    void Start()
    {
        introPanel.SetActive(true);
        timer = introDuration;
        gameplayGroup.SetActive(false);
        goodEndingPanel.SetActive(false);
        badEndingPanel.SetActive(false);
        timer = scenarioDuration;
    }

    void Update()
    {
        if (!scenarioStarted)
        {
            if (Input.anyKeyDown)
            {
                StartScenario();
            }
            return;
        }

        if (scenarioEnded) return;

        timer -= Time.deltaTime;

        if (itemMover.HasReachedDestination && !scenarioEnded)
        {
            scenarioEnded = true;
            EndScenario(true);
        }
        else if (timer <= 0f && !scenarioEnded)
        {
            scenarioEnded = true;
            EndScenario(false);
        }
    }

    void StartScenario()
    {
        introPanel.SetActive(false);
        gameplayGroup.SetActive(true);
        itemMover.StartMoving();
        scenarioStarted = true;
    }

    void EndScenario(bool isGoodEnding)
    {
        itemMover.StopMoving();

        if (isGoodEnding)
        {
            goodEndingPanel.SetActive(true);
            PointManager.Instance?.AddPoints(0);
        }
        else
        {
            badEndingPanel.SetActive(true);
            PointManager.Instance?.AddPoints(1);
        }
            Invoke(nameof(GoToNext), 3.0f);
    }
    private void GoToNext()
    {
        Debug.Log("Scenario1 selesai. Menuju scenario berikutnya...");
        GameManager.Instance?.LoadNextStep();
    }
}
