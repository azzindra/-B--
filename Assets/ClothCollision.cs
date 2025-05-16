using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothCollision : MonoBehaviour
{
    private Scenario8Controller scenarioController;

    void Start()
    {
        scenarioController = FindObjectOfType<Scenario8Controller>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Basket"))
        {
            scenarioController.TriggerEnding(true);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Floor"))
        {
            scenarioController.TriggerEnding(false);
            Destroy(gameObject);
        }
    }
}
