using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public static MainMenuController instance;
    [SerializeField] private SceneLoader sceneLoader;

    protected void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Button Play
    public void PlayGame()
    {
        sceneLoader.LoadNextScene("DesertWorld");
    }

    // Button Quit
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}