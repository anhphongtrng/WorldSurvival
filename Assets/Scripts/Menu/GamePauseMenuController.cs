using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePauseMenuController : MonoBehaviour
{
    [SerializeField] protected GameObject gamePauseMenu;
    [SerializeField] protected SceneLoader sceneLoader;

    private bool isPaused;

    private void Start()
    {
        isPaused = false;
    }

    public void PauseGame()
    {
        UIController.instance.ShowGamePauseMenu(isPaused);
        GamePauseController.instance.SetMenuPause(true);
        isPaused = true;
    }

    public void ContinueGame()
    {
        UIController.instance.ShowGamePauseMenu(isPaused);
        GamePauseController.instance.SetMenuPause(false);
        isPaused = false;
    }

    public void QuitGame()
    {
        Debug.Log("Quit Gamessss");
        sceneLoader.LoadNextScene("MainMenu");
    }

    public void OpenOption()
    {
        Debug.Log("Open Option");
    }
}
