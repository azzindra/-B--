using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scenario1Controller : MonoBehaviour
{
    public GameObject introPanel;
    public GameObject scenarioGameplay;
    public GameObject endingPanelGood;
    public GameObject endingPanelBad;

    public float introDuration = 2f;
    public float gameplayDuration = 20f;
    private float timer;
    private bool isGameActive = false;
    private bool isScenarioEnded = false;

    public int pointGood = 100;
    public int pointBad = 0;

    public Transform playerTransform; // referensi ke Player

    private void Start()
    {
        // Auto-assign jika belum diisi
        if (introPanel == null) introPanel = GameObject.Find("IntroPanel");
        if (scenarioGameplay == null) scenarioGameplay = GameObject.Find("ScenarioGameplay");
        if (endingPanelGood == null) endingPanelGood = GameObject.Find("EndingPanel_Good");
        if (endingPanelBad == null) endingPanelBad = GameObject.Find("EndingPanel_Bad");

        // Cari player jika belum diset
        if (playerTransform == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null) playerTransform = player.transform;
            else Debug.LogWarning("Player dengan tag 'Player' tidak ditemukan!");
        }

        // Inisialisasi panel
        introPanel?.SetActive(true);
        scenarioGameplay?.SetActive(false);
        endingPanelGood?.SetActive(false);
        endingPanelBad?.SetActive(false);

        Invoke(nameof(StartGameplay), introDuration);
    }

    private void StartGameplay()
    {
        introPanel?.SetActive(false);
        scenarioGameplay?.SetActive(true);

        timer = gameplayDuration;
        isGameActive = true;
    }

    private void Update()
    {
        if (!isGameActive || isScenarioEnded) return;

        timer -= Time.deltaTime;

        if (playerTransform != null && playerTransform.position.x > 5f)
        {
            EndScenario(true);
        }

        if (timer <= 0f)
        {
            EndScenario(false);
        }
    }

    public void ForceGoodEnding()
    {
        if (!isScenarioEnded && isGameActive)
        {
            EndScenario(true);
        }
    }


    private void EndScenario(bool isGood)
    {
        if (isScenarioEnded) return; // Proteksi tambahan
        isScenarioEnded = true;
        isGameActive = false;

        scenarioGameplay?.SetActive(false);

        if (isGood)
        {
            endingPanelGood?.SetActive(true);
            PointManager.Instance?.AddPoints(0);;
        }
        else
        {
            endingPanelBad?.SetActive(true);
            PointManager.Instance?.AddPoints(1);;
        }

        GameManager.Instance?.AdvanceDay();
        Invoke(nameof(GoToNext), 3.0f);
    }

    private void GoToNext()
    {
        Debug.Log("Scenario1 selesai. Menuju scenario berikutnya...");
        GameManager.Instance?.LoadNextStep();
    }
}
