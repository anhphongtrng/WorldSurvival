using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    [SerializeField] protected GameObject bossPrefab;
    [SerializeField] protected Transform bossSpawnPoint;
    [SerializeField] private int killsToSpawnBoss = 20;
    public bool isBossSpawned = false;

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

    public void CallBoss()
    {
        if ((EnemySpawner.instance.enemiesKilled >= killsToSpawnBoss && !isBossSpawned) || (Timer.instance.remaningTime <= 20 && !isBossSpawned))
        {
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
        Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
    }

    public void LoadNextStage()
    {
        EnemySpawner.instance.ResetSpawner();
        Timer.instance.remaningTime = 60f;
        Destroy(GameObject.Find("SandBossEnemy(Clone)"));
        isBossSpawned = false;
        Debug.Log("Next Stage Loaded");
    }
}
