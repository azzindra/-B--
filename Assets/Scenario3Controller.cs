using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class Scenario3Controller : MonoBehaviour
{
    public GameObject selector;
    public Transform[] foodSlots; // Posisi 4 makanan
    public TextMeshProUGUI[] orderTexts; // UI jumlah pesanan
    public float scenarioDuration = 10f;

    public TextMeshProUGUI timerText;


    private int currentIndex = 0;
    private int[] foodCounts = new int[4];
    private float timer;
    private bool scenarioEnded = false;

    void Start()
    {
        timer = scenarioDuration;
        UpdateSelectorPosition();
        UpdateOrderUI();
    }

    void Update()
    {
        if (scenarioEnded) return;

        timer -= Time.deltaTime;

        if (timerText != null)
        {
            timerText.text = "Time: " + Mathf.CeilToInt(timer).ToString();
        }


        if (Input.GetKeyDown(KeyCode.A))
        {
            currentIndex = (currentIndex - 1 + foodSlots.Length) % foodSlots.Length;
            UpdateSelectorPosition();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            currentIndex = (currentIndex + 1) % foodSlots.Length;
            UpdateSelectorPosition();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            if (foodCounts[currentIndex] < 20)
            {
                foodCounts[currentIndex]++;
                UpdateOrderUI();
            }
        }

        if (timer <= 0f)
        {
            scenarioEnded = true;
            CheckEndingCondition();
        }
    }

    void UpdateSelectorPosition()
    {
        selector.transform.position = foodSlots[currentIndex].position;
    }

    void UpdateOrderUI()
    {
        for (int i = 0; i < foodCounts.Length; i++)
        {
            orderTexts[i].text = "Food " + (i + 1) + "   " + foodCounts[i].ToString();
        }
    }

    void CheckEndingCondition()
{
    int totalOrders = foodCounts.Sum();

    if (totalOrders >= 11)
    {
        PointManager.Instance?.AddPoints(2); // BadEnding1
        TriggerCutscene("BadEnding1");
    }
    else if (totalOrders >= 3 && totalOrders <= 10)
    {
        PointManager.Instance?.AddPoints(0); // GoodEnding
        TriggerCutscene("GoodEnding");
    }
    else
    {
        PointManager.Instance?.AddPoints(1); // BadEnding2
        TriggerCutscene("BadEnding2");
    }

    Invoke(nameof(GoToNext), 3.0f);
}



    void TriggerCutscene(string cutsceneName)
    {
        // Implementasi bisa pakai animator atau pindah scene/cutscene canvas
        // Misal:
        CutsceneScenario3.Instance.PlayCutscene(cutsceneName);
    }
    
    private void GoToNext()
    {
        Debug.Log("Scenario1 selesai. Menuju scenario berikutnya...");
        GameManager.Instance?.LoadNextStep();
    }
}
