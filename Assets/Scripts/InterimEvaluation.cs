using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class InterimEvaluation : MonoBehaviour
{
    public TextMeshProUGUI evaluationText; // Ganti Text dengan TextMeshProUGUI

    void Start()
    {
        if (evaluationText != null)
        {
            evaluationText.text = "Interim Evaluation: Keep going!"; // Bisa diubah sesuai kondisi
        }
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("LoadingScreen"); // Kembali ke loading screen sebelum minigame berikutnya
        }
    }
}
