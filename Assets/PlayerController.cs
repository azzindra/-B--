using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float horizontalSpeed = 3f;
    public Animator animator;
    private Rigidbody2D rb;
    private bool isSlowed = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");

        // Set animation parameter
        animator.SetFloat("Speed", Mathf.Abs(h));

        Vector2 movement = new Vector2(h * horizontalSpeed, moveSpeed);
        if (isSlowed)
            movement.y *= 0.3f;

        rb.velocity = movement;
    }

    public void SlowDown(float duration)
    {
        StartCoroutine(SlowEffect(duration));
    }

    private IEnumerator SlowEffect(float duration)
    {
        isSlowed = true;
        yield return new WaitForSeconds(duration);
        isSlowed = false;
    }
}
