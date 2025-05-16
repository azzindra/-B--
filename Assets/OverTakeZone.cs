using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvertakeTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<Scenario5Controller>().TriggerOvertakeEnding();
        }
    }
}
