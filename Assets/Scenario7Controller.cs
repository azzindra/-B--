using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scenario7Controller : MonoBehaviour
{
    public GameObject introPanel;
    public GameObject endingGoodPanel;
    public GameObject endingBadPanel;
    public GameObject gameplayRoot;
    public float introDuration = 2f;
    public float maxDuration = 10f;

    public FireController fireController;

    private float timer = 0f;
    private bool gameplayStarted = false;
    private bool endingShown = false;

    void Start()
    {
        introPanel.SetActive(true);
        gameplayRoot.SetActive(false);
        endingGoodPanel.SetActive(false);
        endingBadPanel.SetActive(false);

        Invoke(nameof(StartGameplay), introDuration);
    }

    void StartGameplay()
    {
        introPanel.SetActive(false);
        gameplayRoot.SetActive(true);
        gameplayStarted = true;
        fireController.StartMoving();
    }

    void Update()
    {
        if (!gameplayStarted || endingShown) return;

        timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            fireController.IncreaseSpeed();
            Debug.Log("Space ditekan, kecepatan api: " + Time.time);
        }

        if (fireController.HasReachedEnd)
        {
            Debug.Log("Api mencapai ujung!");
            ShowEnding(true);
        }
        else if (timer >= maxDuration)
        {
            Debug.Log("Waktu habis!");
            ShowEnding(false);
        }
    }


    void ShowEnding(bool isGood)
    {
        endingShown = true;
        gameplayRoot.SetActive(false);

        if (isGood)
            endingGoodPanel.SetActive(true);
        else
            endingBadPanel.SetActive(true);
    }

}
