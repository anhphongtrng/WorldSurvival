using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePauseMenuController : MonoBehaviour
{
    [SerializeField] protected GameObject gamePauseMenu;
    [SerializeField] protected SceneLoader sceneLoader;


    public void PauseGame()
    {
        UIController.instance.ShowGamePauseMenu(true);
        GamePauseController.instance.SetMenuPause(true);
    }

    public void ContinueGame()
    {
        UIController.instance.ShowGamePauseMenu(false);
        GamePauseController.instance.SetMenuPause(false);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Gamessss");
        sceneLoader.LoadNextScene("MainMenu");
    }

    public void OpenOptions()
    {
        UIController.instance.SetVolumeSettingsPanel(true);
        Debug.Log("Open Option");
    }
}
