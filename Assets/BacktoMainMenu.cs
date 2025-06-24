using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMainMenu : MonoBehaviour
{
    public void BackToMenu()
    {
        PointManager.Instance?.ResetPoints(); // ✅ Reset poin total
        SceneManager.LoadScene("MainMenu");   // Ganti "MainMenu" sesuai nama scene Anda
    }
}
