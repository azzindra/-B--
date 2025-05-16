using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollisionHandler : MonoBehaviour
{
   private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Scenario9Controller.Instance == null || Scenario9Controller.Instance.IsScenarioEnded()) return;

        if (collision.collider.CompareTag("ObstacleCar"))
        {
            Debug.Log("Tabrakan dengan mobil parkir!");
            Scenario9Controller.Instance.TriggerCrashEnding();
        }
    }

}
