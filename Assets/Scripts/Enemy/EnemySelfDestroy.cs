using UnityEngine;

public class EnemySelfDestroy : SelfDestroy
{
    EnemyController enemyController;

    private void Awake()
    {
        enemyController = GetComponent<EnemyController>();
    }

    private void Update()
    {
        if (enemyController.damageReceiver.IsDead())
            DestroySelf();
    }
}
