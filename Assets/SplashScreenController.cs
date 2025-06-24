using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenController : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "MainMenu"; // Ganti sesuai nama scene Main Menu Anda
    private bool hasClicked = false;

    void Update()
    {
        if (Input.anyKeyDown || Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            if (!hasClicked)
            {
                hasClicked = true;
                LoadNextScene();
            }
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
