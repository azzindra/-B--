using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonController : MonoBehaviour
{
    public Animator movieAnimator;
    public TextMeshProUGUI resultText;
    public float movieDuration = 10f;
    public RectTransform customCursor;
    public Slider durationSlider;

    private float elapsed = 0f;
    private float pauseTimer = 0f;
    private bool finished = false;
    private bool hasPressedAnyButton = false;

    private Button ffButton;
    private Button pauseButton;
    private Button rewindButton;

    private enum VideoState { Normal, FastForward, Rewind, Paused }
    private VideoState videoState = VideoState.Normal;

    void Start()
    {
        ffButton = GameObject.Find("ButtonFastForward").GetComponent<Button>();
        pauseButton = GameObject.Find("ButtonPause").GetComponent<Button>();
        rewindButton = GameObject.Find("ButtonRewind").GetComponent<Button>();
    }

    void Update()
    {
        if (finished) return;

        Vector2 cursorPos = customCursor.position;

        if (!hasPressedAnyButton && Input.GetKeyDown(KeyCode.Space))
        {
            if (IsCursorOverButton(ffButton, cursorPos))
            {
                hasPressedAnyButton = true;
                videoState = VideoState.FastForward;
                movieAnimator.speed = 2f;
            }
            else if (IsCursorOverButton(pauseButton, cursorPos))
            {
                hasPressedAnyButton = true;
                videoState = VideoState.Paused;
                movieAnimator.speed = 0f;
                pauseTimer = 0f;
            }
            else if (IsCursorOverButton(rewindButton, cursorPos))
            {
                hasPressedAnyButton = true;
                videoState = VideoState.Rewind;
                movieAnimator.speed = -1f;
            }
        }

        // State Handling
        switch (videoState)
        {
            case VideoState.Normal:
                elapsed += Time.deltaTime;
                if (elapsed >= movieDuration)
                {
                    EndScenario("What a great movie....");
                }
                break;

            case VideoState.FastForward:
                elapsed += Time.deltaTime * 2f;
                if (elapsed >= movieDuration)
                {
                    EndScenario("come on what we watching right now bro.....");
                }
                break;

            case VideoState.Rewind:
                elapsed -= Time.deltaTime;
                if (elapsed <= 0f)
                {
                    elapsed = 0f;
                    EndScenario("come on what we watching right now bro.....");
                }
                break;

            case VideoState.Paused:
                pauseTimer += Time.deltaTime;
                if (pauseTimer >= 3f)
                {
                    EndScenario("come on what we watching right now bro.....");
                }
                break;
        }

        // Update slider
        durationSlider.value = Mathf.Clamp01(elapsed / movieDuration);
    }

    bool IsCursorOverButton(Button button, Vector2 cursorPos)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(
            button.GetComponent<RectTransform>(), cursorPos, null
        );
    }

    void EndScenario(string message)
    {
        if (finished) return;
        finished = true;
        movieAnimator.speed = 0f;
        resultText.text = message;
        Debug.Log("Scenario Selesai: " + message);
    }
}
