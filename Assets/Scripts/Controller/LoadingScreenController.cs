using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenController : MonoBehaviour
{
    [Header("References")]
    public GameObject loadingCanvas;
    public GameObject loginCanvas;
    public Slider progressSlider;
    public TextMeshProUGUI progressText;
    public LoginUI loginUI;

    [Header("Settings")]
    public float fakeLoadDuration = 2.5f;

    void Start()
    {
        loginCanvas.SetActive(false);
        loadingCanvas.SetActive(true);
        StartCoroutine(FakeLoadingRoutine());
    }

    IEnumerator FakeLoadingRoutine()
    {
        float elapsed = 0f;
        while (elapsed < fakeLoadDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            float progress = Mathf.Clamp01(elapsed / fakeLoadDuration);
            progressSlider.value = progress;
            if (progressText != null)
                progressText.text = Mathf.RoundToInt(progress * 100f) + "%";
            yield return null;
        }

        loadingCanvas.SetActive(false);

        bool wasRemembered = PlayerPrefs.GetInt("RememberMeChecked", 0) == 1;

        if (wasRemembered)
        {
            loginCanvas.SetActive(true);
            loginUI.TryAutoLogin();
        }
        else
        {
            loginCanvas.SetActive(true);
        }
    }
}