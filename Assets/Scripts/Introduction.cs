using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Introduction : MonoBehaviour
{
    private bool hasLoaded = false;

    void Update()
    {
        if (!hasLoaded && Input.anyKeyDown)
        {
            hasLoaded = true;

            // Panggil LoadNextStep hanya sekali, saat pemain menekan tombol
            GameManager.Instance?.LoadNextStep();
        }
    }
}
