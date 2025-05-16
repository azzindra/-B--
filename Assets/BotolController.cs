using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleController : MonoBehaviour
{
    [Header("Control Settings")]
    public float horizontalSpeed = 3f;                    // Kecepatan kontrol kiri-kanan oleh pemain
    public Vector2 initialVelocity = new Vector2(0f, 0f); // Akan diatur manual nanti

    private Rigidbody2D rb;
    private bool isLaunched = false;
    private bool isInteractable = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0.1f;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
    }

    public void LaunchBottle()
    {
        if (isLaunched) return;

        isLaunched = true;
        rb.isKinematic = false;
        rb.velocity = Vector2.zero; // supaya murni jatuh karena gravitasi lambat

        // Botol langsung jatuh dari atas (tanpa dorongan ke samping)
        rb.velocity = new Vector2(0f, -1f); // atau Vector2.zero jika ingin langsung jatuh
    }

    void Update()
    {
        if (!isInteractable || !isLaunched) return;

        // Tambahkan gaya horizontal, bukan langsung set velocity
        float moveX = Input.GetAxis("Horizontal");
        Vector2 currentVelocity = rb.velocity;
        rb.velocity = new Vector2(moveX * horizontalSpeed, currentVelocity.y);
    }

    public void DisableInteraction()
    {
        isInteractable = false;
        rb.velocity = Vector2.zero;
    }
}
