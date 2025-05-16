using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    [Header("Assign GameObjects")]
    public GameObject cutsceneVisuals;
    public GameObject scenarioGameplay;

    [Header("Cutscene Settings")]
    public float cutsceneDuration = 3.0f;

    void Start()
    {
        // Matikan gameplay saat awal
        scenarioGameplay.SetActive(false);

        // Mulai cutscene
        StartCoroutine(PlayCutscene());
    }

    IEnumerator PlayCutscene()
    {
        // Aktifkan visual cutscene
        cutsceneVisuals.SetActive(true);

        // Tunggu durasi
        yield return new WaitForSeconds(cutsceneDuration);

        // Matikan cutscene
        cutsceneVisuals.SetActive(false);

        // Aktifkan gameplay
        scenarioGameplay.SetActive(true);
    }
}
