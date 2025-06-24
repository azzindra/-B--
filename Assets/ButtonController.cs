using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonController : MonoBehaviour
{
    public RectTransform customCursor;

    private Button ffButton;
    private Button pauseButton;
    private Button rewindButton;

    private MoviePlayer movie;

    private bool isPauseTriggeredOnce = false;

    void Start()
    {
        ffButton = GameObject.Find("ButtonFastForward").GetComponent<Button>();
        pauseButton = GameObject.Find("ButtonPause").GetComponent<Button>();
        rewindButton = GameObject.Find("ButtonRewind").GetComponent<Button>();

        movie = FindObjectOfType<MoviePlayer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector2 cursorPos = customCursor.position;

            if (!isPauseTriggeredOnce && IsCursorOverButton(pauseButton, cursorPos))
            {
                movie.videoState = MoviePlayer.VideoState.Paused;
                isPauseTriggeredOnce = true;
            }
            else if (!isPauseTriggeredOnce)
            {
                if (IsCursorOverButton(ffButton, cursorPos))
                    movie.videoState = MoviePlayer.VideoState.FastForward;
                else if (IsCursorOverButton(rewindButton, cursorPos))
                    movie.videoState = MoviePlayer.VideoState.Rewind;
                else
                    movie.videoState = MoviePlayer.VideoState.Normal;
            }
        }
    }

    bool IsCursorOverButton(Button button, Vector2 cursorPos)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(
            button.GetComponent<RectTransform>(), cursorPos, null
        );
    }
}
