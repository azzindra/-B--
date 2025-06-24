using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario2Controller : MonoBehaviour
{
    public GameObject gameplayScenario;
    public GameObject goodEndingPanel;
    public GameObject badEndingPanel;
    public float scenarioDuration = 10f;

    private bool isScenarioEnded = false;
    private float pauseTimer = 0f;
    private bool isPauseCounting = false;

    private MoviePlayer moviePlayer;

    void Start()
    {
        gameplayScenario.SetActive(true);
        moviePlayer = FindObjectOfType<MoviePlayer>();
    }

    void Update()
    {
        if (isScenarioEnded || moviePlayer == null) return;

        float time = moviePlayer.currentTime;

        // PAUSE CASE
        if (moviePlayer.videoState == MoviePlayer.VideoState.Paused)
        {
            if (!isPauseCounting)
            {
                isPauseCounting = true;
                pauseTimer = 0f;
            }

            pauseTimer += Time.deltaTime;

            if (pauseTimer >= 2.5f)
            {
                EndScenario("bad");
            }

            return; // jangan lanjutkan evaluasi skenario lainnya
        }

        // REWIND BAD END
        if (moviePlayer.videoState == MoviePlayer.VideoState.Rewind && time <= 0f)
        {
            EndScenario("bad");
        }

        // ENDING SAAT NORMAL / FF
        else if (time >= scenarioDuration)
        {
            string ending = moviePlayer.videoState == MoviePlayer.VideoState.FastForward ? "bad" : "good";
            EndScenario(ending);
        }
    }

    void EndScenario(string type)
    {
        if (isScenarioEnded) return;

        isScenarioEnded = true;
        gameplayScenario?.SetActive(false);

        switch (type)
        {
            case "good":
                goodEndingPanel?.SetActive(true);
                PointManager.Instance?.AddPoints(0); // ✅ good = 100
                break;

            case "bad":
                badEndingPanel?.SetActive(true);
                PointManager.Instance?.AddPoints(1);  // ✅ bad = 30
                break;
        }

        GameManager.Instance?.AdvanceDay();
        Invoke(nameof(GoToNext), 3.0f);
    }


    void GoToNext()
    {
        GameManager.Instance?.LoadNextStep();
    }
}
