using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoviePlayer : MonoBehaviour
{
    public Animator movieAnimator;
    public float movieDuration = 10f;
    public Slider timelineSlider;
    public TextMeshProUGUI resultText;

    private float currentTime = 0f;
    private bool isPlaying = true;
    private bool scenarioFinished = false;

    void Update()
    {
        if (scenarioFinished) return;

        if (isPlaying)
        {
            currentTime += Time.deltaTime * movieAnimator.speed;
            timelineSlider.value = currentTime / movieDuration;

            if (currentTime >= movieDuration)
            {
                EndScenario("What a great movie....");
            }
        }
    }

    public void Play()
    {
        movieAnimator.speed = 1f;
        isPlaying = true;
    }

    public void Pause()
    {
        movieAnimator.speed = 0f;
        isPlaying = false;
    }

    public void FastForward()
    {
        movieAnimator.speed = 2f;
    }

    public void Rewind()
    {
        movieAnimator.speed = -1f; // Harus punya clip yang bisa main balik
    }

    void EndScenario(string message)
    {
        scenarioFinished = true;
        resultText.text = message;
        movieAnimator.speed = 0f;
    }
}
