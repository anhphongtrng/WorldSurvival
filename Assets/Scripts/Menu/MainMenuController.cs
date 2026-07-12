using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public static MainMenuController instance;
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private VolumeSettings volumeSettings;
    [SerializeField] private GameObject rankBoardPanel;
    [SerializeField] private TextMeshProUGUI welcomeText;


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

    private void Start()
    {
        ShowPlayerName();
    }

    // Button Play
    public void PlayGame()
    {
        sceneLoader.LoadNextScene("WorldSelector");
    }

    // Button Quit
    public void QuitGame()
    {
        Debug.Log("Logging out! Returning to login screen...");

        // Delete Remember Me data
        PlayerPrefs.DeleteKey("SavedRefreshToken");
        PlayerPrefs.SetInt("RememberMeChecked", 0);
        PlayerPrefs.Save();

        sceneLoader.LoadNextScene("Login");
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

    private void ShowPlayerName()
    {
        string displayName = FirebaseAuthController.CurrentDisplayName;

        if (string.IsNullOrEmpty(displayName))
        {
            welcomeText.text = "Welcome, Player!";
        }
        else
        {
            welcomeText.text = "Welcome, " + displayName + "!";
        }
    }
}