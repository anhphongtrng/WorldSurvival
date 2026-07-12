using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LoginUI : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_InputField confirmPasswordInput;
    public TMP_InputField displayNameInput;
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
        authManager = FirebaseController.instance.authController;
        dbManager = FirebaseController.instance.dbController;

        loginPanel.SetActive(true);
        registerPanel.SetActive(false);

        //TryAutoLogin();
    }

    // Kiem tra xem co refresh token da luu khong, neu co thi tu dong dang nhap
    public void TryAutoLogin()
    {
        bool wasRemembered = PlayerPrefs.GetInt(PREF_REMEMBER, 0) == 1;
        string savedToken = PlayerPrefs.GetString(PREF_REFRESH_TOKEN, "");

        if (wasRemembered && !string.IsNullOrEmpty(savedToken))
        {
            statusText.text = "Auto login...";
            authManager.RefreshLogin(savedToken, (success, message) =>
            {
                if (success)
                {
                    // RefreshLogin khong tra ve displayName, can goi them lookup de lay day du thong tin user
                    authManager.CheckEmailVerified((checkSuccess, isVerified, checkMsg) =>
                    {
                        statusText.text = isVerified ? "Login successful!" : "Please verify your email.";

                        if (isVerified)
                        {
                            SaveRememberMe();
                            sceneLoader.LoadNextScene("MainMenu");
                        }
                    });
                }
                else
                {
                    statusText.text = message;
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
        if (string.IsNullOrEmpty(displayNameInput.text))
        {
            statusText.text = "Please enter a display name.";
            return;
        }

        if (string.IsNullOrEmpty(emailInput.text) || string.IsNullOrEmpty(passwordInput.text))
        {
            statusText.text = "Please fill in all fields.";
            return;
        }

        if (passwordInput.text != confirmPasswordInput.text)
        {
            statusText.text = "Passwords do not match.";
            return;
        }

        if (passwordInput.text.Length < 6)
        {
            statusText.text = "Password must be at least 6 characters.";
            return;
        }

        statusText.text = "Registering...";
        authManager.Register(emailInput.text, passwordInput.text, (success, message) =>
        {
            if (success)
            {
                // Gan display name ngay sau khi tao tai khoan xong
                authManager.UpdateDisplayName(displayNameInput.text, (updateSuccess, updateMsg) =>
                {
                    Debug.Log(updateMsg);

                    // Gui email xac thuc sau khi da gan display name
                    authManager.SendEmailVerification((sent, sendMsg) =>
                    {
                        statusText.text = sent
                            ? "Account created! A verification email has been sent to your inbox."
                            : sendMsg;
                    });
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
                        Debug.Log("Display Name: " + FirebaseAuthController.CurrentDisplayName);
                        SaveRememberMe();
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

    public void OnForgotPasswordClicked()
    {
        if (string.IsNullOrEmpty(emailInput.text))
        {
            statusText.text = "Please enter your email first.";
            return;
        }

        statusText.text = "Sending reset email...";
        authManager.SendPasswordReset(emailInput.text, (success, message) =>
        {
            statusText.text = message;
            Debug.Log(message);
        });
    }
}