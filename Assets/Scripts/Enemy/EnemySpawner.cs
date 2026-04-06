using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            Vector3 spawnPosition = new(Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0);
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            enemy.name = "Mummy Enemy " + (i + 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
