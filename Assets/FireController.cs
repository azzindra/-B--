using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public Transform[] pathPoints;
    public float baseSpeed = 1f;
    public float speedIncrement = 0.5f;

    private int currentIndex = 0;
    private float currentSpeed;

    private bool isMoving = false;

    public bool HasReachedEnd => currentIndex >= pathPoints.Length -1;

    private void Start()
    {
        currentSpeed = baseSpeed;
        transform.position = pathPoints[0].position;
    }

    public void StartMoving()
    {
        isMoving = true;
    }

    public void IncreaseSpeed()
    {
        currentSpeed += speedIncrement;
    }

    void Update()
    {
        if (!isMoving || HasReachedEnd) return;

        Transform target = pathPoints[currentIndex];
        transform.position = Vector3.MoveTowards(transform.position, target.position, currentSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.01f)
        {
            currentIndex++;
        }
    }
}
