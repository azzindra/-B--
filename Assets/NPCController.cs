using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public float speed = -2f;
    public Sprite normalSprite;
    public Sprite hitSprite;

    private bool isHit = false;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        // Debug cek komponen
        if (normalSprite == null || hitSprite == null)
            Debug.LogWarning("Sprite belum di-assign di Inspector!", this);
        if (rb == null || sr == null)
            Debug.LogError("Missing Rigidbody2D atau SpriteRenderer!", this);
    }

    void Update()
    {
        if (!isHit)
        {
            rb.velocity = new Vector2(0, speed);
        }
    }

    public void GetHit(float stopDuration)
    {
        Debug.Log("NPC: GetHit dipanggil!", this);
        StartCoroutine(HitEffect(stopDuration));
    }

    private IEnumerator HitEffect(float duration)
    {
        isHit = true;

        if (hitSprite != null)
        {
            sr.sprite = hitSprite;
            Debug.Log("Sprite diubah ke hitSprite", this);
        }
        else
        {
            Debug.LogError("hitSprite null!", this);
        }

        rb.velocity = Vector2.zero;

        yield return new WaitForSeconds(duration);

        if (normalSprite != null)
        {
            sr.sprite = normalSprite;
            Debug.Log("Sprite dikembalikan ke normalSprite", this);
        }

        isHit = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("NPC bertabrakan dengan Player!", this);

            Scenario12Controller controller = FindObjectOfType<Scenario12Controller>();
            if (controller != null)
            {
                PlayerController player = other.GetComponent<PlayerController>();
                controller.RegisterCollision(player, this);
            }
            else
            {
                Debug.LogError("Scenario12Controller tidak ditemukan!", this);
            }
        }
    }
}
