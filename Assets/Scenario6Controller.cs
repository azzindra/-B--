using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Scenario6Controller : MonoBehaviour
{
    [Header("Cutscene Intro")]
    public GameObject cutsceneIntro1;
    public float cutscene1Duration = 2f;

    [Header("Gameplay Settings")]
    public GameObject Botol;
    public GameObject inputHandler;
    public GameObject ScenarioGameplay;
    public float scenarioDuration = 10f;
    private float timer;
    private bool isScenarioActive = false;
    private bool isEndingTriggered = false;

    [Header("Ending Cutscenes")]
    public GameObject HazardEnding;
    public GameObject GoodEnding;
    public GameObject OrganicEnding;
    public GameObject LitteringEnding;

    void Start()
    {
        inputHandler.SetActive(false);
        StartCoroutine(PlayIntroCutscenes());
    }

    IEnumerator PlayIntroCutscenes()
    {
        if (cutsceneIntro1 != null)
        {
            cutsceneIntro1.SetActive(true);
            yield return new WaitForSeconds(cutscene1Duration);
            cutsceneIntro1.SetActive(false);
        }

        StartScenario();
    }

    void StartScenario()
    {
        if (ScenarioGameplay != null)
            ScenarioGameplay.SetActive(true);

        timer = scenarioDuration;
        isScenarioActive = true;

        // Meluncurkan botol
        Botol.GetComponent<BottleController>()?.LaunchBottle();
    }

    void Update()
    {
        if (!isScenarioActive || isEndingTriggered) return;

        timer -= Time.deltaTime;

        if (timer <= 0f && !isEndingTriggered)
        {
            TriggerLitteringEnding();
        }
    }

    public void TriggerHazardEnding() => TriggerEnding(HazardEnding, 1);
    public void TriggerOrganicEnding() => TriggerEnding(OrganicEnding, 1);
    public void TriggerLitteringEnding() => TriggerEnding(LitteringEnding, 2);
    public void TriggerGoodEnding() => TriggerEnding(GoodEnding, 0);


    private void TriggerEnding(GameObject endingObject, int pointValue)
    {
        if (isEndingTriggered) return;

        isEndingTriggered = true;
        isScenarioActive = false;

        Botol.GetComponent<BottleController>()?.DisableInteraction();

        if (endingObject != null)
        {
            endingObject.SetActive(true);
            PointManager.Instance?.AddPoints(pointValue); // ✅ Tambahkan poin
            Invoke(nameof(GoToNext), 3.0f);
        }
    }

    private void GoToNext()
    {
        Debug.Log("Scenario1 selesai. Menuju scenario berikutnya...");
        GameManager.Instance?.LoadNextStep();
    }
}
