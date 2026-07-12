using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class FirebaseDatabaseController : MonoBehaviour
{
    // Dien URL database cua ban vao day, lay tu Firebase Console > Realtime Database > Data > URL
    private const string DATABASE_URL = "https://mygame-5bf0f-default-rtdb.asia-southeast1.firebasedatabase.app/";

    // Ghi du lieu (VD: luu save game, diem so...)
    // path vi du: "players/" + userId + "/save"
    public void SetData(string path, string jsonData, Action<bool, string> onComplete)
    {
        StartCoroutine(PutData(path, jsonData, onComplete));
    }

    // Doc du lieu
    public void GetData(string path, Action<bool, string> onComplete)
    {
        StartCoroutine(FetchData(path, onComplete));
    }

    private IEnumerator PutData(string path, string jsonData, Action<bool, string> onComplete)
    {
        string url = BuildUrl(path);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);

        using UnityWebRequest request = new(url, "PUT");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
            onComplete?.Invoke(true, request.downloadHandler.text);
        else
            onComplete?.Invoke(false, "Error writing data: " + request.error);
    }

    private IEnumerator FetchData(string path, Action<bool, string> onComplete)
    {
        string url = BuildUrl(path);

        using UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
            onComplete?.Invoke(true, request.downloadHandler.text);
        else
            onComplete?.Invoke(false, "Error reading data: " + request.error);
    }

    // Ghep URL kem token xac thuc (neu nguoi dung da login) de tuan theo rule bao mat
    private string BuildUrl(string path)
    {
        string url = DATABASE_URL + "/" + path + ".json";

        if (!string.IsNullOrEmpty(FirebaseAuthController.CurrentIdToken))
        {
            url += "?auth=" + FirebaseAuthController.CurrentIdToken;
        }

        return url;
    }

    // Luu best time len Firebase, gan voi userId va displayName
    public void SaveRankToFirebase(string sceneName, float timeUsed)
    {
        string userId = FirebaseAuthController.CurrentUserId;
        if (string.IsNullOrEmpty(userId))
        {
            Debug.Log("User not logged in, skip Firebase rank save");
            return;
        }

        string displayName = FirebaseAuthController.CurrentDisplayName;
        if (string.IsNullOrEmpty(displayName)) displayName = "Player";

        string path = "leaderboards/" + sceneName + "/" + userId;
        string jsonData = "{\"time\":" + timeUsed.ToString(System.Globalization.CultureInfo.InvariantCulture)
                         + ",\"displayName\":\"" + displayName + "\""
                         + ",\"timestamp\":" + System.DateTimeOffset.UtcNow.ToUnixTimeSeconds() + "}";

        SetData(path, jsonData, (success, response) =>
        {
            Debug.Log(success ? "Rank synced to Firebase" : "Failed to sync rank: " + response);
        });
    }
}