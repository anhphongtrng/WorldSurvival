using UnityEngine;

public class MummySelfDestroy : SelfDestroy
{
    protected MummyController mummyController;

    protected void Awake()
    {
        mummyController = GetComponent<MummyController>();
    }

    void Update()
    {
        DestroySelf();
    }

    public override void DestroySelf()
    {
        if (isDead) return;

        if (mummyController.damageReceiver.IsDead())
        {
            isDead = true;
            mummyController.mummyAnimation.SetDead(true);
            EnemySpawner.instance.OnEnemiesKilled(gameObject);
            EnemySpawner.instance.aliveEnemies.Remove(gameObject);
            Destroy(gameObject, 1f);
        }
    }
}
