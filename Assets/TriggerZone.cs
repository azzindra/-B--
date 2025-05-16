using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Scenario1Controller controller = FindObjectOfType<Scenario1Controller>();
            if (controller != null)
            {
                controller.ForceGoodEnding(); // Buat method ini di controller kalau belum ada
            }
        }
    }
}
