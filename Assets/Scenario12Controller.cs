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
    private bool scenarioActive = false; // ✅ Pastikan ini ADA

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
        PointManager.Instance?.AddPoints(0); // ✅ poin untuk Good Ending
        GameManager.Instance?.AdvanceDay();
        Invoke(nameof(GoToNext), 3.0f);
    }

    private void ShowBadEnding()
    {
        badEndingPanel.SetActive(true);
        PointManager.Instance?.AddPoints(1); // ✅ poin untuk Good Ending
        GameManager.Instance?.AdvanceDay();
        Invoke(nameof(GoToNext), 3.0f);
    }

    public void RegisterCollision(PlayerController player, NPCController npc)
    {
        if (!hasCollided && scenarioActive) // ✅ Pakai hanya jika butuh
        {
            hasCollided = true;
            Debug.Log("==> Collision Terdeteksi: BAD END akan dipicu!");
        }

        Debug.Log("==> Menjalankan efek tabrakan: SlowDown + GetHit");
        player.SlowDown(slowDuration);
        npc.GetHit(slowDuration);
    }
    private void GoToNext()
    {
        Debug.Log("Scenario1 selesai. Menuju scenario berikutnya...");
        GameManager.Instance?.LoadNextStep();
    }
}
