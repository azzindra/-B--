using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenarioTimer : MonoBehaviour
{
    public float gameTime = 20f;
    private bool gameEnded = false;
    private int debugCompleteCounter = 0; // Untuk debugging

    void Update()
    {
        if (gameEnded) return;

        gameTime -= Time.deltaTime;

        if (gameTime <= 0)
        {
            EndScenario("By Design");
        }

        // Debugging: Menyelesaikan game dengan menekan "P" 5 kali
        if (Input.GetKeyDown(KeyCode.P))
        {
            debugCompleteCounter++;
            if (debugCompleteCounter >= 5)
            {
                EndScenario("Debug: Lulus");
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            EndScenario("Baik");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EndScenario("Buruk");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            EndScenario("By Design");
        }
    }

    public void EndScenario(string result)
    {
        if (gameEnded) return;
        
        gameEnded = true;
        Debug.Log("Scenario selesai dengan hasil: " + result);
        
        GameManager.Instance.LoadNextStep();  // Pilih skenario baru setelah menyelesaikan satu skenario
    }
}
