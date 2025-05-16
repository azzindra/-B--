using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenarioManager : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Scenario Dimulai: " + SceneManager.GetActiveScene().name);
    }
}
