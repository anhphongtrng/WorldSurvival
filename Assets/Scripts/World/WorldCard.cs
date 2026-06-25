using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WorldCard : MonoBehaviour
{
    [Header("World Data")]
    public WorldDataSO worldData;

    [Header("UI Elements")]
    public Image thumbnailImage;
    public Image backgroundImage;
    public TextMeshProUGUI worldNameText;
    public TextMeshProUGUI descriptionText;
    public Button playButton;
    public GameObject lockOverlay;
    public Image lockIconImage;

    [Header("Lock Visual Settings")]
    [Range(0f, 1f)]
    public float lockedAlpha = 0.4f;    // Do mo khi bị lock
    public Color lockedTintColor = new(0.5f, 0.5f, 0.5f, 1f); // Mau sac khi bị lock

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }


    public void Setup(WorldDataSO data, System.Action<WorldDataSO> onPlayCallback)
    {
        worldData = data;
 
        if (worldNameText)  worldNameText.text  = data.worldName;
        if (descriptionText) descriptionText.text = data.worldDescription;
        if (thumbnailImage && data.worldThumbnail) thumbnailImage.sprite = data.worldThumbnail;
        if (backgroundImage) backgroundImage.color = data.worldColor;
 
        bool unlocked = WorldController.instance.IsWorldUnlocked(data.worldIndex);
        bool completed = WorldController.instance.IsWorldCompleted(data.worldIndex);
 
        ApplyLockState(unlocked);
 
        playButton.onClick.RemoveAllListeners();
        playButton.onClick.AddListener(() => onPlayCallback?.Invoke(data));
 
        if (completed && unlocked)
            ShowCompletedState();
    }

    public void ApplyLockState(bool unlocked)
    {
        playButton.interactable = unlocked;

        canvasGroup.alpha = unlocked ? 1f : lockedAlpha;
        canvasGroup.interactable = unlocked;
        canvasGroup.blocksRaycasts = true; // Van cho raycast

        if (lockOverlay)
            lockOverlay.SetActive(!unlocked);
    }

    public void ShowCompletedState()
    {

        if (backgroundImage)
        {
            Color c = backgroundImage.color;
            backgroundImage.color = new Color(c.r * 0.85f, c.g, c.b * 0.85f, c.a);
        }
    }

    public void Refresh()
    {
        if (worldData == null) return;
        bool unlocked = WorldController.instance.IsWorldUnlocked(worldData.worldIndex);
        ApplyLockState(unlocked);
    }
}
