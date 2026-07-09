using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LoginUI : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_Text statusText;
    public Toggle rememberMeToggle;

    [Header("Panels")]
    public GameObject loginPanel;
    public GameObject registerPanel;

    [Header("Verification")]
    public GameObject resendVerificationButton;

    private FirebaseAuthController authManager;
    private FirebaseDatabaseController dbManager;
    [SerializeField] protected SceneLoader sceneLoader;

    private const string PREF_REFRESH_TOKEN = "SavedRefreshToken";
    private const string PREF_REMEMBER = "RememberMeChecked";

    void Start()
    {
        authManager = gameObject.AddComponent<FirebaseAuthController>();
        dbManager = gameObject.AddComponent<FirebaseDatabaseController>();

        loginPanel.SetActive(true);
        registerPanel.SetActive(false);

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
        statusText.text = "Registering...";
        authManager.Register(emailInput.text, passwordInput.text, (success, message) =>
        {
            if (success)
            {
                // Gui email xac thuc ngay sau khi tao tai khoan xong
                authManager.SendEmailVerification((sent, sendMsg) =>
                {
                    statusText.text = sent
                        ? "Account created! A verification email has been sent to your inbox."
                        : sendMsg;
                });
            }
            else
            {
                statusText.text = message;
            }
            Debug.Log(message);
        });
    }

    public void OnLoginClicked()
    {
        statusText.text = "Logging in...";
        resendVerificationButton.SetActive(false); // an nut gui lai email xac thuc
        authManager.Login(emailInput.text, passwordInput.text, (success, message) =>
        {
            Debug.Log(message);

            if (success)
            {
                // Kiem tra email da xac thuc chua truoc khi cho vao game
                authManager.CheckEmailVerified((checkSuccess, isVerified, checkMsg) =>
                {
                    if (isVerified)
                    {
                        statusText.text = "Login successful!";
                        SaveRememberMe();
                        TestWriteData();
                        sceneLoader.LoadNextScene("MainMenu");
                    }
                    else
                    {
                        statusText.text = "Please verify your email before logging in.";
                        resendVerificationButton.SetActive(true);
                    }
                });
            }
            else
            {
                statusText.text = message;
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

    public void ShowRegisterPanel()
    {
        loginPanel.SetActive(false);
        registerPanel.SetActive(true);
        statusText.text = ""; // xoa thong bao cu
    }

    public void ShowLoginPanel()
    {
        registerPanel.SetActive(false);
        loginPanel.SetActive(true);
        statusText.text = "";
    }

    public void OnResendVerificationClicked()
    {
        if (string.IsNullOrEmpty(FirebaseAuthController.CurrentIdToken))
        {
            statusText.text = "Please log in first to resend verification email.";
            return;
        }

        statusText.text = "Sending...";
        authManager.SendEmailVerification((sent, message) =>
        {
            statusText.text = message;
        });
    }
}