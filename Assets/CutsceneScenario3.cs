using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneScenario3 : MonoBehaviour
{
    public static CutsceneScenario3 Instance;

    public GameObject goodEndingPanel;
    public GameObject badEnding1Panel;
    public GameObject badEnding2Panel;

    void Awake()
    {
        Instance = this;
    }

    public void PlayCutscene(string name)
    {
        switch (name)
        {
            case "GoodEnding":
                goodEndingPanel.SetActive(true);
                break;
            case "BadEnding1":
                badEnding1Panel.SetActive(true);
                break;
            case "BadEnding2":
                badEnding2Panel.SetActive(true);
                break;
        }
    }
}

