using UnityEngine;

public class GamePauseController : MonoBehaviour
{
    public static GamePauseController instance;

    private bool isOverTimePaused;
    private bool isGameOverPaused;
    private bool isStatPaused;
    private bool isMenuPaused;

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

    private void UpdatePauseState()
    {
        if (isOverTimePaused || isGameOverPaused || isStatPaused || isMenuPaused)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }
}
