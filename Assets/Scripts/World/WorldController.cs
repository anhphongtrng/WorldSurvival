using UnityEngine;

public class WorldController : MonoBehaviour
{
    public static WorldController instance;

    private const string WORLD_COMPLETED_KEY = "World_{0}_Completed";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool IsWorldUnlocked(int worldIndex)
    {
        if (worldIndex == 0) return true;

        return IsWorldCompleted(worldIndex - 1);
    }

    public bool IsWorldCompleted(int worldIndex)
    {
        string key = string.Format(WORLD_COMPLETED_KEY, worldIndex);
        return PlayerPrefs.GetInt(key, 0) == 1;
    }

    public void CompleteWorld(int worldIndex)
    {
        string key = string.Format(WORLD_COMPLETED_KEY, worldIndex);
        PlayerPrefs.SetInt(key, 1);
        PlayerPrefs.Save();
        Debug.Log($"World {worldIndex} completed & saved!");
    }

    public void Debug_ResetAll()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("All progress reset!");
    }
}
