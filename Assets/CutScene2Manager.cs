using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene2Manager : MonoBehaviour
{
    [Header("Cutscene Stages")]
    public GameObject cutscene1Visuals;
    public GameObject cutscene2Visuals;

    [Header("Scenario Gameplay Root")]
    public GameObject scenarioGameplay;

    [Header("Durasi Setiap Cutscene")]
    public float cutscene1Duration = 3f;
    public float cutscene2Duration = 3f;

    void Start()
    {
        scenarioGameplay.SetActive(false);
        StartCoroutine(PlayCutscenes());
    }

    IEnumerator PlayCutscenes()
    {
        // Cutscene 1
        cutscene1Visuals.SetActive(true);
        yield return new WaitForSeconds(cutscene1Duration);
        cutscene1Visuals.SetActive(false);

        // Cutscene 2
        cutscene2Visuals.SetActive(true);
        yield return new WaitForSeconds(cutscene2Duration);
        cutscene2Visuals.SetActive(false);

        // Aktifkan gameplay
        scenarioGameplay.SetActive(true);
    }
}
