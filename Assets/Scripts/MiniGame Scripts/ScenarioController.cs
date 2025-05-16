using UnityEngine;

public class ScenarioController : MonoBehaviour
{
    public float gameTime = 20f; // Timer 20 detik
    private int pressCount = 0;  // Jumlah tombol "P" ditekan
    private bool gameEnded = false;

    void Start()
    {
        Invoke("TimeOut", gameTime); // Mulai timer
    }

    void Update()
    {
        if (gameEnded) return;

        // Debugging: Menang jika tekan "P" 5 kali
        if (Input.GetKeyDown(KeyCode.P))
        {
            pressCount++;
            Debug.Log("Tombol P ditekan: " + pressCount + " kali");

            if (pressCount >= 5)
            {
                EndScenario("Baik");
            }
        }
    }

    void EndScenario(string result)
    {
        if (gameEnded) return;

        gameEnded = true;
        Debug.Log("Scenario selesai dengan hasil: " + result);

        // **Pilih Scenario Baru dari GameManager**
        GameManager.Instance.LoadNextStep();
    }

    void TimeOut()
    {
        if (!gameEnded)
        {
            EndScenario("By Design");
        }
    }
}
