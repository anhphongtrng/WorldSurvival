using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGameMenuController : MonoBehaviour
{
    [SerializeField] protected GameObject gamePauseMenu;
    [SerializeField] protected SceneLoader sceneLoader;

    private bool isPaused;

    private void Start()
    {
        isPaused = false;
        gamePauseMenu.SetActive(isPaused);
    }

    public void PauseGame()
    {
        gamePauseMenu.SetActive(true);
        GamePauseController.instance.SetMenuPause(true);
        isPaused = true;
    }

    public void ContinueGame()
    {
        gamePauseMenu.SetActive(false);
        GamePauseController.instance.SetMenuPause(false);
        isPaused = false;
    }

    public void QuitGame()
    {
        Debug.Log("Quit Gamessss");
        sceneLoader.LoadNextScene("MainMenu");
        //SceneManager.LoadScene("MainMenu");
    }

    public void OpenOption()
    {
        Debug.Log("Open Option");
    }
}
