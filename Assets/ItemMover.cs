using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMover : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float baseSpeed = 1f;
    public float boostSpeed = 2f;

    private bool moving = false;
    private Rigidbody2D rb;

    public bool HasReachedDestination { get; private set; } = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.position = startPoint.position;
    }

    void FixedUpdate()
    {
        if (!moving || HasReachedDestination) return;

        float currentSpeed = Input.GetKey(KeyCode.Space) ? boostSpeed : baseSpeed;

        Vector2 direction = (endPoint.position - transform.position).normalized;
        rb.velocity = direction * currentSpeed;

        // Check distance
        if (Vector2.Distance(transform.position, endPoint.position) < 0.1f)
        {
            HasReachedDestination = true;
            rb.velocity = Vector2.zero;
        }
    }

    public void StartMoving()
    {
        moving = true;
    }

    public void StopMoving()
    {
        moving = false;
        rb.velocity = Vector2.zero;
    }
}
