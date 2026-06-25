using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public static MainMenuController instance;
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private VolumeSettings volumeSettings;
    [SerializeField] private GameObject rankBoardPanel;


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
        sceneLoader.LoadNextScene("WorldSelector");
    }

    // Button Quit
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void OpenOption()
    {
        volumeSettings.OpenVolumeSettings();
        Debug.Log("Open Option");
    }
    public void OpenRankBoard()
    {
        rankBoardPanel.SetActive(true);
    }

    public void CloseRankBoard()
    {
        rankBoardPanel.SetActive(false);
    }
}