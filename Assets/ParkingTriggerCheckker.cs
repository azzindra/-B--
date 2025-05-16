using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkingTriggerChecker : MonoBehaviour
{
    private float timeInZone = 0f;
    public float requiredTime = 2f;
    private bool inZone = false;
    private string currentZoneTag = "";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("NormalParking") || other.CompareTag("DisabilityParking"))
        {
            inZone = true;
            currentZoneTag = other.tag;
            timeInZone = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (inZone && other.tag == currentZoneTag)
        {
            inZone = false;
            currentZoneTag = "";
            timeInZone = 0f;
        }
        Debug.Log("Keluar dari zona parkir: " + other.tag);

    }


    private void Update()
    {
        if (!inZone || Scenario9Controller.Instance == null || Scenario9Controller.Instance.IsScenarioEnded()) return;

        timeInZone += Time.deltaTime;
        if (timeInZone >= requiredTime)
        {
            if (currentZoneTag == "NormalParking")
            {
                Scenario9Controller.Instance.TriggerGoodEnding();
            }
            else if (currentZoneTag == "DisabilityParking")
            {
                Scenario9Controller.Instance.TriggerDisabilityEnding();
            }

            inZone = false; // stop memicu ulang
        }
    }
}
