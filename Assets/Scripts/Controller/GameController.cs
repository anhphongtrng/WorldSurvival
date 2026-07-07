using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    [Header("Boss Prefabs")]
    [SerializeField] private GameObject miniBossPrefab;
    [SerializeField] protected GameObject currentMiniBoss;
    [SerializeField] private GameObject finalBossPrefab;

    [Header("Boss Spawn")]
    [SerializeField] private int killsToSpawnBoss = 20;

    [Header("Mini Boss Progress")]
    [SerializeField] private int miniBossesNeeded = 3;

    public int miniBossesKilled = 0;
    public int worldLevel = 1;

    [Header("Stage Progress")]
    public bool isBossSpawned = false;
    [SerializeField] private bool isFinalBossUnlocked = false;
    [SerializeField] private bool isStageComplete = false;
    protected float delayBeforeEndGame = 1f;

    [Header("Music")]
    public AudioClip playerDeathClip;
    public AudioClip normalPhaseClip;
    public AudioClip miniBossPhaseClip;
    public AudioClip finalBossPhaseClip;
    public AudioClip winGameClip;

    [Header("Boss Phase")]
    public BossPhase currentBossPhase = BossPhase.MiniBoss;

    [SerializeField] protected BossArrowIndicator bossArrowIndicator;
    [SerializeField] protected GameSceneController gameSceneController;

    public enum BossPhase
    {
        MiniBoss,
        FinalBoss
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        CallBoss();
    }

    // =========================
    // BOSS SPAWN
    // =========================

    public void CallBoss()
    {
        if (isBossSpawned) return;
        if (isStageComplete) return;

        if (isFinalBossUnlocked)
        {
            SpawnBoss();
            isBossSpawned = true;
            return;
        }

        if (EnemySpawner.instance.enemiesKilled >= killsToSpawnBoss || Timer.instance.remaningTime <= 20)
        {
            SpawnBoss();
            isBossSpawned = true;
        }
    }

    private void SpawnBoss()
    {
        Vector2 spawnPosition = new(Random.Range(-13f, 13f), Random.Range(-18f, 16f));
        switch (currentBossPhase)
        {
            case BossPhase.MiniBoss:

                currentMiniBoss = Instantiate(miniBossPrefab, spawnPosition, Quaternion.identity);
                bossArrowIndicator.boss = currentMiniBoss.transform;
                bossArrowIndicator.player = PlayerController.instance.transform;
                bossArrowIndicator.gameObject.SetActive(true);
                Debug.Log("Mini Boss Spawned");
                AudioController.instance.PlayBGM(miniBossPhaseClip);
                break;

            case BossPhase.FinalBoss:

                GameObject finalBoss = Instantiate(finalBossPrefab, spawnPosition, Quaternion.identity);
                bossArrowIndicator.boss = finalBoss.transform;
                bossArrowIndicator.player = PlayerController.instance.transform;
                bossArrowIndicator.gameObject.SetActive(true);
                Debug.Log("Final Boss Spawned");
                AudioController.instance.PlayBGM(finalBossPhaseClip);
                break;
        }
    }

    // =========================
    // MINI BOSS DEAD
    // =========================

    public void OnMiniBossDead()
    {
        miniBossesKilled++;

        isBossSpawned = false;

        Debug.Log("Mini Boss Killed: " + miniBossesKilled + "/" + miniBossesNeeded);
        if (miniBossesKilled < miniBossesNeeded)
        {
            EndStageForUpgrade();
        }

        else
        {
            UnlockFinalBoss();
        }
    }

    // =========================
    // FINAL BOSS UNLOCK
    // =========================

    private void UnlockFinalBoss()
    {
        currentBossPhase = BossPhase.FinalBoss;
        isFinalBossUnlocked = true;
        isBossSpawned = false;
        Debug.Log("FINAL BOSS UNLOCKED!");
    }

    // =========================
    // FINAL BOSS DEAD
    // =========================

    public IEnumerator OnFinalBossDeadOrWinGame(GameObject finalBoss)
    {
        Debug.Log("FINAL BOSS DEFEATED!");
        yield return new WaitForSeconds(delayBeforeEndGame);
        GamePauseController.instance.SetEndGamePause(true);
        UIController.instance.ShowGameCompleteMenu(true);
        Destroy(finalBoss);
        SetTimeRankWhenWinGame();
        gameSceneController.CompleteCurrentWorld();
        AudioController.instance.StopBGM();
        AudioController.instance.PlaySFX(winGameClip);
    }

    public void SetTimeRankWhenWinGame()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        float timeUsed = Timer.instance.GetElapsedTime();
        RankController.SaveBestTime(currentSceneName, timeUsed);
    }

    // =========================
    // STAGE SYSTEM
    // =========================

    private void EndStageForUpgrade()
    {
        isStageComplete = true;
        //Timer.instance.remaningTime = 0f;
        Timer.instance.SetTimeToZero();
        Debug.Log("Stage Complete - Upgrade Time");
    }

    public void LoadNextStage()
    {
        EnemySpawner.instance.ResetSpawner();
        //Timer.instance.remaningTime = 60f;
        Timer.instance.ResetTime();
        Destroy(currentMiniBoss);
        UIController.instance.SetTalentsCanvas(false);
        isBossSpawned = false;
        isStageComplete = false;
        worldLevel++;
        AudioController.instance.PlayBGM(normalPhaseClip);
    }

    public void OnPlayerDead()
    {
        GameOver();
    }

    public void GameOver()
    {
        AudioController.instance.PlaySFX(playerDeathClip);
        AudioController.instance.StopBGM();
        GamePauseController.instance.SetGameOverPause(true);
        UIController.instance.ShowGameOverMenu(true);
        Debug.Log("Game Over");
    }

    public void RestartGame()
    {
        //SceneLoader.instance.LoadNextScene(SceneManager.GetActiveScene().name);
        //UIController.instance.ShowGameOverMenu(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Game Restarted");
    }
}
