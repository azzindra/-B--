using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
    public float delay = 2.5f;
    public TextMeshPro dayText; // atau TMP_Text kalau pakai TextMeshPro

    void Start()
    {
        int day = GameManager.Instance != null ? GameManager.Instance.GetCurrentDay() + 1 : 0;
        if (dayText != null) dayText.text = "Day " + day.ToString();

        Invoke("LoadScenario", delay);
    }

    void LoadScenario()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.LoadCurrentScenario();
    }
}
