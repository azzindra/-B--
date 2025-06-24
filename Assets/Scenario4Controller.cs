using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario4Controller : MonoBehaviour
{
    [Header("Movement Bounds")]
    public RectTransform movementBounds; // Drag Canvas parent dari tangan

    [Header("Player Control")]
    public RectTransform handTransform;
    public float moveSpeed = 200f;

    [Header("Interaction")]
    public Transform interactionPoint;
    public float interactionRadius = 30f;
    public LayerMask appLayerMask;

    [Header("UI Endings")]
    public GameObject goodEndPanel;
    public GameObject badEndPanel;
    public GameObject stupidEnd1Panel;
    public GameObject stupidEnd2Panel;

    [Header("Gameplay Timer")]
    public float gameplayDuration = 10f;
    private float timer;
    private bool isGameActive = false;
    private bool hasSelectedApp = false;

    void Start()
    {
        timer = gameplayDuration;
        isGameActive = true;
    }

    void Update()
    {
        if (!isGameActive || hasSelectedApp) return;

        HandleMovement();
        HandleInteraction();

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            ShowBadEnding();
        }
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(moveX, moveY).normalized * moveSpeed * Time.deltaTime;
        Vector2 newPos = handTransform.anchoredPosition + movement;

        // Dapatkan batas dari RectTransform parent (Canvas area)
        Vector2 minBounds = movementBounds.rect.min;
        Vector2 maxBounds = movementBounds.rect.max;

        // Clamp posisi
        newPos.x = Mathf.Clamp(newPos.x, minBounds.x, maxBounds.x);
        newPos.y = Mathf.Clamp(newPos.y, minBounds.y, maxBounds.y);

        handTransform.anchoredPosition = newPos;
    }


    void HandleInteraction()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Collider2D hit = Physics2D.OverlapCircle(interactionPoint.position, interactionRadius, appLayerMask);
            if (hit != null)
            {
                AppButton appButton = hit.GetComponent<AppButton>();
                if (appButton != null)
                {
                    hasSelectedApp = true;
                    isGameActive = false;

                    switch (appButton.assignedEnding)
                    {
                        case AppButton.EndingType.Good:
                            goodEndPanel.SetActive(true);
                            PointManager.Instance?.AddPoints(0);
                            break;

                        case AppButton.EndingType.Bad:
                            badEndPanel.SetActive(true);
                            PointManager.Instance?.AddPoints(1);
                            break;

                        case AppButton.EndingType.Stupid1:
                            stupidEnd1Panel.SetActive(true);
                            PointManager.Instance?.AddPoints(1);
                            break;

                        case AppButton.EndingType.Stupid2:
                            stupidEnd2Panel.SetActive(true);
                            PointManager.Instance?.AddPoints(2);
                            break;
                    }

                    Invoke(nameof(GoToNext), 3.0f); // Pindahkan ke dalam if
                }
            }
        }
    }


    void ShowBadEnding()
    {
        hasSelectedApp = true;
        isGameActive = false;
        badEndPanel.SetActive(true);
        PointManager.Instance?.AddPoints(3); // Atur poin default saat waktu habis
    }


    private void GoToNext()
    {
        Debug.Log("Scenario1 selesai. Menuju scenario berikutnya...");
        GameManager.Instance?.LoadNextStep();
    }
}
