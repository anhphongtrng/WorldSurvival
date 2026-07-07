using UnityEngine;
using TMPro;

public class LoginUI : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_Text statusText;
    public UnityEngine.UI.Toggle rememberMeToggle;

    private FirebaseAuthController authManager;
    private FirebaseDatabaseController dbManager;
    [SerializeField] protected SceneLoader sceneLoader;

    private const string PREF_REFRESH_TOKEN = "SavedRefreshToken";
    private const string PREF_REMEMBER = "RememberMeChecked";

    void Start()
    {
        authManager = gameObject.AddComponent<FirebaseAuthController>();
        dbManager = gameObject.AddComponent<FirebaseDatabaseController>();

        TryAutoLogin();
    }

    // Kiem tra xem co refresh token da luu khong, neu co thi tu dong dang nhap
    private void TryAutoLogin()
    {
        bool wasRemembered = PlayerPrefs.GetInt(PREF_REMEMBER, 0) == 1;
        string savedToken = PlayerPrefs.GetString(PREF_REFRESH_TOKEN, "");

        if (wasRemembered && !string.IsNullOrEmpty(savedToken))
        {
            statusText.text = "Currently auto logging in...";
            authManager.RefreshLogin(savedToken, (success, message) =>
            {
                statusText.text = message;
                Debug.Log(message);

                if (success)
                {
                    SaveRememberMe(); // cap nhat refresh token moi neu co
                    sceneLoader.LoadNextScene("MainMenu");
                }
            });
        }
    }

    private void SaveRememberMe()
    {
        if (rememberMeToggle != null && rememberMeToggle.isOn)
        {
            PlayerPrefs.SetInt(PREF_REMEMBER, 1);
            PlayerPrefs.SetString(PREF_REFRESH_TOKEN, FirebaseAuthController.CurrentRefreshToken);
        }
        else
        {
            PlayerPrefs.SetInt(PREF_REMEMBER, 0);
            PlayerPrefs.DeleteKey(PREF_REFRESH_TOKEN);
        }
        PlayerPrefs.Save();
    }

    public void OnRegisterClicked()
    {
        statusText.text = "Currently registering...";
        authManager.Register(emailInput.text, passwordInput.text, (success, message) =>
        {
            statusText.text = message;
            Debug.Log(message);
        });
    }

    public void OnLoginClicked()
    {
        statusText.text = "Currently logging in...";
        authManager.Login(emailInput.text, passwordInput.text, (success, message) =>
        {
            statusText.text = message;
            Debug.Log(message);

            if (success)
            {
                SaveRememberMe();
                TestWriteData();
                sceneLoader.LoadNextScene("MainMenu");
            }
        });
    }

    private void TestWriteData()
    {
        string userId = FirebaseAuthController.CurrentUserId;
        string jsonData = "{\"lastLogin\":\"" + System.DateTime.Now.ToString() + "\",\"level\":1}";
        dbManager.SetData("players/" + userId, jsonData, (success, response) =>
        {
            Debug.Log(success ? "Data write successful: " + response : response);
        });
    }
}