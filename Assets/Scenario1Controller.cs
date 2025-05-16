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
        isScenarioEnded = true;
        isGameActive = false;

        scenarioGameplay?.SetActive(false);

        if (isGood)
        {
            endingPanelGood?.SetActive(true);
            GameManager.Instance?.AddScore(pointGood);
        }
        else
        {
            endingPanelBad?.SetActive(true);
            GameManager.Instance?.AddScore(pointBad);
        }

        GameManager.Instance?.AdvanceDay();
        Invoke(nameof(GoToNext), 2.5f);
    }

    private void GoToNext()
    {
        GameManager.Instance?.LoadNextScenario();
    }
}
