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
    private const string SEND_VERIFICATION_URL = "https://identitytoolkit.googleapis.com/v1/accounts:sendOobCode?key=";
    private const string LOOKUP_URL = "https://identitytoolkit.googleapis.com/v1/accounts:lookup?key=";
    private const string UPDATE_PROFILE_URL = "https://identitytoolkit.googleapis.com/v1/accounts:update?key=";

    // Luu lai sau khi login thanh cong, dung de goi Database sau nay
    public static string CurrentIdToken { get; private set; }
    public static string CurrentUserId { get; private set; }
    public static string CurrentRefreshToken { get; private set; }
    public static string CurrentDisplayName { get; private set; }

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

    [Serializable]
    private class OobCodeRequest
    {
        public string requestType = "VERIFY_EMAIL";
        public string idToken;
    }

    [Serializable]
    private class LookupRequest
    {
        public string idToken;
    }

    [Serializable]
    private class LookupResponse
    {
        public UserInfo[] users;
    }

    [Serializable]
    private class UserInfo
    {
        public string localId;
        public string email;
        public bool emailVerified;
        public string displayName;
    }

    [Serializable]
    private class UpdateProfileRequest
    {
        public string idToken;
        public string displayName;
        public bool returnSecureToken = true;
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

    // Goi ham nay sau khi Register thanh cong de gui email xac thuc
    public void SendEmailVerification(Action<bool, string> onComplete)
    {
        StartCoroutine(SendVerificationRequest(onComplete));
    }

    // Goi ham nay de kiem tra xem user da xac thuc email chua
    public void CheckEmailVerified(Action<bool, bool, string> onComplete)
    {
        StartCoroutine(CheckVerifiedRequest(onComplete));
    }

    private IEnumerator SendVerificationRequest(Action<bool, string> onComplete)
    {
        string url = SEND_VERIFICATION_URL + API_KEY;
        OobCodeRequest reqBody = new() { idToken = CurrentIdToken };
        string jsonBody = JsonUtility.ToJson(reqBody);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);

        using UnityWebRequest request = new(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            onComplete?.Invoke(true, "Verification email sent. Please check your inbox.");
        }
        else
        {
            string errorMsg = ParseErrorMessage(request.downloadHandler.text);
            onComplete?.Invoke(false, "Failed to send verification email: " + errorMsg);
        }
    }

    // bool thu 2 la trang thai emailVerified (true/false), bool thu 1 la request co thanh cong khong
    private IEnumerator CheckVerifiedRequest(Action<bool, bool, string> onComplete)
    {
        string url = LOOKUP_URL + API_KEY;
        LookupRequest reqBody = new() { idToken = CurrentIdToken };
        string jsonBody = JsonUtility.ToJson(reqBody);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);

        using UnityWebRequest request = new(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            LookupResponse response = JsonUtility.FromJson<LookupResponse>(request.downloadHandler.text);
            bool verified = response.users != null && response.users.Length > 0 && response.users[0].emailVerified;
            CurrentDisplayName = response.users != null && response.users.Length > 0 ? response.users[0].displayName : "";
            onComplete?.Invoke(true, verified, verified ? "Email verified" : "Email not verified yet");
        }
        else
        {
            onComplete?.Invoke(false, false, "Could not check verification status");
        }
    }

    // Goi ham nay sau khi Register thanh cong de gan display name cho tai khoan
    public void UpdateDisplayName(string displayName, Action<bool, string> onComplete)
    {
        StartCoroutine(SendUpdateProfileRequest(displayName, onComplete));
    }

    private IEnumerator SendUpdateProfileRequest(string displayName, Action<bool, string> onComplete)
    {
        string url = UPDATE_PROFILE_URL + API_KEY;
        UpdateProfileRequest reqBody = new()
        {
            idToken = CurrentIdToken,
            displayName = displayName
        };
        string jsonBody = JsonUtility.ToJson(reqBody);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);

        using UnityWebRequest request = new(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            onComplete?.Invoke(true, "Display name updated");
        }
        else
        {
            string errorMsg = ParseErrorMessage(request.downloadHandler.text);
            onComplete?.Invoke(false, "Failed to update display name: " + errorMsg);
        }
    }
}