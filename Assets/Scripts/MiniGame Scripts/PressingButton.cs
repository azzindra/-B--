using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressingButton : MonoBehaviour
{
    public Text countdownText;
    private float currentTime = 0f;
    private bool gameEnded = false;
    private int debugCompleteCounter = 0; // Untuk menyelesaikan dengan menekan "P" 5 kali

    void Update()
    {
        if (gameEnded) return;

        currentTime += Time.deltaTime;
        countdownText.text = "Waktu: " + currentTime.ToString("F2");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentTime >= 5f && currentTime <= 6f)
            {
                EndMiniGame("Baik"); // Sukses
            }
            else
            {
                EndMiniGame("Buruk"); // Gagal
            }
        }

        if (Input.GetKeyDown(KeyCode.P)) 
        {
            debugCompleteCounter++;
            if (debugCompleteCounter >= 5) // Jika "P" ditekan 5 kali
            {
                EndMiniGame("Debug: Lulus");
            }
        }

        if (currentTime >= 20f)
        {
            EndMiniGame("By Design"); // Waktu habis
        }
    }

    void EndMiniGame(string result)
    {
        if (gameEnded) return;

        gameEnded = true;
        Debug.Log("Minigame selesai dengan hasil: " + result);
        GameManager.Instance.LoadNextStep(); // Kembali ke GameManager untuk memilih skenario baru
    }
}