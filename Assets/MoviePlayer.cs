using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoviePlayer : MonoBehaviour
{
    public enum VideoState { Normal, FastForward, Rewind, Paused }

    public VideoState videoState = VideoState.Normal;
    public Animator movieAnimator;
    public float movieDuration = 10f;
    public Slider timelineSlider;

    [HideInInspector]
    public float currentTime = 0f;

    private bool isPaused = false;

    void Update()
    {
        if (isPaused || videoState == VideoState.Paused)
        {
            movieAnimator.speed = 0f;
            return;
        }

        float delta = Time.deltaTime;

        switch (videoState)
        {
            case VideoState.Normal:
                movieAnimator.speed = 1f;
                currentTime += delta;
                break;

            case VideoState.FastForward:
                movieAnimator.speed = 2f;
                currentTime += delta * 2f;
                break;

            case VideoState.Rewind:
                movieAnimator.speed = 1f; // tetap forward, tapi currentTime mundur
                currentTime -= delta * 2f;
                break;
        }

        currentTime = Mathf.Clamp(currentTime, 0f, movieDuration);
        timelineSlider.value = currentTime / movieDuration;
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
    }

    public bool IsPaused()
    {
        return isPaused;
    }
}
