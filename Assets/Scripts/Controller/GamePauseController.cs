using UnityEngine;
using UnityEngine.InputSystem;

public class GamePauseController : MonoBehaviour
{
    public static GamePauseController instance;

    public bool isPaused;
    private bool isOverTimePaused;
    private bool isGameOverPaused;
    private bool isStatPaused;
    private bool isMenuPaused;
    private bool isGuidePaused;
    private bool isEndGamePaused;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        UpdatePauseState();
    }

    public void SetOverTimePause(bool value)
    {
        isOverTimePaused = value;
    }

    public void SetGameOverPause(bool value)
    {
        isGameOverPaused = value;
    }

    public void SetStatPause(bool value)
    {
        isStatPaused = value;
    }

    public void SetMenuPause(bool value)
    {
        isMenuPaused = value;
    }

    public void SetGuidePause(bool value)
    {
        isGuidePaused = value;
    }

    public void SetEndGamePause(bool value)
    {
        isEndGamePaused = value;
    }

    public void PressToPause()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            isMenuPaused = !isMenuPaused;
            if (isMenuPaused)
            {
                UIController.instance.ShowGamePauseMenu(true);
            }
            else
            {
                UIController.instance.ShowGamePauseMenu(false);
            }
        }
    }

    private void UpdatePauseState()
    {
        PressToPause();
        if (isOverTimePaused || isGameOverPaused || isStatPaused || isMenuPaused || isGuidePaused || isEndGamePaused)
        {
            isPaused = true;
            Time.timeScale = 0f;
        }
        else
        {
            isPaused = false;
            Time.timeScale = 1f;
        }
    }
}
