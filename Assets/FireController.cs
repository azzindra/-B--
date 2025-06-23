using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public Transform[] waypoints;
    public float moveSpeed = 2f;
    public float boostedSpeed = 5f;

    private int currentIndex = 0;
    private bool isMoving = true;

    public bool HasReachedEnd { get; private set; } = false;

    void Update()
    {
        if (!isMoving || HasReachedEnd || waypoints.Length == 0) return;

        float speed = Input.GetKey(KeyCode.Space) ? boostedSpeed : moveSpeed;
        Vector3 target = waypoints[currentIndex].position;

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        float distance = Vector3.Distance(transform.position, target);

        if (distance < 0.05f)
        {
            currentIndex++;
            if (currentIndex >= waypoints.Length)
            {
                HasReachedEnd = true;
                Debug.Log("🔥 Api sudah sampai di ujung petasan!");
            }
        }
    }
}
