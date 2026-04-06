using UnityEngine;

public class SelfDestroy : MonoBehaviour
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

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
