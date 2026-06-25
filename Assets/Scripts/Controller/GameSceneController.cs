using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneController : MonoBehaviour
{

    public void CompleteCurrentWorld()
    {
        int currentWorldIndex = PlayerPrefs.GetInt("CurrentWorldIndex", 0);
        WorldController.instance.CompleteWorld(currentWorldIndex);

        Debug.Log($"World {currentWorldIndex} completed! Next world unlocked.");
    }

    public void ReturnToWorldSelector()
    {
        SceneManager.LoadScene("WorldSelector");
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
