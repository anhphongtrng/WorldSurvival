using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float timeBetweenSpawns = 0.5f;
    [SerializeField] private int maxEnemies = 30;
    [SerializeField] public int enemiesKilled = 0;

    private List<GameObject> aliveEnemies = new();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemyCoroutine());
    }

    private IEnumerator SpawnEnemyCoroutine()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    public void SpawnEnemy()
    {
        //Debug.Log(aliveEnemies.Count);
        aliveEnemies.RemoveAll(enemy => enemy == null);

        if (aliveEnemies.Count >= maxEnemies)
            return;

        GameObject enemyPrefab =
            enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        Vector3 spawnPosition = new(
            Random.Range(-20f, 20f),
            Random.Range(-20f, 20f),
            0
        );

        GameObject spawnedEnemy =
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        spawnedEnemy.transform.SetParent(transform);

        aliveEnemies.Add(spawnedEnemy);
    }

    public void OnEnemiesKilled(GameObject enemy)
    {
        aliveEnemies.Remove(enemy);
        enemiesKilled++;
        Debug.Log($"Enemies killed: {enemiesKilled}");
    }
}