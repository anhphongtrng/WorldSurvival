using UnityEngine;

public class RankController : MonoBehaviour
{
    public static void SaveBestTime(string sceneName, float timeUsed)
    {
        string keyName = GetScopedKey(sceneName);
        bool isNewBest = false;

        if (PlayerPrefs.HasKey(keyName))
        {
            float bestTime = PlayerPrefs.GetFloat(keyName);
            if (timeUsed < bestTime)
            {
                PlayerPrefs.SetFloat(keyName, timeUsed);
                Debug.Log("New best time for " + sceneName + ": " + timeUsed);
                isNewBest = true;
            }
        }
        else
        {
            PlayerPrefs.SetFloat(keyName, timeUsed);
            Debug.Log("First time for " + sceneName + ": " + timeUsed);
            isNewBest = true;
        }
        PlayerPrefs.Save();

        // Chi dong bo len Firebase khi co ky luc moi, va khi da co FirebaseManager + dang login
        if (isNewBest && FirebaseController.instance != null && !string.IsNullOrEmpty(FirebaseAuthController.CurrentUserId))
        {
            FirebaseController.instance.dbController.SaveRankToFirebase(sceneName, timeUsed);
        }
    }

    public static bool HasRecord(string sceneName)
    {
        return PlayerPrefs.HasKey(GetScopedKey(sceneName));
    }

    public static float GetBestTime(string sceneName)
    {
        string keyName = GetScopedKey(sceneName);
        if (PlayerPrefs.HasKey(keyName))
        {
            return PlayerPrefs.GetFloat(keyName);
        }
        else
        {
            return -1f; // khong co Record
        }
    }

    // Gan them userId vao key de tach du lieu giua cac tai khoan tren cung 1 may
    // Neu chua login (guest), dung chung key "guest" rieng, khong dinh voi acc nao
    private static string GetScopedKey(string sceneName)
    {
        string userId = string.IsNullOrEmpty(FirebaseAuthController.CurrentUserId)
            ? "guest"
            : FirebaseAuthController.CurrentUserId;
        return "BestTimeOf" + sceneName + "_" + userId;
    }
}