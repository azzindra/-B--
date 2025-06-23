using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorCollisionDetector : MonoBehaviour
{
    public Scenario11Controller controller;
    private bool hasTriggered = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered) return;

        if (other.CompareTag("Puddle"))
        {
            Debug.Log("Motor menyentuh puddle! BAD ENDING triggered.");
            hasTriggered = true;
            controller.ForceBadEnding();
            controller.CheckEndingCondition(); // << WAJIB DIPANGGIL!
        }
    }
}
