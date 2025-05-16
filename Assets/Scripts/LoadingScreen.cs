using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
    public TextMeshProUGUI dayText;
    public float delayBeforeLoad = 2f;

    void Start()
    {
        int day = GameManager.Instance.GetCurrentDay();
        dayText.text = (day == 15) ? "FINAL DAY" : $"DAY {day:00}";

        Invoke(nameof(LoadScenario), delayBeforeLoad);
    }

    void LoadScenario()
    {
        GameManager.Instance.LoadCurrentScenario();
    }
}
