using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        PointManager.Instance?.ResetPoints();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Introduction"); // Ganti dari GameManager ke Introduction
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}