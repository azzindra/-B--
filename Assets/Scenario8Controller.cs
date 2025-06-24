using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario8Controller : MonoBehaviour
{
    public GameObject introPanel;
    public GameObject gameplayArea;
    public GameObject endingPanelGood;
    public GameObject endingPanelBad;
    public float introDuration = 2f;

    private bool scenarioEnded = false;

    void Start()
    {
        introPanel.SetActive(true);
        gameplayArea.SetActive(false);
        endingPanelGood.SetActive(false);
        endingPanelBad.SetActive(false);
        Invoke(nameof(StartGameplay), introDuration);
    }

    void StartGameplay()
    {
        introPanel.SetActive(false);
        gameplayArea.SetActive(true);
    }

    public void TriggerEnding(bool isGood)
    {
        if (scenarioEnded) return;
        scenarioEnded = true;

        gameplayArea.SetActive(false);
        if (isGood)
        {
            endingPanelGood.SetActive(true);
            PointManager.Instance?.AddPoints(0); // ✅ poin untuk ending baik
        }
        else
        {
            endingPanelBad.SetActive(true);
            PointManager.Instance?.AddPoints(1); // ✅ poin untuk ending buruk
        }

        Invoke(nameof(GoToNext), 3.0f);
    }

    private void GoToNext()
    {
        Debug.Log("Scenario1 selesai. Menuju scenario berikutnya...");
        GameManager.Instance?.LoadNextStep();
    }
}
