using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
            orderTexts[i].text = "food " + (i + 1) + " : " + foodCounts[i].ToString();
        }
    }

    void CheckEndingCondition()
    {
        int totalOrders = 0;
        

        foreach (int count in foodCounts)
        {
            totalOrders += count;
            
        }

        if (totalOrders >= 11)
        {
            // Trigger Cutscene Ending Buruk 1
            Debug.Log("Ending Buruk 1");
            TriggerCutscene("BadEnding1");
        }
        else if (totalOrders >= 3 && totalOrders <= 10)
        {
            // Trigger Cutscene Ending Baik
            Debug.Log("Ending Baik");
            TriggerCutscene("GoodEnding");
        }
        else if (totalOrders == 0 && totalOrders <= 2)
        {
            // Trigger Cutscene Ending Buruk 2
            Debug.Log("Ending Buruk 2");
            TriggerCutscene("BadEnding2");
        }
    }

    void TriggerCutscene(string cutsceneName)
    {
        // Implementasi bisa pakai animator atau pindah scene/cutscene canvas
        // Misal:
        CutsceneScenario3.Instance.PlayCutscene(cutsceneName);
    }
}
