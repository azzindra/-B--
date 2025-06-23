using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scenario12Controller : MonoBehaviour
{
    public GameObject introPanel;
    public GameObject goodEndingPanel;
    public GameObject badEndingPanel;

    public float introDuration = 2f;
    public float scenarioDuration = 10f;

    public float slowDuration = 1f;
    private bool hasCollided = false;

    private bool scenarioActive = false;

    void Start()
    {
        StartCoroutine(ScenarioFlow());
    }

    private IEnumerator ScenarioFlow()
    {
        introPanel.SetActive(true);
        yield return new WaitForSeconds(introDuration);
        introPanel.SetActive(false);
        scenarioActive = true;

        yield return new WaitForSeconds(scenarioDuration);

        if (hasCollided)
            ShowBadEnding();
        else
            ShowGoodEnding();
    }

    private void ShowGoodEnding()
    {
        goodEndingPanel.SetActive(true);
    }

    private void ShowBadEnding()
    {
        badEndingPanel.SetActive(true);
    }

    public void RegisterCollision(PlayerController player, NPCController npc)
    {
        if (!hasCollided)
        {
            hasCollided = true;
            Debug.Log("==> Collision Terdeteksi: BAD END akan dipicu!");
        }

        Debug.Log("==> Menjalankan efek tabrakan: SlowDown + GetHit");
        player.SlowDown(slowDuration);
        npc.GetHit(slowDuration);
    }
}
