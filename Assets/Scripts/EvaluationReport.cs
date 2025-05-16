using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EvaluationReport : MonoBehaviour
{
    public TextMeshProUGUI reportText;

    void Start()
    {
        if (reportText != null)
        {
            reportText.text = "Final Evaluation: Well Done!"; // Bisa diubah sesuai skor atau hasil pemain
        }
        else
        {
            Debug.LogError("ReportText is not assigned in the Inspector!");
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("MainMenu"); // Kembali ke menu utama
    }
}
