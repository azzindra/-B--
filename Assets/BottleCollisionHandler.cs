using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleCollisionHandler : MonoBehaviour
{
    private Scenario6Controller scenarioController;

    void Start()
    {
        scenarioController = FindObjectOfType<Scenario6Controller>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (scenarioController == null) return;

        switch (other.tag)
        {
            case "HazardBin":
                scenarioController.TriggerHazardEnding();
                break;
            case "OrganicBin":
                scenarioController.TriggerOrganicEnding();
                break;
            case "GoodBin":
                scenarioController.TriggerGoodEnding();
                break;
            case "Floor":
                scenarioController.TriggerLitteringEnding();
                break;
        }
    }
}
