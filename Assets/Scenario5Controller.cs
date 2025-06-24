using UnityEngine;
using System.Collections;

public class Scenario5Controller : MonoBehaviour
{
    [Header("Cutscene Intro")]
    public GameObject cutsceneIntro1;
    public GameObject cutsceneIntro2;
    public float cutscene1Duration = 2f;
    public float cutscene2Duration = 2f;

    [Header("Gameplay Settings")]
    public GameObject playerMotor;
    public GameObject inputHandler;

    public GameObject ScenarioGameplay;
    private PlayerMotorController playerMotorScript;
    private Rigidbody playerRb;
    public float scenarioDuration = 10f;
    private float timer;
    private bool isScenarioActive = false;
    private bool isEndingTriggered = false;

    [Header("Ending Cutscenes")]
    public GameObject ending1Cutscene;
    public GameObject ending2Cutscene;
    public GameObject goodEndingCutscene;

    void Start()
    {
        playerMotorScript = playerMotor.GetComponent<PlayerMotorController>();
        if (playerMotorScript != null)
            playerMotorScript.enabled = false;

        inputHandler.SetActive(false);

        // Freeze Rigidbody selama cutscene
        playerRb = playerMotor.GetComponent<Rigidbody>();
        if (playerRb != null)
        {
            playerRb.velocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;
            playerRb.constraints = RigidbodyConstraints.FreezeAll;
        }

        StartCoroutine(PlayIntroCutscenes());
    }

    IEnumerator PlayIntroCutscenes()
    {
        // Cutscene Intro 1
        if (cutsceneIntro1 != null)
        {
            cutsceneIntro1.SetActive(true);
            yield return new WaitForSeconds(cutscene1Duration);
            cutsceneIntro1.SetActive(false);
        }

        // Cutscene Intro 2
        if (cutsceneIntro2 != null)
        {
            cutsceneIntro2.SetActive(true);
            yield return new WaitForSeconds(cutscene2Duration);
            cutsceneIntro2.SetActive(false);
        }

        // Setelah kedua cutscene selesai, mulai gameplay
        StartScenario();
    }

    void StartScenario()
    {
        if (ScenarioGameplay != null)
        {
            ScenarioGameplay.SetActive(true);
        }

        timer = scenarioDuration;

        if (playerMotorScript != null)
            playerMotorScript.enabled = true;

        inputHandler.SetActive(true);

        // Unfreeze Rigidbody kecuali rotasi
        if (playerRb != null)
        {
            playerRb.constraints = RigidbodyConstraints.None;
            playerRb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
    }

    void Update()
    {
        if (!isScenarioActive || isEndingTriggered) return;

        timer -= Time.deltaTime;

        if (timer <= 0f && !isEndingTriggered)
        {
            TriggerGoodEnding();
        }
    }

    public void TriggerOvertakeEnding()
    {
        if (isEndingTriggered) return;

        isEndingTriggered = true;
        isScenarioActive = false;

        ending1Cutscene?.SetActive(true);
        PointManager.Instance?.AddPoints(1);
        Invoke(nameof(GoToNext), 3.0f);
    }

    public void TriggerCrashEnding()
    {
        if (isEndingTriggered) return;

        isEndingTriggered = true;
        isScenarioActive = false;

        ending2Cutscene?.SetActive(true);
        PointManager.Instance?.AddPoints(2);
        Invoke(nameof(GoToNext), 3.0f);
    }

    void TriggerGoodEnding()
    {
        isEndingTriggered = true;
        isScenarioActive = false;

        goodEndingCutscene?.SetActive(true);
        PointManager.Instance?.AddPoints(0);
        Invoke(nameof(GoToNext), 3.0f);
    }
    private void GoToNext()
    {
        Debug.Log("Scenario1 selesai. Menuju scenario berikutnya...");
        GameManager.Instance?.LoadNextStep();
    }
}
