using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Scenario11Controller : MonoBehaviour
{
    [Header("References")]
    public GameObject introPanel;
    public GameObject gameplayObjects;
    public GameObject vehicleObject;
    public GameObject lampuMerahObject;
    public Transform puddleTransform;
    public TextMeshProUGUI timerText;

    [Header("Ending Panels")]
    public GameObject endingPanelGood;
    public GameObject endingPanelBad;

    [Header("Settings")]
    public float lampuMerahDuration = 7f;
    public float stretchSpeed = 2f;
    public float maxWidth = 3f;
    public float minWidth = 0.1f;

    [Header("Colliders")]
    public Collider2D puddleCollider;
    public Collider2D motorCollider;
    [HideInInspector] public bool forceBadEnding = false;

    private float currentTimer;
    private bool isControllable = false;
    private bool hasTriggeredEnding = false;

    void Start()
    {
        introPanel.SetActive(true);
        gameplayObjects.SetActive(false);
        vehicleObject.SetActive(false);

        endingPanelGood.SetActive(false);
        endingPanelBad.SetActive(false);

        currentTimer = lampuMerahDuration;
    }

    void Update()
    {
        if (!isControllable && Input.anyKeyDown && introPanel.activeSelf)
        {
            StartGameplay();
        }

        if (isControllable)
        {
            HandleInput();

            currentTimer -= Time.deltaTime;
            timerText.text = Mathf.Ceil(currentTimer).ToString();

            if (currentTimer <= 0)
            {
                TriggerVehiclePass();
            }
        }
    }

    void StartGameplay()
    {
        introPanel.SetActive(false);
        gameplayObjects.SetActive(true);
        isControllable = true;
    }

    void HandleInput()
    {
        Vector3 scale = puddleTransform.localScale;

        if (Input.GetKey(KeyCode.D))
        {
            scale.x += stretchSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            scale.x -= stretchSpeed * Time.deltaTime;
        }

        scale.x = Mathf.Clamp(scale.x, minWidth, maxWidth);
        puddleTransform.localScale = scale;
    }

    void TriggerVehiclePass()
    {
        isControllable = false;

        // Ubah lampu merah jadi hijau
        if (lampuMerahObject.TryGetComponent<SpriteRenderer>(out var sr))
        {
            sr.color = Color.green;
        }

        // Aktifkan kendaraan dan mulai animasi
        vehicleObject.SetActive(true);

        if (vehicleObject.TryGetComponent<Animator>(out var animator))
        {
            animator.SetTrigger("Start");
        }

        // Mulai pengecekan setelah animasi (2 detik delay)
        Invoke(nameof(CheckEndingCondition), 2f);
    }

    public void CheckEndingCondition()
    {
        if (hasTriggeredEnding) return;
        hasTriggeredEnding = true;

        bool isTouching = puddleCollider.IsTouching(motorCollider);
        bool isMotorTouchingPuddle = isTouching || forceBadEnding;

        ShowEnding(isMotorTouchingPuddle ? "Bad" : "Good");
    }

    void ShowEnding(string result)
    {
        gameplayObjects.SetActive(false);

        if (result == "Good")
        {
            endingPanelGood.SetActive(true);
            PointManager.Instance?.AddPoints(0);
        }
        else
        {
            endingPanelBad.SetActive(true);
            PointManager.Instance?.AddPoints(1);
        }
        Debug.Log("Ending: " + result);
        Invoke(nameof(GoToNext), 3.0f);
    }

    public void ForceBadEnding()
    {
        forceBadEnding = true;
    }
    private void GoToNext()
    {
        Debug.Log("Scenario1 selesai. Menuju scenario berikutnya...");
        GameManager.Instance?.LoadNextStep();
    }

}
