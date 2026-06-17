using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;

    [Header("Prefabs")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private GameObject addPointEffectPrefab;

    [SerializeField] private float timeBetweenSpawns = 0.5f;
    [SerializeField] private int maxEnemies = 30;
    public int enemiesKilled = 0;

    public List<GameObject> aliveEnemies = new();

    public static event Action<int> OnAddSkillPoint;

    [Header("Music")]
    public AudioClip addPointClip;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        enemiesKilled = 0;
        SpawnEnemyWhenStart();
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

    public void SpawnEnemyWhenStart()
    {
        for (int i = 0; i < 15; i++)
        {
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        //Debug.Log(aliveEnemies.Count);
        aliveEnemies.RemoveAll(enemy => enemy == null);
        if (aliveEnemies.Count >= maxEnemies)
            return;
        GameObject enemyPrefab = enemyPrefabs[UnityEngine.Random.Range(0, enemyPrefabs.Length)];
        Vector3 spawnPosition = new(UnityEngine.Random.Range(-13f, 13f), UnityEngine.Random.Range(-18f, 16f), 0);
        GameObject spawnedEnemy =Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        spawnedEnemy.transform.SetParent(transform);
        aliveEnemies.Add(spawnedEnemy);
        //Debug.Log($"Alive enemies: {aliveEnemies.Count}");
    }

    public void OnEnemiesKilled(GameObject enemy)
    {
        aliveEnemies.Remove(enemy);
        enemiesKilled++;
        CheckCanAddPoint();
        
    }

    public void CheckCanAddPoint()
    {
        if (enemiesKilled % 10 == 0)
        {
            OnAddSkillPoint?.Invoke(1);
            AudioController.instance.PlaySFX(addPointClip);
            GameObject addPointEffect = Instantiate(addPointEffectPrefab, PlayerController.instance.transform.position, Quaternion.identity);
            addPointEffect.transform.SetParent(PlayerController.instance.transform);
            Destroy(addPointEffect, 0.5f);
        }
    }

    public void ResetSpawner()
    {
        enemiesKilled = 0;
        SpawnEnemyWhenStart();
        Debug.Log("Enemy Spawner Reset");
    }
}