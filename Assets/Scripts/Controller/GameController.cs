using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    [Header("Boss Prefabs")]
    [SerializeField] private GameObject miniBossPrefab;
    [SerializeField] private GameObject finalBossPrefab;

    [Header("Boss Spawn")]
    [SerializeField] private int killsToSpawnBoss = 20;

    [Header("Mini Boss Progress")]
    [SerializeField] private int miniBossesNeeded = 3;

    private int miniBossesKilled = 0;

    public bool isBossSpawned = false;
    private bool isFinalBossUnlocked = false;
    private bool isStageComplete = false;

    public BossPhase currentBossPhase = BossPhase.MiniBoss;

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

        // Nếu đã unlock final boss
        // thì spawn final boss luôn
        if (isFinalBossUnlocked)
        {
            Debug.Log("abc " + isFinalBossUnlocked);
            SpawnBoss();
            isBossSpawned = true;
            return;
        }

        // Chưa unlock final boss
        // thì phải đủ điều kiện mới spawn miniboss
        if (EnemySpawner.instance.enemiesKilled >= killsToSpawnBoss
            || Timer.instance.remaningTime <= 20)
        {
            Debug.Log("Spawning Boss...");
            SpawnBoss();
            isBossSpawned = true;
        }
    }

    private void SpawnBoss()
    {
        Vector2 spawnPosition = new(
            Random.Range(-17f, 17f),
            Random.Range(-17f, 17f)
        );

        switch (currentBossPhase)
        {
            case BossPhase.MiniBoss:

                Instantiate(
                    miniBossPrefab,
                    spawnPosition,
                    Quaternion.identity
                );

                Debug.Log("Mini Boss Spawned");

                break;

            case BossPhase.FinalBoss:

                Instantiate(
                    finalBossPrefab,
                    spawnPosition,
                    Quaternion.identity
                );

                Debug.Log("Final Boss Spawned");

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

        Debug.Log(
            "Mini Boss Killed: "
            + miniBossesKilled
            + "/"
            + miniBossesNeeded
        );

        // Chưa đủ 3 miniboss
        if (miniBossesKilled < miniBossesNeeded)
        {
            EndStageForUpgrade();
        }

        // Đủ 3 miniboss
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

    public void OnFinalBossDead()
    {
        Debug.Log("FINAL BOSS DEFEATED!");

        LoadNextStage();
    }

    // =========================
    // STAGE SYSTEM
    // =========================

    private void EndStageForUpgrade()
    {
        isStageComplete = true;
        Timer.instance.remaningTime = 0f;
        Debug.Log("Stage Complete - Upgrade Time");
    }

    public void LoadNextStage()
    {
        EnemySpawner.instance.ResetSpawner();
        Timer.instance.remaningTime = 60f;
        Destroy(GameObject.Find("SandBossEnemy(Clone)"));
        Destroy(GameObject.Find("SkeletonBossGroup(Clone)"));
        isBossSpawned = false;
        isStageComplete = false;
        Debug.Log("Next Stage Loaded"); 
        Debug.Log(EnemySpawner.instance.enemiesKilled);
    }

    public void OnPlayerDead()
    {
        EndGame();
    }

    public void EndGame()
    {
        GamePauseController.instance.SetGameOverPause(true);
        UIController.instance.ShowGameOverMenu(true);
        Debug.Log("Game Over");
    }

    public void RestartGame()
    {
        SceneLoader.instance.LoadNextScene(SceneManager.GetActiveScene().name);
        UIController.instance.ShowGameOverMenu(false);
    }
}
