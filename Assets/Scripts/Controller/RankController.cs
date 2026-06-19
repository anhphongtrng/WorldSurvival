using UnityEngine;

public class RankController : MonoBehaviour
{

    public static void SaveBestTime(string sceneName, float timeUsed)
    {
        string keyName = "BestTimeOf" + sceneName;
        if(PlayerPrefs.HasKey(keyName))
        {
            float bestTime = PlayerPrefs.GetFloat(keyName);
            if(timeUsed < bestTime)
            {
                PlayerPrefs.SetFloat(keyName, timeUsed);
                Debug.Log("New best time for " + sceneName + ": " + timeUsed);
            }
        }
        else
        {
            PlayerPrefs.SetFloat(keyName, timeUsed);
            Debug.Log("First time for " + sceneName + ": " + timeUsed);
        }
        PlayerPrefs.Save();
    }

    public static bool HasRecord(string sceneName)
    {
        return PlayerPrefs.HasKey("BestTimeOf" + sceneName);
    }

    public static float GetBestTime(string sceneName)
    {
        string keyName = "BestTimeOf" + sceneName;
        if(PlayerPrefs.HasKey(keyName))
        {
            return PlayerPrefs.GetFloat(keyName);
        }
        else
        {
            return -1f; // khong co Record
        }
    }
}
