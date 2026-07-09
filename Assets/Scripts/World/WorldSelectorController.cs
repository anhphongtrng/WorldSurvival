using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class WorldSelectorController : MonoBehaviour
{
    [Header("Worlds Data")]
    public WorldDataSO[] worlds;

    [Header("Card Prefab & Container")]
    public GameObject worldCardPrefab;  
    public Transform cardContainer;

    [Header("Confirmation Panel")]
    public GameObject confirmPanel;
    public TextMeshProUGUI confirmWorldName;
    public Button confirmYesButton;
    public Button confirmNoButton;

    [Header("Loading")]
    //public GameObject loadingPanel;

    [Header("Button")]
    public Button backButton;
    public Button resetButton;

    private WorldCard[] spawnedCards;
    private WorldDataSO pendingWorld;

    [SerializeField] private SceneLoader sceneLoader;

    private void Start()
    {
        SpawnWorldCards();
        SetupUI();
    }

    public void SpawnWorldCards()
    {
        spawnedCards = new WorldCard[worlds.Length];

        for (int i = 0; i < worlds.Length; i++)
        {
            GameObject cardGO = Instantiate(worldCardPrefab, cardContainer);
            WorldCard card = cardGO.GetComponent<WorldCard>();
            card.Setup(worlds[i], OnWorldSelected);
            spawnedCards[i] = card;
        }
    }


    public void SetupUI()
    {
        //if (loadingPanel) loadingPanel.SetActive(false);

        // Confirm panel
        if (confirmPanel)
        {
            confirmPanel.SetActive(false);
            confirmYesButton?.onClick.AddListener(OnConfirmYes);
            confirmNoButton?.onClick.AddListener(OnConfirmNo);
        }
    }


    public void OnWorldSelected(WorldDataSO selectedWorld)
    {
        pendingWorld = selectedWorld;

        if (confirmPanel != null)
        {
            if (confirmWorldName) confirmWorldName.text ="Go to " + selectedWorld.worldName + "?";
            confirmPanel.SetActive(true);
        }
        else
        {
            LoadWorld(selectedWorld);
        }
    }

    public void OnConfirmYes()
    {
        confirmPanel.SetActive(false);
        LoadWorld(pendingWorld);
    }

    public void OnConfirmNo()
    {
        confirmPanel.SetActive(false);
        pendingWorld = null;
    }

    public void LoadWorld(WorldDataSO world)
    {
        StartCoroutine(LoadWorldCoroutine(world));
    }

    IEnumerator LoadWorldCoroutine(WorldDataSO world)
    {
        PlayerPrefs.SetInt("CurrentWorldIndex", world.worldIndex);
        PlayerPrefs.Save();

        sceneLoader.LoadNextScene(world.worldSceneName);
        yield break;
    }

    public void OnBackButtonPressed()
    {
        sceneLoader.LoadNextScene("MainMenu");
    }

    public void RefreshAllCards()
    {
        if (spawnedCards == null) return;
        foreach (var card in spawnedCards)
            card?.Refresh();
    }

    public void OnResetProgress()
    {
        WorldController.instance.Debug_ResetAll();
        RefreshAllCards();
    }
}