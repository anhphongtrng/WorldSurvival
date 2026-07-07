using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class FirebaseAuthController : MonoBehaviour
{
    // DIEN API KEY CUA BAN VAO DAY (lay tu Firebase Console > Project Settings > General > Web API Key)
    private const string API_KEY = "AIzaSyAzpC94kFlsdMxUQludKoeP273_gXuEans";
    private const string SIGNUP_URL = "https://identitytoolkit.googleapis.com/v1/accounts:signUp?key=";
    private const string SIGNIN_URL = "https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=";
    private const string REFRESH_URL = "https://securetoken.googleapis.com/v1/token?key=";

    // Luu lai sau khi login thanh cong, dung de goi Database sau nay
    public static string CurrentIdToken { get; private set; }
    public static string CurrentUserId { get; private set; }
    public static string CurrentRefreshToken { get; private set; }

    [Serializable]
    private class AuthRequest
    {
        public string email;
        public string password;
        public bool returnSecureToken = true;
    }

    [Serializable]
    private class AuthResponse
    {
        public string idToken;
        public string localId;
        public string email;
        public string refreshToken;
        public string message; // chua ma loi neu co, VD: EMAIL_EXISTS

    }

    [Serializable]
    private class RefreshResponse
    {
        public string id_token;
        public string user_id;
        public string refresh_token;
    }

    // Goi ham nay de dang ky tai khoan moi
    public void Register(string email, string password, Action<bool, string> onComplete)
    {
        StartCoroutine(SendAuthRequest(SIGNUP_URL, email, password, onComplete));
    }

    // Goi ham nay de dang nhap, neu thanh cong se luu lai idToken, userId va refreshToken
    public void Login(string email, string password, Action<bool, string> onComplete)
    {
        StartCoroutine(SendAuthRequest(SIGNIN_URL, email, password, onComplete));
    }

    // Goi ham nay luc mo game de tu dong dang nhap lai bang refresh token da luu (Remember Me)
    public void RefreshLogin(string refreshToken, Action<bool, string> onComplete)
    {
        StartCoroutine(SendRefreshRequest(refreshToken, onComplete));
    }

    private IEnumerator SendAuthRequest(string baseUrl, string email, string password, Action<bool, string> onComplete)
    {
        string url = baseUrl + API_KEY;
        AuthRequest reqBody = new() { email = email, password = password };
        string jsonBody = JsonUtility.ToJson(reqBody);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);
        using UnityWebRequest request = new(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        string responseText = request.downloadHandler.text;
        if (request.result == UnityWebRequest.Result.Success)
        {
            AuthResponse response = JsonUtility.FromJson<AuthResponse>(responseText);
            CurrentIdToken = response.idToken;
            CurrentUserId = response.localId;
            CurrentRefreshToken = response.refreshToken;
            onComplete?.Invoke(true, "Complete! UserID: " + response.localId);
        }
        else
        {
            // Firebase tra loi dang JSon, co gang doc message loi cu the
            string errorMsg = ParseErrorMessage(responseText);
            onComplete?.Invoke(false, "Error! " + errorMsg);
        }
    }

    private IEnumerator SendRefreshRequest(string refreshToken, Action<bool, string> onComplete)
    {
        string url = REFRESH_URL + API_KEY;
        string form = "grant_type=refresh_token&refresh_token=" + refreshToken;
        byte[] bodyRaw = Encoding.UTF8.GetBytes(form);

        using UnityWebRequest request = new(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");

        yield return request.SendWebRequest();
        string responseText = request.downloadHandler.text;

        if (request.result == UnityWebRequest.Result.Success)
        {
            RefreshResponse response = JsonUtility.FromJson<RefreshResponse>(responseText);
            CurrentIdToken = response.id_token;
            CurrentUserId = response.user_id;
            CurrentRefreshToken = response.refresh_token;
            onComplete?.Invoke(true, "Auto login successful");
        }
        else
        {
            onComplete?.Invoke(false, "Session expired, please log in again");
        }
    }

    private string ParseErrorMessage(string json)
    {
        // Firebase tra loi dang: {"error":{"code":400,"message":"EMAIL_EXISTS",...}}
        try
        {
            int idx = json.IndexOf("\"message\"");
            if (idx >= 0)
            {
                int start = json.IndexOf(':', idx) + 2;
                int end = json.IndexOf('"', start);
                return json[start..end];
            }
        }
        catch { }
        return json;
    }
}